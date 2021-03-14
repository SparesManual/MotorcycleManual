namespace Email.Templates
{
  /// <summary>
  /// Registration confirmation model
  /// </summary>
  public class RegistrationConfirmationModel
  {
    /// <summary>
    /// Account confirmation URL code
    /// </summary>
    public string Code { get; set; } = string.Empty;
  }
}
