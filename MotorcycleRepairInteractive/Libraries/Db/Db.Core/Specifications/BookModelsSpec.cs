using System.Linq;
using Db.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Specifications for querying <see cref="Model"/> entities based on their parent <see cref="Book"/> entity
  /// </summary>
  public class BookModelsSpec
    : BaseSpecification<Book, Model>
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="id">Book id</param>
    public BookModelsSpec(int id)
    {
      SetExtractor(books => books
        .Where(book => book.Id.Equals(id))
        .Include(book => book.BookModels)
        .SelectMany(book => book.BookModels));

      SetOrderBy(model => model.Year);
    }
  }
}