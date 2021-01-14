using Models.Interfaces.Entities;

namespace Models.Entities
{
  public record PropertyModel
    : IProperty
  {
    /// <inheritdoc />
    public int PropertyTypeId { get; init; }

    /// <inheritdoc />
    public string PropertyName { get; init; }

    /// <inheritdoc />
    public string PropertyValue { get; init; }

    /// <inheritdoc />
    public string Type { get; init; }

    /// <inheritdoc />
    public string Unit { get; init; }
  }
}