using System;
using System.Collections.Generic;
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
    : LayoutComponentBase, IView<TViewModel>, IDisposable
    where TViewModel : class, IViewModel
  {
    private IReadOnlyCollection<INotifyPropertyChanged> m_properties = null!;

    /// <inheritdoc />
    [Inject]
    public TViewModel? ViewModel { get; set; } = null;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
      static bool IsObservable(Type? type)
      {
        if (type is null
            || !type.IsGenericType
            || !type.IsClass)
          return false;

        var observable = typeof(ObservableCollection<object>);
        return type.Name.Equals(observable.Name);
      }

      base.OnInitialized();
      if (ViewModel == null)
        throw new Exception("Invalidly configured ViewModel");

      ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
      m_properties = ViewModel
        .GetType()
        .GetProperties()
        .Where(x => IsObservable(x.PropertyType))
        .Select(x => x.GetValue(ViewModel))
        .OfType<INotifyPropertyChanged>()
        .ToArray();

      foreach (var property in m_properties)
        property.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
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

      ViewModel!.PropertyChanged -= ViewModelOnPropertyChanged;
      foreach (var property in m_properties)
        property.PropertyChanged -= ViewModelOnPropertyChanged;
    }
  }
}