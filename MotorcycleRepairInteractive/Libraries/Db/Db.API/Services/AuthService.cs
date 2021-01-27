using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

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

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public AuthService(ILogger<AuthService> logger)
    {
      m_logger = logger;
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> LoginUser(UserRequest request, ServerCallContext context)
    {
      var http = context.GetHttpContext();
      var claim = new Claim(ClaimTypes.Email, request.Email);
      var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      await http.SignInAsync(claimsPrincipal).ConfigureAwait(false);

      return new BooleanReply {Reply = false};
    }

    /// <inheritdoc />
    public override async Task<BooleanReply> Logout(Nothing request, ServerCallContext context)
    {
      await context.GetHttpContext().SignOutAsync().ConfigureAwait(false);

      return new BooleanReply {Reply = true};
    }
  }
}