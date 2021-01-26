using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  public class IdentityContext
    : IdentityDbContext<IdentityUser, IdentityRole, string>
  {
  }
}