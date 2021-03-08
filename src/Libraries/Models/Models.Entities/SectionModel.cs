using Models.Interfaces.Entities;

namespace Models.Entities
{
  /// <summary>
  /// Model representing a section
  /// </summary>
  public record SectionModel
    : ISection
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public int BookId { get; init; }

    /// <inheritdoc />
    public string Header { get; init; }

    /// <inheritdoc />
    public int StartPage { get; init; }

    /// <inheritdoc />
    public int EndPage { get; init; }

    /// <inheritdoc />
    public int FigureNumber { get; init; }

    /// <inheritdoc />
    public string FigureDescription { get; init; }
  }
}