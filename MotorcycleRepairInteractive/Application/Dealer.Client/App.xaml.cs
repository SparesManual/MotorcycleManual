using Autofac;
using Components.Auth;
using Components.Auth.WPF.Pages;
using Db.Interfaces;
using MRI.Auth;
using MRI.MVVM.Interfaces;
using MRI.MVVM.WPF.Helpers;
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
      builder.RegisterType<APIAuth>().As<IAPIAuth>();

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
      // NavigationManager.Register<IRegisterView>("register");

      var container = builder.Build();
      TypeResolver.Initialize(container);
    }
  }
}
