using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Db.Core.Entities
{
  /// <summary>
  /// <see cref="Part"/> model type
  /// </summary>
  /// <remarks>
  /// Specific type of <see cref="Model"/> that can have properties but no other models grouped inside it
  /// </remarks>
  [Table(nameof(Part))]
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Part
    : Model
  {
    /// <summary>
    /// <see cref="Part"/> makers number
    /// </summary>
    [MaxLength(32)]
    public string MakersNumber { get; set; } = string.Empty;

    /// <summary>
    /// Description of the <see cref="Part"/>
    /// </summary>
    [MaxLength(128)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// References to parent <see cref="Assembly"/> instances
    /// </summary>
    public List<AssemblyParts> ParentAssemblies { get; set; } = new List<AssemblyParts>();
  }
}