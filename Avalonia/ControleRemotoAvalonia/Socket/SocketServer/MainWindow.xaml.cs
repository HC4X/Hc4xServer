using System;
using System.Windows;
using System.Windows.Input;
using Interop.SocketLib;

namespace SocketServer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    #region Node
    private RawScktServer ndServer { get; set; }
    #endregion
    #region Action
    private string ServerMessage(string parMessage, hc4x_SocketStatus parStatus) {
      string retValue = "";
      switch (parStatus) {
        case hc4x_SocketStatus.Received:
          retValue = txtAnswer.Text;
          break;
        case hc4x_SocketStatus.None:
        case hc4x_SocketStatus.Err:
        case hc4x_SocketStatus.Log:
          break;
      }
      TxtServerLog.Text += parMessage + "\n";
      return (retValue);
    }
    #endregion
    #region Event
    private void Cmd_Central(object sender, MouseButtonEventArgs e) {
      string[] arSend;
      FrameworkElement objElement;
      try {
        objElement = sender as FrameworkElement;
        switch (objElement.Name) {
          case "CmdStartServer":
            arSend = txtServer.Text.Split(':');
            ndServer.Init(arSend[0], int.Parse(arSend[1]));
            break;
          case "CmdStopServer":
            ndServer.Stop();
            break;
          case "SendAnswer":
            ndServer.Answer(txtAnswer.Text);
            break;
        }
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Cmd_Central)); }
    }
    #endregion
    #region Constructor
    public MainWindow() {
      InitializeComponent();
      //# ndServer = new RawTCPServer(ServerMessage);
      //# ndClient = new RawTCPClient(ClientMessage);
      ndServer = new RawScktServer(ServerMessage);
      //# ndClient = new RawScktClient(ClientMessage);
      //# SocketTest.ExecuteServer();
    }
    #endregion
  }
}
