namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a manual
  /// </summary>
  public class Book
    : BaseEntity
  {
    /// <summary>
    /// Book title
    /// </summary>
    public string Title { get; set; } = string.Empty;
  }
}
