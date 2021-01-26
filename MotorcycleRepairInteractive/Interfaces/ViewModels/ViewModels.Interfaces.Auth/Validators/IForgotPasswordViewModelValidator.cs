using FluentValidation;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Interfaces.Auth.Validators
{
  /// <summary>
  /// Interface for validating forgot password view models
  /// </summary>
  public interface IForgotPasswordViewModelValidator
    : IValidator<IForgotPasswordViewModel>
  {
  }
}