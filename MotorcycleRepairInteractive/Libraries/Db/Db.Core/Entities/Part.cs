using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Specific part
  /// </summary>
  /// <remarks>
  /// Can occur in a <see cref="Section"/> entity via the <see cref="SectionParts"/> mapping table
  /// </remarks>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Part
    : BaseEntity
  {
    /// <summary>
    /// Part number
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string PartNumber { get; set; } = string.Empty;

    /// <summary>
    /// Makers part number
    /// </summary>
    [MaxLength(64)]
    public string? MakersPartNumber { get; set; }

    /// <summary>
    /// Description of the part
    /// </summary>
    [Required]
    [MaxLength(128)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Section"/> entities have this given part
    /// </summary>
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<SectionParts> PartSections { get; set; } = new List<SectionParts>();
  }
}