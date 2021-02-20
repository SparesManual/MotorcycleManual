using Autofac;
using Components.Auth.WPF.Pages;
using Db.Interfaces;
using MRI.Auth;
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
      NavigationManager.Register<LoginPage>("login");
      NavigationManager.Register<RegisterPage>("register");

      var builder = new ContainerBuilder();
      builder.RegisterType<APIAuth>().As<IAPIAuth>();
      builder.RegisterType<LoginValidator>().As<ILoginViewModelValidator>();
      builder.RegisterType<RegisterValidator>().As<IRegisterViewModelValidator>();
      builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
      builder.RegisterType<RegisterViewModel>().As<IRegisterViewModel>();

      var container = builder.Build();
      ViewModelResolver.Initialize(container);
    }
  }
}
