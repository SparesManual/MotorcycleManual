using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Base class for all entities with keywords
  /// </summary>
  public abstract class BaseEntityWithKeywords
    : BaseEntity, IHasKeywords
  {
    /// <inheritdoc />
    [Required]
    public string Keywords { get; set; } = string.Empty;
  }
}