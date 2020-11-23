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
    private readonly IAPIProvider m_provider;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    public BooksViewModel(IAPIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<IBook>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetBooksAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(true);
  }
}
