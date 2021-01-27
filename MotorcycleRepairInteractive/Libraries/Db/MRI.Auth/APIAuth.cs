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
    {
      m_channel = GrpcChannel.ForAddress("https://localhost:5001");
      m_client = new Db.API.Auth.AuthClient(m_channel);
    }

    /// <inheritdoc />
    public async void Dispose()
    {
      await m_channel.ShutdownAsync().ConfigureAwait(false);
      m_channel.Dispose();
    }

    /// <inheritdoc />
    public async ValueTask<bool> LoginUser(string email, string password, CancellationToken cancellationToken = default)
    {
      var result = await m_client.LoginUserAsync(new UserRequest {Email = email, Password = password}, cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }

    /// <inheritdoc />
    public async ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default)
    {
      var result = await m_client.LogoutAsync(new Nothing(), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);
      return result.Reply;
    }
  }
}