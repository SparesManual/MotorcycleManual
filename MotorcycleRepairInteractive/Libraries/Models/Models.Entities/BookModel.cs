using Models.Interfaces.Entities;

namespace Models.Entities
{
  public record BookModel
    : IBook
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Title { get; init; }
  }
}
