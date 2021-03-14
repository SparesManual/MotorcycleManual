namespace Db.Interfaces
{
  /// <summary>
  /// Interface for entities with keywords
  /// </summary>
  public interface IHasKeywords
    : IEntity
  {
    /// <summary>
    /// Entity keywords
    /// </summary>
    string Keywords { get; }
  }
}