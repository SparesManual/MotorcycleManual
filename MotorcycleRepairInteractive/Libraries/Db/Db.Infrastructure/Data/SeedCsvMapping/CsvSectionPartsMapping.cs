using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvSectionPartsMapping
    : CsvMapping<SectionParts>
  {
    public CsvSectionPartsMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.SectionId);
      MapProperty(2, x => x.PartId);
      MapProperty(3, x => x.ParentSectionPartsId);
      MapProperty(4, x => x.PageNumber);
      MapProperty(6, x => x.AdditionalInfo);
      MapProperty(7, x => x.Quantity);
      MapProperty(8, x => x.Remarks);
    }
  }
}