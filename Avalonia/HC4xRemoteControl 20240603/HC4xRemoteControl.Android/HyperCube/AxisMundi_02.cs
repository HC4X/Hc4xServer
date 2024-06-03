using System;
using Avalonia.Threading;

namespace Android.HyperCube {
  internal class QRDecoderTimer {
    private const string Name = nameof(QRDecoderTimer);
    #region Axis
    private AndroidLifetime ndLifetime => fwCameraCtrl.ndLifetime;
    #endregion
    #region Framework
    private CameraControl fwCameraCtrl { get; set; }
    #endregion
    #region Attribute
    public string atResult { get; private set; }
    private int atCounter { get; set; }
    private int atMax { get; set; }
    #endregion
    #region Node
    private readonly DispatcherTimer ndtimer;
    #endregion
    #region Method
    public bool Start() {
      ndtimer?.Start();
      return true;
    }
    public bool Stop() {
      ndtimer?.Stop();
      return true;
    }
    #endregion
    #region Event
    private void Timer_Tick(object? sender, EventArgs e) {
      try {
        if (atCounter < atMax)
          fwCameraCtrl?.QrCodeDecode();
        else
          Stop();
        ++atCounter;
      }
      catch (Exception Err) { Stop(); ndLifetime?.ShowException(Err, Name, nameof(Timer_Tick)); }
    }
    #endregion
    #region Constructor
    public QRDecoderTimer(CameraControl parCameraCtrl, double parMilisecs, int parMax) {
      fwCameraCtrl = parCameraCtrl;
      ndtimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(parMilisecs) };
      atMax = parMax;
      atCounter = 0;
      ndtimer.Tick += Timer_Tick;
    }
    public void Close() {
      Stop();
      ndtimer.Tick -= Timer_Tick;
      fwCameraCtrl = default;
    }
    #endregion
  }
}
