using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Shapes;
using LibModel;

namespace HyperCube.Platform {
  public delegate string SocketStatus(string parMessage, hc4x_SocketStatus parStatus);
  public static class GearControl {
    private const string Name = nameof(GearControl);
    #region Method
    public static RawKeyValue PanelKeyValue(StackPanel parStackPanel, string parFieldList) {
      string[] arField;
      RawKeyValue retValue;
      try {
        retValue = new RawKeyValue();
        arField = GearAux.CommaSplit(parFieldList, true);
        foreach (string itField in arField) retValue.Add(itField, parStackPanel.GetControl<TextBox>(itField).Text);
      }
      catch (Exception Err) { retValue = default; AxisMundi.ShowException(Err, Name, nameof(PanelKeyValue)); }
      return (retValue);
    }
    public static StyledElement GetElement(Control parControl) {
      StyledElement retValue = null;
      if (parControl is ContentPresenter objPresenter)
        retValue = objPresenter.Parent;
      else if (parControl is AccessText objAccessText)
        retValue = objAccessText.Parent;
      else if(parControl is Path objPath) 
        retValue = objPath.Parent;
      else if (!string.IsNullOrWhiteSpace(parControl.Name))
        retValue = parControl;
      return (retValue);
    }
    #endregion
  }
  public static class GearXAML {
    private const string Name = nameof(GearXAML);
    #region Method
    public static Control LoadXamlControl(string parContent) {
      UserControl objUsrCtrl;
      Control retValue = default;
      try {
        if (!parContent.StartsWith("<UserControl")) parContent = c_usrctrl + parContent + c_usrctrlclose;
        objUsrCtrl = AvaloniaRuntimeXamlLoader.Parse<UserControl>(parContent);
        retValue = objUsrCtrl;
      }
      catch (Exception Err) { retValue = default; AxisMundi.ShowException(Err, Name, nameof(LoadXamlControl)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const string c_usrctrl = "<UserControl xmlns=\"https://github.com/avaloniaui\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">";
    private const string c_usrctrlclose = "</UserControl>";
    #endregion
  }
}
