using Db.Core.Entities;

namespace Db.Core.Specifications
{
  public class CarburetorSpec
    : BaseSpecification<Carburetor>
  {
    public CarburetorSpec(string? search, int size, int index)
      : base(carb => string.IsNullOrEmpty(search) || carb.Name.Contains(search))
    {
      ApplyPaging(size, index);
      SetOrderBy(carb => carb.Name);
    }
  }
}