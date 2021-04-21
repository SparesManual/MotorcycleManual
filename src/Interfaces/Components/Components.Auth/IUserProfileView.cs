using MRI.MVVM.Interfaces.Views;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth
{
  /// <summary>
  /// Interface for the user profile view
  /// </summary>
  public interface IUserProfileView
    : IView<IUserProfileViewModel>
  {
  }
}