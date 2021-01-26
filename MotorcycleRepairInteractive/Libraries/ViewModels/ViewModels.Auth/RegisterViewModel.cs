using System.Security;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for the register views
  /// </summary>
  public class RegisterViewModel
    : BaseFormViewModel<IRegisterViewModelValidator>, IRegisterViewModel
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
    public RegisterViewModel(IRegisterViewModelValidator validator)
      : base(validator)
    {
    }
  }
}