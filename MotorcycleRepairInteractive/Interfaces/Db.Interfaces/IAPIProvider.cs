using System;
using System.Threading;
using System.Threading.Tasks;
using Models.Interfaces.Entities;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface for API providers
  /// </summary>
  public interface IAPIProvider
    : IDisposable
  {
    /// <summary>
    /// Get a book based on the given <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the book</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Retrieved book</returns>
    Task<IBook> GetBookAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all books
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Book fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of books</returns>
    Task<IPaging<IBook>> GetBooksAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a part based on the given <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the part</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Retrieved part</returns>
    Task<IPart> GetPartAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all properties for a part with the given <paramref name="partId"/>
    /// </summary>
    /// <param name="partId">Id of the part</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Part fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of properties</returns>
    Task<IPaging<IProperty>> GetPartPropertiesAsync(int partId, int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a section based on the given <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the section</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Retrieved section</returns>
    Task<ISection> GetSectionAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all sections
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Part fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of sections</returns>
    Task<IPaging<ISection>> GetSectionsAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all parts
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Part fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    Task<IPaging<IPart>> GetPartsAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all parts belonging to a book with the given <paramref name="bookId"/>
    /// </summary>
    /// <param name="bookId">Id of the parent book</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Part fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    Task<IPaging<IPart>> GetPartsFromBookAsync(int bookId, int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all parts belonging to a section with the given <paramref name="sectionId"/>
    /// </summary>
    /// <param name="sectionId">Id of the parent section</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="search">Part fuzzy search</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    Task<IPaging<IPart>> GetPartsFromSectionAsync(int sectionId, int size, int index, string? search = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all child section parts based on the given <paramref name="parentId"/> of the section part
    /// </summary>
    /// <param name="parentId">Id of the parent section parts</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of section parts</returns>
    Task<IPaging<ISectionParts>> GetSectionPartChildren(int parentId, int size, int index, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all property types
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of property types</returns>
    Task<IPaging<IPropertyType>> GetPropertyTypesAsync(int size, int index, CancellationToken cancellationToken = default);
  }
}