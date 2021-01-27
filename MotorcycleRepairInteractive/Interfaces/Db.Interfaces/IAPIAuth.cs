using System;
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
    /// <param name="username">User name</param>
    /// <param name="password">User password</param>
    /// <returns>True if logged in</returns>
    ValueTask<bool> LoginUser(string username, string password);

    /// <summary>
    /// Attempts to logout the user
    /// </summary>
    /// <returns>True if logged out</returns>
    ValueTask<bool> LogoutUser();
  }
}