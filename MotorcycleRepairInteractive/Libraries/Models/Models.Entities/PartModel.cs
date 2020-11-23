using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class PartModel
    : BaseReplyModel, IPart
  {
    /// <inheritdoc />
    public int Id { get; }

    /// <inheritdoc />
    public string PartNumber { get; }

    /// <inheritdoc />
    public string MakersPartNumber { get; }

    /// <inheritdoc />
    public string Description { get; }

    /// <inheritdoc />
    public string MakersDescription { get; }

    /// <inheritdoc />
    public PartModel(int id, string partNumber, string makersPartNumber, string description, string makersDescription)
    {
      Id = id;
      PartNumber = partNumber;
      MakersPartNumber = makersPartNumber;
      Description = description;
      MakersDescription = makersDescription;
    }
  }
}