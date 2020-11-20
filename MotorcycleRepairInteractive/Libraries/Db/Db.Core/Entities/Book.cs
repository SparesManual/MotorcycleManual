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
    /// Validates whether the <paramref name="book"/> matches the <paramref name="search"/> specification
    /// </summary>
    /// <param name="search">Fuzzy search string</param>
    /// <param name="book">Entity to validate</param>
    /// <returns>True if this <paramref name="book"/> is a match</returns>
    public static bool StringSearch(string? search, Book book)
      => string.IsNullOrEmpty(search?.Trim()) || book.Title.Contains(search);
  }
}