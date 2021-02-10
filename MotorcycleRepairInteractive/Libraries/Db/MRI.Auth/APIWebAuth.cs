using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace MRI.Auth
{
  /// <summary>
  /// Provider of API method calls to the Identity Database for web applications
  /// </summary>
  public class APIWebAuth
    : APIAuth
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public APIWebAuth()
      : base(GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
      {
        HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())
      })) { }
  }
}