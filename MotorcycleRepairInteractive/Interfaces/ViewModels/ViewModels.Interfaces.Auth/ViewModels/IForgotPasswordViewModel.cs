using System.ComponentModel.DataAnnotations;
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
    // Task ResetAsync(CancellationToken cancellationToken = default);
  }
}