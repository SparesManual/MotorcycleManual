using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Model that is displayed in <see cref="Drawing"/> instances
  /// </summary>
  public class Model
    : BaseEntity
  {
    /// <summary>
    /// Identification of the given model
    /// </summary>
    [MaxLength(32)]
    public string ModelNumber { get; set; } = string.Empty;

    /// <summary>
    /// Remarks to the model
    /// </summary>
    [MaxLength(128)]
    public string Remarks { get; set; } = string.Empty;

    /// <summary>
    /// Many-to-many reference to <see cref="Drawing"/> instances
    /// </summary>
    public List<DrawingModels> DrawingModels { get; set; } = new List<DrawingModels>();
  }
}