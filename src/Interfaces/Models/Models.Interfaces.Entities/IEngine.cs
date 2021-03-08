namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for engine reply
  /// </summary>
  public interface IEngine
    : IReply
  {
    /// <summary>
    /// Engine id
    /// </summary>
    int Id { get; }
    /// <summary>
    /// Engine name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Engine displacement
    /// </summary>
    int Displacement { get; }
    /// <summary>
    /// Engine carburetors count
    /// </summary>
    int Carburetors { get; }
    /// <summary>
    /// Engine carburetor name
    /// </summary>
    string Carburetor { get; }
    /// <summary>
    /// Engine transmission
    /// </summary>
    int Transmission { get; }
  }
}