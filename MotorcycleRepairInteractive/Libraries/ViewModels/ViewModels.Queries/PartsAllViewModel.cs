using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using MRI.Db;
using MRI.MVVM.Helpers;

namespace ViewModels.Queries
{
  public class PartsAllViewModel
    : BasePagedViewModel<PartReply>
  {
    private readonly APIProvider m_provider;

    public PartsAllViewModel(APIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<PartReply>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetPartsAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}
