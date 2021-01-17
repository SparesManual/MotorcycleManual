using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Helpers
{
  /// <summary>
  /// Base view model for displaying an item
  /// </summary>
  /// <typeparam name="T">Item type</typeparam>
  public abstract class BaseItemViewModel<T>
    : BasePropertyChanged, IItemViewModel<T>
  {
    #region Fields

    /// <summary>
    /// API provider instance
    /// </summary>
    protected readonly IAPIProvider m_provider;
    private bool m_loading;

    #endregion

    #region Properties

    /// <inheritdoc />
    public bool Loading
    {
      get => m_loading;
      private set
      {
        m_loading = value;
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public int Id { get; set; }

    /// <inheritdoc />
    public T? Item { get; protected set; }

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    protected BaseItemViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <summary>
    /// Queries the item
    /// </summary>
    /// <param name="id">Item id</param>
    /// <param name="cancellationToken">Process cancellation</param>
    /// <returns>Queried item</returns>
    protected abstract Task<T> GetItem(int id, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public async Task LoadItem()
    {
      Loading = true;

      Item = await GetItem(Id).ConfigureAwait(true);

      Loading = false;

      OnPropertyChanged(nameof(Item));
    }
  }
}