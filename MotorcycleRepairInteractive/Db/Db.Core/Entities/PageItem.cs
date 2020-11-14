namespace Db.Core.Entities
{
  /// <summary>
  /// Entity representing an item of a <see cref="Page"/>
  /// </summary>
  public class PageItem
    : BaseEntity
  {
    /// <summary>
    /// Item source <see cref="Entities.Page"/> reference
    /// </summary>
    public Page? Page { get; set; }

    /// <summary>
    /// Item source <see cref="Entities.Page"/> foreign key
    /// </summary>
    public int PageId { get; set; }

    /// <summary>
    /// Reference to a <see cref="Entities.Part"/>
    /// </summary>
    public Part? Part { get; set; }

    /// <summary>
    /// Foreign key of the given <see cref="Part"/>
    /// </summary>
    public int PartId { get; set; }

    /// <summary>
    /// Item index
    /// </summary>
    public int IndexNumber { get; set; }

    /// <summary>
    /// Item occurrence
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Item remarks
    /// </summary>
    public string Remarks { get; set; } = string.Empty;
  }
}
