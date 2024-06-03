using System;
using Android.Views;
using HC4xRC.AndroidPlatform;
using HyperCube.Platform;

namespace Android.HyperCube {
  public class CameraControl {
    private const string Name = nameof(CameraControl);
    #region Framework
    public TextureView fwTextureView { get; set; }
    #endregion
    #region Axis
    internal AndroidLifetime ndLifetime => (AndroidLifetime)AxisMundi.ndLifetime;
    private Evaluator ndEvaluator => ndLifetime.ndEvaluator;
    private MainActivity ndMainActivity => ndLifetime.ndMainActivity;
    #endregion
    #region Attribute
    public string atQRCode { get; private set; }
    #endregion
    #region Node
    internal RawCamera2 ndCamera2 { get; private set; }
    private QRDecoderTimer ndQRDecoder { get; set; }
    #endregion
    #region QRCode
    public bool QrCodeDecode() {
      bool retValue = false;
      byte[] arByte;
      try {
        if (fwTextureView != null) {
          arByte = ndLifetime.ScreenShot(fwTextureView);
          if (arByte != null)
            atQRCode = GearQRCode.Decode(arByte, fwTextureView.Width, fwTextureView.Height);
        }
        if (!string.IsNullOrWhiteSpace(atQRCode)) {
          ndEvaluator.QRCodeString(atQRCode);
          Close();
        }
        retValue = true;
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(QrCodeDecode)); }
      return retValue;
    }
    #endregion
    #region Method
    internal bool EnableCapture() {
      bool retValue = false;
      try {
        ndQRDecoder = new QRDecoderTimer(this, 2500, 10);
        ndQRDecoder.Start();
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(EnableCapture)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    internal bool Init() {
      bool retValue = false;
      try {
        fwTextureView = ndMainActivity.fwCamTexture;
        ndCamera2 = new RawCamera2(ndMainActivity.fwCameraManager);
        if (ndCamera2.Init(this))
          retValue = ndCamera2.GetBackCamera();
      }
      catch (Exception Err) { ndLifetime.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    internal void Close() {
      ndQRDecoder?.Close();
      ndCamera2?.Close();
      ndMainActivity.EndCamera();
      ndQRDecoder = default;
      ndCamera2 = default;
    }
    #endregion
  }
}

