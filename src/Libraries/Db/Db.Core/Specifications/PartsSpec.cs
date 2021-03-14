using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Part"/> entities
  /// </summary>
  public class PartsSpec
    : BaseSpecification<Part>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Full-text search query</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public PartsSpec(string? search, int size, int index)
      : base(part => string.IsNullOrEmpty(search)
                     || !string.IsNullOrEmpty(part.Keywords) && part.Keywords.Contains(search)
                     || part.Description.Contains(search)
                     || part.PartNumber.Contains(search)
                     || !string.IsNullOrEmpty(part.MakersPartNumber) && part.MakersPartNumber.Contains(search)
      )
    {
      ApplyPaging(size, index);
      SetOrderBy(part => part.PartNumber);
    }
  }
}