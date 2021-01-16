using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all part properties
  /// </summary>
  public class PartPropertiesViewModel
    : BasePagedViewModel<IProperty>, IPartPropertiesViewModel
  {
    /// <inheritdoc />
    public int PartId { get; set; }

    /// <inheritdoc />
    protected override async Task<IPaging<IProperty>> GetItems(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => await m_provider.GetPartPropertiesAsync(PartId, pageSize, pageIndex, search, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public PartPropertiesViewModel(IAPIProvider provider)
      : base(provider)
    {
    }
  }
}