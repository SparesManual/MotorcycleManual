using System.Threading.Tasks;
using Db.API;
using Grpc.Net.Client;

namespace Console.APITester
{
  public static class Program
  {
    private static async Task Main()
    {
      using var channel = GrpcChannel.ForAddress("https://localhost:5001");
      var client = new Provider.ProviderClient(channel);
      var reply = await client.GetPartAsync(new IdRequest { Id = 1 });

      System.Console.WriteLine($"{reply.PartNumber}\t{reply.MakersPartNumber}\t{reply.Description}\t{reply.MakersDescription}");
    }
  }
}
