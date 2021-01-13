using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a model
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Model
    : BaseEntity
  {
    [AllowNull]
    public int? BookId { get; set; } = null;

    public Book Book { get; set; } = null!;

    [Required]
    public int MakeId { get; set; }

    public Make Make { get; set; } = null!;

    [Required]
    public int EngineId { get; set; }

    /// <summary>
    /// Model engine
    /// </summary>
    public Engine Engine { get; set; } = null!;

    /// <summary>
    /// Model name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Model year
    /// </summary>
    public int Year { get; set; }

    public List<SectionModels> ModelSections { get; set; } = null!;
  }
}
