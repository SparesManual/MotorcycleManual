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
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if logged in</returns>
    ValueTask<bool> LoginUser(string email, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to logout the user
    /// </summary>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>True if logged out</returns>
    ValueTask<bool> LogoutUser(CancellationToken cancellationToken = default);
  }
}