using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Email.API.Services
{
  /// <summary>
  /// Mailing API service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class MailerService
    : Mailer.MailerBase
  {
    private readonly ILogger<MailerService> m_logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    public MailerService(ILogger<MailerService> logger)
    {
      m_logger = logger;
    }

    /// <inheritdoc />
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      return Task.FromResult(new HelloReply
      {
        Message = "Hello " + request.Name
      });
    }
  }
}