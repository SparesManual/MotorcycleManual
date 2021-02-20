using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MRI.MVVM.Interfaces.ViewModels;
using MRI.MVVM.Interfaces.Views;

namespace MRI.MVVM.WPF.Helpers
{
  /// <summary>
  /// Helper class for managing transitions between views
  /// </summary>
  public static class NavigationManager
  {
    private static readonly Dictionary<string, Type> VIEWS;

    /// <summary>
    /// Invoked when navigating to a new page
    /// </summary>
    public static event EventHandler<string>? NavigationChanged;

    /// <summary>
    /// The currently displayed page
    /// </summary>
    public static string CurrentPage { get; private set; } = string.Empty;

    /// <summary>
    /// Static constructor
    /// </summary>
    static NavigationManager()
      => VIEWS = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// Register a new view to the manager
    /// </summary>
    /// <typeparam name="TView">View type</typeparam>
    /// <param name="key">Key under which the view is to be registered and later referenced when navigating to it</param>
    public static void Register<TView>(string key)
      where TView : class, IView<IViewModel>
    {
      if (VIEWS.ContainsKey(key))
        throw new DuplicateNameException($"The manager cannot have two entries with the same name: {key}");

      var constructors = typeof(TView).GetConstructors().Any(c => c.GetParameters().Length == 0 && !c.IsStatic && !c.IsAbstract && !c.IsPrivate);
      if (!constructors)
        throw new NotSupportedException($"The provided type to register must have a parameterless constructor");

      VIEWS.Add(key, typeof(TView));
    }

    /// <summary>
    /// Navigate to a different view of a given <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the view to navigate to</param>
    /// <returns>True if the navigation request was successful</returns>
    public static bool NavigateTo(string name)
    {
      if (!VIEWS.ContainsKey(name))
        return false;

      if (NavigationChanged is null)
        throw new EntryPointNotFoundException("The application is not configured correctly as there is no-one to send the request to");

      NavigationChanged.Invoke(null, name);
      CurrentPage = name;

      return true;
    }

    /// <summary>
    /// Retrieves a view of the given <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the view to retrieve</param>
    /// <returns>Retrieved view instance</returns>
    public static IView<IViewModel> ResolveView(string name)
    {
      if (!VIEWS.TryGetValue(name, out var view))
        throw new KeyNotFoundException($"No views with the name {name} are registered in the manager");

      var constructor = view!.GetConstructors().First(c => c.GetParameters().Length == 0);
      var instance = constructor.Invoke(null);

      return (IView<IViewModel>)instance;
    }
  }
}
