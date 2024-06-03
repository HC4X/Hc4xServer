using System;
using System.Security.Cryptography;
using System.Text;
using LibModel;

namespace HyperCube.Platform {
  #region Socket
  public enum hc4x_ScktComm {
    None,
    CliHello,
    CliGdbye,
    CliCmmnd,
    CliFlask,
    SrvHello,
    SrvGdbye,
    SrvRfsed,
    SrvOk,
    SrvFlans
  }
  public class SocketComm : RawXml {
    private const string Name = nameof(SocketComm);
    #region Attribute
    public string atComm => ValueStr(c_scktcomm);
    #endregion
    #region Method
    public hc4x_ScktComm ScktComm() => NodeSocket.Parse(atComm);
    #endregion
    #region Constructor
    public SocketComm(string parXml) : base(parXml, hc4x_NodeType.XmlContent) { }
    public SocketComm(RawXml rawXml) : base(rawXml) { }
    #endregion
    #region Static
    public static SocketComm ParseComm(string parContent) {
      SocketComm retValue;
      JSonParser objJson;
      try {
        if (parContent.StartsWith("{")) {
          objJson = new JSonParser();
          objJson.Init(parContent);
          retValue = objJson.ParseAttributes<SocketComm>();
        }
        else if (parContent.StartsWith("<"))
          retValue = new SocketComm(parContent);
        else
          retValue = null;
      }
      catch (Exception Err) { retValue = null; AxisMundi.ShowException(Err, Name, nameof(ParseComm)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const string c_scktcomm = "SocketComm";
    #endregion
  }
  public class SocketClient : NodeSocket {
    private const string Name = nameof(SocketClient);
    #region Attribute
    public string atSessionId => ValueStr("sessionid");
    #endregion
    #region Method
    public hc4x_ScktComm EvalCliMessage(string parMessage) {
      hc4x_ScktComm retValue = hc4x_ScktComm.None;
      SocketComm objCliComm = null;
      try {
        objCliComm = SocketComm.ParseComm(parMessage);
        if (objCliComm != null)
          retValue = objCliComm.ScktComm();
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(EvalCliMessage)); }
      return (retValue);
    }
    public string GetCliCmmnd(string parCmmnd) {
      string retValue;
      SocketComm objScktComm;
      try {
        retValue = GetNodeContent(c_cli_cmmnd);
        objScktComm = SocketComm.ParseComm(retValue);
        objScktComm.SetValue("param", parCmmnd);
        retValue = objScktComm.ParseJSon();
      }
      catch (Exception Err) { retValue = string.Empty; AxisMundi.ShowException(Err, Name, nameof(GetCliCmmnd)); }
      return (retValue);
    }
    public string GetCliHello() => GetNodeContent(c_cli_hello);
    public string GetCliGdBye() => GetNodeContent(c_cli_gdbye);
    public string GetCliTextFile(string parFileName) => GetNodeContent(c_cli_flask);
    public void SetSessionId(string parSessionId) => SetValue(c_sessionid, parSessionId);
    #endregion
    #region Constructor
    public SocketClient(RawXml parXml) : base(parXml) { }
    #endregion
  }
  public class SocketServer : NodeSocket {
    private const string Name = nameof(SocketServer);
    #region Method
    public string GetSrvOk() => GetNodeContent(c_srv_ok);
    public string GetSrvHello() {
      string retValue = "";
      string strContent;
      SocketComm objComm;
      try {
        strContent = GetNodeContent(c_srv_hello);
        if (string.IsNullOrWhiteSpace(strContent)) return (retValue);
        retValue = UniqueHash(8);
        objComm = SocketComm.ParseComm(strContent);
        objComm.SetValue("sessionid", retValue);
        retValue = objComm.ParseJSon();
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(GetSrvHello)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public SocketServer(RawXml parXml) : base(parXml) { }
    #endregion
    #region Static
    private static string UniqueHash(int parLength) {
      byte[] arByte;
      string retValue;
      StringBuilder objStrBuilder;
      arByte = new byte[parLength];
      using (var provider = new RNGCryptoServiceProvider())
        provider.GetBytes(arByte);
      objStrBuilder = new StringBuilder();
      foreach (byte b in arByte)
        objStrBuilder.AppendFormat("{0:x2}", b);
      retValue = objStrBuilder.ToString();
      return (retValue);
    }
    #endregion
  }
  public class NodeSocket : RawXml {
    private const string Name = nameof(NodeSocket);
    #region Node
    private SectorNode scSocket { get; set; }
    #endregion
    #region Method
    protected SocketComm GetSocketComm(string parId) {
      SocketComm retValue = default;
      try {
        parId = GetNodeContent(parId);
        if (!string.IsNullOrWhiteSpace(parId))
          retValue = SocketComm.ParseComm(parId);
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(GetSocketComm)); }
      return (retValue);
    }
    protected string GetNodeContent(string parId) {
      string retValue = string.Empty;
      RawXml objXml;
      try {
        objXml = scSocket.ValueAs<RawXml>(parId);
        if (objXml != null) retValue = objXml.atContent;
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(GetNodeContent)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public NodeSocket(RawXml parXml) : base(parXml) => scSocket = SectorOf<RawXml>("NodeSocket", "id");
    #endregion
    #region Static
    public static hc4x_ScktComm Parse(string parScktComm) {
      hc4x_ScktComm retValue;
      switch (parScktComm) {
        case c_cli_hello:
          retValue = hc4x_ScktComm.CliHello;
          break;
        case c_cli_gdbye:
          retValue = hc4x_ScktComm.CliGdbye;
          break;
        case c_cli_cmmnd:
          retValue = hc4x_ScktComm.CliCmmnd;
          break;
        case c_cli_flask:
          retValue = hc4x_ScktComm.CliFlask;
          break;
        case c_srv_hello:
          retValue = hc4x_ScktComm.SrvHello;
          break;
        case c_srv_gdbye:
          retValue = hc4x_ScktComm.SrvGdbye;
          break;
        case c_srv_rfsed:
          retValue = hc4x_ScktComm.SrvRfsed;
          break;
        case c_srv_ok:
          retValue = hc4x_ScktComm.SrvOk;
          break;
        case c_srv_flans:
          retValue = hc4x_ScktComm.SrvFlans;
          break;
        default:
          retValue = hc4x_ScktComm.None;
          break;
      }
      return (retValue);
    }
    #endregion
    #region Constant
    protected const string c_cli_hello = "hello-server";
    protected const string c_cli_gdbye = "gdbye-server";
    protected const string c_cli_cmmnd = "cmmnd-server";
    protected const string c_cli_flask = "flask-server";
    protected const string c_sessionid = "sessionid";
    protected const string c_srv_hello = "hello-client";
    protected const string c_srv_gdbye = "gdbye-client";
    protected const string c_srv_rfsed = "rfsed-client";
    protected const string c_srv_ok = "ok-client";
    protected const string c_srv_flans = "flans-client";
    #endregion
  }
  public class SectorSocket : RawXml {
    private const string Name = nameof(SectorSocket);
    #region Attribute
    public string atServerIp {
      get => ValueStr(c_ip);
      set => SetValue(c_ip, value);
    }
    public string atServerPort {
      get => ValueStr(c_port);
      set => SetValue(c_port, value);
    }
    #endregion
    #region Node
    internal SocketClient ndClient { get; set; }
    internal SocketServer ndServer { get; set; }
    #endregion
    #region Constructor
    public SectorSocket(RawXml parXml) : base(parXml) {
      ndClient = GetOrAdd<SocketClient>(c_scclient);
      ndServer = GetOrAdd<SocketServer>(c_scserver);
    }
    #endregion
    #region Constant
    private const string c_ip = "ServerIp";
    private const string c_port = "ServerPort";
    private const string c_scclient = "SectorClient";
    private const string c_scserver = "SectorServer";
    internal const string c_sector = "SectorSocket";
    #endregion
  }
  #endregion
}
