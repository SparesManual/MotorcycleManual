using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace MRI.Db
{
  /// <summary>
  /// Provider of API method calls to the Database for web applications
  /// </summary>
  public class APIWebProvider
    : APIProvider
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public APIWebProvider()
      : base(GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
      {
        HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())
      })) { }
  }
}