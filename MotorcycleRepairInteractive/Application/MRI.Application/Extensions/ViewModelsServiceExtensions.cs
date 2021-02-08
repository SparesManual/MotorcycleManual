using Microsoft.Extensions.DependencyInjection;
using Validators.Auth;
using ViewModels.Auth;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;
using ViewModels.Interfaces.Queries;
using ViewModels.Queries;

namespace MRI.Application.Extensions
{
  public static class ViewModelsServiceExtensions
  {
    public static IServiceCollection AddManualViewModels(this IServiceCollection services)
    {
      services
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

      return services;
    }

    public static IServiceCollection AddIdentityViewModels(this IServiceCollection services)
    {
      services
        .AddScoped<ILoginViewModel, LoginViewModel>()
        .AddScoped<IRegisterViewModel, RegisterViewModel>()
        .AddScoped<IForgotPasswordViewModel, ForgotPasswordViewModel>()
        .AddScoped<IResetPasswordViewModel, ResetPasswordViewModel>();

      services
        .AddScoped<ILoginViewModelValidator, LoginValidator>()
        .AddScoped<IRegisterViewModelValidator, RegisterValidator>()
        .AddScoped<IForgotPasswordViewModelValidator, ForgotPasswordValidator>()
        .AddScoped<IResetPasswordViewModelValidator, ResetPasswordValidator>();

      return services;
    }
  }
}