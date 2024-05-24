using Avalonia.Threading;
using System;

namespace Android.HyperCube
{
  internal class QRDecoderTimer
  {
    private const string Name = nameof(QRDecoderTimer);
    #region Axis
    private AndroidLifetime ndLifetime => fwCameraCtrl.ndLifetime;
    #endregion
    #region Framework
    private CameraControl fwCameraCtrl { get; set; }
    #endregion
    #region Attribute
    public string atResult { get; private set; }
    #endregion
    #region Node
    private readonly DispatcherTimer ndtimer;
    #endregion
    #region Method
    public bool Start()
    {
      ndtimer?.Start();
      return true;
    }
    public bool Stop()
    {
      ndtimer?.Stop();
      return true;
    }
    #endregion
    #region Event
    private void Timer_Tick(object? sender, EventArgs e)
    {
      try { fwCameraCtrl?.QrCodeDecode(); }
      catch (Exception Err) { Stop(); ndLifetime?.ShowException(Err, Name, nameof(Timer_Tick)); }
    }
    #endregion
    #region Constructor
    public QRDecoderTimer(CameraControl parCameraCtrl)
    {
      fwCameraCtrl = parCameraCtrl;
      ndtimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };
      ndtimer.Tick += Timer_Tick;
    }
    public void Close()
    {
      Stop();
      ndtimer.Tick -= Timer_Tick;
      fwCameraCtrl = default;
    }
    #endregion
  }
}
