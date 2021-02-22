using MRI.MVVM.WPF.Helpers;

namespace Dealer.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public MainWindow()
    {
      NavigationManager.NavigationChanged += OnNavigationChanged;
      InitializeComponent();

      NavigationManager.NavigateTo("login");
    }

    private void OnNavigationChanged(object? sender, string name)
      => MainContent.Content = NavigationManager.ResolveView(name);
  }
}
