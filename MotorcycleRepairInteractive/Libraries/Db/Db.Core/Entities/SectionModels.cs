using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps <see cref="Entities.Model"/> to a given <see cref="Entities.Section"/> entity
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class SectionModels
    : IEntity
  {
    [Required]
    public int SectionId { get; set; }

    public Section Section { get; set; } = null!;

    [Required]
    public int ModelId { get; set; }

    public Model Model { get; set; } = null!;
  }
}