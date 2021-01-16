using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  public interface IBookSectionsViewModel
    : IPagedViewModel<ISection>
  {
    int BookId { get; set; }
  }
}