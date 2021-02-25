using System.Windows.Input;
using Db.Interfaces;
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
    #region Fields

    private readonly IAPIAuth m_apiAuth;
    private string m_email = string.Empty;
    private string m_password = string.Empty;
    private string m_confirmPassword = string.Empty;

    #endregion

    #region Properties

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
      => new RelayCommand(() => { });

    #endregion

    /// <inheritdoc />
    public RegisterViewModel(IRegisterViewModelValidator validator, IAPIAuth apiAuth)
      : base(validator)
      => m_apiAuth = apiAuth;
  }
}