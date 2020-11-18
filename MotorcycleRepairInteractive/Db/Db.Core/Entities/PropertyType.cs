using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// <see cref="Property"/> type
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class PropertyType
    : BaseEntity
  {
    /// <summary>
    /// Property type name
    /// </summary>
    [Required]
    [MaxLength(32)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Property unit
    /// </summary>
    [MaxLength(16)]
    public string Unit { get; set; } = string.Empty;
  }
}