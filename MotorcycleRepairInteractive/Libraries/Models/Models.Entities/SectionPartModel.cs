using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class SectionPartModel
    : BaseReplyModel, ISectionParts
  {
    /// <inheritdoc />
    public int Id { get; }

    /// <inheritdoc />
    public int PartId { get; }

    /// <inheritdoc />
    public int PageNumber { get; }

    /// <inheritdoc />
    public string Remarks { get; }

    /// <inheritdoc />
    public string AdditionalInfo { get; }

    /// <inheritdoc />
    public int Quantity { get; }

    /// <inheritdoc />
    public SectionPartModel(int id, int partId, int pageNumber, string remarks, string additionalInfo, int quantity)
    {
      Id = id;
      PartId = partId;
      PageNumber = pageNumber;
      Remarks = remarks;
      AdditionalInfo = additionalInfo;
      Quantity = quantity;
    }
  }
}