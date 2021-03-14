using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace MRI.Email
{
  /// <summary>
  /// FluentEmail API mailing web Client
  /// </summary>
  public class EmailWebClient
    : EmailClient
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public EmailWebClient()
      : base(GrpcChannel.ForAddress("https://localhost:5003", new GrpcChannelOptions
      {
        HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())
      })) { }
  }
}