using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvCarburetorMapping
    : CsvMapping<Carburetor>
  {
    public CsvCarburetorMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Name);
    }
  }
}