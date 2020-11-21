using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Db.API;
using MRI.Db;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;

namespace ViewModels.Queries
{
  public class BooksViewModel
    : BasePropertyChanged, IViewModel
  {
    private readonly APIProvider m_provider;

    #region Properties

    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int PageItems { get; set; }
    public int TotalItems { get; set; }
    public bool Loading { get; set; }
    public ObservableCollection<BookReply> Books { get; } = new ObservableCollection<BookReply>();
    public ObservableCollection<string> AutocompleteBooks { get; } = new ObservableCollection<string>();

    #endregion

    public BooksViewModel(APIProvider provider)
      => m_provider = provider;

    public async Task ReloadAutocomplete(string search)
    {
      AutocompleteBooks.Clear();

      if (string.IsNullOrEmpty(search))
        return;

      var result = await m_provider.GetBooksAsync(5, 1, search).ConfigureAwait(true);

      await foreach (var book in result.ReadAll().ConfigureAwait(false))
        AutocompleteBooks.Add(book.Title);
    }

    public async Task LoadBooks()
    {
      Books.Clear();

      Loading = true;

      var result = await m_provider.GetBooksAsync(PageSize, PageIndex).ConfigureAwait(true);

      PageItems = result.PageItems;
      PageIndex = result.PageIndex;
      TotalItems = result.TotalItems;

      await foreach (var book in result.ReadAll().ConfigureAwait(false))
        Books.Add(book);

      Loading = false;
    }
  }
}
