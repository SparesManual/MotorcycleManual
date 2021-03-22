using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying a models engine
  /// </summary>
  public class EngineViewModel
    : BaseItemViewModel<IEngine>, IEngineViewModel
  {
    private readonly IAPIProvider m_provider;

    /// <inheritdoc />
    public EngineViewModel(IAPIProvider provider)
    {
      m_provider = provider;
    }

    /// <inheritdoc />
    protected override async Task<IEngine> GetItem(int id, CancellationToken cancellationToken = default)
      => await m_provider.GetModelEngineAsync(id, cancellationToken).ConfigureAwait(false);
  }
}