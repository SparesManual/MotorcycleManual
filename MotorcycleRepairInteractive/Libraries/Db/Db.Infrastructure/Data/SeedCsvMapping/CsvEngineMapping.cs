using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvEngineMapping
    : CsvMapping<Engine>
  {
    public CsvEngineMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Name);
      MapProperty(2, x => x.Displacement);
      MapProperty(3, x => x.Carburetors);
      MapProperty(4, x => x.CarburetorId);
      MapProperty(5, x => x.Transmission);
    }
  }
}