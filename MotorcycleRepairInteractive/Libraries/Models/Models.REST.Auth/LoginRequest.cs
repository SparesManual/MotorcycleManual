namespace Models.REST.Auth
{
  /// <summary>
  /// Model representing a login request
  /// </summary>
  public record LoginRequest
  {
    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; init; }
    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; init; }
    /// <summary>
    /// Determines whether the user login session should be remembered
    /// </summary>
    public bool RememberMe { get; init; }
  }
}
