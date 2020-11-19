using Db.Core.Entities;

namespace Db.Core.Specifications
{
  public class PartPropertySpec
    : BaseSpecification<Property>
  {
    public PartPropertySpec(int id, int size, int index)
      : base(property => property.PartId.Equals(id))
    {
      AddInclude(property => property.PropertyType!);
      ApplyPaging(size, index);
    }
  }
}
