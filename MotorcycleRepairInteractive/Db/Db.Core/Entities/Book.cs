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
  }
}