using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for book sections view models
  /// </summary>
  public interface IBookSectionsViewModel
    : IPagedViewModel<ISection>
  {
    /// <summary>
    /// Parent book id
    /// </summary>
    int BookId { get; set; }
  }
}