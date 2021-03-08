using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a carburetor
  /// </summary>
  public class Carburetor
    : BaseEntity
  {
    /// <summary>
    /// Carburetor name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;
  }
}