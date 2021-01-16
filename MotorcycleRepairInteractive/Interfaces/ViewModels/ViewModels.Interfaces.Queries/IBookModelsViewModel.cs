using System.Collections.Generic;
using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for book models view models
  /// </summary>
  public interface IBookModelsViewModel
    : IItemViewModel<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>>
  {
    /// <summary>
    /// Parent book id
    /// </summary>
    int BookId { get; }
  }
}