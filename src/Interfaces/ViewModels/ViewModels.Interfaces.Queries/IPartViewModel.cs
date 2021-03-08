using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for part view models
  /// </summary>
  public interface IPartViewModel
    : IItemViewModel<IPart>
  {
  }
}