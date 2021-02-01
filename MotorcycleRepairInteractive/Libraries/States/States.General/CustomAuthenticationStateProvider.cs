using System.Security.Claims;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace States.General
{
  /// <summary>
  /// Authentication state provider with custom logic
  /// </summary>
  public class CustomAuthenticationStateProvider
    : AuthenticationStateProvider
  {
    private readonly IAPIAuth m_provider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected authentication API provider</param>
    public CustomAuthenticationStateProvider(IAPIAuth provider)
    {
      m_provider = provider;
    }

    /// <inheritdoc />
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var userEmail = await m_provider.GetUserAsync();

      if (userEmail is null)
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

      var claim = new Claim(ClaimTypes.Email, userEmail);
      var claimsIdentity = new ClaimsIdentity(new[] {claim}, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      return new AuthenticationState(claimsPrincipal);
    }
  }
}