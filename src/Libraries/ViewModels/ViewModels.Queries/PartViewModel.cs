using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying <see cref="IPart"/> information
  /// </summary>
  public class PartViewModel
    : BaseItemViewModel<IPart>, IPartViewModel
  {
    private readonly IAPIProvider m_provider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider instance</param>
    public PartViewModel(IAPIProvider provider)
    {
      m_provider = provider;
    }

    /// <inheritdoc />
    protected override async Task<IPart> GetItem(int id, CancellationToken cancellationToken = default)
      => await m_provider.GetPartAsync(id, cancellationToken).ConfigureAwait(false);
  }
}