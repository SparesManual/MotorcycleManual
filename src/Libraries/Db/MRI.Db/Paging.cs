using System.Collections.Generic;
using Db.Interfaces;

namespace MRI.Db
{
  internal class Paging<T>
    : IPaging<T>
  {
    #region Properties

    public IReadOnlyCollection<T> Items { get; set; }

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
    /// <param name="items"></param>
    /// <param name="totalItems">Total number of times - sum of items in all batches</param>
    /// <param name="pageItems">Items in given batch</param>
    /// <param name="pageIndex">Index of given batch</param>
    public Paging(IReadOnlyCollection<T> items, int totalItems, int pageItems, int pageIndex)
    {
      Items = items;
      TotalItems = totalItems;
      PageItems = pageItems;
      PageIndex = pageIndex;
    }
  }
}
