using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvMakeMapping
    : CsvMapping<Make>
  {
    public CsvMakeMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Name);
    }
  }
}