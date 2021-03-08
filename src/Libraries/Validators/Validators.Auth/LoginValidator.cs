using FluentValidation;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Validators.Auth
{
  /// <summary>
  /// Validator for the <see cref="ILoginViewModel"/> view models
  /// </summary>
  public class LoginValidator
    : AbstractValidator<ILoginViewModel>, ILoginViewModelValidator
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public LoginValidator()
    {
      RuleFor(x => x.Email).NotEmpty().EmailAddress();
      RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
  }
}