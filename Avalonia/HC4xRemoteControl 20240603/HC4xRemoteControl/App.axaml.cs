using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HC4xRemoteControl.Views;
using HyperCube.Platform;

namespace HC4xRemoteControl {
  public partial class App : Application {
    #region Axis
    private LandLifetime ndLifetime => AxisMundi.ndLifetime;
    #endregion
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
    public override void OnFrameworkInitializationCompleted() {
      if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
        if (AxisMundi.Init()) desktop.MainWindow = (MainWindow)ndLifetime.fwMainWindow;
      }
      else if (ApplicationLifetime is ISingleViewApplicationLifetime objAndroid) {
        if (AxisMundi.Init()) objAndroid.MainView = ndLifetime.fwMainWindow;
      }
      base.OnFrameworkInitializationCompleted();
    }
  }
}