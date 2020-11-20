using System.Collections.Generic;
using System.Threading;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface for paging batches
  /// </summary>
  /// <typeparam name="T">Type of batched item</typeparam>
  public interface IPaging<T>
  {
    #region Properties

    /// <summary>
    /// Total items count
    /// </summary>
    int TotalItems { get; }

    /// <summary>
    /// Items in given page batch
    /// </summary>
    int PageItems { get; }

    /// <summary>
    /// Index of page batch
    /// </summary>
    int PageIndex { get; }

    #endregion

    /// <summary>
    /// Get the async stream
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Async item stream</returns>
    IAsyncEnumerable<T> ReadAll(CancellationToken cancellationToken = default);
  }
}