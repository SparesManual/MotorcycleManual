using System;
using System.Net.Http;
using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Components.Auth;
using Components.Auth.Avalonia;
using Db.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using MRI.Auth;
using MRI.Dealer.Views;
using MRI.MVVM.Interfaces;
using States.General;
using Validators.Auth;
using ViewModels.Auth;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace MRI.Dealer
{
  public class App
    : Application
  {
    public override void Initialize()
    {
      var builder = new ContainerBuilder();
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:5001")
      };

      builder.RegisterInstance(httpClient).SingleInstance();
      builder.RegisterType<ApiAuthenticationStateProvider>().As<AuthenticationStateProvider>().SingleInstance();
      builder.RegisterType<APIAuth>().As<IAPIAuth>();
      builder.RegisterType<AvaloniaStorage>().As<IStorage>();

      builder.RegisterType<LoginView>().As<ILoginView>();
      //builder.RegisterType<ForgotPasswordPage>().As<IForgotPasswordView>();

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

      AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
      if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        desktop.MainWindow = new MainWindow();

      base.OnFrameworkInitializationCompleted();
    }
  }
}