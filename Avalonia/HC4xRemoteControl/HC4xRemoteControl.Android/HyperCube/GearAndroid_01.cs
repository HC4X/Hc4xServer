using Android.Graphics;
using Android.Views;
using System;

namespace Android.HyperCube
{
  internal static class GearAndroid
  {
    private const string Name = nameof(GearAndroid);
    #region Method
    internal static byte[] ParseJPEG(TextureView parTextureView)
    {
      byte[] retValue;
      Bitmap objBitmap;
      try
      {
        if (parTextureView == null) return (default);
        objBitmap = parTextureView.Bitmap;
        int width = objBitmap.Width;
        int height = objBitmap.Height;
        int[] pixels = new int[width * height];
        objBitmap.GetPixels(pixels, 0, width, 0, 0, width, height);

        byte[] pixelsByte = new byte[pixels.Length * 3];
        for (int i = 0; i < pixels.Length; i++)
        {
          int pixel = pixels[i];
          pixelsByte[i * 3] = (byte)((pixel >> 16) & 0xFF);
          pixelsByte[i * 3 + 1] = (byte)((pixel >> 8) & 0xFF);
          pixelsByte[i * 3 + 2] = (byte)(pixel & 0xFF);
        }
        retValue = pixelsByte;
      }
      catch (Exception) { throw; }
      return (retValue);
    }
    #endregion
  }
}
