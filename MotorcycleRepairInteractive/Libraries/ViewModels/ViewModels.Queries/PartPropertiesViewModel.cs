using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  public class PartPropertiesViewModel
    : BasePagedViewModel<IProperty>, IPartPropertiesViewModel
  {
    private readonly IAPIProvider m_provider;

    /// <inheritdoc />
    public int PartId { get; set; }

    public PartPropertiesViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<IProperty>> GetItems(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => await m_provider.GetPartPropertiesAsync(PartId, pageSize, pageIndex, search, cancellationToken).ConfigureAwait(false);
  }
}