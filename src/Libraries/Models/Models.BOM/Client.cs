namespace Models.BOM
{
  /// <summary>
  /// Model representing client information
  /// </summary>
  public record Client
  {
    /// <summary>
    /// Client name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Client email
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Client message
    /// </summary>
    public string? Message { get; init; }
  }
}