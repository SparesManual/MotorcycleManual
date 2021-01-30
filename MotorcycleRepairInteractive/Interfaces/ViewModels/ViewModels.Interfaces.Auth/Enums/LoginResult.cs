namespace ViewModels.Interfaces.Auth.Enums
{
  /// <summary>
  /// Login operation result
  /// </summary>
  public enum LoginResult
  {
    /// <summary>
    /// Success
    /// </summary>
    Success,
    /// <summary>
    /// Failed with invalid credentials
    /// </summary>
    InvalidCredentials,
    /// <summary>
    /// Failed with server error
    /// </summary>
    ServerError
  }
}