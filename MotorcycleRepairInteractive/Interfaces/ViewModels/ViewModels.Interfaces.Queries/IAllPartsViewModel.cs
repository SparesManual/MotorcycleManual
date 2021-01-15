using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Models.Interfaces.Entities;
using MRI.MVVM.Interfaces;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Queries
{
  /// <summary>
  /// Interface for all part view models
  /// </summary>
  public interface IAllPartsViewModel
    : IPagedViewModel<IPart>
  {
    /// <summary>
    /// List of items to show in autocompletion
    /// </summary>
    ObservableCollection<string> Autocomplete { get; }

    /// <summary>
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    Task UpdateAutocomplete(string? search);
  }
}