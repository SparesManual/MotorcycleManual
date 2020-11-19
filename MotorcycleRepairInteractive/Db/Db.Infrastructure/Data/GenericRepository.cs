using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <summary>
  /// Generic repository for accessing the <see cref="ManualContext"/> entity sets
  /// </summary>
  /// <typeparam name="T">Type of entity</typeparam>
  public class GenericRepository<T>
    : IGenericRepository<T>
    where T : class, IEntity
  {
    #region Fields

    private readonly ManualContext m_context;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="context">Injected context</param>
    public GenericRepository(ManualContext context)
      => m_context = context;

    #region Methods

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(int id)
      => await m_context.Set<T?>().FindAsync(id).ConfigureAwait(false);

    /// <inheritdoc />
    public IQueryable<T> GetAllAsync()
      => m_context.Set<T>();

    /// <inheritdoc />
    public async Task<T?> GetEntityWithSpecificationAsync(ISpecification<T> specification)
      => await ApplySpecification(specification).FirstOrDefaultAsync().ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<TChild?> GetEntityWithSpecificationAsync<TChild>(ISpecificationEx<T, TChild> specification)
      where TChild : class, IEntity
      => await ApplySpecification(specification).FirstOrDefaultAsync().ConfigureAwait(false);

    /// <inheritdoc />
    public IQueryable<T> GetAllAsync(ISpecification<T> specification)
      => ApplySpecification(specification);

    /// <inheritdoc />
    public IAsyncEnumerable<TChild> GetAllAsync<TChild>(ISpecificationEx<T, TChild> specification)
      where TChild : class, IEntity
      => ApplySpecification(specification).AsAsyncEnumerable();

    /// <inheritdoc />
    public async ValueTask<int> CountAsync(ISpecification<T> specification)
      => await ApplySpecification(specification, true).CountAsync().ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<int> CountAsync<TChild>(ISpecificationEx<T, TChild> specification)
      where TChild : class, IEntity
      => await ApplySpecification(specification, true).CountAsync().ConfigureAwait(false);

    private IQueryable<T> ApplySpecification(ISpecification<T> specification, bool countOnly = false)
      => m_context
        .Set<T>()
        .AsQueryable()
        .GetQuery(specification, countOnly);

    private IQueryable<TChild> ApplySpecification<TChild>(ISpecificationEx<T, TChild> specification, bool countOnly = false)
      where TChild : class, IEntity
      => m_context
        .Set<T>()
        .AsQueryable()
        .GetQuery(specification, countOnly);

    #endregion
  }
}
