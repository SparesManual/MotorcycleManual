using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class PropertyModel
    : BaseReplyModel, IProperty
  {
    /// <inheritdoc />
    public int PropertyTypeId { get; }

    /// <inheritdoc />
    public string PropertyName { get; }

    /// <inheritdoc />
    public string PropertyValue { get; }

    /// <inheritdoc />
    public string Type { get; }

    /// <inheritdoc />
    public string Unit { get; }

    /// <inheritdoc />
    public PropertyModel(int propertyTypeId, string propertyName, string propertyValue, string type, string unit)
    {
      PropertyTypeId = propertyTypeId;
      PropertyName = propertyName;
      PropertyValue = propertyValue;
      Type = type;
      Unit = unit;
    }
  }
}