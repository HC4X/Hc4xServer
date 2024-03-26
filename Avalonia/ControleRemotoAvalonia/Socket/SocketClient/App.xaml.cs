using System;
using System.Threading.Tasks;
using System.Windows;

namespace SocketClient {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application { }
  public static class AxisMundi {
    public static async Task<bool> WaitBool(bool parValue) { return (await Task.Run(() => parValue)); }
    public static void ShowException(Exception parErr, string parName, string parFunction) {
      MessageBox.Show(parErr.Message, string.Format("{0}.{1}", parName, parFunction), MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }
}
