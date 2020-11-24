using System.Collections.Generic;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a model
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Model
    : BaseEntity
  {
    /// <summary>
    /// Model name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Collection of parent <see cref="Make"/> mappings
    /// </summary>
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<MakeModels> ParentMakes { get; set; } = new List<MakeModels>();
  }
}
