using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth.WPF.Pages
{
  /// <summary>
  /// Interaction logic for LoginPage.xaml
  /// </summary>
  public partial class LoginPage
    : ILoginView
  {
    /// <inheritdoc />
    public ILoginViewModel ViewModel { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public LoginPage(ILoginViewModel viewModel)
    {
      InitializeComponent();

      ViewModel = viewModel;
      DataContext = ViewModel;
    }
  }
}
