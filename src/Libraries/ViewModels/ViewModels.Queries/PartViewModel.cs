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
    /// <inheritdoc />
    public PartViewModel(IAPIProvider provider)
      : base(provider) { }

    /// <inheritdoc />
    protected override async Task<IPart> GetItem(int id, CancellationToken cancellationToken = default)
      => await m_provider.GetPartAsync(id, cancellationToken).ConfigureAwait(false);
  }
}