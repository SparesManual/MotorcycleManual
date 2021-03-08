using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvPropertyMapping
    : CsvMapping<Property>
  {
    public CsvPropertyMapping()
    {
      MapProperty(0, x => x.PartId);
      MapProperty(1, x => x.PropertyTypeId);
      MapProperty(2, x => x.FormatTypeId);
      MapProperty(3, x => x.PropertyName);
      MapProperty(4, x => x.PropertyValue);
    }
  }
}