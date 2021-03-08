using System.Windows.Input;
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
    private string m_email = string.Empty;

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
      => new RelayCommand(() => { });

    /// <inheritdoc />
    public ICommand BackToLoginCommand
      => new RelayCommand(() => m_navigator.NavigateTo("/login"));

    /// <inheritdoc />
    public ForgotPasswordViewModel(INavigator navigator, IForgotPasswordViewModelValidator validator)
      : base(validator)
    {
      m_navigator = navigator;
    }
  }
}