using System.Collections.Generic;
using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  public interface IBookModelsViewModel
    : IItemViewModel<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>>
  {
    int BookId { get; }
  }
}