using AntDesign;
using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views.Managers;

namespace MRI.MVVM.Web.Helpers.Managers
{
  /// <summary>
  /// Manager for AntDesign paging
  /// </summary>
  public class AntDesignPagingManager
    : IPagingManager
  {
    /// <inheritdoc />
    public int GetPageIndex<T>(IPagedViewModel<T> viewModel, object paging)
    {
      var data = (PaginationEventArgs) paging;

      return data.Page;
    }
  }
}