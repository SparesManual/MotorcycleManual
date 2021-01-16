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
  public class BaseItemComponent<TViewModel, TItem>
    : BaseComponent<TViewModel>
    where TViewModel : class, IItemViewModel<TItem>
  {
    /// <summary>
    /// Id of the item to display
    /// </summary>
    [Parameter]
    public int ItemId
    {
      get => ViewModel.Id;
      set => ViewModel.Id = value;
    }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
      => await ViewModel.LoadItem().ConfigureAwait(false);
  }
}