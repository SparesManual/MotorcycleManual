using System.Security;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Auth.ViewModels
{
  /// <summary>
  /// Interface for registration view models
  /// </summary>
  public interface IRegisterViewModel
    : IFormViewModel
  {
    #region Properties

    /// <summary>
    /// User name
    /// </summary>
    string Username { get; set; }

    /// <summary>
    /// User email
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    SecureString Password { get; set; }

    /// <summary>
    /// User password confirmation
    /// </summary>
    SecureString ConfirmPassword { get; set; }

    #endregion
  }
}