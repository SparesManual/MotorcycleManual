using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps an <see cref="Entities.Assembly"/> to a <see cref="Entities.Part"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class AssemblyParts
    : IEntity
  {
    /// <summary>
    /// Referenced <see cref="Entities.Assembly"/> instance
    /// </summary>
    public Assembly? Assembly { get; set; }

    /// <summary>
    /// Foreign key of the <see cref="Entities.Assembly"/>
    /// </summary>
    [Required]
    public int AssemblyId { get; set; }

    /// <summary>
    /// Referenced <see cref="Entities.Part"/> instance
    /// </summary>
    public Part? Part { get; set; }

    /// <summary>
    /// Foreign key of the <see cref="Entities.Part"/>
    /// </summary>
    [Required]
    public int PartId { get; set; }

    /// <summary>
    /// The number of occurrences of the given <see cref="Part"/> inside the given <see cref="Assembly"/>
    /// </summary>
    [Required]
    public int PartOccurrence { get; set; }
  }
}