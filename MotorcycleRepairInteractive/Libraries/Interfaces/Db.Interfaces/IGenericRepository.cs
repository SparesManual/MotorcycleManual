using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface for generic repositories
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public interface IGenericRepository<T>
    where T : class, IEntity
  {
    /// <summary>
    /// Gets an <see cref="IEntity"/> by its <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the entity to find</param>
    /// <returns>The retrieved <see cref="IEntity"/> by its <paramref name="id"/> or null of not found</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Gets all known <see cref="IEntity"/> entries
    /// </summary>
    /// <returns>Collection of <see cref="IEntity"/></returns>
    IQueryable<T> GetAllAsync();

    /// <summary>
    /// Gets an <see cref="IEntity"/> using a query <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>The retrieved <see cref="IEntity"/> using a query <paramref name="specification"/> or null of not found</returns>
    Task<T?> GetEntityWithSpecificationAsync(ISpecification<T> specification);

    /// <summary>
    /// Gets an <see cref="IEntity"/> using a query <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>The retrieved <see cref="IEntity"/> using a query <paramref name="specification"/> or null of not found</returns>
    Task<TChild?> GetEntityWithSpecificationAsync<TChild>(ISpecificationEx<T, TChild> specification) where TChild : class, IEntity;

    /// <summary>
    /// Gets all <see cref="IEntity"/> entries that match the supplied query <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>Query of <see cref="IEntity"/></returns>
    IQueryable<T> GetAllAsync(ISpecification<T> specification);

    /// <summary>
    /// Gets all <see cref="IEntity"/> entries that match the supplied query <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>Query of <see cref="IEntity"/></returns>
    IAsyncEnumerable<TChild> GetAllAsync<TChild>(ISpecificationEx<T, TChild> specification) where TChild : class, IEntity;

    /// <summary>
    /// Counts the number of results found with the given <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>Number of results</returns>
    ValueTask<int> CountAsync(ISpecification<T> specification);

    /// <summary>
    /// Counts the number of results found with the given <paramref name="specification"/>
    /// </summary>
    /// <param name="specification">Query specification</param>
    /// <returns>Number of results</returns>
    ValueTask<int> CountAsync<TChild>(ISpecificationEx<T, TChild> specification) where TChild : class, IEntity;
  }
}