namespace Db.Core.Entities
{
  /// <summary>
  /// Base class for all entities
  /// </summary>
  public abstract class BaseEntity
  {
    /// <summary>
    /// Id of the given entity
    /// </summary>
    public int Id { get; set; }
  }
}
