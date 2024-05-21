using System;
using Avalonia;
using Avalonia.ReactiveUI;
using HyperCube.Desktop;
using HyperCube.RemoteControl;

namespace HC4xRemoteControl.Desktop {
  class Program {
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {
      if (AxisMundi.CreateLifetime(new DesktopLifetime()))
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
  }
}