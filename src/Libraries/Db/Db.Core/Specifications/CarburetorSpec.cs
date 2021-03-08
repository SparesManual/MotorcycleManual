using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Carburetor"/> entities
  /// </summary>
  public class CarburetorSpec
    : BaseSpecification<Carburetor>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Fuzzy carburetor search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public CarburetorSpec(string? search, int size, int index)
      : base(carb => string.IsNullOrEmpty(search) || carb.Name.Contains(search))
    {
      ApplyPaging(size, index);
      SetOrderBy(carb => carb.Name);
    }
  }
}