using FluentValidation;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Interfaces.Auth.Validators
{
  /// <summary>
  /// Interface for validating reset password view models
  /// </summary>
  public interface IResetPasswordViewModelValidator
    : IValidator<IResetPasswordViewModel>
  {
  }
}