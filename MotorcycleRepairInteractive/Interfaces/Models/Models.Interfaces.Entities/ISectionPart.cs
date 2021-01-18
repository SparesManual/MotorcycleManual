namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for section part replies
  /// </summary>
  public interface ISectionPart
    : IPart
  {
    /// <summary>
    /// Page number in the book containing the parent section
    /// </summary>
    int PageNumber { get; }
    /// <summary>
    /// Remarks regarding the part in the section
    /// </summary>
    string Remarks { get; }
    /// <summary>
    /// Additional parent section information
    /// </summary>
    string AdditionalInfo { get; }
    /// <summary>
    /// Occurrence of the part in the parent section
    /// </summary>
    int Quantity { get; }
  }
}