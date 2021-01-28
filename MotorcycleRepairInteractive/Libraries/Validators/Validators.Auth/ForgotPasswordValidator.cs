using FluentValidation;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Validators.Auth
{
  /// <summary>
  /// Validator for the <see cref="IForgotPasswordViewModel"/> view models
  /// </summary>
  public class ForgotPasswordValidator
    : AbstractValidator<IForgotPasswordViewModel>, IForgotPasswordViewModelValidator
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public ForgotPasswordValidator()
    {
      RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
  }
}