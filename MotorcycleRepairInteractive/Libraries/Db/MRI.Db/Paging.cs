using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using MRI.Helpers;

namespace MRI.Db
{
  internal class Paging<T>
    : IPaging<T>
  {
    private readonly Func<CancellationToken, IAsyncEnumerable<T>> m_stream;

    #region Properties

    /// <inheritdoc />
    public int TotalItems { get; }

    /// <inheritdoc />
    public int PageItems { get; }

    /// <inheritdoc />
    public int PageIndex { get; }

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="stream">Batch stream of items</param>
    /// <param name="totalItems">Total number of times - sum of items in all batches</param>
    /// <param name="pageItems">Items in given batch</param>
    /// <param name="pageIndex">Index of given batch</param>
    public Paging(Func<CancellationToken, IAsyncEnumerable<T>> stream, int totalItems, int pageItems, int pageIndex)
    {
      m_stream = stream;
      TotalItems = totalItems;
      PageItems = pageItems;
      PageIndex = pageIndex;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<T> ReadAll(CancellationToken cancellationToken = default)
      => m_stream(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<T>> GetItems(CancellationToken cancellationToken = default)
    {
      var result = new ConcurrentBag<T>();
      await foreach(var item in ReadAll(cancellationToken).WithCancellation(cancellationToken).ConfigureAwait(false))
        result.Add(item);

      return result
        .Reverse()
        .ToReadOnlyCollection();
    }
  }
}
