namespace Models.REST.Auth
{
  /// <summary>
  /// Model representing a registration request
  /// </summary>
  public record RegistrationRequest
  {
    /// <summary>
    /// Registration request email
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Registration request password
    /// </summary>
    public string Password { get; init; }
  }
}