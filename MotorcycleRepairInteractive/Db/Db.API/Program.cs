using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Db.API
{
  /// <summary>
  /// Main API Class
  /// </summary>
  public static class Program
  {
    /// <summary>
    /// Program entry point
    /// </summary>
    /// <param name="args">Console arguments</param>
    public static void Main(string[] args)
      => CreateHostBuilder(args)
        .Build()
        .Run();

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
