using Models.Interfaces.Entities;

namespace Models.Entities
{
  /// <summary>
  /// Model representing a carburetor
  /// </summary>
  public record CarburetorModel
    : ICarburetor
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }
  }
}