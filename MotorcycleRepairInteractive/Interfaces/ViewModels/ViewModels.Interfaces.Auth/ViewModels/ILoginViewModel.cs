using System.Threading.Tasks;
using MRI.MVVM.Interfaces.ViewModels;

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
    /// Authentication user name
    /// </summary>
    string Username { get; set; }

    /// <summary>
    /// Authentication user password
    /// </summary>
    string Password { get; set; }

    /// <summary>
    /// Authentication caching
    /// </summary>
    bool RememberMe { get; set; }

    #endregion

    /// <summary>
    /// Attempts to login the user
    /// </summary>
    /// <returns>True if logged in successfully</returns>
    ValueTask<bool> LoginUser();
  }
}