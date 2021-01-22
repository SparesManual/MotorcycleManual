using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Helpers
{
  /// <summary>
  /// Base view model for displaying items
  /// </summary>
  /// <typeparam name="T">Item type</typeparam>
  public abstract class BaseItemsViewModel<T>
    : BasePropertyChanged, IItemsViewModel<T>
  {
    /// <summary>
    /// API provider instance
    /// </summary>
    protected readonly IAPIProvider m_provider;

    #region Properties

    /// <inheritdoc />
    public bool Loading { get; set; }

    /// <inheritdoc />
    public ConcurrentBag<T> Items { get; } = new();

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    protected BaseItemsViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <summary>
    /// Queries the items
    /// </summary>
    /// <param name="cancellationToken">Process cancellation</param>
    /// <returns>Queried items</returns>
    protected abstract IAsyncEnumerable<T> GetItems(CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public async Task LoadItems()
    {
      // Remove old items
      ClearItems();

      // Enter loading state
      Loading = true;

      // For every queried item..
      await foreach (var item in GetItems().Reverse().ConfigureAwait(true))
        // Add it to the view
        Items.Add(item);

      // Exit loading state
      Loading = false;

      // Notify the view of the data update
      OnPropertyChanged(nameof(Items));
    }

    /// <inheritdoc />
    public void ClearItems()
    {
      // Clear the current items
      Items.Clear();
      // Notify the view of the data update
      OnPropertyChanged(nameof(Items));
    }

  }
}