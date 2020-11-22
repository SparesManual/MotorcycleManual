using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using MRI.MVVM.Interfaces;

namespace MRI.MVVM.Helpers
{
  public abstract class BasePagedViewModel<T>
    : BasePropertyChanged, IViewModel
  {
    private int m_pageIndex = 1;
    private int m_pageSize = 10;
    private int m_pageItems;
    private int m_totalItems;
    private bool m_loading;

    #region Properties

    public int PageIndex
    {
      get => m_pageIndex;
      set
      {
        m_pageIndex = value;
        OnPropertyChanged();
      }
    }

    public int PageSize
    {
      get => m_pageSize;
      set
      {
        m_pageSize = value;
        OnPropertyChanged();
      }
    }

    public int PageItems
    {
      get => m_pageItems;
      set
      {
        m_pageItems = value;
        OnPropertyChanged();
      }
    }

    public int TotalItems
    {
      get => m_totalItems;
      set
      {
        m_totalItems = value;
        OnPropertyChanged();
      }
    }

    public bool Loading
    {
      get => m_loading;
      set
      {
        m_loading = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

    #endregion

    protected abstract Task<IPaging<T>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default);

    public async Task LoadItems()
    {
      Items.Clear();

      Loading = true;

      var result = await GetItems(PageSize, PageIndex).ConfigureAwait(true);

      PageItems = result.PageItems;
      PageIndex = result.PageIndex;
      TotalItems = result.TotalItems;

      await foreach (var item in result.ReadAll().ConfigureAwait(true))
        Items.Add(item);

      Loading = false;
    }
  }
}