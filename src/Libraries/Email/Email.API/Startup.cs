using System.Net.Mail;
using Email.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Email.API
{
  /// <summary>
  /// API Client startup class
  /// </summary>
  public class Startup
  {
    #region Fields

    private readonly IConfiguration m_configuration;
    private const string NO_REPLY_MAIL = "noreply@sparesmanual.com";
    private const string SENDER_NAME = "Spares Manual";
    private const string CONFIG_SECTION = "MailSettings";

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
      services
        .AddFluentEmail(NO_REPLY_MAIL, SENDER_NAME)
        .AddRazorRenderer()
        .AddSmtpSender(new SmtpClient
        {
          EnableSsl = bool.Parse(m_configuration.GetSection(CONFIG_SECTION)["SSL"]),
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Host = m_configuration.GetSection(CONFIG_SECTION)["Host"],
          Port = int.Parse(m_configuration.GetSection(CONFIG_SECTION)["Port"])
        });
    }

    /// <summary>
    /// Configures the API server application functionality
    /// </summary>
    /// <param name="app">Application context</param>
    /// <param name="env">Application hosting environment provider</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<MailerService>();

        endpoints.MapGet("/", context => context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909"));
      });
    }
  }
}