using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <inheritdoc />
  public class IdentityContext
    : IdentityDbContext<IdentityUser, IdentityRole, string>
  {
  }
}