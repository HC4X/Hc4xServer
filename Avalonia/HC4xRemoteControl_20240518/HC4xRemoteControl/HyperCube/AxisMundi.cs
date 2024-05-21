using System;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace HyperCube.RemoteControl {
  public static class AxisMundi {
    private const string Name = nameof(AxisMundi);
    #region Node
    public static LandLifetime ndLifetime { get; private set; }
    public static HyperApplication scHyperCubeApp => ndLifetime.scHyperCubeApp;
    #endregion
    #region Error
    public static void ShowException(Exception parErr, string parClass, string parMethod) { ShowException(parErr, parClass, parMethod, ""); }
    public static void ShowException(Exception parErr, string parClass, string parMethod, string parExtraInfo) {
      IMsBox<ButtonResult> objMsgBox = MessageBoxManager.GetMessageBoxStandard(
            new MessageBoxStandardParams {
              ButtonDefinitions = ButtonEnum.Ok,
              ContentTitle = $"{parClass}.{parMethod}",
              ContentMessage = parErr.Message,
              Icon = Icon.Error,
              WindowStartupLocation = WindowStartupLocation.CenterOwner,
              CanResize = false,
              MaxWidth = 800,
              MaxHeight = 600,
              SizeToContent = SizeToContent.WidthAndHeight,
              ShowInCenter = true,
              Topmost = false
            });
      if (ndLifetime != null)
        ndLifetime.ErrMsgBox(objMsgBox);
      else
        objMsgBox.ShowAsync();
    }
    #endregion
    #region Constructor
    public static bool Init() => ndLifetime.Init();
    public static bool CreateLifetime(LandLifetime parLifetime) { ndLifetime = parLifetime; return (true); }
    #endregion
  }
}
