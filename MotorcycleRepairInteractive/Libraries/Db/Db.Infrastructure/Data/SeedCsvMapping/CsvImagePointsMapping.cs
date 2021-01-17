using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvImagePointsMapping
    : CsvMapping<ImagePoint>
  {
    public CsvImagePointsMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.SectionPartsId);
      MapProperty(2, x => x.PositionX);
      MapProperty(3, x => x.PositionY);
    }
  }
}