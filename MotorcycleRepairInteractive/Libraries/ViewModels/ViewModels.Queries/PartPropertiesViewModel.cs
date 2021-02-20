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
    public PartPropertiesViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override ValueTask<IPaging<IProperty>> GetItemsAsync(int pageSize, int pageIndex, string? search,
      CancellationToken cancellationToken = default)
      => m_provider.GetPartPropertiesAsync(PartId, pageSize, pageIndex, search, cancellationToken);
  }
}