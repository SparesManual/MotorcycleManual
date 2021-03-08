using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvSectionPartParentsMapping
    : CsvMapping<SectionPartParents>
  {
    public CsvSectionPartParentsMapping()
    {
      MapProperty(0, x => x.ParentId);
      MapProperty(1, x => x.ChildId);
    }
  }
}