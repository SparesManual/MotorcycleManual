using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Base class for all entities
  /// </summary>
  public abstract class BaseEntity
    : IEntity
  {
    /// <summary>
    /// Id of the given entity
    /// </summary>
    [Required]
    public int Id { get; set; }
  }
}
