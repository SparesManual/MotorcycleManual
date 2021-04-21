using System.Threading.Tasks;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Interface for view models displaying an item
  /// </summary>
  /// <typeparam name="T">Item type</typeparam>
  public interface IItemViewModel<out T>
    : IViewModel
  {
    /// <summary>
    /// Indicates whether the current item is being loaded
    /// </summary>
    bool Loading { get; }

    /// <summary>
    /// Item id
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// View model item
    /// </summary>
    T? Item { get; }

    /// <summary>
    /// Loads the view models item
    /// </summary>
    Task LoadItem();
  }
}