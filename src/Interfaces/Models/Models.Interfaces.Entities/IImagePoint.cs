namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for image point replies
  /// </summary>
  public interface IImagePoint
    : IReply
  {
    /// <summary>
    /// Id of the part on the image
    /// </summary>
    int PartId { get; }

    /// <summary>
    /// Position of the part on the X axis
    /// </summary>
    double PositionX { get; }

    /// <summary>
    /// Position of the part on the Y axis
    /// </summary>
    double PositionY { get; }

    /// <summary>
    /// Number of the part on the image
    /// </summary>
    string PartNumber { get; }
  }
}