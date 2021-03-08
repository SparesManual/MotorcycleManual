using System;
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
    public event EventHandler<string>? NavigationChanged;

    /// <inheritdoc />
    public void NavigateTo(string name)
    {
      NavigationChanged?.Invoke(null, name);
      m_manager.NavigateTo(name);
    }
  }
}