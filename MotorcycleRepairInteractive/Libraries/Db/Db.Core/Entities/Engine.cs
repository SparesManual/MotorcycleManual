using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing an engine
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Engine
    : BaseEntity
  {
    /// <summary>
    /// Engine name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Entity displacement
    /// </summary>
    [Range(1, short.MaxValue)]
    public short Displacement { get; set; }

    /// <summary>
    /// Engine carburetor count
    /// </summary>
    [Range(1, short.MaxValue)]
    public short Carburetors { get; set; }

    /// <summary>
    /// Foreign key reference to the <see cref="Entities.Carburetor"/> entity
    /// </summary>
    [Required]
    public int CarburetorId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Carburetor"/> entity
    /// </summary>
    public Carburetor Carburetor { get; set; } = null!;

    /// <summary>
    /// Engine transmission count
    /// </summary>
    [Range(1, short.MaxValue)]
    public short Transmission { get; set; }
  }
}