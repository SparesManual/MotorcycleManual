namespace Db.Core.Entities
{
  /// <summary>
  /// Interface for entities
  /// </summary>
  public interface IEntity
  {
    /// <summary>
    /// Id of the given entity
    /// </summary>
    public int Id { get; set; }
  }
}