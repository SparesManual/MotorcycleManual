using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views.Managers;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Base component for displaying paged content
  /// </summary>
  /// <typeparam name="TViewModel">View model for the view</typeparam>
  /// <typeparam name="TItem">Paged items type</typeparam>
  public abstract class BasePagedComponent<TViewModel, TItem>
    : BaseComponent<TViewModel>
    where TViewModel : class, IPagedViewModel<TItem>
  {
    [Inject]
    private IPagingManager PagingManager { get; set; } = null!;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
      // If the view model is not null..
      if (ViewModel is not null)
        // Load the view items
        await ViewModel.LoadItemsAsync().ConfigureAwait(true);
    }

    /// <summary>
    /// Load paged data
    /// </summary>
    /// <param name="filter">Full-text filter string</param>
    /// <param name="paging">Paging data</param>
    /// <param name="sizeChanged">Determines whether the page size had changed</param>
    protected async Task LoadData(string filter, object paging, bool sizeChanged = false)
    {
      // If the view model is null..
      if (ViewModel is null)
        // Exit
        return;

      // Get the page index
      var newPage = PagingManager.GetPageIndex(ViewModel, paging);
      // If the filter and page index are unchanged..
      if (ViewModel.Search.Equals(filter)
          && ViewModel.PageIndex == newPage
          && !sizeChanged)
        // Exit
        return;

      // Update the filter
      ViewModel.Search = filter;
      // Update the page index
      ViewModel.PageIndex = newPage;

      // Load the view items
      await ViewModel.LoadItemsAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Clears the currently displayed data
    /// </summary>
    protected void ClearData()
      => ViewModel?.ClearItems();

    /// <summary>
    /// Reloads the current data
    /// </summary>
    /// <param name="filter">Filter for the data</param>
    protected async Task ReloadData(string filter)
    {
      // If the view model is null..
      if (ViewModel is null)
        // Exit
        return;

      // Update the filter
      ViewModel.Search = filter;
      // Move the first page
      ViewModel.PageIndex = 1;

      // Load the view items
      await ViewModel.LoadItemsAsync().ConfigureAwait(false);

      // Go to the first page
      GoToPage(0);
    }

    /// <inheritdoc />
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
      // If the persisting page number is greater than one..
      if (ViewModel?.PageIndex > 1)
        // Navigate to the persisting page
        GoToPage(ViewModel.PageIndex - 1);

      return base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    /// Change paging to given <paramref name="page"/> number
    /// </summary>
    /// <param name="page">Page number</param>
    protected abstract void GoToPage(int page);
  }
}