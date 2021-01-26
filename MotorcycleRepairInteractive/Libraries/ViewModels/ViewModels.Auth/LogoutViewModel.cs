using FluentValidation;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
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