using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Base class for all entities
  /// </summary>
  public abstract class BaseEntity
    : IEntity
  {
    /// <inheritdoc />
    [Required]
    public int Id { get; set; }
  }
}
