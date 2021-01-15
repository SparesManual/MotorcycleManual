using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  public interface IPartPropertiesViewModel
    : IPagedViewModel<IProperty>
  {
    public int PartId { get; set; }
  }
}