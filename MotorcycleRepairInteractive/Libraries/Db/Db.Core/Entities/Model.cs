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
    /// <summary>
    /// Foreign key reference to the <see cref="Entities.Book"/> entity covering this model
    /// </summary>
    [AllowNull]
    public int? BookId { get; set; } = null;

    /// <summary>
    /// Reference to the <see cref="Entities.Book"/> entity covering this model
    /// </summary>
    public Book Book { get; set; } = null!;

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
    /// Foreign key reference to the <see cref="Entities.Engine"/> entity
    /// </summary>
    [Required]
    public int EngineId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Engine"/> entity
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
