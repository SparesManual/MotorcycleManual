using Db.Core.Entities;

namespace Db.Core.Specifications
{
  public class ModelSpec
    : BaseSpecification<Model>
  {
    public ModelSpec(string? search, int size, int index)
      : base(model => string.IsNullOrEmpty(search)
                      || model.Name.Contains(search)
                      || search.Contains(model.Year.ToString()))
    {
      ApplyPaging(size, index);
      SetOrderBy(model => model.Year);
    }

    public ModelSpec(int id, bool includeEngine)
      : base(model => model.Id.Equals(id))
    {
      if (includeEngine)
        AddInclude(model => model.Engine);
    }
  }
}