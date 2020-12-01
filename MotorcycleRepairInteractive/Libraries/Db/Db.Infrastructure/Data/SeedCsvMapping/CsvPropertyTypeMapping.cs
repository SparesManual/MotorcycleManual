using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvPropertyTypeMapping
    : CsvMapping<PropertyType>
  {
    public CsvPropertyTypeMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Name);
      MapProperty(2, x => x.Unit);
    }
  }
}