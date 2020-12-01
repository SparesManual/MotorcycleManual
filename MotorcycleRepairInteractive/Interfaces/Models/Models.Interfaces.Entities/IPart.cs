namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for part reply
  /// </summary>
  public interface IPart
    : IReply
  {
    /// <summary>
    /// Part id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Part number
    /// </summary>
    string PartNumber { get; }

    /// <summary>
    /// Makers part number
    /// </summary>
    string MakersPartNumber { get; }

    /// <summary>
    /// Description of the part
    /// </summary>
    string Description { get; }
  }
}