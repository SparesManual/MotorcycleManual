using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Maps a parenting <see cref="SectionParts"/> entity to the child <see cref="SectionParts"/>
  /// </summary>
  public class SectionPartParents
    : IEntity
  {
    /// <summary>
    /// Foreign key reference to the parent <see cref="SectionParts"/> entity
    /// </summary>
    [Required]
    public int ParentId { get; set; }

    /// <summary>
    /// Reference to the parent <see cref="SectionParts"/> entity
    /// </summary>
    public SectionParts? Parent { get; set; }

    /// <summary>
    /// Foreign key reference to the child <see cref="SectionParts"/> entity
    /// </summary>
    [Required]
    public int ChildId { get; set; }

    /// <summary>
    /// Reference to the child <see cref="SectionParts"/> entity
    /// </summary>
    public SectionParts? Child { get; set; }
  }
}