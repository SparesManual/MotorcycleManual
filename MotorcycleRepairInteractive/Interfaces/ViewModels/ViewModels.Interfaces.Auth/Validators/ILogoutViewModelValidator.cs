using FluentValidation;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Interfaces.Auth.Validators
{
  /// <summary>
  /// Interface for validating logout view models
  /// </summary>
  public interface ILogoutViewModelValidator
    : IValidator<ILogoutViewModel>
  {
  }
}