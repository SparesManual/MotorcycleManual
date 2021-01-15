using System.Net.Http;
using Db.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MRI.Db;
using ViewModels.Interfaces.Queries;
using ViewModels.Queries;

namespace MRI.Application
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddSingleton<IAPIProvider, APIProvider>();
      services.AddScoped<HttpClient>();
      services.AddScoped<IBooksViewModel, BooksViewModel>();
      services.AddScoped<IAllPartsViewModel, PartsAllViewModel>();
      services.AddScoped<IPartViewModel, PartViewModel>();
      services.AddScoped<IPartPropertiesViewModel, PartPropertiesViewModel>();
      services.AddServerSideBlazor();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}