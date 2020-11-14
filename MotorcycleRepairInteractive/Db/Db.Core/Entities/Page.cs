namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a single page of a <see cref="Book"/>
  /// </summary>
  public class Page
    : BaseEntity
  {
    /// <summary>
    /// Page source <see cref="Entities.Book"/> reference
    /// </summary>
    public Book? Book { get; set; }

    /// <summary>
    /// Page source <see cref="Entities.Book"/> foreign key
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Beginning of the page
    /// </summary>
    public int StartPageNumber { get; set; }

    /// <summary>
    /// Ending of the page
    /// </summary>
    public int EndPageNumber { get; set; }

    /// <summary>
    /// Page header
    /// </summary>
    public string PageHeader { get; set; } = string.Empty;

    /// <summary>
    /// Figure number
    /// </summary>
    public int FigureNumber { get; set; }

    /// <summary>
    /// Figure description
    /// </summary>
    public string FigureDescription { get; set; } = string.Empty;
  }
}
