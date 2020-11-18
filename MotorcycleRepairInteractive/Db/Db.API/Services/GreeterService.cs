using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Db.API
{
  /// <summary>
  /// Hello world service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class GreeterService
    : Greeter.GreeterBase
  {
    private readonly ILogger<GreeterService> m_logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="logger">Injected logger</param>
    public GreeterService(ILogger<GreeterService> logger)
      => m_logger = logger;

    /// <summary>
    /// Hello world API method
    /// </summary>
    /// <param name="request">Received request data</param>
    /// <param name="context">API request context</param>
    /// <returns>Generated reply</returns>
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      m_logger.LogInformation($"{DateTime.Now.TimeOfDay}\tReceived {nameof(SayHello)} request.");

      return Task.FromResult(new HelloReply
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
