using System;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace HyperCube.Platform {
  public static class AxisMundi {
    private const string Name = nameof(AxisMundi);
    #region Node
    public static LandLifetime ndLifetime { get; private set; }
    public static HyperApplication scHyperCubeApp => ndLifetime.scHyperCubeApp;
    #endregion
    #region Error
    public static void ShowException(Exception parErr, string parClass, string parMethod) { ShowException(parErr, parClass, parMethod, ""); }
    /// <summary></summary>
    ///! Date: 25/05/2024
    public static void ShowException(Exception parErr, string parClass, string parMethod, string parExtraInfo) {
      IMsBox<ButtonResult> objMsgBox;
      MessageBoxStandardParams objMessageBox;
      try {
        objMessageBox = new MessageBoxStandardParams {
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
        };
        objMsgBox = MessageBoxManager.GetMessageBoxStandard(objMessageBox);
        if (ndLifetime != null) {
          ndLifetime.ErrMsgBox(objMsgBox);
        }
        else {
          objMessageBox.ContentMessage = $"{parClass}\n{parMethod}\n{parErr.Message}";
          objMsgBox.ShowAsync();
        }
      }
      catch (Exception Err) { }
    }
    #endregion
    #region Constructor
    public static bool Init() => ndLifetime.Init();
    public static bool CreateLifetime(LandLifetime parLifetime) { ndLifetime = parLifetime; return (true); }
    #endregion
  }
}
