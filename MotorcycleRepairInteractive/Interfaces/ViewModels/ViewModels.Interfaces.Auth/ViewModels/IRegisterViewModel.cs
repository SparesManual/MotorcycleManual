using System.Windows.Input;
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
    string Password { get; set; }

    /// <summary>
    /// User password confirmation
    /// </summary>
    string ConfirmPassword { get; set; }

    #endregion

    /// <summary>
    /// Submits the registration request
    /// </summary>
    ICommand SubmitCommand { get; }
  }
}