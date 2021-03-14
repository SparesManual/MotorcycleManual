using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="ImagePoint"/> entities
  /// </summary>
  public class ImagePointsSpec
    : BaseSpecification<ImagePoint, ImagePoint>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Section id</param>
    public ImagePointsSpec(int id)
    {
      SetExtractor(imagePoints => imagePoints
        .Select(imagePoint => imagePoint)
        .Include(imagePoint => imagePoint.SectionParts)
        .ThenInclude(sectionParts => sectionParts.Part)
        .Where(imagePoint => imagePoint.SectionParts.SectionId.Equals(id)));
    }
  }
}