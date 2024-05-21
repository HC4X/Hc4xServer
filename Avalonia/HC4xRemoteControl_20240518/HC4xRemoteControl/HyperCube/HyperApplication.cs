using System;
using System.IO;
using LibModel;

namespace HyperCube.RemoteControl {
  public class HyperApplication : FileApplication {
    #region Node
    public RemoteDoc scRemote { get; private set; }
    #endregion
    #region Constructor
    public HyperApplication(Stream parStream) : base(GearXml.ParseStream(parStream), true) { scRemote = GetOrAdd<RemoteDoc>("SectorRemote"); }
    public HyperApplication(string parFileName) : base(parFileName, true) { scRemote = GetOrAdd<RemoteDoc>("SectorRemote"); }
    #endregion
  }
}
