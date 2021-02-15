using System;
using System.Net.Http;
using System.Threading.Tasks;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MRI.Auth;
using MRI.Db;
using MRI.MVVM.Interfaces.Views.Managers;
using MRI.MVVM.Web.Helpers.Managers;
using States.General;
using Validators.Auth;
using ViewModels.Auth;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;
using ViewModels.Interfaces.Queries;
using ViewModels.Queries;

namespace MRI.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.AddSingleton<IAPIProvider, APIWebProvider>();
      builder.Services.AddHttpClient<IAPIAuth, APIRESTAuth>("WasmClient", client => client.BaseAddress = new Uri("https://localhost:5001"));
      builder.Services.AddScoped<HttpClient>();
      builder.Services.AddScoped<IPagingManager, RadzenPagingManager>();
      builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
      builder.Services.AddAuthorizationCore();
      builder.Services
        .AddScoped<IBooksViewModel, BooksViewModel>()
        .AddScoped<IAllPartsViewModel, PartsAllViewModel>()
        .AddScoped<IPartViewModel, PartViewModel>()
        .AddScoped<IPartPropertiesViewModel, PartPropertiesViewModel>()
        .AddScoped<IBookSectionsViewModel, BookSectionsViewModel>()
        .AddScoped<IBookViewModel, BookViewModel>()
        .AddScoped<IBookModelsViewModel, BookModelsViewModel>()
        .AddScoped<ISectionPartsViewModel, SectionPartsViewModel>()
        .AddScoped<IModelViewModel, ModelViewModel>()
        .AddScoped<IEngineViewModel, EngineViewModel>();
      builder.Services
        .AddScoped<ILoginViewModel, LoginViewModel>()
        .AddScoped<IRegisterViewModel, RegisterViewModel>()
        .AddScoped<IForgotPasswordViewModel, ForgotPasswordViewModel>()
        .AddScoped<IResetPasswordViewModel, ResetPasswordViewModel>();
      builder.Services
        .AddScoped<ILoginViewModelValidator, LoginValidator>()
        .AddScoped<IRegisterViewModelValidator, RegisterValidator>()
        .AddScoped<IForgotPasswordViewModelValidator, ForgotPasswordValidator>()
        .AddScoped<IResetPasswordViewModelValidator, ResetPasswordValidator>();

      // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
      await builder.Build().RunAsync().ConfigureAwait(false);
    }
  }
}
