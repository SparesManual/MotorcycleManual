using System;
using MRI.MVVM.Interfaces;

namespace MRI.MVVM.Helpers
{
  /// <summary>
  /// Command which is invoked by a View and executes a defined action
  /// </summary>
  public class RelayCommand
    : IRelayCommand
  {
    #region Fields

    private readonly Action m_execute;
    private readonly Func<bool> m_canExecute;

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="execute">Action to execute</param>
    /// <param name="canExecute">Set to true if command can be executed</param>
    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
      m_execute = execute ?? throw new ArgumentNullException(nameof(execute));

      m_canExecute = canExecute ?? (() => true);
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when changes occur that affect whether the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    #endregion

    #region Methods

    /// <inheritdoc />
    public void RaiseCanExecuteChanged()
      => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">This parameter will always be ignored.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
      => m_canExecute();

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">This parameter will always be ignored.</param>
    public virtual void Execute(object parameter)
    {
      if (CanExecute(parameter))
        m_execute.Invoke();
    }

    #endregion
  }

  /// <summary>
  /// Command which is invoked by a View with a CommandParameter and executes a defined action
  /// </summary>
  /// <typeparam name="T">CommandParameter type</typeparam>
  public class RelayCommand<T>
    : IRelayCommand
  {
    #region Fields

    private readonly Action<T> m_execute;
    private readonly Func<bool> m_canExecute;

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="execute">Action to execute</param>
    /// <param name="canExecute">Set to true if command can be executed</param>
    public RelayCommand(Action<T> execute, Func<bool>? canExecute = null)
    {
      m_execute = execute ?? throw new ArgumentNullException(nameof(execute));

      m_canExecute = canExecute ?? (() => true);
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when changes occur that affect whether the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    #endregion

    #region Methods

    /// <inheritdoc />
    public void RaiseCanExecuteChanged()
      => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">This parameter will always be ignored.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
      => m_canExecute();

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    public virtual void Execute(object parameter)
    {
      if (CanExecute(parameter))
        m_execute.Invoke((T)parameter);
    }

    #endregion
  }
}