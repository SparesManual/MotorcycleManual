using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a manual
  /// </summary>
  public class Book
    : BaseEntity
  {
    /// <summary>
    /// Book title
    /// </summary>
    [Required]
    public string Title { get; set; } = string.Empty;
  }
}
