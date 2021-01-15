using System.Threading;
using System.Threading.Tasks;
using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Helpers
{
  public abstract class BaseItemViewModel<T>
    : BasePropertyChanged, IItemViewModel<T>
    where T : IReply
  {
    private bool m_loading;

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

    protected abstract Task GetItem(int id, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public async Task LoadItem()
    {
      Loading = true;

      await GetItem(Id).ConfigureAwait(true);

      Loading = false;
    }
  }
}