using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a repair manual
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Book
    : BaseEntity
  {
    /// <summary>
    /// Book title
    /// </summary>
    [Required]
    [MaxLength(128)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Engine number
    /// </summary>
    [MaxLength(32)]
    public string EngineNumber { get; set; } = string.Empty;

    /// <summary>
    /// Makes covered by this book
    /// </summary>
    public List<BookMakes> BookMakes { get; set; } = new List<BookMakes>();
  }
}