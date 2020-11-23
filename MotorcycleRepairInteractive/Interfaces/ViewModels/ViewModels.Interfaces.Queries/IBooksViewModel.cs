using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces;

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