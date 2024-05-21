using System;
using System.Windows;
using System.Windows.Input;
using Interop.SocketLib;

namespace SocketClient {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    #region Node
    private Evaluator ndEvaluator { get; set; }
    #endregion
    #region Action
    private string ClientMessage(string parMessage, hc4x_SocketStatus parStatus) {
      string retValue = "";
      try {
        switch (ndEvaluator.EvalMessage(parMessage)) {
          case EnumSocketStatus.HelloReceived:
            PreviousPage.IsEnabled = true;
            NextPage.IsEnabled = true;
            break;
          default:
            break;
        }
        TxtClientLog.Text += parMessage + "\n";
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(ClientMessage)); }
      return (retValue);
    }
    #endregion
    #region Event
    private void Cmd_Central(object sender, MouseButtonEventArgs e) {
      FrameworkElement objElement;
      try {
        objElement = sender as FrameworkElement;
        switch (objElement.Name) {
          case "SendHello":
            ndEvaluator.SendHello(txtSendTo.Text, txtMessage.Text);
            break;
          default:
            ndEvaluator.SendStr(objElement.Name);
            break;
        }
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Cmd_Central)); }
    }
    #endregion
    #region Constructor
    public MainWindow() {
      InitializeComponent();
      ndEvaluator = new Evaluator(ClientMessage);
    }
    #endregion
  }
}
