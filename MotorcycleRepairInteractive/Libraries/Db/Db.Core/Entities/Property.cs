using System.ComponentModel.DataAnnotations;
using Db.Interfaces;

namespace Db.Core.Entities
{
  /// <summary>
  /// Property assigned to parts
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Property
    : IEntity
  {
    /// <summary>
    /// Reference to the <see cref="Entities.Part"/> with the given property
    /// </summary>
    public Part Part { get; set; } = null!;

    /// <summary>
    /// Foreign key of the <see cref="Entities.Part"/>
    /// </summary>
    [Required]
    public int PartId { get; set; }

    /// <summary>
    /// Reference to the property type
    /// </summary>
    public PropertyType PropertyType { get; set; } = null!;

    /// <summary>
    /// Type of the given property
    /// </summary>
    [Required]
    public int PropertyTypeId { get; set; }

    /// <summary>
    /// Reference to the property value format type
    /// </summary>
    public FormatType FormatType { get; set; } = null!;

    /// <summary>
    /// Foreign key of the property value format type
    /// </summary>
    [Required]
    public int FormatTypeId { get; set; }

    /// <summary>
    /// Property name
    /// </summary>
    [Required]
    [MaxLength(80)]
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Property value
    /// </summary>
    [Required]
    [MaxLength(256)]
    public string PropertyValue { get; set; } = string.Empty;
  }
}