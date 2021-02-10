using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
    /// Paging items
    /// </summary>
    public IReadOnlyCollection<T> Items { get; set; }

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
  }
}