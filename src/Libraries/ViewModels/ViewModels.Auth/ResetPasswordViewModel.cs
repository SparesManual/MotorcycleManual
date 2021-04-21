using System.Windows.Input;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for the reset password views
  /// </summary>
  public class ResetPasswordViewModel
    : BaseFormViewModel<IResetPasswordViewModelValidator>, IResetPasswordViewModel
  {
    private readonly INavigator m_navigator;
    private readonly IAPIAuth m_apiAuth;

    #region Properties

    /// <inheritdoc />
    public string UserId { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Token { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Password { get; set; } = string.Empty;

    /// <inheritdoc />
    public string ConfirmPassword { get; set; } = string.Empty;

    #endregion

    /// <inheritdoc />
    public ICommand SubmitCommand
      => new RelayCommand(async () =>
      {
        if (await m_apiAuth.ResetPasswordAsync(UserId, Token, Password).ConfigureAwait(false))
          m_navigator.NavigateTo("/login", UserId);
      });

    /// <inheritdoc />
    public ResetPasswordViewModel(INavigator navigator, IAPIAuth apiAuth, IResetPasswordViewModelValidator validator)
      : base(validator)
    {
      m_navigator = navigator;
      m_apiAuth = apiAuth;
    }
  }
}