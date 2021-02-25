using System;
using System.Net.Http;
using Autofac;
using Components.Auth;
using Components.Auth.WPF.Pages;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using MRI.Auth;
using MRI.MVVM.Interfaces;
using MRI.MVVM.WPF.Helpers;
using States.General;
using Validators.Auth;
using ViewModels.Auth;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Dealer.Client
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    public App()
    {
      var builder = new ContainerBuilder();
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:5001")
      };

      builder.RegisterInstance(httpClient).SingleInstance();
      builder.RegisterType<ApiAuthenticationStateProvider>().As<AuthenticationStateProvider>().SingleInstance();
      builder.RegisterType<APIRESTAuth>().As<IAPIAuth>();
      builder.RegisterType<WPFStorage>().As<IStorage>();

      builder.RegisterType<LoginPage>().As<ILoginView>();
      builder.RegisterType<ForgotPasswordPage>().As<IForgotPasswordView>();

      builder.RegisterType<LoginValidator>().As<ILoginViewModelValidator>();
      builder.RegisterType<RegisterValidator>().As<IRegisterViewModelValidator>();
      builder.RegisterType<ForgotPasswordValidator>().As<IForgotPasswordViewModelValidator>();

      builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
      builder.RegisterType<RegisterViewModel>().As<IRegisterViewModel>();
      builder.RegisterType<ForgotPasswordViewModel>().As<IForgotPasswordViewModel>();

      var navigator = new NavigationManager();
      builder.RegisterInstance(navigator).As<INavigator>().SingleInstance();

      navigator.Register<ILoginView>("/login");
      navigator.Register<IForgotPasswordView>("/forgotPassword");

      // TODO: Add more views

      // NavigationManager.Register<IRegisterView>("register");

      var container = builder.Build();
      TypeResolver.Initialize(container);
    }
  }
}
