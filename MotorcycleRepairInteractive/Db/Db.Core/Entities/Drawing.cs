using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Drawing of one or more <see cref="Model"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Drawing
    : BaseEntity
  {
    /// <summary>
    /// Reference to the <see cref="Entities.Manual"/> to which this <see cref="Drawing"/> belongs
    /// </summary>
    public Manual? Manual { get; set; }

    /// <summary>
    /// Foreign key of the <see cref="Entities.Manual"/> to which this <see cref="Drawing"/> belongs
    /// </summary>
    [Required]
    public int ManualId { get; set; }

    /// <summary>
    /// Drawing figure number
    /// </summary>
    [Required]
    public int Number { get; set; }

    /// <summary>
    /// Drawing figure name
    /// </summary>
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Page number on which the <see cref="Drawing"/> first occurs in the <see cref="Entities.Manual"/>
    /// </summary>
    /// <remarks>
    /// A <see cref="Drawing"/> can span across multiple pages
    /// </remarks>
    [Required]
    public int PageStart { get; set; }

    /// <summary>
    /// Page number on which the <see cref="Drawing"/> last occurs in the <see cref="Entities.Manual"/>
    /// </summary>
    /// <remarks>
    /// A <see cref="Drawing"/> can span across multiple pages
    /// </remarks>
    [Required]
    public int PageEnd { get; set; }

    /// <summary>
    /// Many-to-many reference to <see cref="Model"/> instances
    /// </summary>
    public List<DrawingModels> DrawingModels { get; set; } = new List<DrawingModels>();
  }
}