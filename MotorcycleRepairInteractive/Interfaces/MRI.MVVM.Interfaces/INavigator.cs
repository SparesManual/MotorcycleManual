using System;

namespace MRI.MVVM.Interfaces
{
  /// <summary>
  /// Interface for paging managers
  /// </summary>
  public interface INavigator
  {
    /// <summary>
    /// Invoked when navigating to a new page
    /// </summary>
    event EventHandler<string>? NavigationChanged;

    /// <summary>
    /// Navigates to a view of a given <paramref name="name"/>
    /// </summary>
    /// <param name="name">View name</param>
    void NavigateTo(string name);
  }
}