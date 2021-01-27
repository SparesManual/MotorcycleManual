using System;
using System.Threading.Tasks;
using Db.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Db.API
{
  /// <summary>
  /// Main API Class
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class Program
  {
    /// <summary>
    /// Program entry point
    /// </summary>
    /// <param name="args">Console arguments</param>
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      using var scope = host.Services.CreateScope();
      var services = scope.ServiceProvider;
      var loggerFactory = services.GetRequiredService<ILoggerFactory>();

      try
      {
        var context = services.GetRequiredService<ManualContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);
        await context.SeedAsync(loggerFactory).ConfigureAwait(false);

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var identityContext = services.GetRequiredService<IdentityContext>();
        await identityContext.Database.MigrateAsync().ConfigureAwait(false);
        await userManager.SeedUsersAsync().ConfigureAwait(false);
      }
      catch (Exception e)
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An error occurred during migration");
      }

      await host.RunAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the API host
    /// </summary>
    /// <param name="args">Supplied console arguments</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public static IHostBuilder CreateHostBuilder(string[] args)
      => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
  }
}
