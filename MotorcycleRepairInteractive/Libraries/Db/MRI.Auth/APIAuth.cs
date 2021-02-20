using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using Grpc.Net.Client;

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
    private readonly Db.API.Auth.AuthClient m_client;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public APIAuth()
      : this(GrpcChannel.ForAddress("https://localhost:5001"))
    {
    }

    /// <summary>
    /// Provider constructor
    /// </summary>
    /// <param name="channel">Grpc channel instance</param>
    protected APIAuth(GrpcChannel channel)
    {
      m_channel = channel;
      m_client = new Db.API.Auth.AuthClient(m_channel);
    }

    /// <inheritdoc />
    public async void Dispose()
    {
      await m_channel.ShutdownAsync().ConfigureAwait(false);
      m_channel.Dispose();
    }

    /// <inheritdoc />
    public async ValueTask<(bool, int)> LoginUser(string email, string password, bool rememberMe = default,
      CancellationToken cancellationToken = default)
    {
      var result = await m_client.LoginUserAsync(new LoginRequest {Email = email, Password = password, RememberMe = rememberMe}, cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return (result.Reply, result.Error);
    }

    /// <inheritdoc />
    public async ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default)
    {
      var result = await m_client.LogoutAsync(new Nothing(), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }

    /// <inheritdoc />
    public async ValueTask<string> GetUserAsync(CancellationToken cancellationToken = default)
    {
      var result = await m_client.LoggedInEmailAsync(new Nothing(), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }
  }
}