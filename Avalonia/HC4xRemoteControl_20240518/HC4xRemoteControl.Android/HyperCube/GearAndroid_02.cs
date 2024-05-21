using System;
using System.Collections.Generic;
using ZXing;
using ZXing.Common;

namespace Android.HyperCube {
  internal class GearQRCode {
    internal static string Decode(byte[] parByte, int parWidth, int parHeight) {
      string retValue = "";
      BarcodeReaderGeneric ZXingReader;
      Result objResult;
      try {
        LuminanceSource source = new RGBLuminanceSource(parByte, parWidth, parHeight);
        ZXingReader = new BarcodeReaderGeneric() {
          AutoRotate = true,
          Options = new DecodingOptions {
            TryHarder = true,
            CharacterSet = "UTF-8",
            PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE },
            TryInverted = true
          }
        };
        objResult = ZXingReader.Decode(source);
        if (objResult != null) 
          retValue = objResult.Text;
      }
      catch (Exception Err) { throw Err; }
      return (retValue);
    }
  }
}
