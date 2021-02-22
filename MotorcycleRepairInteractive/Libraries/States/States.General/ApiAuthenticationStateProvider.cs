using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace States.General
{
  /// <summary>
  /// Custom authentication state provider
  /// </summary>
  public class ApiAuthenticationStateProvider
    : AuthenticationStateProvider
  {
    private readonly HttpClient m_httpClient;
    private readonly IStorage m_storage;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="httpClient">Injected client instance</param>
    /// <param name="storage">Injected storage manager</param>
    public ApiAuthenticationStateProvider(HttpClient httpClient, IStorage storage)
    {
      m_httpClient = httpClient;
      m_storage = storage;
    }

    /// <summary>
    /// Retrieves the current authentication state
    /// </summary>
    /// <returns>Authentication state</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var savedToken = await m_storage.GetItemAsync<string>("authToken").ConfigureAwait(false);

      if (string.IsNullOrWhiteSpace(savedToken))
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

      m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
    }

    /// <summary>
    /// Marks the given <paramref name="email"/> as authenticated
    /// </summary>
    /// <param name="email">Email to mark</param>
    public void MarkUserAsAuthenticated(string email)
    {
      var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
      var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
      NotifyAuthenticationStateChanged(authState);
    }

    /// <summary>
    /// Marks the currently authenticated user as unauthenticated
    /// </summary>
    public void MarkUserAsLoggedOut()
    {
      var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
      var authState = Task.FromResult(new AuthenticationState(anonymousUser));
      NotifyAuthenticationStateChanged(authState);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
      var claims = new List<Claim>();
      var payload = jwt.Split('.')[1];
      var jsonBytes = ParseBase64WithoutPadding(payload);
      var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

      keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

      if (roles != null)
      {
        if (roles.ToString().Trim().StartsWith("[", StringComparison.InvariantCultureIgnoreCase))
        {
          var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

          claims.AddRange(parsedRoles.Select(parsedRole => new Claim(ClaimTypes.Role, parsedRole)));
        }
        else
          claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));

        keyValuePairs.Remove(ClaimTypes.Role);
      }

      claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

      return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
      switch (base64.Length % 4)
      {
        case 2:
          base64 += "==";
          break;
        case 3:
          base64 += "=";
          break;
      }
      return Convert.FromBase64String(base64);
    }
  }
}