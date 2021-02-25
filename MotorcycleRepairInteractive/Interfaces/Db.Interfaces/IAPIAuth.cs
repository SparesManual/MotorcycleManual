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
    ValueTask<(bool, int)> LoginUserAsync(string email, string password, bool rememberMe = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to logout the user
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if logged out</returns>
    ValueTask<bool> LogoutUserAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to register a new user
    /// </summary>
    /// <param name="email">New users email</param>
    /// <param name="password">New users password</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if registered</returns>
    ValueTask<bool> RegisterUserAsync(string email, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user with the given <paramref name="email"/> exists
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if exists</returns>
    ValueTask<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the currently signed in user email
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>The signed in user email. Null if not signed in</returns>
    ValueTask<string> GetUserAsync(CancellationToken cancellationToken = default);
  }
}