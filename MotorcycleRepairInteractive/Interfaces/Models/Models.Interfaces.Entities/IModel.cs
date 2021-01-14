namespace Models.Interfaces.Entities
{
  /// <summary>
  /// Interface for model reply
  /// </summary>
  public interface IModel
    : IReply
  {
    /// <summary>
    /// Model id
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Id of the book covering this model
    /// </summary>
    int BookId { get; }

    /// <summary>
    /// Model make id
    /// </summary>
    int MakeId { get; }

    /// <summary>
    /// Model engine id
    /// </summary>
    int EngineId { get; }

    /// <summary>
    /// Model name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Model year
    /// </summary>
    int Year { get; }
  }
}