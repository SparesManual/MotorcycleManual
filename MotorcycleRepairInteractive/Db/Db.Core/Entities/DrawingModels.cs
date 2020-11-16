using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps a <see cref="Entities.Drawing"/> to a <see cref="Entities.Model"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class DrawingModels
    : IEntity
  {
    /// <summary>
    /// Foreign key of the <see cref="DrawingId"/>
    /// </summary>
    [Required]
    public int DrawingId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Drawing"/>
    /// </summary>
    public Drawing? Drawing { get; set; }

    /// <summary>
    /// Foreign key pf the <see cref="Model"/>
    /// </summary>
    [Required]
    public int ModelId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Model"/>
    /// </summary>
    public Model? Model { get; set; }

    /// <summary>
    /// <see cref="Model"/> instance reference number on the given <see cref="Drawing"/>
    /// </summary>
    [Required]
    public int PartReference { get; set; }

    /// <summary>
    /// Number of the page on which the <see cref="Model"/> occurs
    /// </summary>
    [Required]
    public int PageNumber { get; set; }

    /// <summary>
    /// Horizontal position of the <see cref="Model"/> on the <see cref="Drawing"/>
    /// </summary>
    public double PositionX { get; set; }

    /// <summary>
    /// Vertical position of the <see cref="Model"/> on the <see cref="Drawing"/>
    /// </summary>
    public double PositionY { get; set; }
  }
}