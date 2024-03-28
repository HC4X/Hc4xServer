using LibModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.XmlMapper
{
    #region RemoteDoc
    public class NodeKey : RawXml
    {
        private const string Key = nameof(NodeKey);
        #region Attributes
        public string atId
        {
            get => ValueStr(c_atid);
            set => SetValue(c_atid, value);
        }
        public string atValue
        {
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
    public class SectorNumeric : RawXml
    {
        #region Attributes
        #endregion
        #region Node
        public NodeKey ndKey { get; private set; }
        #endregion
        #region Constructor
        public SectorNumeric(RawXml parXml) : base(parXml)
        {
            ndKey = GetOrAdd<NodeKey>(c_ndkey);

        }
        #endregion
        #region Constant
        public const string c_sector = "SectorNumeric";
        private const string c_ndkey = NodeKey.c_node;
        #endregion
    }
    public class SectorDirection : RawXml
    {
        #region Attributes
        #endregion
        #region Node
        public NodeKey ndKey { get; private set; }
        #endregion
        #region Constructor
        public SectorDirection(RawXml parXml) : base(parXml)
        {
            ndKey = GetOrAdd<NodeKey>(c_ndkey);
        }
        #endregion
        #region Constant
        public const string c_sector = "SectorDirection";
        private const string c_ndkey = NodeKey.c_node;
        #endregion
    }
    public class SectorZoom : RawXml
    {
        #region Attributes
        #endregion
        #region Node
        public NodeKey ndKey { get; private set; }
        #endregion
        #region Constructor
        public SectorZoom(RawXml parXml) : base(parXml)
        {
            ndKey = GetOrAdd<NodeKey>(c_ndkey);
        }
        #endregion
        #region Constant
        public const string c_sector = "SectorZoom";
        private const string c_ndkey = NodeKey.c_node;
        #endregion
    }
    public class RemoteDoc : RawXml
    {
        #region Attributes
        #endregion
        #region Node
        public SectorNumeric scNumeric { get; private set; }
        public SectorDirection scDirection { get; private set; }
        public SectorZoom scZoom { get; private set; }

        #endregion
        #region Constructor
        public RemoteDoc(string parFileName) : base(parFileName, hc4x_NodeType.XmlFileOpen)
        {
            scNumeric = GetOrAdd<SectorNumeric>(c_scnumeric);
            scDirection = GetOrAdd<SectorDirection>(c_scdirection);
            scZoom = GetOrAdd<SectorZoom>(c_sczoom);

        }
        public new void Close()
        {
            scNumeric?.Close();
            scDirection?.Close();
            scZoom?.Close();
            base.Close();
        }
        #endregion
        #region Constant
        public const string c_sector = "SectorRemote";
        private const string c_scnumeric = SectorNumeric.c_sector;
        private const string c_scdirection = SectorDirection.c_sector;
        private const string c_sczoom = SectorZoom.c_sector;
        #endregion
    }
    #endregion
}

