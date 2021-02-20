using MRI.MVVM.Interfaces.Views;
using MRI.MVVM.WPF.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth.WPF.Pages
{
  /// <summary>
  /// Interaction logic for RegisterPage.xaml
  /// </summary>
  public partial class RegisterPage
    : IView<IRegisterViewModel>
  {
    /// <inheritdoc />
    public IRegisterViewModel ViewModel { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public RegisterPage()
    {
      ViewModel = ViewModelResolver.Resolve<IRegisterViewModel>();
      DataContext = ViewModel;

      InitializeComponent();
    }
  }
}
