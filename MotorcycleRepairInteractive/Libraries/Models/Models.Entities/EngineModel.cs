using Models.Interfaces.Entities;

namespace Models.Entities
{
  public record EngineModel
    : IEngine
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public int Displacement { get; init; }

    /// <inheritdoc />
    public int Carburetors { get; init; }

    /// <inheritdoc />
    public string Carburetor { get; init; }

    /// <inheritdoc />
    public int Transmission { get; init; }
  }
}