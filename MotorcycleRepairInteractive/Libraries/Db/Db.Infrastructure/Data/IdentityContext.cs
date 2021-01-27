using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <inheritdoc />
  public class IdentityContext
    : IdentityDbContext
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public IdentityContext(DbContextOptions<IdentityContext> options)
      : base(options)
    {
    }
  }
}