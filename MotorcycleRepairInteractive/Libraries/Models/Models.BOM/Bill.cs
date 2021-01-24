using System;
using System.Collections.Generic;

namespace Models.BOM
{
  /// <summary>
  /// Entity representing a bill of materials
  /// </summary>
  public record Bill
  {
    /// <summary>
    /// Client information
    /// </summary>
    public Client? Client { get; init; }

    /// <summary>
    /// Bill materials
    /// </summary>
    public IReadOnlyCollection<Material> Materials { get; init; } = Array.Empty<Material>();
  }
}