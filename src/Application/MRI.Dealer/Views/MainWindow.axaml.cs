using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using MRI.MVVM.Interfaces;

namespace MRI.Dealer.Views
{
  public class MainWindow
    : Window
  {
    private readonly NavigationManager m_navigator;
    private ContentPresenter m_mainContent;

    public MainWindow()
    {
      m_navigator = (NavigationManager)TypeResolver.Resolve<INavigator>();
      m_navigator.NavigationChanged += OnNavigationChanged;
      InitializeComponent();

      m_navigator.NavigateTo("/login");
#if DEBUG
      this.AttachDevTools();
#endif
    }

    private void OnNavigationChanged(object? sender, INavigator.ViewData data)
    {
      m_mainContent.Content = m_navigator.ResolveView(data.Name);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
      m_mainContent = this.FindControl<ContentPresenter>("MainContent");
    }
  }
}