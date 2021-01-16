using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Helpers
{
  public abstract class BaseItemViewModel<T>
    : BasePropertyChanged, IItemViewModel<T>
  {
    #region Fields

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

    protected BaseItemViewModel(IAPIProvider provider)
      => m_provider = provider;

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