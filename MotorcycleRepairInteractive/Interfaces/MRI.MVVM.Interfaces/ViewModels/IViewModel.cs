using System.ComponentModel;
using MRI.MVVM.Interfaces.Views;

namespace MRI.MVVM.Interfaces.ViewModels
{
  /// <summary>
  /// Base interface for view models consumed by <see cref="IView{TViewModel}"/>
  /// </summary>
  public interface IViewModel
    : INotifyPropertyChanged
  {
  }
}