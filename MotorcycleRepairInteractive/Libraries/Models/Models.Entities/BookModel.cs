using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class BookModel
    : BaseReplyModel, IBook
  {
    /// <inheritdoc />
    public int Id { get; }

    /// <inheritdoc />
    public string Title { get; }

    /// <inheritdoc />
    public BookModel(int id, string title)
    {
      Id = id;
      Title = title;
    }
  }
}
