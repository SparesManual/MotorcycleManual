using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Email.API
{
  /// <summary>
  /// Main API Class
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
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

    private static IHostBuilder CreateHostBuilder(string[] args)
      => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}