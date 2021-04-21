using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MRI.MVVM.Interfaces;

namespace MRI.Client
{
  public class Navigator
    : INavigator
  {
    private readonly NavigationManager m_manager;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Navigator(NavigationManager manager)
    {
      m_manager = manager;
    }

    /// <inheritdoc />
    public event EventHandler<INavigator.ViewData>? NavigationChanged;

    /// <inheritdoc />
    public void NavigateTo(string name)
    {
      NavigationChanged?.Invoke(this, new INavigator.ViewData(name));
      m_manager.NavigateTo(name);
    }

    /// <inheritdoc />
    public void NavigateTo(string name, params string[] arguments)
    {
      NavigationChanged?.Invoke(this, new INavigator.ViewData(name, arguments));
      m_manager.NavigateTo($"{name}/{string.Join("/", arguments)}");
    }

    /// <inheritdoc />
    public void NavigateTo(string name, IReadOnlyDictionary<string, string> arguments)
    {
      NavigationChanged?.Invoke(this, new INavigator.ViewData(name, arguments));
      m_manager.NavigateTo($"{name}?{string.Join("&", arguments.Select(arg => $"{arg.Key}={arg.Value}"))}");
    }
  }
}