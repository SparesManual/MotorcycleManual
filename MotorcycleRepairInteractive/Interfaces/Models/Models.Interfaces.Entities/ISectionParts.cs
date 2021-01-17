namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for section parts reply
  /// </summary>
  public interface ISectionParts
    : IReply
  {
    /// <summary>
    /// Section parts id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Part id
    /// </summary>
    int PartId { get; }

    /// <summary>
    /// Number of the page in the parent book of the section
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// Mapping remarks
    /// </summary>
    string Remarks { get; }

    /// <summary>
    /// Additional section information
    /// </summary>
    string AdditionalInfo { get; }

    /// <summary>
    /// Occurrence of the part in the parent section
    /// </summary>
    int Quantity { get; }
  }
}