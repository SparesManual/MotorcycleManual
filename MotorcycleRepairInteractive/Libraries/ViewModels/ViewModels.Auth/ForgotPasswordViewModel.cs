using MRI.MVVM.Helpers;
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
    /// <inheritdoc />
    public string Email { get; set; } = string.Empty;

    /// <inheritdoc />
    public ForgotPasswordViewModel(IForgotPasswordViewModelValidator validator)
      : base(validator)
    {
    }
  }
}