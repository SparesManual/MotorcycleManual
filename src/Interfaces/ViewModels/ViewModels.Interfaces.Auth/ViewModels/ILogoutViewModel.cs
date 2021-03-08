using System.Threading.Tasks;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Auth.ViewModels
{
  /// <summary>
  /// Interface for logout view models
  /// </summary>
  public interface ILogoutViewModel
    : IFormViewModel
  {
    /// <summary>
    /// Attempts to logout the user
    /// </summary>
    /// <returns>True if logged out</returns>
    ValueTask<bool> LogoutUser();
  }
}