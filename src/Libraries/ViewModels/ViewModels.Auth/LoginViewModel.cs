using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;
using ReactiveUI;
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
    private bool m_requiresConfirmation;
    private bool m_invalidCredentials;

    #endregion

    #region Properties

    /// <inheritdoc />
    public bool InvalidCredentials
    {
      get => m_invalidCredentials;
      set
      {
        m_invalidCredentials = value;
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public bool RequiresConfirmation
    {
      get => m_requiresConfirmation;
      set
      {
        m_requiresConfirmation = value;
        OnPropertyChanged();
      }
    }

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
    public ICommand SubmitCommand { get; }

    /// <inheritdoc />
    public ICommand ForgotPasswordCommand { get; }

    #endregion

    /// <inheritdoc />
    public LoginViewModel(INavigator navigator, ILoginViewModelValidator validator, IAPIAuth authProvider)
      : base(validator)
    {
      m_navigator = navigator;
      m_authProvider = authProvider;
      SubmitCommand = ReactiveCommand.CreateFromTask(Submit);
      ForgotPasswordCommand = ReactiveCommand.Create(() => m_navigator.NavigateTo("/forgotPassword"));

      ClearErrors();
    }

    /// <inheritdoc />
    public async Task<LoginResult> LoginUser(CancellationToken cancellation)
    {
      var result = await m_authProvider.LoginUserAsync(Email, Password, cancellationToken: cancellation).ConfigureAwait(false);
      return result switch
      {
        (false, 404) => LoginResult.InvalidCredentials,
        (false, 403) => LoginResult.RequiresConfirmation,
        (true, _) => LoginResult.Success,
        _ => LoginResult.ServerError
      };
    }

    private async Task Submit(CancellationToken cancellation)
    {
      var result = await LoginUser(cancellation).ConfigureAwait(false);
      switch (result)
      {
        case LoginResult.Success:
          m_navigator.NavigateTo("/");
          break;
        case LoginResult.InvalidCredentials:
          InvalidCredentials = true;
          RequiresConfirmation = false;
          break;
        case LoginResult.RequiresConfirmation:
          InvalidCredentials = false;
          RequiresConfirmation = true;
          break;
      }
    }
  }
}