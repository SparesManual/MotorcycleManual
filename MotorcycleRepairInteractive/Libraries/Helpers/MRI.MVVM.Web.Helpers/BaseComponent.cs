using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views;

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
      static bool IsObservable(Type? type)
      {
        if (type is null || !type.IsGenericType || !type.IsClass)
          return false;

        var observable = typeof(ObservableCollection<object>);
        return type.Name.Equals(observable.Name);
      }

      base.OnInitialized();
      if (ViewModel == null)
        throw new Exception("Invalidly configured ViewModel");

      ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
      var properties = ViewModel
        .GetType()
        .GetProperties()
        .Where(x => IsObservable(x.PropertyType))
        .Select(x => x.GetValue(ViewModel))
        .OfType<INotifyPropertyChanged>();

      foreach (var property in properties)
        property.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
      => StateHasChanged();
  }
}
