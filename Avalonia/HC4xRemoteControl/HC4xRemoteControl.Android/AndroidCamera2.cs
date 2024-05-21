using System;
using System.Collections.Generic;
using Android.OS;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Runtime;
using Android.Util;
using Android.Views;
using HC4xRemoteControl.Android;
using HyperCube.RemoteControl;
using LibModel;

namespace Android.HyperCube {
  internal class CaptureSession_CB : CameraCaptureSession.StateCallback {
    private const string Name = nameof(CaptureSession_CB);
    #region Node
    private Handler objHandler { get; set; }
    private Camera_CB ndCamera_CB { get; set; }
    private MainActivity ndActivity => ndCamera_CB.ndActivity;
    #endregion
    #region CameraCaptureSession.StateCallback
    public override void OnConfigured(CameraCaptureSession parSession) => UpdatePreview(parSession);
    public override void OnConfigureFailed(CameraCaptureSession parSession) { }
    #endregion
    #region Method
    private void UpdatePreview(CameraCaptureSession parSession) {
      if (ndCamera_CB.fwCamera2 == null) return;
      parSession.SetRepeatingRequest(ndCamera_CB.Builder.Build(), null, objHandler);
      ndActivity.EnableCapture();
    }
    private void Startbackgroundthread() {
      HandlerThread Thread;
      Thread = new HandlerThread("Camera Background");
      Thread.Start();
      objHandler = new Handler(Thread.Looper);
    }
    #endregion
    #region Constructor
    public CaptureSession_CB(Camera_CB parCamera_CB) => ndCamera_CB = parCamera_CB;
    #endregion
  }
  internal class Camera_CB : CameraDevice.StateCallback {
    private const string Name = nameof(Camera_CB);
    #region Axis
    private RawCamera2 ndCamera2 { get; set; }
    internal MainActivity ndActivity => ndCamera2.ndMainActivity;
    private TextureView ndTextureView => ndActivity.ndTextureView;
    private AndroidLifetime ndLifetime => (AndroidLifetime)AxisMundi.ndLifetime;
    #endregion
    #region Framework
    public CaptureRequest.Builder Builder { get; private set; }
    public CameraDevice fwCamera2 { get; private set; }
    #endregion
    #region CameraDevice.StateCallback
    public override void OnDisconnected(CameraDevice parCamera) {
      parCamera.Close();
      fwCamera2 = default;
    }
    public override void OnError(CameraDevice parCamera, [GeneratedEnum] CameraError error) {
      OnDisconnected(parCamera);
      ndLifetime.ShowException(new Exception(), Name, nameof(OnError), error.ToString());
    }
    public override void OnOpened(CameraDevice parCamera) => CameraPreview(fwCamera2 = parCamera);
    #endregion
    #region Method
    private void CameraPreview(CameraDevice parCamera) {
      Size imgDim;
      SurfaceTexture objSurfaceTexture;
      Surface objSurface;
      List<Surface> outputs = new List<Surface>();
      try {
        imgDim = ndCamera2.ImageDimension(parCamera.Id);
        objSurfaceTexture = ndTextureView.SurfaceTexture;
        objSurfaceTexture.SetDefaultBufferSize(imgDim.Width, imgDim.Height);
        objSurface = new Surface(objSurfaceTexture);
        Builder = parCamera.CreateCaptureRequest(CameraTemplate.StillCapture);
        Builder.AddTarget(objSurface);
        outputs.Add(objSurface);
        parCamera.CreateCaptureSession(outputs, new CaptureSession_CB(this), null);
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(CameraPreview)); }
    }
    #endregion
    #region Constructor
    public bool Init() {
      bool retValue = false;
      try {
        ndActivity.SetContentView(ndActivity.atResMainLayout);
        retValue = ndActivity.LoadTextureView();
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public Camera_CB(RawCamera2 parCamera2) => ndCamera2 = parCamera2;
    #endregion
  }
  public class RawCamera2 {
    private const string Name = nameof(RawCamera2);
    #region Framework
    private CameraManager fwCamManager { get; set; }
    #endregion
    #region Axis
    internal AndroidLifetime ndLifetime => (AndroidLifetime)AxisMundi.ndLifetime;
    internal MainActivity ndMainActivity => ndLifetime.ndMainActivity;
    #endregion
    #region Node
    private Camera_CB ndCamera_CB { get; set; }
    #endregion
    #region Attribute
    public string atIdCamBack { get; private set; }
    public string atIdCamFront { get; private set; }
    #endregion
    #region Method
    public Size ImageDimension(string parCamId) {
      Size retValue;
      CameraCharacteristics objCharacteristics = GetCharacteristics(parCamId);
      StreamConfigurationMap map = (StreamConfigurationMap)objCharacteristics.Get(CameraCharacteristics.ScalerStreamConfigurationMap);
      retValue = map.GetOutputSizes(256)[0];
      return (retValue);
    }
    public bool GetBackCamera() => GetCameraById(atIdCamBack);
    public bool GetFrontCamera() => GetCameraById(atIdCamFront);
    public CameraCharacteristics GetCharacteristics(string parCamId) => fwCamManager.GetCameraCharacteristics(parCamId);
    private bool GetCameraById(string parCamId) {
      bool retValue = false;
      try {
        ndCamera_CB = new Camera_CB(this);
        ndCamera_CB.Init();
        fwCamManager.OpenCamera(parCamId, ndCamera_CB, null);
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(GetCameraById)); }
      return (retValue);
    }
    public bool GetCameraId() {
      bool retValue = false;
      string[] arCamera;
      LensFacing enFacing;
      try {
        arCamera = fwCamManager?.GetCameraIdList();
        foreach (string itCamera in arCamera) {
          CameraCharacteristics objCharacteristics = fwCamManager.GetCameraCharacteristics(itCamera);
          enFacing = GearBase.ParseEnum<LensFacing>(objCharacteristics.Get(CameraCharacteristics.LensFacing));
          switch (enFacing) {
            case LensFacing.Back:
              atIdCamBack = itCamera;
              break;
            case LensFacing.Front:
              atIdCamFront = itCamera;
              break;
          }
        }
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(GetCameraId)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public RawCamera2(CameraManager parCamManager) { fwCamManager = parCamManager; }
    public void Close() => fwCamManager = default;
    #endregion
  }
}