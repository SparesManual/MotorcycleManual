using MRI.MVVM.Interfaces.Views;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth
{
  /// <summary>
  /// Interface for login views
  /// </summary>
  public interface ILoginView
    : IView<ILoginViewModel>
  {
  }
}