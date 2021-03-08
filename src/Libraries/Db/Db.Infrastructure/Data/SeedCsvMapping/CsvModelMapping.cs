using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvModelMapping
    : CsvMapping<Model>
  {
    public CsvModelMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Year);
      MapProperty(2, x => x.MakeId);
      MapProperty(3, x => x.EngineId);
      MapProperty(4, x => x.Name);
      MapProperty(5, x => x.BookId);
    }
  }
}