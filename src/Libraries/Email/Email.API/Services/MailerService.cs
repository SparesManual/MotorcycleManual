using System.Threading.Tasks;
using FluentEmail.Core;
using Grpc.Core;

namespace Email.API.Services
{
  /// <summary>
  /// Mailing API service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class MailerService
    : Mailer.MailerBase
  {
    private readonly IFluentEmail m_emailSender;

    /// <summary>
    /// Default constructor
    /// </summary>
    public MailerService(IFluentEmail emailSender)
    {
      m_emailSender = emailSender;
    }

    /// <inheritdoc />
    public override async Task<Boolean> SendPasswordRecovery(EmailAndCode request, ServerCallContext context)
    {
      var response = await m_emailSender
        .To(request.Email)
        .Subject("Spares Manual - Password Recovery")
        .SendAsync(context.CancellationToken)
        .ConfigureAwait(false);

      return new Boolean
      {
        Success = response.Successful
      };
    }

    /// <inheritdoc />
    public override async Task<Boolean> SendRegistrationConfirmation(EmailAndCode request, ServerCallContext context)
    {
      var response = await m_emailSender
        .To(request.Email)
        .Subject("Action required: Please confirm your email")
        .SendAsync(context.CancellationToken)
        .ConfigureAwait(false);

      return new Boolean
      {
        Success = response.Successful
      };
    }
  }
}