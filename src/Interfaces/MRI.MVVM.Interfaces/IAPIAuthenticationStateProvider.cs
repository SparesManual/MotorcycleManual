namespace MRI.MVVM.Interfaces
{
  /// <summary>
  /// Interface for authentication state providers
  /// </summary>
  public interface IAPIAuthenticationStateProvider
  {
    /// <summary>
    /// Marks the given <paramref name="email"/> as authenticated
    /// </summary>
    /// <param name="email">Email to mark</param>
    void MarkUserAsAuthenticated(string email);

    /// <summary>
    /// Marks the currently authenticated user as unauthenticated
    /// </summary>
    void MarkUserAsLoggedOut();
  }
}