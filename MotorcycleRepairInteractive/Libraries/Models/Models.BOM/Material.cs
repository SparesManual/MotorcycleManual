namespace Models.BOM
{
  /// <summary>
  /// Model representing a material
  /// </summary>
  public record Material
  {
    /// <summary>
    /// Database id
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Material part number
    /// </summary>
    public string PartNumber { get; init; }

    /// <summary>
    /// Material makers number
    /// </summary>
    public string? MakersPartNumber { get; init; }

    /// <summary>
    /// Material occurrence in a bill
    /// </summary>
    public uint Quantity { get; set; } = 0;

    /// <summary>
    /// Material part description
    /// </summary>
    public string Description { get; init; }
  }
}