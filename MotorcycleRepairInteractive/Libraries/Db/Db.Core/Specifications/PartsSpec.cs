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
    /// <param name="search"></param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public PartsSpec(string? search, int size, int index)
      : base(part => Part.StringSearch(part, search))
    {
      ApplyPaging(size, index);
      SetOrderBy(part => part.PartNumber);
    }
  }
}