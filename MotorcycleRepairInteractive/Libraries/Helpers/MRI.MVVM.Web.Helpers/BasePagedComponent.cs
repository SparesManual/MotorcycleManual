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
      if (ViewModel != null) await ViewModel.LoadItems().ConfigureAwait(true);
    }

    /// <summary>
    /// Load paged data
    /// </summary>
    /// <param name="filter">Full-text filter string</param>
    /// <param name="paging">Paging data</param>
    protected async Task LoadData(string filter, object paging)
    {
      if (ViewModel is null)
        return;

      var newPage = PagingManager.GetPageIndex(ViewModel, paging);
      if (ViewModel.Search.Equals(filter)
          && ViewModel.PageIndex == newPage)
        return;

      ViewModel.Search = filter;
      ViewModel.PageIndex = newPage;

      await ViewModel.LoadItems().ConfigureAwait(false);
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
      if (ViewModel is null)
        return;

      ViewModel.Search = filter;
      ViewModel.PageIndex = 1;

      await ViewModel.LoadItems().ConfigureAwait(false);

      GoToPage(0);
    }

    /// <inheritdoc />
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
      if (ViewModel?.PageIndex > 1)
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