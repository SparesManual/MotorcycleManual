using Models.Interfaces.Entities;

namespace Models.Entities
{
  public class PropertyTypeModel
    : IPropertyType
  {
    /// <inheritdoc />
    public int Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public string Unit { get; init; }

  }
}