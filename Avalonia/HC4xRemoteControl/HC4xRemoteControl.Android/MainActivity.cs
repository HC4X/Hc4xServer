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
using HyperCube.RemoteControl;
using System;
using System.Linq;

namespace HC4xRemoteControl.Android
{
  [Activity(
    Label = "HC4x Remote Control",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
  public class MainActivity : AvaloniaMainActivity<App>
  {
    #region Axis
    internal CameraManager ndCameraManager => (CameraManager)GetSystemService(CameraService);
    #endregion
    #region Attribute
    internal int atResMainLayout => Resource.Layout.Main;
    internal int atResSurfaceView => Resource.Id.surfaceView1;
    internal int atResButton => Resource.Id.button1;
    #endregion
    #region Node
    private AndroidLifetime ndLifetime => (AndroidLifetime)AxisMundi.ndLifetime;
    public TextureView ndTextureView { get; private set; }
    #endregion
    #region Event
    private void ObjButton_Click(object? sender, EventArgs e) => ndLifetime.QrCodeDecode();
    internal void EnableCapture()
    {
      Button objButton;
      objButton = FindViewById<Button>(Resource.Id.button1);
      objButton.Click += ObjButton_Click;
    }
    internal bool LoadTextureView()
    {
      ndTextureView = (TextureView)FindViewById(atResSurfaceView);
      return (true);
    }
    #endregion
    #region Permission
    private void CheckAndRequestPermissions()
    {
      string[] permissionsNeeded = {
        Manifest.Permission.Camera,
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
    protected override void OnCreate(Bundle savedInstanceState)
    {
      AxisMundi.CreateLifetime(new AndroidLifetime());
      base.OnCreate(savedInstanceState);
      CheckAndRequestPermissions();
      ndLifetime.SetActivity(this);
    }
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
      return base.CustomizeAppBuilder(builder)
          .WithInterFont()
          .UseReactiveUI();
    }
    #endregion
    #region Constant
    private const int REQUEST_CAMERA_PERMISSION = 1;
    private const int REQUEST_PERMISSIONS_CODE = 1;
    #endregion
  }
}