using System.Security;
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
    SecureString Password { get; set; }

    /// <summary>
    /// Password confirmation
    /// </summary>
    SecureString ConfirmPassword { get; set; }

    #endregion
  }
}