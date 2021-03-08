using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Interfaces.Views.Managers
{
  /// <summary>
  /// Interface for paging managers
  /// </summary>
  public interface IPagingManager
  {
    /// <summary>
    /// Gets the currently selected page index
    /// </summary>
    /// <param name="viewModel">View model containing paging settings</param>
    /// <param name="paging">Paging data</param>
    /// <typeparam name="T">View model paged item type</typeparam>
    /// <returns>Page index</returns>
    int GetPageIndex<T>(IPagedViewModel<T> viewModel, object paging);
  }
}