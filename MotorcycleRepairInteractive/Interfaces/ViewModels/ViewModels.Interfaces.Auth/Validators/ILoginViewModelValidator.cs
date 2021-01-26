using FluentValidation;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Interfaces.Auth.Validators
{
  /// <summary>
  /// Interface for validating login view models
  /// </summary>
  public interface ILoginViewModelValidator
    : IValidator<ILoginViewModel>
  {
  }
}