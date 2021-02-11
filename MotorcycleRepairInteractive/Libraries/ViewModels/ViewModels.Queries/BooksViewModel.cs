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
    public BooksViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override Task<IPaging<IBook>> GetItemsAsync(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => m_provider.GetBooksAsync(pageSize, pageIndex, search, cancellationToken);
  }
}
