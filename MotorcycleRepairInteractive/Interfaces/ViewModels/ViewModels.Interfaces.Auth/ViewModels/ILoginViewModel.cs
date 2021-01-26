using System.Security;
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
    SecureString Password { get; set; }

    /// <summary>
    /// Authentication caching
    /// </summary>
    bool RememberMe { get; set; }

    #endregion
  }
}