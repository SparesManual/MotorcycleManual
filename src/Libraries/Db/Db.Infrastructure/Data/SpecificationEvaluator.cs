using System;
using System.IO;
using System.Linq;
using Db.Interfaces;
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
    /// <param name="criteriaOnly">Skip applying modifications and only filter data using the defined <see cref="ISpecification{T}.Criteria"/></param>
    /// <returns>Modified query</returns>
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification, bool criteriaOnly = false)
      where TEntity : class, IEntity
    {
      var query = inputQuery;

      // If filtering criteria are defined..
      if (specification.Criteria != null)
        // apply filtering criteria
        query = query.Where(specification.Criteria);

      // If modifications are not allowed..
      if (criteriaOnly)
        // return the filtered query
        return query;

      // If ascending ordering is defined..
      if (specification.OrderBy != null)
        // order the query ascending using defined rule
        query = query.OrderBy(specification.OrderBy);

      // If descending ordering is defined..
      if (specification.OrderByDescending != null)
        // order the query descending using defined rule
        query = query.OrderByDescending(specification.OrderByDescending);

      // If paging is enabled..
      if (specification.IsPagingEnabled)
      {
        // select a batch from the query
        query = query
          // skip a set of results
          .Skip(specification.Skip)
          // take a set of results
          .Take(specification.Take);

        if (specification.OrderBy is null && specification.OrderByDescending is null)
          throw new NotSupportedException("Cannot enable pagination without ordering specified");
      }

      // Include referenced entities
      query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

      // Return the result
      return query;
    }

    /// <summary>
    /// Applies given <paramref name="specification"/> to the <paramref name="inputQuery"/>
    /// </summary>
    /// <typeparam name="TParent">Type of parent containing the <typeparamref name="TEntity"/> child type</typeparam>
    /// <typeparam name="TEntity">Type of extracted and further modified entity</typeparam>
    /// <param name="inputQuery">Query to which the <paramref name="specification"/> is to be applied</param>
    /// <param name="specification">Query specification</param>
    /// <param name="criteriaOnly">Skip applying modifications and only filter data using the defined <see cref="ISpecification{T}.Criteria"/></param>
    /// <exception cref="InvalidDataException">If the <paramref name="specification"/> required <see cref="ISpecificationEx{TInput,TOutput}.Extractor"/> property is null</exception>
    /// <returns>Modified query</returns>
    public static IQueryable<TEntity> GetQuery<TParent, TEntity>(this IQueryable<TParent> inputQuery, ISpecificationEx<TParent, TEntity> specification, bool criteriaOnly = false)
      where TParent : class, IEntity
      where TEntity : class, IEntity
    {
      if (specification.Extractor is null)
        throw new InvalidDataException(nameof(specification.Extractor));

      // Extract the required entities
      var query = specification.Extractor(inputQuery);

      // Apply the rest of the specification
      return GetQuery(query, specification, criteriaOnly);
    }
  }
}
