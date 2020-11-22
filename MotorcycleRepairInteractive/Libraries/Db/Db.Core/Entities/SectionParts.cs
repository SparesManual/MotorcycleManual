using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
    public Section? Section { get; set; }

    /// <summary>
    /// Foreign key reference to the child <see cref="Entities.Part"/> entity
    /// </summary>
    public int PartId { get; set; }

    /// <summary>
    /// Reference to the child <see cref="Entities.Part"/> entity
    /// </summary>
    public Part? Part { get; set; }

    #endregion

    #region Self Reference

    /// <summary>
    /// Foreign key reference to the parent <see cref="SectionParts"/> entity
    /// </summary>
    [AllowNull]
    public int? ParentSectionPartsId { get; set; } = null;

    /// <summary>
    /// Reference to the parent <see cref="SectionParts"/> entity
    /// </summary>
    public SectionParts? ParentSectionParts { get; set; }

    /// <summary>
    /// Reference to parenting <see cref="SectionParts"/> entities
    /// </summary>
    /// <remarks>
    /// Required for EF Core self reference
    /// </remarks>
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<SectionParts?> ParentSectionPartsList { get; set; } = new List<SectionParts?>();

    #endregion

    #region Additional Attributes

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