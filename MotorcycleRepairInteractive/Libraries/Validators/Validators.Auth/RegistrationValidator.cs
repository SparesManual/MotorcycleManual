using FluentValidation;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Validators.Auth
{
  /// <summary>
  /// Validators for the <see cref="IRegisterViewModel"/> view models
  /// </summary>
  public class RegisterValidator
    : AbstractValidator<IRegisterViewModel>, IRegisterViewModelValidator
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public RegisterValidator()
    {
      RuleFor(x => x.Email).NotEmpty().EmailAddress();
      RuleFor(x => x.Password).NotEmpty();
      RuleFor(x => x.ConfirmPassword).NotEmpty().NotEqual(x => x.Password);
    }
  }
}