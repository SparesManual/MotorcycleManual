using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all parts
  /// </summary>
  public class PartsAllViewModel
    : BasePagedViewModel<IPart>, IAllPartsViewModel
  {
    private readonly IAPIProvider m_provider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    public PartsAllViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<IPart>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetPartsAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}
