using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Email.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

// ReSharper disable PositionalPropertyUsedProblem

namespace Db.API
{
  /// <summary>
  /// Authentication API service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class AuthService
    : Auth.AuthBase
  {
    #region Field

    private readonly ILogger<AuthService> m_logger;
    private readonly UserManager<IdentityUser> m_userManager;
    private readonly SignInManager<IdentityUser> m_signInManager;
    private readonly IAPIMail m_emailService;
    private readonly IConfiguration m_configuration;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public AuthService(ILogger<AuthService> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAPIMail emailService, IConfiguration configuration)
    {
      m_logger = logger;
      m_userManager = userManager;
      m_signInManager = signInManager;
      m_emailService = emailService;
      m_configuration = configuration;
    }

    /// <inheritdoc />
    public override async Task<LoginResult> LoginUser(LoginRequest request, ServerCallContext context)
    {
      m_logger.LogDebug("Received login request for {0}", request.Email);
      var result = await m_signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false).ConfigureAwait(false);

      if (!result.Succeeded)
      {
        var user = await m_userManager.FindByNameAsync(request.Email).ConfigureAwait(false);
        m_logger.LogWarning("Login request for {0} was not successful", request.Email);

        return new LoginResult
        {
          Success = false, Token = string.Empty, Confirmed = user.EmailConfirmed
        };
      }

      var claims = new[]
      {
        new Claim(ClaimTypes.Name, request.Email)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_configuration["JwtSecurityKey"]));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expiry = DateTime.Now.AddDays(Convert.ToInt32(m_configuration["JwtExpiryInDays"]));

      var token = new JwtSecurityToken(
        m_configuration["JwtIssuer"],
        m_configuration["JwtAudience"],
        claims,
        expires: expiry,
        signingCredentials: credentials
      );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

      m_logger.LogInformation("Login request for {0} was successful", request.Email);

      return new LoginResult
      {
        Success = true,
        Token = tokenString
      };
    }

    /// <inheritdoc />
    public override async Task<StringReply> RegisterUser(RegistrationRequest request, ServerCallContext context)
    {
      m_logger.LogDebug("Received registration request for {0}", request.Email);
      if (await m_userManager.FindByNameAsync(request.Email).ConfigureAwait(false) is not null)
      {
        m_logger.LogWarning("Could not register user {0} because an account already exists", request.Email);

        return new StringReply
        {
          Reply = string.Empty
        };
      }

      var user = new IdentityUser
      {
        UserName = request.Email
      };

      var result = await m_userManager.CreateAsync(user, request.Password).ConfigureAwait(false);
      if (!result.Succeeded)
      {
        m_logger.LogWarning("Could not register user {0}. Errors: {1}", request.Email, string.Join(", ", result.Errors.Select(error => error.Code)));

        return new StringReply
        {
          Reply = string.Empty
        };
      }

      m_logger.LogInformation("Registered new user {0}", request.Email);

      var code = await m_userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
      await m_emailService.SendRegistrationConfirmationAsync(request.Email, user.Id, code, context.CancellationToken).ConfigureAwait(false);

      return new StringReply
      {
        Reply = user.Id
      };
    }

    private delegate Task<IdentityUser?> UserSearch(string identity);

    /// <inheritdoc />
    public override async Task<BooleanReply> ResendVerification(IdAndEmail request, ServerCallContext context)
    {
      (UserSearch methodName, string identity) = string.IsNullOrEmpty(request.Email)
        ? ((UserSearch)m_userManager.FindByIdAsync!, request.Id)
        : ((UserSearch)m_userManager.FindByNameAsync!, request.Email);

      m_logger.LogDebug("Received resend verification request for {0}", identity);
      var user = await methodName(identity).ConfigureAwait(false);
      if (user is null)
      {
        m_logger.LogWarning("Resending verification request failed as no user exists for {0}", identity);

        return new BooleanReply
        {
          Reply = false,
          Error = 404
        };
      }

      var code = await m_userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
      await m_emailService.SendRegistrationConfirmationAsync(user.UserName, user.Id, code, context.CancellationToken).ConfigureAwait(false);

      m_logger.LogInformation("Verification request sent for {0}", identity);
      return new BooleanReply
      {
        Reply = true,
        Error = 0
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> VerifyEmail(VerifyMailRequest request, ServerCallContext context)
    {
      m_logger.LogDebug("Received email verification request for {0}", request.UserId);
      var user = await m_userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);
      if (user is null)
      {
        m_logger.LogWarning("Email verification failed as no user exists for {0}", request.UserId);

        return new BooleanReply
        {
          Error = 404,
          Reply = false
        };
      }

      var code = HttpUtility.UrlDecode(request.Code).Replace(' ', '+');

      var result = await m_userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);
      if (!result.Succeeded)
      {
        m_logger.LogWarning("Email verification failed for {0} {1}. Errors: {2}", request.UserId, code, string.Join(", ", result.Errors.Select(err => err.Code)));

        return new BooleanReply
        {
          Error = 404,
          Reply = false
        };
      }

      m_logger.LogInformation("Email verified for {0}", request.UserId);
      return new BooleanReply
      {
        Error = 0,
        Reply = true
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> RequestPasswordReset(SingleString request, ServerCallContext context)
    {
      m_logger.LogDebug("Received password reset request for {0}", request.Content);
      var user = await m_userManager.FindByNameAsync(request.Content).ConfigureAwait(false);
      if (user is null)
        // The client shouldn't know if the email is registered
        return new BooleanReply
        {
          Error = 0,
          Reply = true
        };

      var token = await m_userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
      await m_emailService.SendRecoveryCodeAsync(request.Content, token, context.CancellationToken).ConfigureAwait(false);

      return new BooleanReply
      {
        Error = 0,
        Reply = true
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> UserExists(SingleString request, ServerCallContext context)
    {
      m_logger.LogDebug("Received user exists request for {0}", request.Content);
      var user = await m_userManager.FindByNameAsync(request.Content).ConfigureAwait(false);

      return new BooleanReply
      {
        Reply = user is not null
      };
    }

    /// <inheritdoc />
    public override async Task<StringReply> GetUserEmail(SingleString request, ServerCallContext context)
    {
      var user = await m_userManager.FindByIdAsync(request.Content).ConfigureAwait(false);
      return new StringReply
      {
        Reply = user.UserName ?? string.Empty
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> Logout(Nothing request, ServerCallContext context)
    {
      await context.GetHttpContext().SignOutAsync().ConfigureAwait(false);

      return new BooleanReply
      {
        Reply = true,
        Error = 0
      };
    }

    /// <inheritdoc />
    public override async Task<StringReply> LoggedInEmail(Nothing request, ServerCallContext context)
    {
      var http = context.GetHttpContext();

      if (http.User.Identity?.IsAuthenticated is not true)
        return new StringReply
        {
          Reply = ""
        };

      var user = await m_userManager.GetUserAsync(http.User).ConfigureAwait(false);
      return new StringReply
      {
        Reply = user.Email
      };
    }
  }
}