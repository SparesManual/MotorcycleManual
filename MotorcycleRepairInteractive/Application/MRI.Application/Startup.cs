using System.Net.Http;
using Db.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MRI.Application.Extensions;
using MRI.Auth;
using MRI.Db;
using MRI.MVVM.Interfaces.Views.Managers;
using MRI.MVVM.Web.Helpers.Managers;

namespace MRI.Application
{
  public class Startup
  {
    public static void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddSingleton<IAPIProvider, APIProvider>();
      services.AddScoped<IAPIAuth, APIAuth>();
      services.AddScoped<HttpClient>();
      services.AddManualViewModels().AddIdentityViewModels();

      services.AddScoped<IPagingManager, RadzenPagingManager>();
      services.AddServerSideBlazor();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}