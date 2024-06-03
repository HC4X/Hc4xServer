using System;
using Android.Views;
using Avalonia.Controls;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using HyperCube.Platform;
using HC4xRemoteControl.Views;
using HC4xRC.AndroidPlatform;

namespace Android.HyperCube {
  public class AndroidLifetime : LandLifetime {
    private const string Name = nameof(AndroidLifetime);
    #region Axis
    public new MainAndroid fwMainWindow => base.fwMainWindow as MainAndroid;
    public MainActivity ndMainActivity { get; private set; }
    #endregion
    #region Method
    public byte[] ScreenShot(TextureView parTextureView) => GearAndroid.ParseJPEG(parTextureView);
    public override bool OpenCamera() {
      bool retValue = false;
      try { retValue = ndMainActivity.InitCameraCtrl(); }
      catch (Exception Err) { ShowException(Err, Name, nameof(OpenCamera)); }
      return (retValue);
    }
    internal void SetActivity(MainActivity parMainActivity) => ndMainActivity = parMainActivity;
    #endregion
    #region Method
    public override bool AndroidStckPnl() => MainStckPnl(fwMainWindow.Content as StackPanel, "hc4x_viewer", "lblMessage");
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
      bool retValue = false;
      try {
        objMsgBox.ShowAsync();
        retValue = base.ErrMsgBox(objMsgBox);

      }
      catch (Exception) { }
      return (retValue);
    }
    #endregion
    #region Constructor
    public override bool Init() {
      bool retValue = false;
      try {
        if (!InitRes()) return (retValue);
        base.fwMainWindow = new MainAndroid();
        if (fwMainWindow != null)
          if (fwMainWindow.Init()) retValue = base.Init();
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public AndroidLifetime() { }
    #endregion
  }
}
