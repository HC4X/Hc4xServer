using Avalonia;
using Avalonia.Controls;
using LibModel;
using System;

namespace HyperCube.Platform
{
  public class Evaluator
  {
    private const string Name = nameof(Evaluator);
    #region Axis
    private LandLifetime ndLifeTime => AxisMundi.ndLifetime;
    private HyperApplication scHyperCubeApp => AxisMundi.scHyperCubeApp;
    private RemoteDoc scRemote => scHyperCubeApp.scRemote;
    private SectorSocket scSocket => scHyperCubeApp.scSocket;
    #endregion
    #region Node
    private RawScktClient ndClient { get; set; }
    #endregion
    #region Framework
    private StackPanel fwStackPanel => ndLifeTime.GetViewerContent<StackPanel>();
    #endregion
    #region Action
    private string ClientSocketStatus(string parMessage, hc4x_SocketStatus parStatus)
    {
      string retValue = "";
      JSonParser objJSON;
      RawXml objXml;
      try
      {
        switch (parStatus)
        {
          case hc4x_SocketStatus.Err:
            if (parMessage.StartsWith("{"))
            {
              objJSON = new JSonParser();
              objJSON.Init(parMessage);
              objXml = objJSON.ParseAttributes();
              parMessage = objXml.ValueStr("Message");
            }
            ndLifeTime.AppMessage(parMessage);
            break;
          default:
            EvalMessage(parMessage);
            break;
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(ClientSocketStatus)); }
      return (retValue);
    }
    #endregion
    #region Method
    public bool QRCodeString(string parResult)
    {
      bool retValue = false;
      string[] arSocket;
      try
      {
        if (string.IsNullOrWhiteSpace(parResult)) return (retValue);
        if (!parResult.StartsWith("hc4x://"))
        {
          ndLifeTime.AppMessage("Invalid QRCode:\n" + parResult);
        }
        else
        {
          arSocket = GearAux.Split(parResult.Replace("hc4x://", ""), "/");
          if (SocketParam(arSocket[0], arSocket[1], arSocket[2]))
            retValue = ConnectServer(arSocket[0], arSocket[1], arSocket[2]);
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(QRCodeString)); }
      return (retValue);
    }
    internal hc4x_ScktComm EvalMessage(string parMessage)
    {
      hc4x_ScktComm retValue = hc4x_ScktComm.None;
      SocketComm objScktComm;
      try
      {
        if (string.IsNullOrWhiteSpace(parMessage)) return (retValue);
        objScktComm = SocketComm.ParseComm(parMessage);
        switch (objScktComm.ScktComm())
        {
          case hc4x_ScktComm.SrvHello:
            scSocket.ndClient.SetSessionId(objScktComm.ValueStr("sessionid"));
            // SendFileRequest("ControlPage.xml");
            ndLifeTime.LoadContent("ControlPage.xml");
            break;
          case hc4x_ScktComm.SrvOk:
            ndLifeTime.AppMessage("Ready");
            break;
          case hc4x_ScktComm.SrvRfsed:
            throw new Exception("O servidor está conectado a outro controlador e recusou a conexão");
          default:
            break;
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(EvalMessage)); }
      return (retValue);
    }
    /// <summary></summary>
    ///! Date: 31/05/2024
    internal bool SendFileRequest(string parFileName)
    {
      bool retValue = false;
      try
      {
        parFileName = scSocket.ndClient.GetCliTextFile(parFileName);
        retValue = SendStr(parFileName);
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendFileRequest)); }
      return (retValue);
    }
    internal bool SendCommand(string parName)
    {
      bool retValue = false;
      try
      {
        parName = scRemote.GetCommandById(parName);
        if (!string.IsNullOrWhiteSpace(parName))
        {
          parName = scSocket.ndClient.GetCliCmmnd(parName);
          retValue = SendStr(parName);
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendCommand)); }
      return (retValue);
    }
    internal bool SendHello(string parServer, string parPort)
    {
      bool retValue = false;
      int iPort;
      string strMessage;
      try
      {
        if (string.IsNullOrWhiteSpace(parServer)) throw new Exception("Empty Server Address");
        if ((iPort = int.Parse(parPort)) == 0) throw new Exception("Invalid Port Number");
        if (ndClient.atIsConnected)
          ndClient.Disconnect();
        ndClient.Init(parServer, iPort);
        strMessage = scSocket.ndClient.GetCliHello();
        retValue = SendStr(strMessage);
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendHello)); }
      return (retValue);
    }
    internal bool SendStr(string parMessage)
    {
      bool retValue = false;
      try
      {
        ndClient.SendStrAsync(parMessage);
        retValue = true;
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(SendStr)); }
      return (retValue);
    }
    internal bool ConnectServer(string parIp, string parPort, string parSessionId)
    {
      bool retValue = false;
      try
      {
        ndLifeTime.AppMessage("Connecting...");
        if (SendHello(parIp, parPort))
        {
          scSocket.atServerIp = parIp;
          scSocket.atServerPort = parPort;
          retValue = true;
        }
      }
      catch (Exception Err) { ndLifeTime.ShowException(Err, Name, nameof(ConnectServer)); }
      return (retValue);
    }
    internal bool EvalPressed(StyledElement parElement)
    {
      bool retValue = false;
      RawKeyValue objKeyValue;
      try
      {
        switch (parElement.Name)
        {
          case "cmdConnect":
            objKeyValue = GearControl.PanelKeyValue(fwStackPanel, "txtIp, txtPort, txtSessionID");
            retValue = ConnectServer(objKeyValue.ValueStr("txtIp"), objKeyValue.ValueStr("txtPort"), objKeyValue.ValueStr("txtSessionID"));
            break;
          case "cmdOpenCamera":
            if (ndLifeTime.AppMessage("Reading QRCode..."))
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
    private bool SocketParam(string parIp, string parPort, string parSessionId)
    {
      fwStackPanel.GetControl<TextBox>("txtIp").Text = parIp;
      fwStackPanel.GetControl<TextBox>("txtPort").Text = parPort;
      fwStackPanel.GetControl<TextBox>("txtSessionID").Text = parSessionId;
      return (true);
    }
    internal bool LoadStartPage() => ndLifeTime.LoadContent("ControlPage.xml");
    #endregion
    #region Constructor
    public Evaluator() => ndClient = new RawScktClient(ClientSocketStatus);
    internal void Close()
    {
      ndClient?.Close();
      ndClient = default;
    }
    #endregion
  }
}