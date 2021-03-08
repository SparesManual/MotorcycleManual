using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth.WPF.Pages
{
  /// <summary>
  /// Interaction logic for ForgotPasswordPage.xaml
  /// </summary>
  public partial class ForgotPasswordPage
    : IForgotPasswordView
  {
    /// <inheritdoc />
    public IForgotPasswordViewModel ViewModel { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public ForgotPasswordPage(IForgotPasswordViewModel viewModel)
    {
      ViewModel = viewModel;
      DataContext = ViewModel;

      InitializeComponent();
    }
  }
}