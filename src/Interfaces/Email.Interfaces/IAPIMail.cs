using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.Interfaces
{
  /// <summary>
  /// Interface for email APIs
  /// </summary>
  public interface IAPIMail
    : IDisposable
  {
    /// <summary>
    /// Sends an account registration <paramref name="code"/> to a given <paramref name="email"/>
    /// </summary>
    /// <param name="email">Recipients email</param>
    /// <param name="userId">User identification</param>
    /// <param name="code">Confirmation code</param>
    /// <param name="cancellationToken">Cancellation</param>
    ValueTask SendRegistrationConfirmationAsync(string email, string userId, string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an account recovery <paramref name="code"/> to a given <paramref name="email"/>
    /// </summary>
    /// <param name="email">Recipients email</param>
    /// <param name="code">Confirmation code</param>
    /// <param name="cancellationToken">Cancellation</param>
    ValueTask SendRecoveryCodeAsync(string email, string code, CancellationToken cancellationToken = default);
  }
}