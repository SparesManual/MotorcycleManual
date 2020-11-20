using System.Collections.Generic;
using System.Threading;
using Db.Interfaces;
using Grpc.Core;

namespace MRI.Db
{
  internal class Paging<T>
    : IPaging<T>
  {
    private readonly IAsyncStreamReader<T> m_stream;

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
    public Paging(IAsyncStreamReader<T> stream, int totalItems, int pageItems, int pageIndex)
    {
      m_stream = stream;
      TotalItems = totalItems;
      PageItems = pageItems;
      PageIndex = pageIndex;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<T> ReadAll(CancellationToken cancellationToken = default)
      => m_stream.ReadAllAsync(cancellationToken);
  }
}
