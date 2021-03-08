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
    public string Keywords { get; set; }
  }
}