using System.Security;
using FluentValidation;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for the reset password views
  /// </summary>
  public class ResetPasswordViewModel
    : BaseFormViewModel, IResetPasswordViewModel
  {
    #region Properties

    /// <inheritdoc />
    public SecureString Password { get; set; } = new ();

    /// <inheritdoc />
    public SecureString ConfirmPassword { get; set; } = new ();

    #endregion

    /// <inheritdoc />
    public ResetPasswordViewModel(IValidator validator)
      : base(validator)
    {
    }
  }
}