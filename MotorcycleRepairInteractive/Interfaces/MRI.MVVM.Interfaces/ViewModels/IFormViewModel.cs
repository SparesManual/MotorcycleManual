using System;
using MRI.MVVM.Interfaces.Views;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Base interface for view models with validation consumed by <see cref="IView{TViewModel}"/>
  /// </summary>
  public interface IFormViewModel
    : IViewModel
  {
    /// <summary>
    /// Used to notify whether the forms error state has changed
    /// </summary>
    event EventHandler<(string property, bool hasError)>? ErrorsChanged;

    /// <summary>
    /// Latest form error
    /// </summary>
    string Error { get; }

    /// <summary>
    /// Determines whether the form has any errors
    /// </summary>
    bool HasErrors { get; }

    /// <summary>
    /// Indexer for retrieving errors for a given <paramref name="property"/>
    /// </summary>
    /// <param name="property"></param>
    /// <returns>Property error</returns>
    string this[string property] { get; }
  }
}