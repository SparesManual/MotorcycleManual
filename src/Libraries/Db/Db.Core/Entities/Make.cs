using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    [Required]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Model"/> entities manufactured by this <see cref="Make"/>
    /// </summary>
    public List<Model> Models { get; set; } = null!;
  }
}