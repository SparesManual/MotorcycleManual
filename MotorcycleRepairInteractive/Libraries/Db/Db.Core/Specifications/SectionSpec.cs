using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="Section"/> entities
  /// </summary>
  public class SectionSpec
    : BaseSpecification<Section>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Full-text search search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionSpec(string? search, int size, int index)
      : base(section => string.IsNullOrEmpty(search)
                        || !string.IsNullOrEmpty(section.SectionHeader) && section.SectionHeader.Contains(search)
                        || !string.IsNullOrEmpty(section.FigureDescription) && section.FigureDescription.Contains(search)
                        || true)
    {
      SetOrderBy(part => part.Id);
      ApplyPaging(size, index);
    }
  }
}