using Models.Interfaces.Entities;

namespace Models.Entities
{
  /// <summary>
  /// Model representing a make
  /// </summary>
  public record MakeModel
    : IMake
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }
  }
}