using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Models.Interfaces.Entities;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Interface for view models with paging support
  /// </summary>
  /// <typeparam name="T">Paged item type</typeparam>
  public interface IPagedViewModel<T>
    : IViewModel
  {
    #region Properties

    /// <summary>
    /// Index of the current page
    /// </summary>
    int PageIndex { get; set; }

    /// <summary>
    /// Number of maximum items to query
    /// </summary>
    int PageSize { get; set; }

    /// <summary>
    /// Number of currently displayed items
    /// </summary>
    int PageItems { get; set; }

    /// <summary>
    /// Total items available for paging
    /// </summary>
    int TotalItems { get; set; }

    /// <summary>
    /// Is paging loading
    /// </summary>
    bool Loading { get; set; }

    /// <summary>
    /// Search filter
    /// </summary>
    string Search { get; set; }

    /// <summary>
    /// Paged items to display
    /// </summary>
    ObservableCollection<T> Items { get; }

    #endregion

    /// <summary>
    /// Loads items
    /// </summary>
    Task LoadItems();
  }
}