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
  public class BasePagedComponent<TViewModel, TItem>
    : BaseComponent<TViewModel>
    where TViewModel : class, IPagedViewModel<TItem>
  {
    [Inject]
    private IPagingManager PagingManager { get; set; } = null!;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
      => await ViewModel.LoadItems().ConfigureAwait(true);

    /// <summary>
    /// Load paged data
    /// </summary>
    /// <param name="filter">Full-text filter string</param>
    /// <param name="paging">Paging data</param>
    protected async void LoadData(string filter, object paging)
    {
      ViewModel.Search = filter;

      ViewModel.PageIndex = PagingManager.GetPageIndex(ViewModel, paging);

      await ViewModel.LoadItems().ConfigureAwait(false);
    }
  }
}