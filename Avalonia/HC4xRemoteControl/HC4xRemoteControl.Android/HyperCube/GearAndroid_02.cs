using System;
using ZXing;

namespace Android.HyperCube
{
  internal class GearQRCode
  {
    internal static string Decode(byte[] parByte, int parWidth, int parHeight)
    {
      string retValue = "";
      BarcodeReaderGeneric reader;
      Result objResult;
      try
      {
        //parByte = LibModel.GearPath.OpenBinary("/data/user/0/com.HyperCube.RemoteControl/cache/myfile.jpeg");

        //Match: BGR32, BGRA32, RGB24, Unknown, UYVY, YUYV
        LuminanceSource source = new RGBLuminanceSource(parByte, parWidth, parHeight);
        reader = new BarcodeReaderGeneric();
        objResult = reader.Decode(source); //ARGB32, BGR24, RBGA32, RGB32, RGB565, RGBA32
        // objResult = reader.Decode(source); // RGBLuminanceSource.BitmapFormat.RGB32);
        if (objResult != null) retValue = objResult.Text;
      }
      catch (Exception Err) { throw Err; }
      return (retValue);
    }
  }
}
