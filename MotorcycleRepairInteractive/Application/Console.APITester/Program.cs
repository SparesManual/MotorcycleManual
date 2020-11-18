using System.Threading.Tasks;
using Db.API;
using Grpc.Core;
using Grpc.Net.Client;

namespace Console.APITester
{
  public static class Program
  {
    private static async Task Main(string[] args)
    {
      using var channel = GrpcChannel.ForAddress("https://localhost:5001");
      var client = new Provider.ProviderClient(channel);
      var reply = await client.GetPartAsync(new IdRequest { Id = 1 });
    }
  }
}
