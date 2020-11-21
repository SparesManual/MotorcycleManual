using MRI.MVVM.Interfaces;
using System;
using Microsoft.AspNetCore.Components;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Base for all components
  /// </summary>
  /// <typeparam name="TViewModel">View model type</typeparam>
  public abstract class BaseComponent<TViewModel>
    : LayoutComponentBase, IView<TViewModel>
    where TViewModel : class, IViewModel
  {
    /// <inheritdoc />
    [Inject]
    public TViewModel ViewModel { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
      base.OnInitialized();
      if (ViewModel == null)
        throw new Exception("Invalidly configured ViewModel");

      ViewModel.PropertyChanged += (o, e) => StateHasChanged();
    }
  }
}
