using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using MRI.MVVM.Interfaces.ViewModels;

namespace ViewModels.Interfaces.Auth.ViewModels
{
  /// <summary>
  /// Interface for forgot password view models
  /// </summary>
  public interface IForgotPasswordViewModel
    : IFormViewModel
  {
    /// <summary>
    /// User email
    /// </summary>
    [Required]
    [EmailAddress]
    string Email { get; set; }

    /// <summary>
    /// Submits the password reset request
    /// </summary>
    ICommand SubmitCommand { get; }

    /// <summary>
    /// Submits a request to navigate to the login page
    /// </summary>
    ICommand BackToLoginCommand { get; }

    // Task ResetAsync(CancellationToken cancellationToken = default);
  }
}