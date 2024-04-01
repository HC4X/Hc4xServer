using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interop.TCPClientServer {
  public delegate bool SocketStatus(string parMessage);
  public class RawTCPClient : RawTCP {
    private const string Name = nameof(RawTCPClient);
    #region Attribute
    private string atIP;
    private int atPort;
    #endregion
    #region Method
    public async void Send(string parMessage) {
      try {
        fwTcpClient = new TcpClient(atIP, atPort);
        if (fwTcpClient.Connected) {
          ndStream = fwTcpClient.GetStream();
          ndStatus.Invoke(await SendMessage(parMessage));
        }
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(Send)); }
    }
    #endregion
    #region Constructor
    public bool Init(string parIP, int parPort) {
      bool retValue = false;
      try {
        atIP = parIP;
        atPort = parPort;
        retValue = true;
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public RawTCPClient(SocketStatus parStatus) : base(parStatus) { }
    #endregion
  }
  public class RawTCPServer : RawTCP {
    private const string Name = nameof(RawTCPServer);
    #region Framework
    private TcpListener fwListener { get; set; }
    #endregion
    #region Attribute
    private bool atIsListening { get; set; }
    #endregion
    #region Method
    public async void Answer(string parMessage) {
      try {
        await SendMessage(parMessage);
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(Answer)); }
    }
    private async void ServerListener() {
      try {
        ndStream = fwTcpClient.GetStream();
        ndStatus.Invoke(await ParseReceived());
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(ServerListener)); }
    }
    public bool Stop() {
      bool retValue = false;
      try {
        fwListener.Stop();
        atIsListening = false;
        ndStatus.Invoke("Server stopped");
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(Stop)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public async void Init(string parIP, int parPort) {
      IPEndPoint objIPEnd;
      try {
        objIPEnd = new IPEndPoint(IPAddress.Parse(parIP), parPort);
        fwListener = new TcpListener(objIPEnd);
        fwListener.Start(10);
        atIsListening = true;
        ndStatus.Invoke("Server Listening");
        while (atIsListening) {
          fwTcpClient = await fwListener.AcceptTcpClientAsync();
          ServerListener();
        }
      }
      catch (ObjectDisposedException) { }
      catch (Exception Err) { ShowException(Err, Name, nameof(Init)); }
    }
    public RawTCPServer(SocketStatus parStatus) : base(parStatus) { }
    public override void Close() {
      fwListener.Stop();
      fwListener = null;
      base.Close();
    }
    #endregion
  }
  public class RawTCP {
    private const string Name = nameof(RawTCP);
    #region Framework
    protected TcpClient fwTcpClient { get; set; }
    protected NetworkStream ndStream { get; set; }
    #endregion
    #region Attribute
    public bool atIsConnected => fwTcpClient != null && fwTcpClient.Connected;
    #endregion
    #region Node
    protected SocketStatus ndStatus { get; private set; }
    #endregion
    #region Method
    protected async Task<string> SendMessage(string parMessage) {
      byte[] arByte;
      string retValue;
      try {
        arByte = Encoding.UTF8.GetBytes(parMessage);
        await ndStream.WriteAsync(arByte, 0, arByte.Length);
        arByte = new byte[256];
        retValue = await ParseReceived();
      }
      catch (Exception Err) { retValue = ""; ShowException(Err, Name, nameof(SendMessage)); }
      return (retValue);
    }
    protected async Task<string> ParseReceived() {
      byte[] arByte;
      int iRead;
      string retValue = "";
      try {
        arByte = new byte[256];
        iRead = await ndStream.ReadAsync(arByte, 0, arByte.Length);
        if (iRead > 0) retValue = Encoding.UTF8.GetString(arByte, 0, iRead);
      }
      catch (System.IO.IOException Err) { ShowException(Err.InnerException, Name, nameof(ParseReceived)); }
      catch (Exception Err) { ShowException(Err, Name, nameof(ParseReceived)); }
      return (retValue);
    }
    protected void ShowException(Exception parErr, string parName, string parFunction) {
      string strJSON;
      strJSON = string.Format("\"Message\":\"{0}\", \"Class\":\"{1}\", \"Function\":\"{2}\"", parErr.Message, parName, parFunction);
      ndStatus.Invoke("{" + strJSON + "}");
    }
    #endregion
    #region Constructor
    public RawTCP(SocketStatus parStatus) { ndStatus = parStatus; }
    public virtual void Close() {
      ndStatus = null;
      fwTcpClient?.Close();
      fwTcpClient?.Dispose();
      fwTcpClient = null;
    }
    #endregion
  }
}
