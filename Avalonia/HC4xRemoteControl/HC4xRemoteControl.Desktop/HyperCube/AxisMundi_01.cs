using System;
using Avalonia.Controls;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using HC4xRemoteControl.Views;
using HyperCube.RemoteControl;

namespace HyperCube.Desktop {
  public class DesktopLifetime : LandLifetime {
    private const string Name = nameof(DesktopLifetime);
    #region Node
    public new MainWindow fwMainWindow => base.fwMainWindow as MainWindow;
    #endregion
    #region Method
    public override bool LoadContent(string parFileName) {
      bool retValue = false;
      UserControl objUsrCtrl;
      try {
        objUsrCtrl = (UserControl)LoadControl(parFileName);
        if (objUsrCtrl == null) return (retValue);
        if (fwMainWindow.Content == null) {
          fwMainWindow.Content = objUsrCtrl.Content;
          retValue = MainStckPnl((fwMainWindow.Content as StackPanel), "hc4x_viewer", "lblMessage");
        }
        else {
          SetViewerContent(objUsrCtrl);
          retValue = AppMessage("Ready");
        }
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(LoadContent)); }
      return (retValue);
    }
    public override bool ErrMsgBox(IMsBox<ButtonResult> objMsgBox) {
      objMsgBox.ShowWindowDialogAsync(fwMainWindow);
      return (base.ErrMsgBox(objMsgBox));
    }
    #endregion
    #region Constructor
    public override bool Init() {
      bool retValue = false;
      try {
#if DEBUG
        if (!InitFS()) return (retValue);
        //if (!InitRes()) return (retValue);
#else
        if (!InitRes()) return (retValue);
#endif
        base.fwMainWindow = new MainWindow();
        fwMainWindow.Show();
        if (fwMainWindow != null)
          if (fwMainWindow.Init()) retValue = base.Init();
      }
      catch (Exception Err) { retValue = default; ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public DesktopLifetime() { }
    #endregion
  }
}
