using System.Threading.Tasks;
using System.Windows.Input;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using MRI.MVVM.Interfaces;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for the forgot password views
  /// </summary>
  public class ForgotPasswordViewModel
    : BaseFormViewModel<IForgotPasswordViewModelValidator>, IForgotPasswordViewModel
  {
    private readonly INavigator m_navigator;
    private readonly IAPIAuth m_apiAuth;
    private string m_email = string.Empty;
    private bool m_requested;

    /// <inheritdoc />
    public bool Requested
    {
      get => m_requested;
      set
      {
        m_requested = value;
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
    public ICommand SubmitCommand
      => new RelayCommand(async () => await RequestPasswordResetAsync(), () => !Requested);

    /// <inheritdoc />
    public ICommand BackToLoginCommand
      => new RelayCommand(() => m_navigator.NavigateTo("/login"));

    /// <inheritdoc />
    public ForgotPasswordViewModel(INavigator navigator, IAPIAuth apiAuth, IForgotPasswordViewModelValidator validator)
      : base(validator)
    {
      m_navigator = navigator;
      m_apiAuth = apiAuth;
    }

    private async ValueTask RequestPasswordResetAsync()
    {
      await m_apiAuth.RequestPasswordResetAsync(Email).ConfigureAwait(false);
      Requested = true;
    }
  }
}