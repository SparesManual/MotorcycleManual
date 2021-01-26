using System.Security;
using FluentValidation;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for the register views
  /// </summary>
  public class RegisterViewModel
    : BaseFormViewModel, IRegisterViewModel
  {
    #region Properties

    /// <inheritdoc />
    public string Username { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Email { get; set; } = string.Empty;

    /// <inheritdoc />
    public SecureString Password { get; set; } = new ();

    /// <inheritdoc />
    public SecureString ConfirmPassword { get; set; } = new ();

    #endregion

    /// <inheritdoc />
    public RegisterViewModel(IValidator validator)
      : base(validator)
    {
    }
  }
}