using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="SectionParts"/> entities based on their parent <see cref="SectionPartParents"/> entity
  /// </summary>
  public class SectionPartChildrenSpec
    : BaseSpecification<SectionPartParents, SectionParts>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Id of the <see cref="SectionPartParents"/></param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionPartChildrenSpec(int id, int size, int index)
    {
      SetExtractor(sectionPartParents =>
        sectionPartParents
          .Where(parent => parent.ParentId == id)
          .Include(parent => parent.Child)
          .Select(x => x.Child));

      SetOrderBy(part => part.SectionId);
      ApplyPaging(size, index);
    }
  }
}