using Android;
using Android.App;
using Android.Content.PM;
using Android.Hardware.Camera2;
using Android.HyperCube;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using HC4xRemoteControl;
using HyperCube.Platform;
using System;
using System.Linq;

namespace HC4xRC.AndroidPlatform
{
  [Activity(
    Label = "HC4x Control",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
  public class MainActivity : AvaloniaMainActivity<App>
  {
    private const string Name = nameof(CameraControl);
    #region Axis
    private AndroidLifetime ndLifetime => (AndroidLifetime)AxisMundi.ndLifetime;
    #endregion
    #region Framework
    public CameraControl fwCameraCtrl { get; private set; }
    public TextureView fwCamTexture { get; private set; }
    public CameraManager fwCameraManager => (CameraManager)GetSystemService(CameraService);
    private View fwMainView { get; set; }
    #endregion
    #region Method
    internal bool EndCamera()
    {
      bool retValue = false;
      ViewGroup objParent;
      try
      {
        objParent = fwMainView.Parent as ViewGroup;
        objParent?.RemoveView(fwMainView);
        fwMainView.Dispose();
        retValue = true;
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(EndCamera)); }
      return (retValue);
    }
    internal bool InitCameraCtrl()
    {
      bool retValue = false;
      Button objButton;
      LayoutInflater objInflater;
      try
      {
        objInflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
        fwMainView = objInflater.Inflate(Resource.Layout.Main, null);
        fwCamTexture = fwMainView.FindViewById<TextureView>(Resource.Id.cam_view);
        objButton = fwMainView.FindViewById<Button>(Resource.Id.btn_cancel);
        objButton.Click += btnCancel_Click;
        AddContentView(fwMainView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
        fwCameraCtrl = new CameraControl();
        fwCameraCtrl.Init();
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(InitCameraCtrl)); }
      return (retValue);
    }
    #endregion
    #region Event
    public override void OnBackPressed()
    {
      if (fwMainView != null)
      {
        ndLifetime.AppMessage("Ready");
        CancelCamera();
      }
      else
        base.OnBackPressed();
    }
    private void btnCancel_Click(object? sender, EventArgs e)
    {
      ndLifetime.AppMessage("Ready");
      CancelCamera();
    }
    private void CancelCamera()
    {
      fwCameraCtrl?.Close();
      fwCameraCtrl = default;
      fwMainView = default;
      fwCamTexture = default;
    }
    #endregion
    #region Permission
    /// <summary></summary>
    ///! Date: 25/05/2024
    private void CheckAndRequestPermissions()
    {
      string[] permissionsNeeded = {
        Manifest.Permission.Camera,
        Manifest.Permission.AccessNetworkState,
        Manifest.Permission.WriteExternalStorage
    };
      var permissionsToRequest = permissionsNeeded.Where(permission => ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted).ToList();
      if (permissionsToRequest.Any())
      {
        ActivityCompat.RequestPermissions(this, permissionsToRequest.ToArray(), REQUEST_PERMISSIONS_CODE);
      }
    }
    #endregion
    #region Constructor
    /// <summary></summary>
    ///! Date: 25/05/2024
    protected override void OnCreate(Bundle savedInstanceState)
    {
      try
      {
        AxisMundi.CreateLifetime(new AndroidLifetime());
        base.OnCreate(savedInstanceState);
        CheckAndRequestPermissions();
        ndLifetime.SetActivity(this);
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(OnCreate)); }
    }
    /// <summary></summary>
    ///! Date: 25/05/2024
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
      AppBuilder retValue;
      try { retValue = base.CustomizeAppBuilder(builder).WithInterFont().UseReactiveUI(); }
      catch (Exception Err) { retValue = null; ndLifetime.ShowException(Err, Name, nameof(CustomizeAppBuilder)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const int REQUEST_PERMISSIONS_CODE = 1;
    #endregion
  }
}