using Db.Infrastructure.Data;
using Db.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Db.API
{
  /// <summary>
  /// API Client startup class
  /// </summary>
  public class Startup
  {
    #region Fields

    private readonly IConfiguration m_configuration;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configuration">Injected configuration</param>
    public Startup(IConfiguration configuration)
      => m_configuration = configuration;

    /// <summary>
    /// Configures the API server injected services
    /// </summary>
    /// <param name="services">Injected services library</param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
      services.AddDbContext<ManualContext>(options => options.UseSqlite(m_configuration.GetConnectionString("DefaultConnection"), migration => migration.MigrationsAssembly("Db.API")));
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    /// <summary>
    /// Configures the API server application functionality
    /// </summary>
    /// <param name="app">Application context</param>
    /// <param name="env">Application hosting environment provider</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<ProviderService>();

        endpoints.MapGet("/", async context =>
        {
          await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });
      });
    }
  }
}
