using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for books view model
  /// </summary>
  public interface IBooksViewModel
    : IPagedViewModel<IBook>
  {
  }
}