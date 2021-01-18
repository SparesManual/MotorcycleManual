using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for section parts view models
  /// </summary>
  public interface ISectionPartsViewModel
    : IPagedViewModel<ISectionPart>
  {
    /// <summary>
    /// Currently selected section id
    /// </summary>
    int? SectionId { get; set; }
  }
}