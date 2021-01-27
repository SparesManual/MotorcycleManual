using System.Threading.Tasks;
using Db.Interfaces;
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
    private readonly IAPIAuth m_authProvider;

    #region Properties

    /// <inheritdoc />
    public string Username { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Password { get; set; } = string.Empty;

    /// <inheritdoc />
    public bool RememberMe { get; set; }

    #endregion

    /// <inheritdoc />
    public LoginViewModel(ILoginViewModelValidator validator, IAPIAuth authProvider)
      : base(validator)
    {
      m_authProvider = authProvider;
    }

    /// <inheritdoc />
    public async ValueTask<bool> LoginUser()
      => await m_authProvider.LoginUser(Username, Password).ConfigureAwait(false);
  }
}