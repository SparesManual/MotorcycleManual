using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for book view models
  /// </summary>
  public interface IBookViewModel
    : IItemViewModel<IBook>
  {
  }
}