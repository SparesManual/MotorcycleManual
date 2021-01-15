using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  public class PartViewModel
    : BaseItemViewModel<IPart>, IPartViewModel
  {
    private readonly IAPIProvider m_provider;

    public PartViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task GetItem(int id, CancellationToken cancellationToken = default)
    {
      Item = await m_provider.GetPartAsync(id, cancellationToken).ConfigureAwait(false);
    }
  }
}