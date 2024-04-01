using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using HyperCubeRemoteControlDesktop.ViewModels;
using HyperCubeRemoteControlDesktop.Views;
using LibModel;
using RemoteControl.XmlMapper;
using SocketClient;
using System;
using System.IO;

namespace HyperCubeRemoteControlDesktop;

public partial class App : Application
{
  #region Node
  private RemoteDoc scRemoteDoc { get; set; }
  #endregion
  private bool fileExists = false;
  public override void Initialize()
  {
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    string strFileName = GearPath.Combine(baseDirectory, "hypercube\\remote.xml");
    var fullPath = Path.GetFullPath(strFileName);

    try
    {
      fileExists = GearPath.FileExists(fullPath);
      if (fileExists)
      {
        scRemoteDoc = new RemoteDoc(fullPath);
        if (scRemoteDoc != null)
        {
          AvaloniaXamlLoader.Load(this);
        }
      }
    }
    catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Initialize)); }
  }

  public override void OnFrameworkInitializationCompleted()
  {
    // Line below is needed to remove Avalonia data validation.
    // Without this line you will get duplicate validations from both Avalonia and CT
    BindingPlugins.DataValidators.RemoveAt(0);

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      desktop.MainWindow = new MainWindow
      {
        DataContext = new MainViewModel()
      };
    }
    else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
    {
      singleViewPlatform.MainView = new MainView
      {
        DataContext = new MainViewModel()
      };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
