namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for property type replies
  /// </summary>
  public interface IPropertyType
    : IReply
  {
    /// <summary>
    /// Property type id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Property type name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Property unit
    /// </summary>
    string Unit { get; }
  }
}