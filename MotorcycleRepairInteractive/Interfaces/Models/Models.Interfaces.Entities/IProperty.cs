namespace Models.Interfaces.Entities
{
  public interface IProperty
    : IReply
  {
    /// <summary>
    /// Type of the given property
    /// </summary>
    int PropertyTypeId { get; }

    /// <summary>
    /// Property name
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// Property value
    /// </summary>
    string PropertyValue { get; }

    /// <summary>
    /// Property type name
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Property unit
    /// </summary>
    string Unit { get; }
  }
}