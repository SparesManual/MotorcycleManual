using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Db.Interfaces;
using System.Linq;

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
    protected BaseSpecification() { }

    /// <summary>
    /// Constructor with <paramref name="criteria"/>
    /// </summary>
    /// <param name="criteria">Filtering criteria</param>
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
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
    /// <param name="pageSize">Maximum number of items per page</param>
    /// <param name="pageIndex">Index of query result batch to take</param>
    protected void ApplyPaging(int pageSize, int pageIndex)
    {
      Skip = pageSize * (pageIndex - 1);
      Take = pageSize > 50
        ? 50
        : pageSize;
      IsPagingEnabled = true;
    }

    #endregion
  }

  /// <summary>
  /// Base class for extended entity query search specifications
  /// </summary>
  /// <typeparam name="TParent">Parent entity</typeparam>
  /// <typeparam name="TChild">Extracted child entity from the parent entity</typeparam>
  public class BaseSpecification<TParent, TChild>
    : BaseSpecification<TChild>,
      ISpecificationEx<TParent, TChild>
    where TParent : IEntity
    where TChild : class, IEntity
  {
    /// <inheritdoc />
    public Func<IQueryable<TParent>, IQueryable<TChild>>? Extractor { get; private set;  }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="criteria">Filtering criteria</param>
    protected BaseSpecification(Expression<Func<TChild, bool>> criteria)
      : base(criteria) { }

    /// <summary>
    /// Default constructor
    /// </summary>
    protected BaseSpecification()
    {
    }

    /// <summary>
    /// Set the <typeparamref name="TChild"/> extractor from the <typeparamref name="TParent"/>
    /// </summary>
    /// <param name="extractor">Extractor to set</param>
    protected void SetExtractor(Func<IQueryable<TParent>, IQueryable<TChild>> extractor)
      => Extractor = extractor;
  }
}
