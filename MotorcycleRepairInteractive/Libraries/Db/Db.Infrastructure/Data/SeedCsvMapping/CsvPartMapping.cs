using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvPartMapping
    : CsvMapping<Part>
  {
    public CsvPartMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.PartNumber);
      MapProperty(2, x => x.MakersPartNumber);
      MapProperty(3, x => x.Description);
    }
  }
}