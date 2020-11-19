using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying part <see cref="Property"/> instances
  /// </summary>
  public class PartPropertySpec
    : BaseSpecification<Property>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Id of the <see cref="Part"/></param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public PartPropertySpec(int id, int size, int index)
      : base(property => property.PartId.Equals(id))
    {
      AddInclude(property => property.PropertyType!);
      ApplyPaging(size, index);
    }
  }
}
