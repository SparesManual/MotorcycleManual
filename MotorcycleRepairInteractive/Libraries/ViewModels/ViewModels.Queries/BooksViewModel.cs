using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using MRI.Db;
using MRI.MVVM.Helpers;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all books
  /// </summary>
  public class BooksViewModel
    : BasePagedViewModel<BookReply>
  {
    private readonly APIProvider m_provider;

    #region Properties

    /// <summary>
    /// Search autocomplete collection
    /// </summary>
    public ObservableCollection<string> AutocompleteBooks { get; } = new ObservableCollection<string>();

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="provider">Injected API provider</param>
    public BooksViewModel(APIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<BookReply>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetBooksAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(true);
  }
}
