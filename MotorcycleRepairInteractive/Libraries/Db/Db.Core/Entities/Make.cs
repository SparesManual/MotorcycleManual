using System.Collections.Generic;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a make
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Make
    : BaseEntity
  {
    /// <summary>
    /// Make name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Reference to the parent book
    /// </summary>
    public BookMakes ParentBook { get; set; } = null!;

    /// <summary>
    /// List of all child models
    /// </summary>
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<MakeModels> MakeModels { get; set; } = new List<MakeModels>();
  }
}