using Db.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Db.API.Extensions
{
  /// <summary>
  /// Helper class for extending the ASP services with identity-specific services
  /// </summary>
  public static class IdentityServiceExtensions
  {
    /// <summary>
    /// Extends the service collection with identity services
    /// </summary>
    /// <param name="services">Collection of services</param>
    /// <returns>Collection of updated services</returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
      var builder = services.AddIdentityCore<IdentityUser>();

      builder = new IdentityBuilder(builder.UserType, builder.Services);
      builder
        .AddEntityFrameworkStores<IdentityContext>()
        .AddSignInManager<SignInManager<IdentityUser>>();

      services.AddAuthentication();

      return services;
    }
  }
}