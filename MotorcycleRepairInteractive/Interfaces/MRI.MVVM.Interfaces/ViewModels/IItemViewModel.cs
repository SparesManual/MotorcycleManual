using System.Threading.Tasks;
using Models.Interfaces.Entities;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Interface for view models displaying an item
  /// </summary>
  /// <typeparam name="T">Item type</typeparam>
  public interface IItemViewModel<out T>
    : IViewModel
    where T : IReply
  {
    /// <summary>
    /// Indicates whether the current item is being loaded
    /// </summary>
    bool Loading { get; }

    /// <summary>
    /// Item id
    /// </summary>
    int Id { get; set; }

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