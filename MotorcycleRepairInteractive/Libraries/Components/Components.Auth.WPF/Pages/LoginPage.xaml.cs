using MRI.MVVM.Interfaces.Views;
using MRI.MVVM.WPF.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth.WPF.Pages
{
  /// <summary>
  /// Interaction logic for LoginPage.xaml
  /// </summary>
  public partial class LoginPage
    : IView<ILoginViewModel>
  {
    /// <inheritdoc />
    public ILoginViewModel ViewModel { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public LoginPage()
    {
      ViewModel = ViewModelResolver.Resolve<ILoginViewModel>();
      DataContext = ViewModel;

      InitializeComponent();
    }
  }
}
