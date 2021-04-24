using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.Extensions.Logging;
using MRI.MVVM.Interfaces.ViewModels;
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace MRI.MVVM.Helpers
{
  /// <summary>
  /// Base View model with paging support
  /// </summary>
  /// <typeparam name="T">Paged item type</typeparam>
  public abstract class BasePagedViewModel<T>
    : BasePropertyChanged, IPagedViewModel<T>
  {
    #region Fields

    /// <summary>
    /// API provider instance
    /// </summary>
    protected readonly IAPIProvider m_provider;

    private readonly ILogger<BasePagedViewModel<T>> m_logger;
    private int m_pageIndex = 1;
    private int m_pageSize = 10;
    private int m_pageItems;
    private int m_totalItems;
    private bool m_loading;
    private string m_search = string.Empty;
    private static readonly List<T> EMPTY = new();

    #endregion

    #region Properties

    /// <summary>
    /// Index of the current page
    /// </summary>
    public int PageIndex
    {
      get => m_pageIndex;
      set
      {
        m_pageIndex = value;
        m_logger.LogDebug($"Page Index changed to {value}");
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Number of maximum items to query
    /// </summary>
    public int PageSize
    {
      get => m_pageSize;
      set
      {
        m_pageSize = value;
        m_logger.LogDebug($"Page size changed to {value}");
        LoadItemsAsync().RunSynchronously();
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Number of currently displayed items
    /// </summary>
    public int PageItems
    {
      get => m_pageItems;
      set
      {
        m_pageItems = value;
        m_logger.LogDebug($"Page items changed to {value}");
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Total items available for paging
    /// </summary>
    public int TotalItems
    {
      get => m_totalItems;
      set
      {
        m_totalItems = value;
        m_logger.LogDebug($"Page total items changed to {value}");
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Is paging loading
    /// </summary>
    public bool Loading
    {
      get => m_loading;
      set
      {
        m_loading = value;
        m_logger.LogDebug($"Loading changed to {value}");
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Filter search expression
    /// </summary>
    public string Search
    {
      get => m_search;
      set
      {
        m_search = value;
        m_logger.LogDebug($"Search changed to {value}");
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Paged items to display
    /// </summary>
    public IReadOnlyCollection<T> Items { get; private set; } = EMPTY;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    /// <param name="logger">Logger instance</param>
    protected BasePagedViewModel(IAPIProvider provider, ILogger<BasePagedViewModel<T>> logger)
    {
      m_provider = provider;
      m_logger = logger;
    }

    /// <summary>
    /// Queries the items
    /// </summary>
    /// <param name="pageSize">Maximum number of items to return</param>
    /// <param name="pageIndex">Index of batch</param>
    /// <param name="search">Optional search filter</param>
    /// <param name="cancellationToken">Process cancellation</param>
    /// <returns>Queried items</returns>
    protected abstract ValueTask<IPaging<T>> GetItemsAsync(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default);

    /// <summary>
    /// Loads items
    /// </summary>
    public async Task LoadItemsAsync()
    {
      m_logger.LogDebug("Loading items started...");
      // Remove old items
      //ClearItems();

      // Enter loading state
      Loading = true;

      // Get the items
      var items = await GetItemsAsync(PageSize, PageIndex, Search).ConfigureAwait(true);

      // TODO: This is page size not items count
      PageItems = items.PageItems;
      // TODO: Is this required?
      // Set the current page index
      PageIndex = items.PageIndex;
      // Set the total available items
      TotalItems = items.TotalItems;
      // Add it to the list
      Items = items.Items;

      // Exit loading state
      Loading = false;

      // Notify the view of the data update
      OnPropertyChanged(nameof(Items));

      m_logger.LogDebug("Loading items ended...");
    }

    /// <inheritdoc />
    public void ClearItems()
    {
      // Clear the current items
      Items = EMPTY;
      // Notify the view of the data update
      OnPropertyChanged(nameof(Items));
    }

    /// <summary>
    /// Parses the Id to an integer
    /// </summary>
    /// <param name="id">Value to parse</param>
    /// <returns>Parsed value</returns>
    protected static int IdToInt(string? id)
      => id is null || !int.TryParse(id, out var intId)
        ? -1
        : intId;
  }
}