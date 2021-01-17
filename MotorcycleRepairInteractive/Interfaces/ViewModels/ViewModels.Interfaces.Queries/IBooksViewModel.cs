using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for books view models
  /// </summary>
  public interface IBooksViewModel
    : IPagedViewModel<IBook>
  {
  }
}