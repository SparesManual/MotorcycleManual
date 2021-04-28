using Avalonia;
using Avalonia.ReactiveUI;

namespace MRI.Dealer
{
  public static class Program
  {
    public static void Main(string[] args)
      => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    private static AppBuilder BuildAvaloniaApp()
      => AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .LogToTrace()
        .UseReactiveUI();
  }
}