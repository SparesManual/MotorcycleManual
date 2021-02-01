using System;
using System.Threading;
using System.Threading.Tasks;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface for authentication APIs
  /// </summary>
  public interface IAPIAuth
    : IDisposable
  {
    /// <summary>
    /// Attempts to login the user using the provided credentials
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="password">User password</param>
    /// <param name="rememberMe"></param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if logged in</returns>
    Task<(bool, int)> LoginUser(string email, string password, bool rememberMe = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to logout the user
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if logged out</returns>
    ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the currently signed in user email
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>The signed in user email. Null if not signed in</returns>
    Task<string?> GetUserAsync(CancellationToken cancellationToken = default);
  }
}