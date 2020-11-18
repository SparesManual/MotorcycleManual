using Db.Core.Entities;
using TinyCsvParser.Mapping;

namespace Db.Infrastructure.Data.SeedCsvMapping
{
  internal class CsvBookMapping
    : CsvMapping<Book>
  {
    public CsvBookMapping()
    {
      MapProperty(0, x => x.Id);
      MapProperty(1, x => x.Title);
    }
  }
}