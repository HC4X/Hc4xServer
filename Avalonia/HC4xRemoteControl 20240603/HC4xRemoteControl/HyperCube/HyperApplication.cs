using System;
using System.IO;
using LibModel;

namespace HyperCube.Platform {
  public class HyperApplication : FileApplication {
    #region Node
    public RemoteDoc scRemote { get; private set; }
    public SectorSocket scSocket { get; private set; }
    #endregion
    #region Constructor
    private void Init() {
      scSocket = GetOrAdd<SectorSocket>(c_scsocket);
      scRemote = GetOrAdd<RemoteDoc>(c_scremote);
    }
    public HyperApplication(Stream parStream) : base(GearXml.ParseStream(parStream), true) => Init();
    public HyperApplication(string parFileName) : base(parFileName, true) => Init();
    #endregion
    #region Constant
    private const string c_scsocket = SectorSocket.c_sector;
    private const string c_scremote = RemoteDoc.c_sector;
    #endregion
  }
}
