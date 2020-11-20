using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="Part"/> entities based on their parent <see cref="SectionParts"/> entities
  /// </summary>
  public class SectionPartsSpec
    : BaseSpecification<SectionParts, Part>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Id of the <see cref="Section"/></param>
    /// <param name="search">Fuzzy search string</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionPartsSpec(int id, string search, int size, int index)
      : base(part => Part.StringSearch(part, search))
    {
      SetExtractor(sections =>
       sections
        .Where(sp => sp.SectionId == id)
        .Include(sp => sp.Part!)
        .Select(sp => sp.Part!)
        .Where(part => part != null)
      );

      SetOrderBy(part => part.PartNumber);
      ApplyPaging(size, index);
    }
  }
}
