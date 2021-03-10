namespace Email.Templates
{
  /// <summary>
  /// Password recovery model
  /// </summary>
  public class PasswordRecoveryModel
  {
    /// <summary>
    /// Password recovery URL code
    /// </summary>
    public string Code { get; set; } = string.Empty;
  }
}
