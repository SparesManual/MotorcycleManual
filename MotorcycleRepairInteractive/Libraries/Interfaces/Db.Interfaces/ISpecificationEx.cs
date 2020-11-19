using System;
using System.Linq;
using System.Linq.Expressions;

namespace Db.Interfaces
{
  /// <summary>
  /// Extended interface for search query specifications
  /// </summary>
  /// <typeparam name="TInput">Input type</typeparam>
  /// <typeparam name="TOutput">Extracted type from the <typeparamref name="TInput"/> type</typeparam>
  public interface ISpecificationEx<TInput, TOutput>
    : ISpecification<TOutput>
    where TInput : IEntity
    where TOutput : IEntity
  {
    /// <summary>
    /// Expression for extracting the <typeparamref name="TOutput"/> from the <typeparamref name="TInput"/>
    /// </summary>
    Func<IQueryable<TInput>, IQueryable<TOutput>> Extractor { get; }
  }
}