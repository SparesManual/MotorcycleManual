using System.Linq;
using Db.Core.Entities;
using Db.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <summary>
  /// Helper class for converting specifications into queries
  /// </summary>
  public static class SpecificationEvaluator
  {
    /// <summary>
    /// Applies given <paramref name="specification"/> to the <paramref name="inputQuery"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <param name="inputQuery">Query to which the <paramref name="specification"/> is to be applied</param>
    /// <param name="specification">Query specification</param>
    /// <returns>Modified query</returns>
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
      where TEntity : class, IEntity
    {
      var query = inputQuery;

      if (specification.Criteria != null)
        query = query.Where(specification.Criteria);

      if (specification.OrderBy != null)
        query = query.OrderBy(specification.OrderBy);

      if (specification.OrderByDescending != null)
        query = query.OrderByDescending(specification.OrderByDescending);

      if (specification.IsPagingEnabled)
        query = query
          .Skip(specification.Skip)
          .Take(specification.Take);

      query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

      return query;
    }
  }
}
