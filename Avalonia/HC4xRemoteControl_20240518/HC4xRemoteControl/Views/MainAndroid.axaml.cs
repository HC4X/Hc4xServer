using System;
using Avalonia.Controls;
using HyperCube.RemoteControl;

namespace HC4xRemoteControl.Views {
  public partial class MainAndroid : UserControl {
    #region Axis
    private LandLifetime ndLifeTime => AxisMundi.ndLifetime;
    private Evaluator ndEvaluator => ndLifeTime.ndEvaluator;
    #endregion
    #region Method
    #endregion
    #region Constructor
    public bool Init() {
      bool retValue = false;
      try {
        if (ndLifeTime.AndroidStckPnl())
          if (ndLifeTime.InitEvaluator())
            retValue = ndEvaluator.LoadStartPage();
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public MainAndroid() => InitializeComponent();
    #endregion
  }
}
