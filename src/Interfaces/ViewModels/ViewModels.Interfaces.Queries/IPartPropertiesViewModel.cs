using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for part properties view models
  /// </summary>
  public interface IPartPropertiesViewModel
    : IPagedViewModel<IProperty>
  {
    /// <summary>
    /// Parent part id
    /// </summary>
    string PartId { get; set; }
  }
}