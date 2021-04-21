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
    /// The account requires email confirmation
    /// </summary>
    RequiresConfirmation,
    /// <summary>
    /// Failed with server error
    /// </summary>
    ServerError
  }
}