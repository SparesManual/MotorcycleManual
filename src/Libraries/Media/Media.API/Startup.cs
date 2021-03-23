using Media.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Media.API
{
  /// <summary>
  /// API Server startup class
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Configures the API server injected services
    /// </summary>
    /// <param name="services">Injected services library</param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
    }

    /// <summary>
    /// Configures the API server application functionality
    /// </summary>
    /// <param name="app">Application context</param>
    /// <param name="env">Application hosting environment provider</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<MediaService>();

        endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"); });
      });
    }
  }
}