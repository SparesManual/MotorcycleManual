using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using MRI.Db;
using MRI.MVVM.Helpers;

namespace ViewModels.Queries
{
  public class BooksViewModel
    : BasePagedViewModel<BookReply>
  {
    private readonly APIProvider m_provider;

    #region Properties

    public ObservableCollection<string> AutocompleteBooks { get; } = new ObservableCollection<string>();

    #endregion

    public BooksViewModel(APIProvider provider)
      => m_provider = provider;

    /// <inheritdoc />
    protected override async Task<IPaging<BookReply>> GetItems(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
      => await m_provider.GetBooksAsync(pageSize, pageIndex, cancellationToken: cancellationToken).ConfigureAwait(true);
  }
}
