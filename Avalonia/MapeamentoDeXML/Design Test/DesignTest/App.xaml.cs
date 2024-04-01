using System;
using System.Windows;

namespace DesignTest {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application { }
  public static class AxisMundi {
    private const string Name = nameof(AxisMundi);
    #region Node
    public static hc4x_SectorData ndSectorData { get; private set; }
    #endregion
    #region Method
    public static bool Init(string parDatabase) {
      bool retValue = false;
      try {
        if (ndSectorData == null) {
          ndSectorData = new hc4x_SectorData(parDatabase);
          }
        retValue = true;
        }
      catch (Exception Err) { throw; }
      return (retValue);
      }
    public static void ShowException(Exception parErr, string parClass, string parMethod) { ShowException(parErr, parClass, parMethod, ""); }
    public static void ShowException(Exception parErr, string parClass, string parMethod, string parExtraInfo) { MessageBox.Show(parErr.Message, parClass + "." + parMethod, MessageBoxButton.OK, MessageBoxImage.Error); }
    #endregion
    }
  }
