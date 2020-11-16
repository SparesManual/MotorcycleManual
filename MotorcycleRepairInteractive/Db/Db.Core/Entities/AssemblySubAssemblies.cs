using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps an <see cref="Entities.Assembly"/> as a parent to another <see cref="Entities.Assembly"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class AssemblySubAssemblies
    : IEntity
  {
    /// <summary>
    /// Reference to the parent <see cref="Entities.Assembly"/>
    /// </summary>
    public Assembly? Assembly { get; set; }

    /// <summary>
    /// Foreign key of the parent <see cref="Entities.Assembly"/>
    /// </summary>
    [Required]
    public int AssemblyId { get; set; }

    /// <summary>
    /// Reference to the child <see cref="Entities.Assembly"/>
    /// </summary>
    public Assembly? SubAssembly { get; set; }

    /// <summary>
    /// Foreign key of the child <see cref="Entities.Assembly"/>
    /// </summary>
    [Required]
    public int SubAssemblyId { get; set; }
  }
}