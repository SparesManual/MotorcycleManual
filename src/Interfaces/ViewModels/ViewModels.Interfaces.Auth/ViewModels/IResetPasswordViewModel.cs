using System.Security;
using System.Windows.Input;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Auth.ViewModels
{
  /// <summary>
  /// Interface for reset password view models
  /// </summary>
  public interface IResetPasswordViewModel
    : IFormViewModel
  {
    #region Properties

    /// <summary>
    /// Password
    /// </summary>
    string Password { get; set; }

    /// <summary>
    /// Password confirmation
    /// </summary>
    string ConfirmPassword { get; set; }

    /// <summary>
    /// User id
    /// </summary>
    string UserId { get; set; }

    /// <summary>
    /// Password reset token
    /// </summary>
    string Token { get; set; }

    #endregion

    /// <summary>
    /// Submits the password reset
    /// </summary>
    ICommand SubmitCommand { get; }
  }
}