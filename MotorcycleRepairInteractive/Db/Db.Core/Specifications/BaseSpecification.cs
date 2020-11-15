using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Db.Core.Entities;

namespace Db.Core.Specifications
{
  /// <summary>
  /// Base class for entity query search specifications
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public class BaseSpecification<T>
    : ISpecification<T> where T : IEntity
  {
    #region Fields

    private readonly LinkedList<Expression<Func<T, object>>> m_includes = new LinkedList<Expression<Func<T, object>>>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public Expression<Func<T, bool>>? Criteria { get; }

    /// <inheritdoc />
    public IReadOnlyCollection<Expression<Func<T, object>>> Includes
      => m_includes;

    /// <inheritdoc />
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    /// <inheritdoc />
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    /// <inheritdoc />
    public int Take { get; private set; }

    /// <inheritdoc />
    public int Skip { get; private set; }

    /// <inheritdoc />
    public bool IsPagingEnabled { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public BaseSpecification() { }

    /// <summary>
    /// Constructor with <paramref name="criteria"/>
    /// </summary>
    /// <param name="criteria"></param>
    public BaseSpecification(Expression<Func<T, bool>> criteria)
      => Criteria = criteria;

    #endregion

    #region Methods

    /// <summary>
    /// Adds columns that are to be included in a query
    /// </summary>
    /// <param name="expression">Entity property selector</param>
    protected void AddInclude(Expression<Func<T, object>> expression)
      => m_includes.AddLast(expression ?? throw new ArgumentNullException(nameof(expression)));

    /// <summary>
    /// Adds an order by function
    /// </summary>
    /// <param name="expression">Order by rule</param>
    protected void SetOrderBy(Expression<Func<T, object>> expression)
      => OrderBy = expression;

    /// <summary>
    /// Adds an order by descending function
    /// </summary>
    /// <param name="expression">Order by descending rule</param>
    protected void SetOrderByDescending(Expression<Func<T, object>> expression)
      => OrderByDescending = expression;

    /// <summary>
    /// Applies paging settings
    /// </summary>
    /// <param name="skip">Items to skip</param>
    /// <param name="take">Items to take</param>
    protected void ApplyPaging(int skip, int take)
    {
      Skip = skip;
      Take = take;
      IsPagingEnabled = true;
    }

    #endregion
  }
}
