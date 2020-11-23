namespace Models.Interfaces.Entities
{
  public interface IBook
    : IReply
  {
    int Id { get; }

    /// <summary>
    /// Book title
    /// </summary>
    string Title { get; }
  }
}