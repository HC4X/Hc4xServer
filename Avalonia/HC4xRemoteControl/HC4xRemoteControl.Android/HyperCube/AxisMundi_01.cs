using Android.Hardware.Camera2;
using Android.Views;
using Avalonia.Controls;
using HC4xRemoteControl.Android;
using HC4xRemoteControl.Views;
using HyperCube.RemoteControl;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using System;

namespace Android.HyperCube
{
  public class AndroidLifetime : LandLifetime
  {
    private const string Name = nameof(AndroidLifetime);
    #region Node
    public new MainAndroid fwMainWindow => base.fwMainWindow as MainAndroid;
    public MainActivity ndMainActivity { get; private set; }
    private TextureView ndTextureView => ndMainActivity.ndTextureView;
    private CameraManager ndCameraManager => ndMainActivity.ndCameraManager;
    private RawCamera2 ndCamera2 { get; set; }
    #endregion
    #region Method
    public override string QrCodeDecode() => GearQRCode.Decode(ScreenShot(), ndTextureView.Width, ndTextureView.Height);
    public override byte[] ScreenShot() => GearAndroid.ParseJPEG(ndTextureView);
    public override bool OpenCamera()
    {
      bool retValue = false;
      try
      {
        ndCamera2 = new RawCamera2(ndCameraManager);
        ndCamera2.GetCameraId();
        ndCamera2.GetBackCamera();
        retValue = true;
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(OpenCamera)); }
      return (retValue);
    }
    internal void SetActivity(MainActivity parMainActivity) => ndMainActivity = parMainActivity;
    #endregion
    #region Method
    public override bool AndroidStckPnl() => MainStckPnl(fwMainWindow.Content as StackPanel, "hc4x_viewer", "lblMessage");
    public override bool LoadContent(string parFileName)
    {
      bool retValue = false;
      UserControl objUsrCtrl;
      try
      {
        objUsrCtrl = (UserControl)LoadControl(parFileName);
        if (objUsrCtrl == null) return (retValue);
        if (fwMainWindow.Content == null)
        {
          fwMainWindow.Content = objUsrCtrl.Content;
          retValue = MainStckPnl((fwMainWindow.Content as StackPanel), "hc4x_viewer", "lblMessage");
        }
        else
        {
          // fwStackPanel.FindControl<Image>("MyImage").Source = new Avalonia.Media.Imaging.Bitmap("/data/user/0/com.HyperCube.RemoteControl/cache/myfile.jpeg");
          SetViewerContent(objUsrCtrl);
          retValue = AppMessage("Ready");
        }
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(LoadContent)); }
      return (retValue);
    }
    public override bool ErrMsgBox(IMsBox<ButtonResult> objMsgBox)
    {
      objMsgBox.ShowAsPopupAsync(fwMainWindow);
      return (base.ErrMsgBox(objMsgBox));
    }
    #endregion
    #region Constructor
    public override bool Init()
    {
      bool retValue = false;
      try
      {
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
