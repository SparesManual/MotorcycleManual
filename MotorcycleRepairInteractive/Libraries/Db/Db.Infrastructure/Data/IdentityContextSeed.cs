using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Db.Infrastructure.Data
{
  /// <summary>
  /// Helper class for seeding data into the identity database
  /// </summary>
  public static class IdentityContextSeed
  {
    /// <summary>
    /// Seeds test data into the database
    /// </summary>
    /// <param name="userManager">Identity user manager</param>
    public static async Task SeedUsersAsync(this UserManager<IdentityUser> userManager)
    {
      if (await userManager.Users.AnyAsync().ConfigureAwait(false))
        return;

      var user = new IdentityUser
      {
        UserName = "bob@test.com",
      };

      await userManager.CreateAsync(user, "Pa$$w0rd").ConfigureAwait(false);
    }
  }
}