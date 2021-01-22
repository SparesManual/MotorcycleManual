using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Model"/> entities
  /// </summary>
  public class ModelSpec
    : BaseSpecification<Model>
  {
    /// <summary>
    /// ModelSpec constructor
    /// </summary>
    /// <param name="search">Fuzzy model search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public ModelSpec(string? search, int size, int index)
      : base(model => string.IsNullOrEmpty(search)
                      || model.Name.Contains(search)
                      || search.Contains(model.Year.ToString()))
    {
      ApplyPaging(size, index);
      SetOrderBy(model => model.Year);
    }

    /// <summary>
    /// ModelSpec constructor
    /// </summary>
    /// <param name="id">Model id</param>
    /// <param name="includeEngine">Include the engine in the result</param>
    public ModelSpec(int id, bool includeEngine)
      : base(model => model.Id.Equals(id))
    {
      if (!includeEngine)
        return;

      AddInclude(model => model.Engine);
      AddInclude(model => model.Engine.Carburetor);
    }
  }
}