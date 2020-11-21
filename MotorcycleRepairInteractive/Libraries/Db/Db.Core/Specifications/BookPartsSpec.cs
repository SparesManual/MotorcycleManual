using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="Part"/> entities based on their parent <see cref="Book"/> entity
  /// </summary>
  public class BookPartsSpec
    : BaseSpecification<Section, Part>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Id of the <see cref="Book"/></param>
    /// <param name="search">Fuzzy book search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public BookPartsSpec(int id, string? search, int size, int index)
      : base(part => string.IsNullOrEmpty(search)
              || part.Description.Contains(search)
              || part.PartNumber.Contains(search)
              || !string.IsNullOrEmpty(part.MakersDescription) && part.MakersDescription.Contains(search)
              || !string.IsNullOrEmpty(part.MakersPartNumber) && part.MakersPartNumber.Contains(search)
            )
    {
      SetExtractor(sections =>
        sections
          .Where(section => section.BookId.Equals(id))
          .Include(section => section.SectionParts)
          .Select(section => section.SectionParts)
          .SelectMany(mapList => mapList.Select(sp => sp))
          .Include(sp => sp.Part)
          .Select(sp => sp.Part!)
          .Where(part => part != null)
      );

      SetOrderBy(part => part.PartNumber);
      ApplyPaging(size, index);
    }
  }
}