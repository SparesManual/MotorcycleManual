using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface for search query specifications
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public interface ISpecification<T>
    where T : IEntity
  {
    /// <summary>
    /// Entity search criteria expression
    /// </summary>
    public Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    /// Entity property selectors
    /// </summary>
    /// <remarks>
    /// The selected properties (columns) will be included in the resulting query
    /// </remarks>
    IReadOnlyCollection<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Query result sorting expression by ascending
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Query result sorting expression by descending
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Number of items to take from the query
    /// </summary>
    /// <remarks>
    /// Used for paging
    /// </remarks>
    int Take { get; }

    /// <summary>
    /// Number of items to skip from the query
    /// </summary>
    /// <remarks>
    /// Used for paging
    /// </remarks>
    int Skip { get; }

    /// <summary>
    /// Determines whether paging is enabled
    /// </summary>
    bool IsPagingEnabled { get; }
  }
}