using System.Security;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for login views
  /// </summary>
  public class LoginViewModel
    : BaseFormViewModel<ILoginViewModelValidator>, ILoginViewModel
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
    public LoginViewModel(ILoginViewModelValidator validator)
      : base(validator)
    {
    }
  }
}