using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using MRI.Db;
using MRI.MVVM.Helpers;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all parts
  /// </summary>
  public class PartsAllViewModel
    : BasePagedViewModel<PartReply>
  {
    private readonly APIProvider m_provider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    public PartsAllViewModel(APIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<PartReply>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetPartsAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}
