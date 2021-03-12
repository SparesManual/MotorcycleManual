using Models.Interfaces.Entities;

namespace Models.Entities
{
  /// <summary>
  /// Model representing an image point
  /// </summary>
  public record ImagePointModel
    : IImagePoint
  {
    /// <inheritdoc />
    public int PartId { get; init; }

    /// <inheritdoc />
    public double PositionX { get; init; }

    /// <inheritdoc />
    public double PositionY { get; init; }

    /// <inheritdoc />
    public string PartNumber { get; init; }
  }
}