using Models.Interfaces.Entities;

namespace Models.Entities
{
  public record CarburetorModel
    : ICarburetor
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }
  }
}