using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a repair manual
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Book
    : BaseEntity
  {
    /// <summary>
    /// Book title
    /// </summary>
    [Required]
    [MaxLength(128)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Model"/> entities covered by this <see cref="Book"/>
    /// </summary>
    public List<Model> BookModels { get; set; } = new List<Model>();

    /// <summary>
    /// <see cref="Section"/> entities in this <see cref="Book"/>
    /// </summary>
    public List<Section> BookSections { get; set; } = new List<Section>();
  }
}