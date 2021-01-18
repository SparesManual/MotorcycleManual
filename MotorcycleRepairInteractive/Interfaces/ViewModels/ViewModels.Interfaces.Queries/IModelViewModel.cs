using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for model view models
  /// </summary>
  public interface IModelViewModel
    : IItemViewModel<IModel>
  {
  }
}