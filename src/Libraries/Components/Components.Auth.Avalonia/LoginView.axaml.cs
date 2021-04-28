using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ViewModels.Interfaces.Auth.ViewModels;

namespace Components.Auth.Avalonia
{
  public class LoginView
    : UserControl, ILoginView
  {
    /// <inheritdoc />
    public ILoginViewModel ViewModel { get; }

    public LoginView()
    {
      InitializeComponent();
      DataContext = ViewModel;
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
