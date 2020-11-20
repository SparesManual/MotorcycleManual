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
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public BookPartsSpec(int id, int size, int index)
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

      SetOrderBy(part => part.Description);
      ApplyPaging(size, index);
    }
  }
}