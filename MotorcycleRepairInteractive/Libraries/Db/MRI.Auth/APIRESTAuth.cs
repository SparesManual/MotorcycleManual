using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;

// ReSharper disable StringLiteralTypo

namespace MRI.Auth
{
  /// <summary>
  /// Provider of API method calls to the Identity database via REST
  /// </summary>
  public class APIRESTAuth
    : IAPIAuth
  {
    private readonly HttpClient m_httpClient;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="httpClient">Injected http client instance</param>
    public APIRESTAuth(HttpClient httpClient)
    {
      m_httpClient = httpClient;
    }

    /// <inheritdoc />
    // ReSharper disable once CA1816
    public void Dispose()
    {
    }

    /// <inheritdoc />
    public async Task<(bool, int)> LoginUser(string email, string password, bool rememberMe = default, CancellationToken cancellationToken = default)
    {
      var result = await m_httpClient.PostAsJsonAsync("auth/signinuser", Tuple.Create(email, password), cancellationToken).ConfigureAwait(false);

      return (result.IsSuccessStatusCode, (int) result.StatusCode);
    }

    /// <inheritdoc />
    public ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default)
      => throw new NotImplementedException();

    /// <inheritdoc />
    public async Task<string> GetUserAsync(CancellationToken cancellationToken = default)
      => await m_httpClient.GetFromJsonAsync<string>("auth/signedinemail", cancellationToken).ConfigureAwait(false) ?? string.Empty;
  }
}
