using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specification for querying <see cref="Book"/> entities
  /// </summary>
  public class BooksSpec
    : BaseSpecification<Book>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="search">Fuzzy book search</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public BooksSpec(string? search, int size, int index)
      : base(book => Book.StringSearch(search, book))
    {
      ApplyPaging(size, index);
      SetOrderBy(book => book.Title);
    }
  }
}