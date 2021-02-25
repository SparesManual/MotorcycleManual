using System.Threading.Tasks;
using System.Windows.Input;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;
using ViewModels.Interfaces.Auth.Enums;
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
    #region Fields

    private readonly INavigator m_navigator;
    private readonly IAPIAuth m_authProvider;
    private string m_email = string.Empty;
    private string m_password = string.Empty;
    private bool m_rememberMe;

    #endregion

    #region Properties

    /// <inheritdoc />
    public string Email
    {
      get => m_email;
      set
      {
        m_email = value;
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public string Password
    {
      get => m_password;
      set
      {
        m_password = value;
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public bool RememberMe
    {
      get => m_rememberMe;
      set
      {
        m_rememberMe = value;
        OnPropertyChanged();
      }
    }

    #endregion

    #region Commands

    /// <inheritdoc />
    public ICommand SubmitCommand
      => new RelayCommand(async () => await Submit().ConfigureAwait(false));

    /// <inheritdoc />
    public ICommand ForgotPasswordCommand
      => new RelayCommand(() => m_navigator.NavigateTo("/forgotPassword"));

    #endregion

    /// <inheritdoc />
    public LoginViewModel(INavigator navigator, ILoginViewModelValidator validator, IAPIAuth authProvider)
      : base(validator)
    {
      m_navigator = navigator;
      m_authProvider = authProvider;

      ClearErrors();
    }

    /// <inheritdoc />
    public async Task<LoginResult> LoginUser()
    {
      var result = await m_authProvider.LoginUserAsync(Email, Password).ConfigureAwait(false);
      return result switch
      {
        (false, 404) => LoginResult.InvalidCredentials,
        (true, _) => LoginResult.Success,
        _ => LoginResult.ServerError
      };
    }

    private async Task Submit()
    {
      var result = await LoginUser().ConfigureAwait(false);
      switch (result)
      {
        case LoginResult.Success:
          m_navigator.NavigateTo("/");
          break;
        case LoginResult.InvalidCredentials:
          break;
      }
    }
  }
}