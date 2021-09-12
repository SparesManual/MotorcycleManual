using Db.Interfaces;
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
    public RegisterValidator(IAPIAuth authProvider)
    {
      RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .MustAsync((x, _, z) => authProvider.UserDoesNotExistAsync(x.Email, z).AsTask())
        .WithMessage("A user with this email exists");
      RuleFor(x => x.Password).NotEmpty();
      RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }
  }
}