using System;
using Interop.SocketLib;

namespace SocketClient {
  internal enum EnumSocketStatus {
    None,
    HelloReceived,
    OKReceived
  }
  internal class Evaluator {
    private const string Name = nameof(Evaluator);
    #region Node
    private RawScktClient ndClient { get; set; }
    #endregion
    #region Method
    internal bool SendStr(string parMessage) {
      bool retValue = false;
      try {
        ndClient.SendStrAsync(parMessage);
        retValue = true;
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(SendStr)); }
      return (retValue);
    }
    internal bool SendHello(string parServer, string parMessage) {
      bool retValue = false;
      string[] arServer;
      try {
        if (!ndClient.atIsConnected) {
          arServer = parServer.Split(':');
          ndClient.Init(arServer[0], int.Parse(arServer[1]));
        }
        retValue = SendStr(parMessage);
      }
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(SendHello)); }
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
      catch (Exception Err) { AxisMundi.ShowException(Err, Name, nameof(EvalMessage)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    internal Evaluator(SocketStatus parStatus) { ndClient = new RawScktClient(parStatus); }
    internal void Close() {
      ndClient?.Close();
      ndClient = null;
    }
    #endregion
  }
}
