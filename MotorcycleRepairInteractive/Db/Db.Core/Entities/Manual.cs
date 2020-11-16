using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a manual
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Manual
    : BaseEntity
  {
    /// <summary>
    /// <see cref="Manual"/> title
    /// </summary>
    [Required]
    [MaxLength(80)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Manual"/> subtitle
    /// </summary>
    [MaxLength(128)]
    public string SubTitle { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Manual"/> version
    /// </summary>
    [MaxLength(16)]
    public string Version { get; set; } = string.Empty;
  }
}
