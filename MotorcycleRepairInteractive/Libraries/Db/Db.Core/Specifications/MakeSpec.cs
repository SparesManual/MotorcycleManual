using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Make"/> entities
  /// </summary>
  public class MakeSpec
    : BaseSpecification<Make>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Full-text search query</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public MakeSpec(string? search, int size, int index)
      : base(make => string.IsNullOrEmpty(search) || make.Name.Contains(search))
    {
      ApplyPaging(size, index);
      SetOrderBy(make => make.Name);
    }
  }
}