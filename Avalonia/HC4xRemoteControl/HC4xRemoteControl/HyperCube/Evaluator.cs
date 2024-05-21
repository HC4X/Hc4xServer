using System;
using Avalonia;
using Avalonia.Controls;
using LibModel;

namespace HyperCube.RemoteControl {
  internal enum EnumSocketStatus {
    None,
    HelloReceived,
    OKReceived
  }
  public class Evaluator {
    private const string Name = nameof(Evaluator);
    #region Axis
    private LandLifetime ndLifeTime => AxisMundi.ndLifetime;
    private HyperApplication scHyperCubeApp => AxisMundi.scHyperCubeApp;
    private RemoteDoc scRemote => scHyperCubeApp.scRemote;
    #endregion
    #region Node
    private RawScktClient ndClient { get; set; }
    #endregion
    #region Framework
    private StackPanel fwStackPanel => ndLifeTime.GetViewerContent<StackPanel>();
    #endregion
    #region Action
    private string ClientSocketStatus(string parMessage, hc4x_SocketStatus parStatus) {
      string retValue = "";
      try {
        switch (EvalMessage(parMessage)) {
          case EnumSocketStatus.HelloReceived:
            ndLifeTime.LoadContent("ControlPage.xml");
            break;
          default:
            break;
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(ClientSocketStatus)); }
      return (retValue);
    }
    #endregion
    #region Method
    internal bool SendStr(string parMessage) {
      bool retValue = false;
      try {
        ndClient.SendStrAsync(parMessage);
        retValue = true;
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendStr)); }
      return (retValue);
    }
    internal bool SendCommand(string parName) {
      bool retValue = false;
      try {
        parName = scRemote.GetCommandById(parName);
        if (!string.IsNullOrWhiteSpace(parName)) retValue = SendStr(parName);
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendCommand)); }
      return (retValue);
    }
    internal bool SendHello(string parServer, string parPort, string parMessage) {
      bool retValue = false;
      int iPort;
      try {
        if (string.IsNullOrWhiteSpace(parServer)) throw new Exception("Empty Server Address");
        if ((iPort = int.Parse(parPort)) == 0) throw new Exception("Invalid Port Number");
        if (!ndClient.atIsConnected) ndClient.Init(parServer, iPort);
        retValue = SendStr(parMessage);
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendHello)); }
      return (retValue);
    }
    internal EnumSocketStatus EvalMessage(string parMessage) {
      EnumSocketStatus retValue = EnumSocketStatus.None;
      try {
        if (parMessage.Equals("Hello Client"))
          retValue = EnumSocketStatus.HelloReceived;
        else if (parMessage.Equals("<OK>"))
          retValue = EnumSocketStatus.OKReceived;
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(EvalMessage)); }
      return (retValue);
    }
    internal bool EvalPressed(StyledElement parElement) {
      bool retValue = false;
      string strIp, strPort;
      RawKeyValue objKeyValue;
      try {
        switch (parElement.Name) {
          case "cmdConnect":
            objKeyValue = GearControl.PanelKeyValue(fwStackPanel, "txtIp, txtPort");
            if (SendHello(strIp = objKeyValue.ValueStr("txtIp"), strPort = objKeyValue.ValueStr("txtPort"), scRemote.atHelloServer)) {
              scRemote.atServerIp = strIp;
              scRemote.atServerPort = strPort;
              retValue = ndLifeTime.AppMessage("Connecting...");
            }
            break;
          case "cmdOpenCamera":
            retValue = ndLifeTime.OpenCamera();
            break;
          case "cmdExit":
            retValue = LoadStartPage();
            break;
          default:
            retValue = SendCommand(parElement.Name);
            break;
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(EvalPressed)); }
      return (retValue);
    }
    internal bool LoadStartPage() {
      bool retValue = false;
      try {
        if (!ndLifeTime.LoadContent("StartPage.xml")) return (retValue);
        fwStackPanel.GetControl<TextBox>("txtIp").Text = scRemote.atServerIp;
        fwStackPanel.GetControl<TextBox>("txtPort").Text = scRemote.atServerPort;
        retValue = true;
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(LoadStartPage)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public Evaluator() => ndClient = new RawScktClient(ClientSocketStatus);
    internal void Close() {
      ndClient?.Close();
      ndClient = default;
    }
    #endregion
  }
}
