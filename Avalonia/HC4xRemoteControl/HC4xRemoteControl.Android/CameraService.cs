using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using ZXing;

namespace HyperCube.RemoteControl {
  public class CameraService {
    #region Node
    private readonly Activity _activity;
    #endregion
    #region Method
    internal void CameraData(Intent parData) {
      Bitmap objBitmap = parData.Extras.Get("data") as Bitmap;
      if (objBitmap == null) return;


      int width = objBitmap.Width;
      int height = objBitmap.Height;
      int[] pixels = new int[width * height];
      objBitmap.GetPixels(pixels, 0, width, 0, 0, width, height);
      // Converte os pixels para o array de bytes necessário
      byte[] pixelsByte = new byte[pixels.Length * 3];
      for (int i = 0; i < pixels.Length; i++) {
        int pixel = pixels[i];
        pixelsByte[i * 3] = (byte)((pixel >> 16) & 0xFF); // Vermelho
        pixelsByte[i * 3 + 1] = (byte)((pixel >> 8) & 0xFF); // Verde
        pixelsByte[i * 3 + 2] = (byte)(pixel & 0xFF); // Azul

        // Cria um source de luminância compatível com ZXing a partir do array de bytes
        LuminanceSource source = new RGBLuminanceSource(pixelsByte, width, height);
        BarcodeReaderGeneric reader = new();
        var result = reader.Decode(source);
        if (result != null)
          AxisMundi.QrCodeDecoded(result.Text);
      }
    }
    public void OpenCamera() {
      Intent takePictureIntent = new Intent(MediaStore.ActionVideoCapture);
      // Verifica se existe uma activity de câmera para lidar com a intent
      if (takePictureIntent.ResolveActivity(_activity.PackageManager) != null) {
        _activity.StartActivityForResult(takePictureIntent, 1);
      }
    }
    #endregion
    #region Constructor
    public CameraService(Activity activity) {
      _activity = activity;
    }
    #endregion
  }
}
