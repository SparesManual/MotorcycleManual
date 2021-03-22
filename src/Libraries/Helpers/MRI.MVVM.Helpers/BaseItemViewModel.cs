using System.Threading;
using System.Threading.Tasks;
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
    public string Id { get; set; } = string.Empty;

    /// <inheritdoc />
    public T? Item { get; protected set; }

    #endregion

    /// <summary>
    /// Queries the item
    /// </summary>
    /// <param name="id">Item id</param>
    /// <param name="cancellationToken">Process cancellation</param>
    /// <returns>Queried item</returns>
    protected abstract Task<T> GetItem(string id, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public async Task LoadItem()
    {
      // Enter loading state
      Loading = true;

      // Retrieve the item by its id
      Item = await GetItem(Id).ConfigureAwait(true);

      // Exit loading state
      Loading = false;

      // Notify the view of data update
      OnPropertyChanged(nameof(Item));
    }

    /// <summary>
    /// Parses the Id to an integer
    /// </summary>
    /// <param name="id">Value to parse</param>
    /// <returns>Parsed value</returns>
    protected static int IdToInt(string? id)
      => id is null || int.TryParse(id, out var intId)
        ? -1
        : intId;
  }
}