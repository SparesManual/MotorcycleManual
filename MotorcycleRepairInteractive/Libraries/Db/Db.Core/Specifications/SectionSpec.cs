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
    /// SectionSpec constructor
    /// </summary>
    /// <param name="search">Full-text section search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionSpec(string? search, int size, int index)
      : base(section => string.IsNullOrEmpty(search)
                        || !string.IsNullOrEmpty(section.SectionHeader) && section.SectionHeader.Contains(search)
                        || !string.IsNullOrEmpty(section.FigureDescription) && section.FigureDescription.Contains(search)
                        || true)
    {
      SetOrderBy(section => section.Id);
      ApplyPaging(size, index);
    }

    /// <summary>
    /// SectionSpec constructor
    /// </summary>
    /// <param name="bookId">Parent book id</param>
    /// <param name="search">Full-text section search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public SectionSpec(int bookId, string search, int size, int index)
      : base(section => section.BookId.Equals(bookId)
                        && (string.IsNullOrEmpty(search)
                            || !string.IsNullOrEmpty(section.SectionHeader) && section.SectionHeader.Contains(search)
                            || !string.IsNullOrEmpty(section.FigureDescription) && section.FigureDescription.Contains(search)
                            || true))
    {
      SetOrderBy(section => section.FigureNumber);
      ApplyPaging(size, index);
    }
  }
}