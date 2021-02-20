using System;
using Autofac;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.WPF.Helpers
{
  /// <summary>
  /// Helper class for resolving view models
  /// </summary>
  public static class ViewModelResolver
  {
    #region Fields

    private static bool m_isInitialized;
    private static IContainer? m_container;

    #endregion

    /// <summary>
    /// Initializes the view model resolver
    /// </summary>
    /// <param name="builder">IoC builder instance</param>
    public static void Initialize(IContainer container)
    {
      if (m_isInitialized)
        throw new NotSupportedException("Resolver was initialized before");

      m_container = container;
      m_isInitialized = true;
    }

    /// <summary>
    /// Resolves an instance of the view model
    /// </summary>
    /// <param name="type">View model interface type</param>
    /// <returns>Resolved view model instance</returns>
    public static IViewModel Resolve(Type type)
    {
      if (type != typeof(IViewModel))
        throw new NotSupportedException($"The given {type} must inherit a {nameof(IViewModel)}");
      if (!type.IsInterface)
        throw new NotSupportedException("Generic type is not an interface");
      if (!m_isInitialized)
        throw new NotSupportedException("Resolver was not initialized");
      if (m_container is null)
        throw new NotSupportedException("Resolver was invalidly initialized");

      using var scope = m_container.BeginLifetimeScope();
      return (IViewModel)scope.Resolve(type);
    }

    /// <summary>
    /// Resolves an instance of the view model
    /// </summary>
    /// <typeparam name="TViewModel">View model interface instance</typeparam>
    /// <returns>Resolved view model instance</returns>
    public static TViewModel Resolve<TViewModel>()
      where TViewModel : IViewModel
    {
      if (!typeof(TViewModel).IsInterface)
        throw new NotSupportedException("Generic type is not an interface");
      if (!m_isInitialized)
        throw new NotSupportedException("Resolver was not initialized");
      if (m_container is null)
        throw new NotSupportedException("Resolver was invalidly initialized");

      using var scope = m_container.BeginLifetimeScope();
      return scope.Resolve<TViewModel>();
    }
  }
}