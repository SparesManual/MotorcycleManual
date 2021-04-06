using System.Threading.Tasks;
using System.Windows.Input;
using MRI.MVVM.Interfaces.ViewModels;
using ViewModels.Interfaces.Auth.Enums;

namespace ViewModels.Interfaces.Auth.ViewModels
{
  /// <summary>
  /// Interface for login view models
  /// </summary>
  public interface ILoginViewModel
    : IFormViewModel
  {
    #region Properties

    /// <summary>
    /// Authentication user email
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// Authentication user password
    /// </summary>
    string Password { get; set; }

    /// <summary>
    /// Authentication caching
    /// </summary>
    bool RememberMe { get; set; }

    #endregion

    #region Command

    /// <summary>
    /// Submits the login form
    /// </summary>
    ICommand SubmitCommand { get; }

    /// <summary>
    /// Submits a request for the password recovery form
    /// </summary>
    ICommand ForgotPasswordCommand { get; }

    /// <summary>
    /// User attempted to login without confirming their email
    /// </summary>
    bool RequiresConfirmation { get; set; }

    #endregion

    /// <summary>
    /// Attempts to login the user
    /// </summary>
    /// <returns>True if logged in successfully</returns>
    Task<LoginResult> LoginUser();
  }
}