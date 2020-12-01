using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Format of the <see cref="Property"/> value
  /// </summary>
  public class FormatType
    : BaseEntity
  {
    /// <summary>
    /// Format name
    /// </summary>
    [Required]
    [MaxLength(32)]
    public string Name { get; set; } = string.Empty;
  }
}
