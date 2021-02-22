using System.Linq;
using System.Text;
using Db.Infrastructure.Data;
using Db.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
      services.AddControllersWithViews();
      services.AddGrpc();
      services
        .AddDbContext<ManualContext>(options => options.UseSqlite(m_configuration.GetConnectionString("DefaultConnection")))
        .AddDbContext<IdentityContext>(options => options.UseSqlite(m_configuration.GetConnectionString("DefaultAuthConnection")))
        .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

      services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<IdentityContext>();

      services.AddCors(options =>
      {
        options.AddPolicy("AllowAll", builder =>
        {
          builder.WithOrigins("https://localhost:4155", "https://localhost:5468")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
        });
      });

      services.AddControllers();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = m_configuration["JwtIssuer"],
            ValidAudience = m_configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_configuration["JwtSecurityKey"]))
          };
        });

      services.AddMvc().AddNewtonsoftJson();
      services.AddResponseCompression(opts =>
      {
        opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
      });

    }

    /// <summary>
    /// Configures the API server application functionality
    /// </summary>
    /// <param name="app">Application context</param>
    /// <param name="env">Application hosting environment provider</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseResponseCompression();

      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseRouting();
      app.UseCors("AllowAll");
      app.UseGrpcWeb();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<ProviderService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<AuthService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapControllers();

        endpoints.MapGet("/", context => context.Response.WriteAsync(""));
      });
    }
  }
}