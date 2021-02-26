using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Authorization;
using States.General;

namespace MRI.Auth
{
  /// <summary>
  /// Provider of API method calls to the Identity Database
  /// </summary>
  public class APIAuth
    : IAPIAuth
  {
    #region Fields

    private readonly GrpcChannel m_channel;
    private readonly HttpClient m_httpClient;
    private readonly IStorage m_storage;
    private readonly AuthenticationStateProvider m_stateProvider;
    private readonly Db.API.Auth.AuthClient m_client;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="httpClient">Injected HttpClient instance</param>
    /// <param name="storage">Injected local storage instance</param>
    /// <param name="stateProvider">Injected authentication state provider instance</param>
    public APIAuth(HttpClient httpClient, IStorage storage, AuthenticationStateProvider stateProvider)
      : this(GrpcChannel.ForAddress("https://localhost:5001"), httpClient, storage, stateProvider)
    {
    }

    /// <summary>
    /// Provider constructor
    /// </summary>
    /// <param name="channel">Grpc channel instance</param>
    /// <param name="httpClient">HttpClient instance</param>
    /// <param name="storage">Local storage instance</param>
    /// <param name="stateProvider">Authentication state provider instance</param>
    protected APIAuth(GrpcChannel channel, HttpClient httpClient, IStorage storage, AuthenticationStateProvider stateProvider)
    {
      m_channel = channel;
      m_httpClient = httpClient;
      m_storage = storage;
      m_stateProvider = stateProvider;
      m_client = new Db.API.Auth.AuthClient(m_channel);
    }

    /// <inheritdoc />
    public async void Dispose()
    {
      await m_channel.ShutdownAsync().ConfigureAwait(false);
      m_channel.Dispose();
    }

    /// <inheritdoc />
    public async ValueTask<(bool, int)> LoginUserAsync(string email, string password, bool rememberMe = default,
      CancellationToken cancellationToken = default)
    {
      var result = await m_client.LoginUserAsync(new LoginRequest {Email = email, Password = password, RememberMe = rememberMe}, cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);

      if (!result.Success || result.Token is null)
        return (false, 403);

      await m_storage.SetItemAsync("authToken", result.Token).ConfigureAwait(false);
      ((ApiAuthenticationStateProvider)m_stateProvider).MarkUserAsAuthenticated(email);
      m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

      return (result.Success, 200);
    }

    /// <inheritdoc />
    public async ValueTask<bool> LogoutUserAsync(CancellationToken cancellationToken = default)
    {
      var result = await m_client.LogoutAsync(new Nothing(), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }

    /// <inheritdoc />
    public ValueTask<bool> RegisterUserAsync(string email, string password, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

    /// <inheritdoc />
    public ValueTask<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

    /// <inheritdoc />
    public async ValueTask<string> GetUserAsync(CancellationToken cancellationToken = default)
    {
      var result = await m_client.LoggedInEmailAsync(new Nothing(), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }
  }
}