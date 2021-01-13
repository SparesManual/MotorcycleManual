using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps <see cref="Entities.Part"/> to a given <see cref="Entities.Section"/> entity
  /// </summary>
  /// <remarks>
  /// Adds additional attributes alongside this relationship
  /// </remarks>
  public class SectionParts
    : BaseEntity
  {
    #region Mapping

    /// <summary>
    /// Foreign key reference to the parent <see cref="Entities.Section"/> entity
    /// </summary>
    [Required]
    public int SectionId { get; set; }

    /// <summary>
    /// Reference to the parent <see cref="Entities.Section"/> entity
    /// </summary>
    public Section Section { get; set; } = null!;

    /// <summary>
    /// Foreign key reference to the child <see cref="Entities.Part"/> entity
    /// </summary>
    [Required]
    public int PartId { get; set; }

    /// <summary>
    /// Reference to the child <see cref="Entities.Part"/> entity
    /// </summary>
    public Part Part { get; set; } = null!;

    /// <summary>
    /// Section parts belonging to this section part
    /// </summary>
    public List<SectionPartParents> ChildSections { get; set; } = new List<SectionPartParents>();

    /// <summary>
    /// Section parts image points
    /// </summary>
    public List<ImagePoint> ImagePoints { get; set; } = new List<ImagePoint>();

    #endregion

    #region Additional Attributes

    /// <summary>
    /// Part reference index number in the section
    /// </summary>
    public int? Reference { get; set; }

    /// <summary>
    /// Number of the page in the parent <see cref="Book"/> of the <see cref="Section"/>
    /// </summary>
    [Required]
    public int PageNumber { get; set; }

    /// <summary>
    /// Additional section information
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Occurrence of the item in the parent <see cref="Section"/>
    /// </summary>
    [Required]
    [DefaultValue(0)]
    public int Quantity { get; set; }

    /// <summary>
    /// Mapping remarks
    /// </summary>
    [MaxLength(128)]
    public string? Remarks { get; set; }

    #endregion
  }
}