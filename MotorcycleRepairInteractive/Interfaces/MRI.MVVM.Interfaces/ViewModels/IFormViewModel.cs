using System;
using System.ComponentModel;
using MRI.MVVM.Interfaces.Views;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Base interface for view models with validation consumed by <see cref="IView{TViewModel}"/>
  /// </summary>
  public interface IFormViewModel
    : IViewModel, IDataErrorInfo
  {
    /// <summary>
    /// Used to notify whether the forms error state has changed
    /// </summary>
    event EventHandler<(string property, bool hasError)>? ErrorsChanged;

    /// <summary>
    /// Determines whether the form has any errors
    /// </summary>
    bool HasErrors { get; }
  }
}