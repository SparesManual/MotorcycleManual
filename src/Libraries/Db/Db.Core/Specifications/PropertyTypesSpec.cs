using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="PropertyType"/> entities
  /// </summary>
  public class PropertyTypesSpec
    : BaseSpecification<PropertyType>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public PropertyTypesSpec(int size, int index)
    {
      ApplyPaging(size, index);
      SetOrderBy(type => type.Name);
    }
  }
}