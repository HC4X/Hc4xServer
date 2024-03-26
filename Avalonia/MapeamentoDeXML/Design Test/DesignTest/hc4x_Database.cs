using System;
using LibModel;

namespace DesignTest {
  public class hc4x_SectorData : RawXml {
    private const string Name = nameof(hc4x_SectorData);
    #region Node
    internal HC4x_MailSender ndMailSender { get; private set; }
    internal HC4x_SectorUser ndSectorUser { get; private set; }
    #endregion
    #region Constructor
    public hc4x_SectorData(string parFileName) : base(parFileName, hc4x_NodeType.XmlFileOpen) {
      ndMailSender = GetOrAdd<HC4x_MailSender>(c_nodemail);
      ndSectorUser = GetOrAdd<HC4x_SectorUser>(c_sectoruser); 
      }
    #endregion
    #region Constant
    private const string c_nodemail = "NodeMail";
    private const string c_sectoruser = "SectorUser";
    #endregion
    }
  }
