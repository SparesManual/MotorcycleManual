namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for the book reply
  /// </summary>
  public interface IBook
    : IReply
  {
    /// <summary>
    /// Book id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Book title
    /// </summary>
    string Title { get; }
  }
}