using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    : BasePropertyChanged, IViewModel
  {
    /// <summary>
    /// API provider instance
    /// </summary>
    protected readonly IAPIProvider m_provider;

    #region Properties

    /// <summary>
    /// Are items loaded
    /// </summary>
    public bool Loading { get; set; }

    /// <summary>
    /// Paged items to display
    /// </summary>
    public ObservableCollection<T> Items { get; } = new();

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

    /// <summary>
    /// Loads items
    /// </summary>
    public async Task LoadItems()
    {
      Items.Clear();

      Loading = true;

      await foreach (var item in GetItems().ConfigureAwait(true))
        Items.Add(item);

      Loading = false;
    }
  }
}