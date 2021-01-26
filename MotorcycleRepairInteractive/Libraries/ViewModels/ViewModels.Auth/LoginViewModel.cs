using System.Security;
using FluentValidation;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for login views
  /// </summary>
  public class LoginViewModel
    : BaseFormViewModel, ILoginViewModel
  {
    #region Properties

    /// <inheritdoc />
    public string Username { get; set; } = string.Empty;

    /// <inheritdoc />
    public SecureString Password { get; set; } = new ();

    /// <inheritdoc />
    public bool RememberMe { get; set; }

    #endregion

    /// <inheritdoc />
    public LoginViewModel(IValidator validator)
      : base(validator)
    {
    }
  }
}