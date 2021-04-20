using System.Threading;
using System.Threading.Tasks;
using Email.API;
using Email.Interfaces;
using Grpc.Net.Client;

namespace MRI.Email
{
  /// <summary>
  /// FluentEmail API mailing Client
  /// </summary>
  public class EmailClient
    : IAPIMail
  {
    #region Fields

    private readonly Mailer.MailerClient m_client;
    private readonly GrpcChannel m_channel;

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public EmailClient()
      : this(GrpcChannel.ForAddress("https://localhost:5003"))
    {
    }

    /// <summary>
    /// API constructor
    /// </summary>
    protected EmailClient(GrpcChannel channel)
    {
      m_channel = channel;
      m_client = new Mailer.MailerClient(m_channel);
    }

    #endregion

    #region API Methods

    /// <inheritdoc />
    public async ValueTask SendRegistrationConfirmationAsync(string email, string userId, string code, CancellationToken cancellationToken = default)
      => await m_client.SendRegistrationConfirmationAsync(new IdAndCode {Code = code, Email = email, UserId = userId}, cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask SendRecoveryCodeAsync(string email, string userId, string code, CancellationToken cancellationToken = default)
      => await m_client.SendPasswordRecoveryAsync(new IdAndCode {Code = code, Email = email, UserId = userId}, cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);

    #endregion

    /// <inheritdoc />
    public void Dispose()
      => m_channel.Dispose();
  }
}