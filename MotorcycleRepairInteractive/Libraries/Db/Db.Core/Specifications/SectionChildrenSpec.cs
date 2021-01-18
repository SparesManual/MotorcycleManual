using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="SectionParts"/> entities based on their parent <see cref="SectionPartParents"/> entity
  /// </summary>
  public class SectionChildrenSpec
    : BaseSpecification<SectionPartParents, Section>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search"></param>
    /// <param name="id">Id of the <see cref="SectionPartParents"/></param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionChildrenSpec(string? search, int id, int size, int index)
    {
      SetExtractor(sectionPartParents =>
        sectionPartParents
          .Where(parent => parent.ParentId == id)
          .Include(parent => parent.Child)
          .Include(x => x.Child.Section)
          .Select(x => x.Child.Section)
          .Where(section => string.IsNullOrEmpty(search) || search.Contains(section.SectionHeader ?? string.Empty)));

      SetOrderBy(section => section.Id);
      ApplyPaging(size, index);
    }
  }
}