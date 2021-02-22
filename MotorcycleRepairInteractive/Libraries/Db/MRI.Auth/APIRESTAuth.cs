using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using States.General;

// ReSharper disable StringLiteralTypo

namespace MRI.Auth
{
  /// <summary>
  /// Provider of API method calls to the Identity database via REST
  /// </summary>
  public class APIRESTAuth
    : IAPIAuth
  {
    #region Fields

    private readonly HttpClient m_httpClient;
    private readonly IStorage m_storage;
    private readonly AuthenticationStateProvider m_stateProvider;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="httpClient">Injected http client instance</param>
    /// <param name="storage">Injected storage manager</param>
    /// <param name="stateProvider">Injected authentication state instance</param>
    public APIRESTAuth(HttpClient httpClient, IStorage storage, AuthenticationStateProvider stateProvider)
    {
      m_httpClient = httpClient;
      m_storage = storage;
      m_stateProvider = stateProvider;
    }

    /// <inheritdoc />
    // ReSharper disable once CA1816
    public void Dispose()
    {
    }

    /// <inheritdoc />
    public async ValueTask<(bool, int)> LoginUser(string email, string password, bool rememberMe = default, CancellationToken cancellationToken = default)
    {
      var data = JsonSerializer.Serialize(new Models.REST.Auth.LoginRequest
      {
        Email = email,
        Password = password,
        RememberMe = rememberMe
      });

      var response = await m_httpClient.PostAsync("auth/SignInUser", new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
      var loginResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      if (!response.IsSuccessStatusCode || loginResult is null)
        return (false, 403);

      await m_storage.SetItemAsync("authToken", loginResult).ConfigureAwait(false);
      ((ApiAuthenticationStateProvider)m_stateProvider).MarkUserAsAuthenticated(email);
      m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult);

      return (response.IsSuccessStatusCode, (int)response.StatusCode);
    }

    /// <inheritdoc />
    public async ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default)
    {
      await m_storage.RemoveItemAsync("authToken").ConfigureAwait(false);
      ((ApiAuthenticationStateProvider)m_stateProvider).MarkUserAsLoggedOut();
      m_httpClient.DefaultRequestHeaders.Authorization = null;

      return true;
    }

    /// <inheritdoc />
    public async ValueTask<string> GetUserAsync(CancellationToken cancellationToken = default)
      => await m_httpClient.GetFromJsonAsync<string>("auth/signedinemail", cancellationToken).ConfigureAwait(false) ?? string.Empty;
  }
}
