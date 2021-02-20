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
  public class ApiAuthenticationStateProvider
    : AuthenticationStateProvider
  {
    private readonly HttpClient m_httpClient;
    private readonly IStorage m_localStorage;

    public ApiAuthenticationStateProvider(HttpClient httpClient, IStorage localStorage)
    {
      m_httpClient = httpClient;
      m_localStorage = localStorage;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var savedToken = await m_localStorage.GetItemAsync<string>("authToken").ConfigureAwait(false);

      if (string.IsNullOrWhiteSpace(savedToken))
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

      m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
    }

    public void MarkUserAsAuthenticated(string email)
    {
      var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
      var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
      NotifyAuthenticationStateChanged(authState);
    }

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
        case 2: base64 += "=="; break;
        case 3: base64 += "="; break;
      }
      return Convert.FromBase64String(base64);
    }
  }
}