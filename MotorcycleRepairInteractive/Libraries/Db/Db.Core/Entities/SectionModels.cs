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
    /// <summary>
    /// Foreign key reference to the <see cref="Section"/>
    /// </summary>
    [Required]
    public int SectionId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Section"/>
    /// </summary>
    public Section Section { get; set; } = null!;

    /// <summary>
    /// Foreign key reference to the <see cref="Model"/>
    /// </summary>
    [Required]
    public int ModelId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Model"/>
    /// </summary>
    public Model Model { get; set; } = null!;
  }
}