namespace Models.Interfaces.Entities
{
  public interface IPropertyType
    : IReply
  {
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