using System.Windows.Input;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;
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
    #region Fields

    private readonly INavigator m_navigator;
    private readonly IAPIAuth m_apiAuth;
    private string m_email = string.Empty;
    private string m_password = string.Empty;
    private string m_confirmPassword = string.Empty;
    private bool m_registrationFailed;

    #endregion

    #region Properties

    /// <inheritdoc />
    public bool RegistrationFailed
    {
      get => m_registrationFailed;
      set
      {
        m_registrationFailed = value;
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public string Username { get; set; } = string.Empty;

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
    public string ConfirmPassword
    {
      get => m_confirmPassword;
      set
      {
        m_confirmPassword = value;
        OnPropertyChanged();
      }
    }

    #endregion

    #region Commands

    /// <inheritdoc />
    public ICommand SubmitCommand
      => new RelayCommand(async () =>
      {
        var userId = await m_apiAuth.RegisterUserAsync(Email, Password).ConfigureAwait(false);
        if (string.IsNullOrEmpty(userId))
        {
          RegistrationFailed = true;
          return;
        }

        RegistrationFailed = false;
        m_navigator.NavigateTo("/registered", userId);
      });

    #endregion

    /// <inheritdoc />
    public RegisterViewModel(IRegisterViewModelValidator validator, INavigator navigator, IAPIAuth apiAuth)
      : base(validator)
    {
      m_navigator = navigator;
      m_apiAuth = apiAuth;
    }
  }
}