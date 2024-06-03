using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibModel;
using System.IO;

namespace HyperCube.Platform {
  public class RawScktClient : RawSocket {
    private const string Name = nameof(RawScktClient);
    #region Attribute
    private string atIP { get; set; }
    private int atPort { get; set; }
    private string atSessionId { get; set; }
    #endregion
    #region Method
    public void Disconnect() {
      if (fwSocket.Connected)
        fwSocket.Disconnect(false);
    }
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
      catch (SocketException Err) { TreatSocketException(Err, nameof(SendStrAsync), $"{atIP}:{atPort}"); }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(SendStrAsync)); }
    }
    protected override void TreatSocketException(SocketException parErr, string parMethod, string parExtraInfo) {
      Exception Err;
      switch (parErr.SocketErrorCode) {
        case SocketError.TimedOut:
          Err = new Exception("tempo excedido tentando conectar: " + parExtraInfo);
          ShowException(Err, Name, parMethod);
          break;
        case SocketError.NetworkUnreachable:
          Err = new Exception("Aparelho não conectado ao HotSpot: " + parExtraInfo);
          ShowException(Err, Name, parMethod);
          break;
        case SocketError.ConnectionRefused:
          Err = new Exception("Servidor desligado ou não conectado no IP/Porta:" + parExtraInfo);
          ShowException(Err, Name, parMethod);
          break;
        default:
          base.TreatSocketException(parErr, parMethod, parExtraInfo);
          break;
      }
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
      catch (SocketException Err) { retValue = string.Empty;  TreatSocketException(Err, nameof(ParseReceived)); }
      catch (Exception Err) { retValue = string.Empty; ShowException(Err, Name, nameof(SendMessage)); }
      return (retValue);
    }
    /// <summary></summary>
    ///! Date: 25/05/2024
    protected async Task<string> ParseReceived() {
      int iRead = 256;
      byte[] arByte;
      string retValue = "";
      try {
        if (fwSocket.Available > iRead) iRead = fwSocket.Available;
        arByte = new byte[iRead];
        iRead = await fwSocket.ReceiveAsync(ParseSegByte(arByte), SocketFlags.None);
        if (iRead > 0) retValue = Encoding.UTF8.GetString(arByte, 0, iRead);
      }
      catch (SocketException Err) { TreatSocketException(Err, nameof(ParseReceived)); }
      catch (IOException Err) { ShowException(Err.InnerException, Name, nameof(ParseReceived)); }
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
    /// <summary></summary>
    ///! Date: 25/05/2024
    private void TreatSocketException(SocketException parErr, string parMethod) => TreatSocketException(parErr, parMethod, "");
    /// <summary></summary>
    ///! Date: 25/05/2024
    protected virtual void TreatSocketException(SocketException parErr, string parMethod, string parExtraInfo) {
      switch (parErr.SocketErrorCode) {
        case SocketError.ConnectionAborted:
          if (this is RawScktClient objScktClient)
            objScktClient.Disconnect();
          break;
        case SocketError.ConnectionReset:
          //# Nothing to do
          break;
        default:
          ShowException(parErr, Name, parMethod);
          break;
      }
    }
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
