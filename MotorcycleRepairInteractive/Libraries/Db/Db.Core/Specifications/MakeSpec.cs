using Db.Core.Entities;

namespace Db.Core.Specifications
{
  public class MakeSpec
    : BaseSpecification<Make>
  {
    public MakeSpec(string? search, int size, int index)
      : base(make => string.IsNullOrEmpty(search) || make.Name.Contains(search))
    {
      ApplyPaging(size, index);
      SetOrderBy(make => make.Name);
    }
  }
}