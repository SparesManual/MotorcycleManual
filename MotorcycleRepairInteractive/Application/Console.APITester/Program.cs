using System.Threading.Tasks;
using MRI.Db;

namespace Console.APITester
{
  public static class Program
  {
    private static async Task Main()
    {
      const int pageSize = 10;
      const int pageIndex = 1;

      using var provider = new APIProvider();

      var parts = await provider.GetPartsFromSectionAsync(1, pageSize, pageIndex).ConfigureAwait(false);

      System.Console.WriteLine($"{parts.PageIndex}: {parts.PageItems}/{parts.TotalItems}");

      var index = pageSize * (pageIndex - 1);
      await foreach (var part in parts.ReadAll().ConfigureAwait(false))
        System.Console.WriteLine($" {++index}\t{part.PartNumber}\t{part.MakersPartNumber}\t{part.Description}\t{part.MakersDescription}");

      System.Console.WriteLine("END");
    }
  }
}
