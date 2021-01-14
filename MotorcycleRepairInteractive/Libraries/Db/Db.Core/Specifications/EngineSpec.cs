using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Engine"/> entities
  /// </summary>
  public class EngineSpec
    : BaseSpecification<Engine>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Full-text search query</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public EngineSpec(string? search, int size, int index)
      : base(engine => string.IsNullOrEmpty(search)
                       || engine.Name.Contains(search)
                       || engine.Carburetor.Name.Contains(search))
    {
      ApplyPaging(size, index);
      AddInclude(engine => engine.Carburetor);
      SetOrderBy(engine => engine.Name);
    }
  }
}