using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Base component for displaying an item
  /// </summary>
  /// <typeparam name="TViewModel">View model for the view</typeparam>
  /// <typeparam name="TItem">Displayed item type</typeparam>
  public abstract class BaseItemComponent<TViewModel, TItem>
    : BaseComponent<TViewModel>
    where TViewModel : class, IItemViewModel<TItem>
  {
    /// <summary>
    /// Id of the item to display
    /// </summary>
    [Parameter]
    public int ItemId
    {
      get => ViewModel?.Id ?? -1;
      set
      {
        // If the view model is not null..
        if (ViewModel is not null)
          // Set the value
          ViewModel.Id = value;
      }
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
      // If the view model is not null..
      if (ViewModel is not null)
        // Load the view items
        await ViewModel.LoadItem().ConfigureAwait(false);
    }
  }
}