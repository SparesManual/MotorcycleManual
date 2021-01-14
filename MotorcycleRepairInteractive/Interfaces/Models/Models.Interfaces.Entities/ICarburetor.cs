namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for carburetor reply
  /// </summary>
  public interface ICarburetor
    : IReply
  {
    /// <summary>
    /// Carburetor id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Carburetor name
    /// </summary>
    string Name { get; }
  }
}