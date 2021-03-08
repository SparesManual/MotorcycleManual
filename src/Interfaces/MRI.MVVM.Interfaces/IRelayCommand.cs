using System.Windows.Input;

namespace MRI.MVVM.Interfaces
{
  /// <summary>
  /// Extension interface of <see cref="ICommand"/>
  /// </summary>
  public interface IRelayCommand
    : ICommand
  {
    /// <summary>
    /// Raises the <see cref="ICommand.CanExecuteChanged" /> event.
    /// </summary>
    void RaiseCanExecuteChanged();
  }
}