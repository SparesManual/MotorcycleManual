using System;
using System.Collections.Generic;
using System.Data;
using MRI.MVVM.Interfaces;

namespace MRI.Dealer
{
  /// <summary>
  /// Helper class for managing transitions between views
  /// </summary>
  public class NavigationManager
    : INavigator
  {
    private readonly Dictionary<string, Type> m_types;

    /// <inheritdoc />
    public event EventHandler<INavigator.ViewData>? NavigationChanged;

    /// <summary>
    /// The currently displayed page
    /// </summary>
    public string CurrentPage { get; private set; } = string.Empty;

    /// <summary>
    /// Static constructor
    /// </summary>
    public NavigationManager()
      => m_types = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

    /// <summary>
    /// Register a new view to the manager
    /// </summary>
    /// <typeparam name="T">View type</typeparam>
    /// <param name="key">Key under which the view is to be registered and later referenced when navigating to it</param>
    public void Register<T>(string key)
    {
      if (m_types.ContainsKey(key))
        throw new DuplicateNameException($"The manager cannot have two entries with the same name: {key}");

      m_types.Add(key, typeof(T));
    }

    /// <summary>
    /// Navigate to a different view of a given <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the view to navigate to</param>
    /// <returns>True if the navigation request was successful</returns>
    public void NavigateTo(string name)
    {
      if (!m_types.ContainsKey(name))
        return;

      if (NavigationChanged is null)
        throw new EntryPointNotFoundException("The application is not configured correctly as there is no-one to send the request to");

      NavigationChanged.Invoke(this, new INavigator.ViewData(name));
      CurrentPage = name;
    }

    /// <inheritdoc />
    public void NavigateTo(string name, params string[] arguments)
    {
      if (!m_types.ContainsKey(name))
        return;

      if (NavigationChanged is null)
        throw new EntryPointNotFoundException("The application is not configured correctly as there is no-one to send the request to");

      NavigationChanged.Invoke(this, new INavigator.ViewData(name, arguments));
      CurrentPage = name;
    }

    /// <inheritdoc />
    public void NavigateTo(string name, IReadOnlyDictionary<string, string> arguments)
    {
      if (!m_types.ContainsKey(name))
        return;

      if (NavigationChanged is null)
        throw new EntryPointNotFoundException("The application is not configured correctly as there is no-one to send the request to");

      NavigationChanged.Invoke(this, new INavigator.ViewData(name, arguments));
      CurrentPage = name;
    }

    /// <summary>
    /// Retrieves a view of the given <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the view to retrieve</param>
    /// <returns>Retrieved view instance</returns>
    public object ResolveView(string name)
    {
      if (!m_types.TryGetValue(name, out var view))
        throw new KeyNotFoundException($"No views with the name {name} are registered in the manager");

      return TypeResolver.ResolveView(view!);
    }
  }
}