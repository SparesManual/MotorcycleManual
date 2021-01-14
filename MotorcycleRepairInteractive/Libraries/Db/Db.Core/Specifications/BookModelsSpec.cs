using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  public class BookModelsSpec
    : BaseSpecification<Book, Model>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Book id</param>
    /// <param name="search">Full-text search query</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    public BookModelsSpec(int id, string? search, int size, int index)
      : base(model => string.IsNullOrEmpty(search)
                      || model.Name.Contains(search)
                      || search.Contains(model.Year.ToString()))
    {
      SetExtractor(books => books
        .Where(book => book.Id.Equals(id))
        .Include(book => book.BookModels)
        .SelectMany(book => book.BookModels));

      SetOrderBy(model => model.Year);
      ApplyPaging(size, index);
    }
  }
}