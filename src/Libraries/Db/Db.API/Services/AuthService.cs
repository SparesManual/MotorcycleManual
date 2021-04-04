using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Email.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

    private readonly UserManager<IdentityUser> m_userManager;
    private readonly SignInManager<IdentityUser> m_signInManager;
    private readonly IAPIMail m_emailService;
    private readonly IConfiguration m_configuration;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAPIMail emailService, IConfiguration configuration)
    {
      m_userManager = userManager;
      m_signInManager = signInManager;
      m_emailService = emailService;
      m_configuration = configuration;
    }

    /// <inheritdoc />
    public override async Task<LoginResult> LoginUser(LoginRequest request, ServerCallContext context)
    {
      var result = await m_signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false).ConfigureAwait(false);

      if (!result.Succeeded)
        return new LoginResult
        {
          Success = false, Token = string.Empty
        };

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

      return new LoginResult
      {
        Success = true,
        Token = tokenString
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> RegisterUser(RegistrationRequest request, ServerCallContext context)
    {
      if (await m_userManager.FindByNameAsync(request.Email).ConfigureAwait(false) is null)
        return new BooleanReply
        {
          Reply = false,
          Error = 403
        };

      var user = new IdentityUser
      {
        UserName = request.Email
      };

      var result = await m_userManager.CreateAsync(user, request.Password).ConfigureAwait(false);
      if (!result.Succeeded)
        return new BooleanReply
        {
          Reply = false,
          Error = 503
        };

      var code = await m_userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
      await m_emailService.SendRegistrationConfirmationAsync(request.Email, user.Id, code, context.CancellationToken).ConfigureAwait(false);

      return new BooleanReply
      {
        Reply = true,
        Error = 0
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> ResendVerification(SingleString request, ServerCallContext context)
    {
      var user = await m_userManager.FindByIdAsync(request.Content).ConfigureAwait(false);
      if (user is null)
        return new BooleanReply
        {
          Reply = false,
          Error = 404
        };

      var code = await m_userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
      await m_emailService.SendRegistrationConfirmationAsync(user.Email, user.Id, code, context.CancellationToken);

      return new BooleanReply
      {
        Reply = true,
        Error = 0
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> VerifyEmail(VerifyMailRequest request, ServerCallContext context)
    {
      var user = await m_userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);
      if (user is null)
        return new BooleanReply
        {
          Error = 404,
          Reply = false
        };

      var result = await m_userManager.ConfirmEmailAsync(user, request.Code).ConfigureAwait(false);
      if (result.Succeeded)
        return new BooleanReply
        {
          Error = 0,
          Reply = true
        };

      return new BooleanReply
      {
        Error = 404,
        Reply = false
      };
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> UserExists(SingleString request, ServerCallContext context)
    {
      var user = await m_userManager.FindByNameAsync(request.Content).ConfigureAwait(false);

      return new BooleanReply
      {
        Reply = user is not null
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