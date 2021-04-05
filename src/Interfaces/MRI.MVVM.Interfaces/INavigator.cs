using System;
using System.Collections.Generic;
using System.Linq;
using MRI.Helpers;

namespace MRI.MVVM.Interfaces
{
  /// <summary>
  /// Interface for paging managers
  /// </summary>
  public interface INavigator
  {
    /// <summary>
    /// Navigation event data
    /// </summary>
    public readonly struct ViewData
    {
      #region Properties

      /// <summary>
      /// View name
      /// </summary>
      public string Name { get; }

      /// <summary>
      /// Has navigation arguments
      /// </summary>
      public bool HasArguments { get; }

      /// <summary>
      /// Named arguments
      /// </summary>
      public IReadOnlyDictionary<string, string> NamedArguments { get; }

      /// <summary>
      /// Nameless arguments
      /// </summary>
      public IReadOnlyCollection<string> Arguments { get; }

      #endregion

      /// <summary>
      /// Constructor for view without arguments
      /// </summary>
      public ViewData(string name)
      {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HasArguments = false;
        NamedArguments = new Dictionary<string, string>();
        Arguments = Array.Empty<string>();
      }

      /// <summary>
      /// Constructor for view with arguments
      /// </summary>
      public ViewData(string name, IEnumerable<string> arguments)
      {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HasArguments = true;
        NamedArguments = new Dictionary<string, string>();
        Arguments = arguments.ToReadOnlyCollection();
      }

      /// <summary>
      /// Constructor for view with named arguments
      /// </summary>
      public ViewData(string name, IEnumerable<KeyValuePair<string, string>> arguments)
      {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HasArguments = true;
        NamedArguments = arguments.ToDictionary(arg => arg.Key, arg => arg.Value);
        Arguments = Array.Empty<string>();
      }
    }

    /// <summary>
    /// Invoked when navigating to a new page
    /// </summary>
    event EventHandler<ViewData>? NavigationChanged;

    /// <summary>
    /// Navigates to a view of a given <paramref name="name"/>
    /// </summary>
    /// <param name="name">View name</param>
    void NavigateTo(string name);

    /// <summary>
    /// Navigates to a view of a given <paramref name="name"/> with provided <paramref name="arguments"/>
    /// </summary>
    /// <param name="name">View name</param>
    /// <param name="arguments">View arguments</param>
    void NavigateTo(string name, params string[] arguments);

    /// <summary>
    /// Navigates to a view of a given <paramref name="name"/> with provided <paramref name="arguments"/>
    /// </summary>
    /// <param name="name">View name</param>
    /// <param name="arguments">View named arguments</param>
    void Navigate(string name, IReadOnlyDictionary<string, string> arguments);
  }
}