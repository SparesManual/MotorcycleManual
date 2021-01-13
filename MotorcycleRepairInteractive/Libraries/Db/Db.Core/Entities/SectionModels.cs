using Db.Interfaces;

namespace Db.Core.Entities
{
  public class SectionModels
    : IEntity
  {
    public int SectionId { get; set; }

    public Section Section { get; set; } = null!;

    public int ModelId { get; set; }

    public Model Model { get; set; } = null!;
  }
}