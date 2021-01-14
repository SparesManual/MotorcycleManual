using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class SectionPartModel
    : ISectionParts
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public int PartId { get; init; }

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