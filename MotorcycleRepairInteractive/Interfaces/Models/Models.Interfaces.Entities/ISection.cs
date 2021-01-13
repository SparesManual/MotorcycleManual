namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for section replies
  /// </summary>
  public interface ISection
    : IReply
  {
    /// <summary>
    /// Section id
    /// </summary>
    int Id { get; }
    /// <summary>
    /// Parenting book id
    /// </summary>
    int BookId { get; }
    /// <summary>
    /// Section header
    /// </summary>
    string Header { get; }
    /// <summary>
    /// Section start page
    /// </summary>
    int StartPage { get; }
    /// <summary>
    /// Section end page
    /// </summary>
    int EndPage { get; }
    /// <summary>
    /// Figure number
    /// </summary>
    int FigureNumber { get; }
    /// <summary>
    /// Figure description
    /// </summary>
    string FigureDescription { get; }
  }
}