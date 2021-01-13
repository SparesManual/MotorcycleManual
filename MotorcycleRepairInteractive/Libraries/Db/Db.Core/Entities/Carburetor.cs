using System.ComponentModel.DataAnnotations;

namespace Db.Core.Entities
{
  public class Carburetor
    : BaseEntity
  {
    [Required]
    public string Name { get; set; } = string.Empty;
  }
}