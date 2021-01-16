using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all book sections
  /// </summary>
  public class BookSectionsViewModel
    : BasePagedViewModel<ISection>, IBookSectionsViewModel
  {
    /// <inheritdoc />
    public int BookId { get; set; }

    /// <inheritdoc />
    public BookSectionsViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override async Task<IPaging<ISection>> GetItems(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => await m_provider.GetSectionsFromBookAsync(BookId, pageSize, pageIndex, search, cancellationToken).ConfigureAwait(false);
  }
}