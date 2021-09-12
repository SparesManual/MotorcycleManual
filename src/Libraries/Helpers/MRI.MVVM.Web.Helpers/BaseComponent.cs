using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MRI.Helpers;
using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Base for all components
  /// </summary>
  /// <typeparam name="TViewModel">View model type</typeparam>
  public abstract class BaseComponent<TViewModel>
    : LayoutComponentBase, IView<TViewModel>, IDisposable
    where TViewModel : class, IViewModel
  {
    #region Fields

    private const string OBSERVABLE = "ObservableCollection`1";
    private IReadOnlyCollection<INotifyPropertyChanged> m_properties = null!;

    #endregion

    /// <inheritdoc />
    [Inject]
    public TViewModel? ViewModel { get; set; }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
      static bool IsObservable(Type? type)
      {
        // If the type is not a generic class..
        if (type is null
            || !type.IsGenericType
            || !type.IsClass)
          // The type is not an observable
          return false;

        // Determine whether the property is an observable
        return type.Name.Equals(OBSERVABLE);
      }

      base.OnInitialized();

      // If the view model injection failed..
      if (ViewModel == null)
        // Throw an exception
        throw new Exception("Invalidly configured ViewModel");

      // Attach to the view model state changed event
      ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
      // Retrieve the bindable observable properties
      m_properties = ViewModel
        // Retrieve the view model type
        .GetType()
        // Get the type properties
        .GetProperties()
        // Find the observable properties
        .Where(x => IsObservable(x.PropertyType))
        // Get the property values
        .Select(x => x.GetValue(ViewModel))
        // Cast them to the corresponding type
        .OfType<INotifyPropertyChanged>()
        // Materialize the collection
        .ToReadOnlyCollection();

      // For every observable property..
      foreach (var property in m_properties)
        // Attach a state changed event
        property.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
      => StateHasChanged();

    /// <inheritdoc />
    void IDisposable.Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases resources
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;

      // Disconnect from the view model state changed event
      ViewModel!.PropertyChanged -= ViewModelOnPropertyChanged;
      // For every observable property..
      foreach (var property in m_properties)
        // Disconnect from the state changed event
        property.PropertyChanged -= ViewModelOnPropertyChanged;
    }
  }
}