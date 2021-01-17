using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying part <see cref="Property"/> instances
  /// </summary>
  public class PartPropertiesSpec
    : BaseSpecification<Property>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Id of the <see cref="Part"/></param>
    /// <param name="search">Fuzzy property search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public PartPropertiesSpec(int id, string? search, int size, int index)
      : base(property => property.PartId.Equals(id) && (string.IsNullOrEmpty(search) || property.PropertyName.Contains(search)))
    {
      AddInclude(property => property.PropertyType!);
      SetOrderBy(property => property.PropertyName);
      ApplyPaging(size, index);
    }
  }
}
