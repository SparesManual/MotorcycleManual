using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views.Managers;
using Radzen;

namespace MRI.MVVM.Web.Helpers.Managers
{
  /// <summary>
  /// Manager for radzen paging
  /// </summary>
  public class RadzenPagingManager
    : IPagingManager
  {
    /// <inheritdoc />
    public int GetPageIndex<T>(IPagedViewModel<T> viewModel, object paging)
    {
      var data = (LoadDataArgs) paging;

      return data.Skip!.Value / viewModel.PageSize + 1;
    }
  }
}