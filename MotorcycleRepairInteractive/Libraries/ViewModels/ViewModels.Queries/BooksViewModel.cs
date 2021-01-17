using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all books
  /// </summary>
  public class BooksViewModel
    : BasePagedViewModel<IBook>, IBooksViewModel
  {
    /// <inheritdoc />
    protected override async Task<IPaging<IBook>> GetItems(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => await m_provider.GetBooksAsync(pageSize, pageIndex, search, cancellationToken).ConfigureAwait(true);

    /// <inheritdoc />
    public BooksViewModel(IAPIProvider provider)
      : base(provider)
    {
    }
  }
}
