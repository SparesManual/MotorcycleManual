using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Db.Core.Entities
{
  /// <summary>
  /// Assembly model type
  /// </summary>
  /// <remarks>
  /// Unlike a <see cref="Part"/>, an <see cref="Assembly"/> is made out of parts and other assemblies
  /// <para>
  /// <see cref="Part"/> instances within a given assembly are covered by the <see cref="Entities.AssemblyParts"/>
  /// </para>
  /// <para>
  /// Assemblies within a given assembly are covered by the <see cref="Entities.AssemblySubAssemblies"/>
  /// </para>
  /// </remarks>
  [Table(nameof(Assembly))]
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Assembly
    : Model
  {
    /// <summary>
    /// References to child <see cref="Part"/> instances
    /// </summary>
    public List<AssemblyParts> AssemblyParts { get; set; } = new List<AssemblyParts>();

    /// <summary>
    /// References to child <see cref="Assembly"/> instances
    /// </summary>
    public List<AssemblySubAssemblies> AssemblySubAssemblies { get; set; } = new List<AssemblySubAssemblies>();

    /// <summary>
    /// References to parent <see cref="Assembly"/> instances
    /// </summary>
    public List<AssemblySubAssemblies> AssemblyParentAssemblies { get; set; } = new List<AssemblySubAssemblies>();
  }
}