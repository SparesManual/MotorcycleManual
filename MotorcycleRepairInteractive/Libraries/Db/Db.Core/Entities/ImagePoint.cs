using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a point on the image of a <see cref="Section"/>
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class ImagePoint
    : BaseEntity
  {
    /// <summary>
    /// Foreign key reference of the <see cref="Entities.SectionParts"/> entity
    /// </summary>
    [Required]
    public int SectionPartsId { get; set; }

    /// <summary>
    /// Reference of the <see cref="Entities.SectionParts"/> entity
    /// </summary>
    public SectionParts? SectionParts { get; set; }

    /// <summary>
    /// Horizontal position of a <see cref="Part"/> in the <see cref="Section"/> mapped by the <see cref="SectionParts"/>
    /// </summary>
    [Required]
    public double PositionX { get; set; }

    /// <summary>
    /// Vertical position of a <see cref="Part"/> in the <see cref="Section"/> mapped by the <see cref="SectionParts"/>
    /// </summary>
    [Required]
    public double PositionY { get; set; }
  }
}