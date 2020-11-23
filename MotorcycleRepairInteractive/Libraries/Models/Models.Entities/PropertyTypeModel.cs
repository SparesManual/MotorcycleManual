using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class PropertyTypeModel
    : BaseReplyModel, IPropertyType
  {
    /// <inheritdoc />
    public int Id { get; }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public string Unit { get; }

    /// <inheritdoc />
    public PropertyTypeModel(int id, string name, string unit)
    {
      Id = id;
      Name = name;
      Unit = unit;
    }
  }
}