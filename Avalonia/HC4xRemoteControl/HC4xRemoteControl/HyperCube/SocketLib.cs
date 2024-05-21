using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibModel;

namespace HyperCube.RemoteControl {
  public class RawScktClient : RawSocket {
    private const string Name = nameof(RawScktClient);
    #region Attribute
    private string atIP;
    private int atPort;
    #endregion
    #region Method
    public async void SendStrAsync(string parMessage) {
      IPEndPoint objIPServer;
      try {
        objIPServer = new IPEndPoint(IPAddress.Parse(atIP), atPort);
        fwSocket = Load(objIPServer.AddressFamily);
        await fwSocket.ConnectAsync(objIPServer);
        if (fwSocket != null) {
          if (fwSocket.Connected) {
            ndStatus.Invoke(await SendMessage(parMessage), hc4x_SocketStatus.Sent);
          }
        }
        else {
          return;
        }
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(SendStrAsync)); }
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
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    public RawScktClient(SocketStatus parStatus) : base(parStatus) { }
    #endregion
  }
  public class RawScktServer : RawSocket {
    private const string Name = nameof(RawScktServer);
    #region Framework
    private Socket fwListener { get; set; }
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
        ndStatus.Invoke(await ParseReceived(), hc4x_SocketStatus.Received);
        //# await SendMessage("Hello client");
      }
      catch (Exception Err) { ShowException(Err, Name, nameof(ServerListener)); }
    }
    public bool Stop() {
      bool retValue = false;
      try {
        fwListener.Disconnect(true);
        atIsListening = false;
        ndStatus.Invoke("Server stopped", hc4x_SocketStatus.Log);
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
        fwListener = Load(objIPEnd.AddressFamily);
        fwListener.Bind(objIPEnd);
        fwListener.Listen(10);
        atIsListening = true;
        ndStatus.Invoke("Server Listening", hc4x_SocketStatus.Log);
        while (atIsListening) {
          fwSocket = await fwListener.AcceptAsync();
          ServerListener();
        }
        fwSocket.Shutdown(SocketShutdown.Both);
        fwSocket.Close();
      }
      catch (ObjectDisposedException) { }
      catch (Exception Err) { ShowException(Err, Name, nameof(Init)); }
    }
    public RawScktServer(SocketStatus parStatus) : base(parStatus) { }
    public override void Close() {
      fwListener.Disconnect(true);
      fwListener.Shutdown(SocketShutdown.Both);
      fwListener.Dispose();
      fwListener = null;
      base.Close();
    }
    #endregion
  }
  public class RawSocket {
    private const string Name = nameof(RawSocket);
    #region Framework
    protected Socket fwSocket { get; set; }
    #endregion
    #region Attribute
    public bool atIsConnected => fwSocket != null && fwSocket.Connected;
    #endregion
    #region Node
    protected SocketStatus ndStatus { get; private set; }
    #endregion
    #region Method
    public async Task<string> SendMessage(string parMessage) {
      byte[] arByte;
      int iLength;
      string retValue;
      try {
        arByte = Encoding.UTF8.GetBytes(parMessage);
        iLength = await fwSocket.SendAsync(ParseSegByte(arByte), SocketFlags.None);
        retValue = await ParseReceived();
      }
      catch (Exception Err) { retValue = ""; ShowException(Err, Name, nameof(SendMessage)); }
      return (retValue);
    }
    protected async Task<string> ParseReceived() {
      int iRead;
      byte[] arByte = new byte[1024];
      string retValue = "";
      try {
        iRead = await fwSocket.ReceiveAsync(ParseSegByte(arByte), SocketFlags.None);
        if (iRead > 0) retValue = Encoding.UTF8.GetString(arByte, 0, iRead);
      }
      catch (System.IO.IOException Err) { ShowException(Err.InnerException, Name, nameof(ParseReceived)); }
      catch (Exception Err) { ShowException(Err, Name, nameof(ParseReceived)); }
      return (retValue);
    }
    protected void ShowException(Exception parErr, string parName, string parFunction) {
      string strJSON;
      strJSON = string.Format("\"Message\":\"{0}\", \"Class\":\"{1}\", \"Function\":\"{2}\"", parErr.Message, parName, parFunction);
      ndStatus.Invoke("{" + strJSON + "}", hc4x_SocketStatus.Err);
    }
    protected ArraySegment<byte> ParseSegByte(byte[] parByte) => new ArraySegment<byte>(parByte);
    protected Socket Load(AddressFamily parAddrFamily) => new Socket(parAddrFamily, SocketType.Stream, ProtocolType.Tcp);
    #endregion
    #region Constructor
    public RawSocket(SocketStatus parStatus) { ndStatus = parStatus; }
    public virtual void Close() {
      ndStatus = null;
      fwSocket?.Close();
      fwSocket?.Dispose();
      fwSocket = null;
    }
    #endregion
  }
}
