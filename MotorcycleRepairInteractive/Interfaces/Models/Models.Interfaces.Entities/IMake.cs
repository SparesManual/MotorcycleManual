namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for the make reply
  /// </summary>
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