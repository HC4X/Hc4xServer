using System;
using Android.Graphics;
using Android.Views;

namespace Android.HyperCube {
  internal static class GearAndroid {
    private const string Name = nameof(GearAndroid);
    #region Method
    internal static byte[] ParseJPEG(TextureView parTextureView) {
      byte[] retValue;
      int width, height;
      int[] arPixel;
      Bitmap objBitmap;
      try {
        if (parTextureView == null) return (default);
        objBitmap = parTextureView.Bitmap;
        if(objBitmap == null) return (default);
        width = objBitmap.Width;
        height = objBitmap.Height;
        arPixel = new int[width * height];
        objBitmap.GetPixels(arPixel, 0, width, 0, 0, width, height);
        retValue = new byte[arPixel.Length * 3];
        for (int i = 0; i < arPixel.Length; i++) {
          int pixel = arPixel[i];
          retValue[i * 3] = (byte)((pixel >> 16) & 0xFF);// Red
          retValue[i * 3 + 1] = (byte)((pixel >> 8) & 0xFF);// Green
          retValue[i * 3 + 2] = (byte)(pixel & 0xFF); // Blue
        }
      }
      catch (Exception) { throw; }
      return (retValue);
    }
    #endregion
  }
}
