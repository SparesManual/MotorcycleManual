using FluentValidation;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Interfaces.Auth.Validators
{
  /// <summary>
  /// Interface for validating register view models
  /// </summary>
  public interface IRegisterViewModelValidator
    : IValidator<IRegisterViewModel>
  {
  }
}