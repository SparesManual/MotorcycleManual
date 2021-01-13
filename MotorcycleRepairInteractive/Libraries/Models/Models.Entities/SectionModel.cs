using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class SectionModel
    : BaseReplyModel, ISection
  {
    /// <inheritdoc />
    public int Id { get; }

    /// <inheritdoc />
    public int BookId { get; }

    /// <inheritdoc />
    public string Header { get; }

    /// <inheritdoc />
    public int StartPage { get; }

    /// <inheritdoc />
    public int EndPage { get; }

    /// <inheritdoc />
    public int FigureNumber { get; }

    /// <inheritdoc />
    public string FigureDescription { get; }

    /// <inheritdoc />
    public SectionModel(int id, int bookId, string header, int startPage, int endPage, int figureNumber, string figureDescription)
    {
      Id = id;
      BookId = bookId;
      Header = header;
      StartPage = startPage;
      EndPage = endPage;
      FigureNumber = figureNumber;
      FigureDescription = figureDescription;
    }
  }
}