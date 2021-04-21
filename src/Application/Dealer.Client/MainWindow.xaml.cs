using MRI.MVVM.Interfaces;
using MRI.MVVM.WPF.Helpers;

namespace Dealer.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    private readonly NavigationManager m_navigator;

    /// <summary>
    /// Default constructor
    /// </summary>
    public MainWindow()
    {
      m_navigator = (NavigationManager)TypeResolver.Resolve<INavigator>();
      m_navigator.NavigationChanged += OnNavigationChanged;
      InitializeComponent();

      m_navigator.NavigateTo("/login");
    }

    private void OnNavigationChanged(object? sender, INavigator.ViewData data)
      => MainContent.Content = m_navigator.ResolveView(data.Name);
  }
}
