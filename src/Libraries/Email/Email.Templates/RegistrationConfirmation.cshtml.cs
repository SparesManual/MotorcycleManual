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
    public string Url { get; set; } = string.Empty;
  }
}
