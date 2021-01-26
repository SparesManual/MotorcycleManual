using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for logout views
  /// </summary>
  public class LogoutViewModel
    : BaseFormViewModel<ILogoutViewModelValidator>, ILogoutViewModel
  {
    /// <inheritdoc />
    public LogoutViewModel(ILogoutViewModelValidator validator)
      : base(validator)
    {
    }
  }
}