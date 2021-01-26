using BOM.Infrastructure;
using Models.BOM;

namespace Console.APITester
{
  public static class Program
  {
    private static void Main()
    {
      var bill = new Bill
      {
        Materials = new[]
        {
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          },
          new Material
          {
            PartNumber = "12345",
            MakersPartNumber = "abcde",
            Description = "a part",
            Quantity = 4
          }
        }
      };

      bill.ToPDF(@"C:\Users\Denis\Desktop\output\result.pdf");
    }
  }
}
