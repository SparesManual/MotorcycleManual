namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing a part
  /// </summary>
  public class Part
    : BaseEntity
  {
    /// <summary>
    /// Number of the part
    /// </summary>
    public int PartNumber { get; set; }
    /// <summary>
    /// Part material
    /// </summary>
    public string Material { get; set; } = string.Empty;
  }
}