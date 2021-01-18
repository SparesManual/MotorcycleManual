using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class SectionPartModel
    : ISectionPart
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string PartNumber { get; init; }

    /// <inheritdoc />
    public string MakersPartNumber { get; init; }

    /// <inheritdoc />
    public string Description { get; init; }

    /// <inheritdoc />
    public int PageNumber { get; init; }

    /// <inheritdoc />
    public string Remarks { get; init; }

    /// <inheritdoc />
    public string AdditionalInfo { get; init; }

    /// <inheritdoc />
    public int Quantity { get; init; }
  }
}