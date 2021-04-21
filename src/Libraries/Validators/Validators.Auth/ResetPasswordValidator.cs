using FluentValidation;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Validators.Auth
{
  /// <summary>
  /// Validator for the <see cref="IResetPasswordViewModel"/> view models
  /// </summary>
  public class ResetPasswordValidator
    : AbstractValidator<IResetPasswordViewModel>, IResetPasswordViewModelValidator
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public ResetPasswordValidator()
    {
      RuleFor(x => x.Password).NotEmpty();
      RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }
  }
}