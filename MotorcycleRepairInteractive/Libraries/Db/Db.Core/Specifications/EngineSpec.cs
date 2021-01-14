using Db.Core.Entities;

namespace Db.Core.Specifications
{
  public class EngineSpec
    : BaseSpecification<Engine>
  {
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