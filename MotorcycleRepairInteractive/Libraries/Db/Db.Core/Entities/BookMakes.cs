using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Mapping of <see cref="Book"/> to a <see cref="Make"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class BookMakes
    : IEntity
  {
    /// <summary>
    /// Foreign key reference to the <see cref="Entities.Book"/> entity
    /// </summary>
    [Required]
    public int BookId { get; set; }

    /// <summary>
    /// Reference to the <see cref="Entities.Book"/> entity
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
    public Make? Make { get; set; }
  }
}