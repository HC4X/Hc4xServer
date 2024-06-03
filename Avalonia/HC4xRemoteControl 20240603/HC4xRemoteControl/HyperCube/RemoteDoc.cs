using System;
using LibModel;

namespace HyperCube.Platform {
  #region RemoteDoc
  public class NodeKey : RawXml {
    private const string Key = nameof(NodeKey);
    #region Attribute
    public string atId {
      get => ValueStr(c_atid);
      set => SetValue(c_atid, value);
    }
    public string atValue {
      get => ValueStr(c_atvalue);
      set => SetValue(c_atvalue, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeKey(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant

    internal const string c_node = "NodeKey";
    private const string c_atid = "id";
    private const string c_atvalue = "value";
    #endregion
  }
  public class SectorNumeric : RawXml {
    #region Attribute
    #endregion
    #region Node
    public NodeKey ndKey { get; private set; }
    #endregion
    #region Constructor
    public SectorNumeric(RawXml parXml) : base(parXml) {
      ndKey = GetOrAdd<NodeKey>(c_ndkey);

    }
    #endregion
    #region Constant
    public const string c_sector = "SectorNumeric";
    private const string c_ndkey = NodeKey.c_node;
    #endregion
  }
  public class SectorDirection : RawXml {
    #region Attribute
    #endregion
    #region Node
    public NodeKey ndKey { get; private set; }
    #endregion
    #region Constructor
    public SectorDirection(RawXml parXml) : base(parXml) {
      ndKey = GetOrAdd<NodeKey>(c_ndkey);
    }
    #endregion
    #region Constant
    public const string c_sector = "SectorDirection";
    private const string c_ndkey = NodeKey.c_node;
    #endregion
  }
  public class SectorZoom : RawXml {
    #region Attribute
    #endregion
    #region Node
    public NodeKey ndKey { get; private set; }
    #endregion
    #region Constructor
    public SectorZoom(RawXml parXml) : base(parXml) {
      ndKey = GetOrAdd<NodeKey>(c_ndkey);
    }
    #endregion
    #region Constant
    public const string c_sector = "SectorZoom";
    private const string c_ndkey = NodeKey.c_node;
    #endregion
  }
  public class RemoteDoc : RawXml {
    private const string Name = nameof(RemoteDoc);
    #region Node
    public SectorNumeric scNumeric { get; private set; }
    public SectorDirection scDirection { get; private set; }
    public SectorZoom scZoom { get; private set; }
    #endregion
    #region Constructor
    public RemoteDoc(RawXml parXml) : base(parXml) {
      scNumeric = GetOrAdd<SectorNumeric>(c_scnumeric);
      scDirection = GetOrAdd<SectorDirection>(c_scdirection);
      scZoom = GetOrAdd<SectorZoom>(c_sczoom);
    }
    public new void Close() {
      scNumeric?.Close();
      scNumeric = default;
      scDirection?.Close();
      scDirection = default;
      scZoom?.Close();
      scZoom = default;
      base.Close();
    }
    #endregion
    #region Constant
    public const string c_sector = "SectorRemote";
    private const string c_scnumeric = SectorNumeric.c_sector;
    private const string c_scdirection = SectorDirection.c_sector;
    private const string c_sczoom = SectorZoom.c_sector;
    #endregion
    public string GetCommandById(string buttonName) {
      foreach (var node in scNumeric.ArrayNode()) {
        if (node.ndXmlKeyValue.ValueStr(0) == buttonName) {
          return node.ndXmlKeyValue.ValueStr(1);
        }
      }
      foreach (var node in scDirection.ArrayNode()) {
        if (node.ndXmlKeyValue.ValueStr(0) == buttonName) {
          return node.ndXmlKeyValue.ValueStr(1);
        }
      }
      foreach (var node in scZoom.ArrayNode()) {
        if (node.ndXmlKeyValue.ValueStr(0) == buttonName) {
          return node.ndXmlKeyValue.ValueStr(1);
        }
      }
      return null;
    }
  }
  #endregion
}