using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MRI.MVVM.Interfaces.ViewModels
{
  public interface IItemsViewModel<T>
    : IViewModel
  {
    #region Properties

    bool Loading { get; set; }

    /// <summary>
    /// Items to display
    /// </summary>
    ObservableCollection<T> Items { get; }

    #endregion

    /// <summary>
    /// Loads items
    /// </summary>
    Task LoadItems();
  }
}