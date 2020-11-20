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
    /// Description of the part by the maker
    /// </summary>
    [MaxLength(128)]
    public string? MakersDescription { get; set; }

    /// <summary>
    /// <see cref="Section"/> entities have this given part
    /// </summary>
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<SectionParts> PartSections { get; set; } = new List<SectionParts>();

    /// <summary>
    /// Validates whether the <paramref name="part"/> matches the <paramref name="search"/> specification
    /// </summary>
    /// <param name="search">Fuzzy search string</param>
    /// <param name="part">Entity to validate</param>
    /// <returns>True if this <paramref name="part"/> is a match</returns>
    public static bool StringSearch(Part part, string? search)
    {
      if (string.IsNullOrEmpty(search?.Trim()))
        return true;

      return part.Description.Contains(search)
             || (part.PartNumber.Contains(search))
             || (part.MakersDescription?.Contains(search) ?? false)
             || (part.MakersPartNumber?.Contains(search) ?? false);
    }
  }
}