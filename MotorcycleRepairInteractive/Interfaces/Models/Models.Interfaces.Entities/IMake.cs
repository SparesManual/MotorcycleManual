namespace Models.Interfaces.Entities
{
  public interface IMake
    : IReply
  {
    /// <summary>
    /// Make id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Make name
    /// </summary>
    string Name { get; }
  }
}