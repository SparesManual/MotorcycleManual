using Models.Interfaces.Entities;

namespace Models.Entities
{
  /// <summary>
  /// Model representing a model
  /// </summary>
  public record ModelModel
    : IModel
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public int BookId { get; init; }

    /// <inheritdoc />
    public int MakeId { get; init; }

    /// <inheritdoc />
    public int EngineId { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public int Year { get; init; }
  }
}