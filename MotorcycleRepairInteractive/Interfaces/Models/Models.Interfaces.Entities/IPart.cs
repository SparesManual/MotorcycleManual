namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for parts
  /// </summary>
  public interface IPart
    : IReply
  {
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

    /// <summary>
    /// Description of the part by the maker
    /// </summary>
    string MakersDescription { get; }
  }
}