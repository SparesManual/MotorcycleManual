using System.Threading.Tasks;

namespace Email.Interfaces
{
  /// <summary>
  /// Interface for email APIs
  /// </summary>
  public interface IAPIMail
  {
    /// <summary>
    /// Sends an account registration <paramref name="code"/> to a given <paramref name="email"/>
    /// </summary>
    /// <param name="email">Recipients email</param>
    /// <param name="code">Confirmation code</param>
    ValueTask SendRegistrationConfirmationAsync(string email, string code);

    /// <summary>
    /// Sends an account recovery <paramref name="code"/> to a given <paramref name="email"/>
    /// </summary>
    /// <param name="email">Recipients email</param>
    /// <param name="code">Confirmation code</param>
    ValueTask SendRecoveryCodeAsync(string email, string code);
  }
}