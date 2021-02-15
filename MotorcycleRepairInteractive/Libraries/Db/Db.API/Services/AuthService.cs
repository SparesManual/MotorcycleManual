using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

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

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public AuthService(UserManager<IdentityUser> userManager)
    {
      m_userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> LoginUser(LoginRequest request, ServerCallContext context)
    {
      var http = context.GetHttpContext();

      var user = await m_userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
      if (user is null)
        return new BooleanReply
        {
          Reply = false,
          Error = 404
        };

      if (!await m_userManager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false))
        return new BooleanReply
        {
          Reply = false,
          Error = 404
        };

      var claims = await m_userManager.GetClaimsAsync(user).ConfigureAwait(false);
      var claimsIdentity = new ClaimsIdentity(claims, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      await http.SignInAsync(claimsPrincipal).ConfigureAwait(false);

      return new BooleanReply
      {
        Reply = true,
        Error = 0
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