using FluentValidation;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for logout views
  /// </summary>
  public class LogoutViewModel
    : BaseFormViewModel, ILogoutViewModel
  {
    /// <inheritdoc />
    public LogoutViewModel(IValidator validator)
      : base(validator)
    {
    }
  }
}