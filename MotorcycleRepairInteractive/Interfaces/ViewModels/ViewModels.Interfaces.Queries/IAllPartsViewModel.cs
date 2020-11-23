using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for all part view models
  /// </summary>
  public interface IAllPartsViewModel
    : IPagedViewModel<IPart>
  {
  }
}