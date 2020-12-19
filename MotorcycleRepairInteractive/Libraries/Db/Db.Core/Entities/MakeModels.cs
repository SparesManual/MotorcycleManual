using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps a <see cref="Make"/> to a <see cref="Model"/>
  /// </summary>
  /// <remarks>
  /// Adds additional attributes to the relation
  /// </remarks>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class MakeModels
    : IEntity
  {
    /// <summary>
    /// Foreign key reference to the <see cref="Entities.Make"/> entity
    /// </summary>
    [Required]
    public int MakeId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Make"/> entity
    /// </summary>
    public Make Make { get; set; } = null!;

    /// <summary>
    /// Foreign key reference to the <see cref="Entities.Model"/> entity
    /// </summary>
    [Required]
    public int ModelId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Model"/> entity
    /// </summary>
    public Model Model { get; set; } = null!;

    /// <summary>
    /// Model start year
    /// </summary>
    public int YearFrom { get; set; }
    /// <summary>
    /// Model end year
    /// </summary>
    public int YearTo { get; set; }
  }
}