using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using LibModel;

namespace DesignTest.Atom
{
  #region ArchDesc
  public class NodeEadArchDesc : RawXml
  {
    private const string Name = nameof(NodeEadArchDesc);
    #region Attributes
    public string atLevel
    {
      get => ValueStr(c_atlevel);
      set => SetValue(c_atlevel, value);
    }
    public string atRelatedencoding
    {
      get => ValueStr(c_atrelatedencoding);
      set => SetValue(c_atrelatedencoding, value);
    }
    #endregion
    #region Node
    public NodeDid ndDid { get; private set; }
    public NodeBioghist ndBioghist { get; private set; }
    public NodeOdd ndOdd { get; private set; }
    public NodeControlAccess ndControlAccess { get; private set; }
    public NodeDsc ndDsc { get; private set; }
    #endregion
    #region Constructor

    public NodeEadArchDesc(RawXml parXml) : base(parXml)
    {
      ndDid = GetOrAdd<NodeDid>(c_nddid);
      ndBioghist = GetOrAdd<NodeBioghist>(c_ndbioghist);
      ndOdd = GetOrAdd<NodeOdd>(c_ndodd);
      ndControlAccess = GetOrAdd<NodeControlAccess>(c_ndcontrolaccess);
      ndDsc = GetOrAdd<NodeDsc>(c_nddsc);
    }
    #endregion
    #region Constant
    internal const string c_node = "archdesc";
    private const string c_atlevel = "level";
    private const string c_atrelatedencoding = "relatedencoding";
    private const string c_nddid = NodeDid.c_node;
    private const string c_ndbioghist = NodeBioghist.c_node;
    private const string c_ndodd = NodeOdd.c_node;
    private const string c_ndcontrolaccess = NodeControlAccess.c_node;
    private const string c_nddsc = NodeDsc.c_node;
    #endregion
  }
  public class NodeDid : RawXml
  {
    private const string Name = nameof(NodeDid);
    #region Attributes
    #endregion
    #region Node
    public NodeUnittitle ndUnittitle { get; set; }
    public NodeUnitid ndUnitid { get; set; }

    public NodeUnitDate ndUnitDate { get; set; }
    public NodePhysdesc ndPhysdesc { get; set; }
    public NodeLangmaterial ndLangmaterial { get; set; }
    public NodeOrigination ndOrigination { get; set; }
    public NodeDao ndDao { get; set; }
    #endregion
    #region Constructor
    public NodeDid(RawXml parXml) : base(parXml)
    {
      ndUnittitle = GetOrAdd<NodeUnittitle>(c_ndunittitle);
      ndUnitid = GetOrAdd<NodeUnitid>(c_ndunitid);
      ndUnitDate = GetOrAdd<NodeUnitDate>(c_ndunitdate);
      ndPhysdesc = GetOrAdd<NodePhysdesc>(c_ndphysdesc);
      ndLangmaterial = GetOrAdd<NodeLangmaterial>(c_ndlangmaterial);
      ndOrigination = GetOrAdd<NodeOrigination>(c_ndorigination);
      ndDao = GetOrAdd<NodeDao>(c_nddao);

    }
    #endregion
    #region Constant
    internal const string c_node = "did";
    private const string c_ndunittitle = NodeUnittitle.c_node;
    private const string c_ndunitid = NodeUnitid.c_node;
    private const string c_ndphysdesc = NodePhysdesc.c_node;
    private const string c_ndlangmaterial = NodeLangmaterial.c_node;
    private const string c_ndorigination = NodeOrigination.c_node;
    private const string c_nddao = NodeDao.c_node;
    private const string c_ndunitdate = NodeUnitDate.c_node;
    #endregion
  }
  public class NodeUnitDate : RawXml
  {
    private const string Name = nameof(NodeUnitDate);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeUnitDate(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "unitdate";
    private const string c_atencodinganalog = "encodinganalog";
    #endregion
  }
  public class NodeDao : RawXml
  {
    private const string Name = nameof(NodeDao);
    #region Attributes
    public string atLinktype
    {
      get => ValueStr(c_atlinktype);
      set => SetValue(c_atlinktype, value);
    }
    public string atRole
    {
      get => ValueStr(c_atrole);
      set => SetValue(c_atrole, value);
    }
    public string atHref
    {
      get => ValueStr(c_athref);
      set => SetValue(c_athref, value);
    }
    public string atActuate
    {
      get => ValueStr(c_atactuate);
      set => SetValue(c_atactuate, value);
    }
    public string atShow
    {
      get => ValueStr(c_atshow);
      set => SetValue(c_atshow, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeDao(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant

    internal const string c_node = "dao";
    private const string c_atlinktype = "linktype";
    private const string c_atrole = "role";
    private const string c_athref = "href";
    private const string c_atactuate = "actuate";
    private const string c_atshow = "show";
    #endregion
  }
  public class NodeUnittitle : RawXml
  {
    private const string Name = nameof(NodeUnittitle);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeUnittitle(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "unittitle";
    private const string c_atencodinganalog = "encodinganalog";
    #endregion
  }
  public class NodeUnitid : RawXml
  {
    private const string Name = nameof(NodeUnitid);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeUnitid(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "unitid";
    private const string c_atencodinganalog = "c_atencodinganalog";
    #endregion
  }
  public class NodePhysdesc : RawXml
  {
    private const string Name = nameof(NodePhysdesc);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodePhysdesc(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "physdesc";
    private const string c_atencodinganalog = "c_atencodinganalog";
    #endregion
  }
  public class NodeLangmaterial : RawXml
  {
    private const string Name = nameof(NodeLangmaterial);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    public NodeLanguage ndLanguage { get; private set; }
    #endregion
    #region Constructor
    public NodeLangmaterial(RawXml parXml) : base(parXml)
    {
      ndLanguage = GetOrAdd<NodeLanguage>(c_ndlanguage);
    }
    #endregion
    #region Constant
    internal const string c_node = "langmaterial";
    private const string c_atencodinganalog = "c_atencodinganalog";
    private const string c_ndlanguage = NodeLanguage.c_node;
    #endregion
  }
  public class NodeOrigination : RawXml
  {
    private const string Name = nameof(NodeOrigination);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    public NodeName ndName { get; private set; }
    #endregion
    #region Constructor
    public NodeOrigination(RawXml parXml) : base(parXml)
    {
      ndName = GetOrAdd<NodeName>(c_ndname);
    }
    #endregion
    #region Constant

    internal const string c_node = "origination";
    private const string c_atencodinganalog = "encodinganalog";
    private const string c_ndname = NodeName.c_node;
    #endregion
  }
  public class NodeBioghist : RawXml
  {
    private const string Name = nameof(NodeBioghist);
    #region Attributes
    public string atId
    {
      get => ValueStr(c_atid);
      set => SetValue(c_atid, value);
    }
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    public NodeNote ndNote { get; private set; }
    #endregion
    #region Constructor
    public NodeBioghist(RawXml parXml) : base(parXml)
    {
      ndNote = GetOrAdd<NodeNote>(c_ndnote);
    }
    #endregion
    #region Constant

    internal const string c_node = "bioghist";
    private const string c_atid = "id";
    private const string c_atencodinganalog = "encodinganalog";
    private const string c_ndnote = NodeNote.c_node;
    #endregion
  }
  public class NodeP : RawXml
  {
    private const string Name = nameof(NodeP);
    #region Attributes
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeP(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant

    internal const string c_node = "p";
    #endregion
  }
  public class NodeNote : RawXml
  {
    private const string Name = nameof(NodeNote);
    #region Attributes
    #endregion
    #region Node
    public NodeP ndP { get; private set; }
    #endregion
    #region Constructor
    public NodeNote(RawXml parXml) : base(parXml)
    {
      ndP = GetOrAdd<NodeP>(c_ndp);
    }
    #endregion
    #region Constant

    internal const string c_node = "note";
    private const string c_ndp = NodeP.c_node;
    #endregion
  }
  public class NodeOdd : RawXml
  {
    private const string Name = nameof(NodeOdd);
    #region Attributes
    public string atType
    {
      get => ValueStr(c_attype);
      set => SetValue(c_attype, value);
    }
    #endregion
    #region Node
    public NodeP ndP { get; private set; }
    #endregion
    #region Constructor
    public NodeOdd(RawXml parXml) : base(parXml)
    {
      ndP = GetOrAdd<NodeP>(c_ndp);
    }
    #endregion
    #region Constant
    internal const string c_node = "odd";
    private const string c_attype = "type";
    private const string c_ndp = NodeP.c_node;
    #endregion
  }
  public class NodeControlAccess : RawXml
  {
    private const string Name = nameof(NodeControlAccess);
    #region Node
    public NodeName ndName { get; private set; }
    public NodeGenreform ndGenreForm { get; private set; }
    public NodeSubject ndSubject { get; private set; }
    #endregion
    #region Constructor
    public NodeControlAccess(RawXml parXml) : base(parXml)
    {
      ndName = GetOrAdd<NodeName>(c_ndname);
      ndGenreForm = GetOrAdd<NodeGenreform>(c_ndgenreform);
      ndSubject = GetOrAdd<NodeSubject>(c_ndsubject);
    }
    #endregion
    #region Constant
    internal const string c_node = "controlaccess";
    private const string c_ndname = NodeName.c_node;
    private const string c_ndgenreform = NodeGenreform.c_node;
    private const string c_ndsubject = NodeSubject.c_node;
    #endregion
  }
  public class NodeName : RawXml
  {
    private const string Name = nameof(NodeName);
    #region Attributes
    public string atId
    {
      get => ValueStr(c_atid);
      set => SetValue(c_atid, value);
    }
    public string atRole
    {
      get => ValueStr(c_atrole);
      set => SetValue(c_atrole, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeName(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant

    internal const string c_node = "name";
    private const string c_atid = "id";
    private const string c_atrole = "role";
    #endregion
  }
  public class NodeDsc : RawXml
  {
    private const string Name = nameof(NodeDsc);
    #region Attributes
    public string atType
    {
      get => ValueStr(c_attype);
      set => SetValue(c_attype, value);
    }
    #endregion
    #region Node
    public NodeC ndC { get; private set; }
    #endregion

    #region Constructor
    public NodeDsc(RawXml parXml) : base(parXml)
    {
      ndC = GetOrAdd<NodeC>(c_ndc);
    }
    #endregion

    #region Constant
    internal const string c_node = "dsc";
    private const string c_ndc = NodeC.c_node;
    private const string c_attype = "type";
    #endregion
  }
  public class NodeGenreform : RawXml
  {
    private const string Name = nameof(NodeGenreform);
    #region Attributes
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeGenreform(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "genreform";
    #endregion
  }
  public class NodeSubject : RawXml
  {
    private const string Name = nameof(NodeSubject);
    #region Attributes
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeSubject(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "subject";
    #endregion
  }
  public class NodeAcqinfo : RawXml
  {
    private const string Name = nameof(NodeAcqinfo);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    public NodeP ndp { get; set; }
    #endregion
    #region Constructor
    public NodeAcqinfo(RawXml parXml) : base(parXml)
    {
      ndp = GetOrAdd<NodeP>(c_ndp);
    }
    #endregion
    #region Constant
    internal const string c_node = "acqinfo";
    private const string c_atencodinganalog = "encodinganalog";
    private const string c_ndp = NodeP.c_node;
    #endregion
  }
  public class NodeAccessRestrict : RawXml
  {
    private const string Name = nameof(NodeAccessRestrict);
    #region Attributes
    public string atEncodinganalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    public NodeP ndp { get; set; }
    #endregion
    #region Constructor
    public NodeAccessRestrict(RawXml parXml) : base(parXml)
    {
      ndp = GetOrAdd<NodeP>(c_ndp);
    }
    #endregion
    #region Constant
    internal const string c_node = "accessrestrict";
    private const string c_atencodinganalog = "encodinganalog";
    private const string c_ndp = NodeP.c_node;
    #endregion
  }
  public class NodeC : RawXml
  {
    private const string Name = nameof(NodeC);
    #region Node
    //public NodeC ndC { get; private set; }
    public NodeDid ndDid { get; private set; }
    public NodeBioghist ndBioghist { get; private set; }
    public NodeOdd ndOdd { get; private set; }
    public NodeControlAccess ndControlAccess { get; private set; }
    public NodeAcqinfo ndAcqinfo { get; private set; }
    public NodeAccessRestrict ndAccessRestrict { get; private set; }
    #endregion
    #region Constructor
    public NodeC(RawXml parXml) : base(parXml)
    {
      ndDid = GetOrAdd<NodeDid>(c_nddid);
      ndBioghist = GetOrAdd<NodeBioghist>(c_ndbioghist);
      ndOdd = GetOrAdd<NodeOdd>(c_ndodd);
      ndControlAccess = GetOrAdd<NodeControlAccess>(c_ndcontrolaccess);
      ndAcqinfo = GetOrAdd<NodeAcqinfo>(c_ndacqinfo);
      ndAccessRestrict = GetOrAdd<NodeAccessRestrict>(c_ndaccessrestrict);
      //ndC = GetOrAdd<NodeC>(c_node);
    }
    #endregion
    #region Constant
    internal const string c_node = "c";
    private const string c_nddid = NodeDid.c_node;
    private const string c_ndbioghist = NodeBioghist.c_node;
    private const string c_ndodd = NodeOdd.c_node;
    private const string c_ndcontrolaccess = NodeControlAccess.c_node;
    private const string c_ndacqinfo = NodeAcqinfo.c_node;
    private const string c_ndaccessrestrict = NodeAccessRestrict.c_node;
    #endregion
  }
  #endregion
  #region EadHeader
  public class NodeEadHeader : RawXml
  {
    private const string Name = nameof(NodeEadHeader);
    #region Attibute
    public string atLangencoding
    {
      get => ValueStr(c_atlangencoding);
      set => SetValue(c_atlangencoding, value);
    }
    public string atCountryencoding
    {
      get => ValueStr(c_atcountryencoding);
      set => SetValue(c_atcountryencoding, value);
    }
    public string atDateencoding
    {
      get => ValueStr(c_atdateencoding);
      set => SetValue(c_atdateencoding, value);
    }
    public string atRepositoryencoding
    {
      get => ValueStr(c_atrepositoryencoding);
      set => SetValue(c_atrepositoryencoding, value);
    }
    public string atScriptencoding
    {
      get => ValueStr(c_atscriptencoding);
      set => SetValue(c_atscriptencoding, value);
    }
    public string atRelatedencoding
    {
      get => ValueStr(c_atrelatedencoding);
      set => SetValue(c_atrelatedencoding, value);
    }
    #endregion
    #region Node
    public NodeEadId ndEadId { get; private set; }
    public NodeFileDesc ndFileDesc { get; private set; }
    public NodeProfileDesc ndProfileDesc { get; private set; }
    #endregion
    #region Constructor
    public NodeEadHeader(RawXml parXml) : base(parXml)
    {
      ndEadId = GetOrAdd<NodeEadId>(c_ndeadid);
      ndFileDesc = GetOrAdd<NodeFileDesc>(c_ndfiledesc);
      ndProfileDesc = GetOrAdd<NodeProfileDesc>(c_ndprofiledesc);
    }
    #endregion
    #region Constant
    public const string c_node = "eadheader";
    private const string c_atlangencoding = "langencoding";
    private const string c_atcountryencoding = "countryencoding";
    private const string c_atdateencoding = "dateencoding";
    private const string c_atrepositoryencoding = "repositoryencoding";
    private const string c_atscriptencoding = "scriptencoding";
    private const string c_atrelatedencoding = "relatedencoding";
    private const string c_ndeadid = NodeEadId.c_node;
    private const string c_ndfiledesc = NodeFileDesc.c_node;
    private const string c_ndprofiledesc = NodeProfileDesc.c_node;
    #endregion
  }
  public class NodeEadId : RawXml
  {
    private const string Name = nameof(NodeEadId);
    #region Attibute
    public string atIdentifier
    {
      get => ValueStr(c_atidentifier);
      set => SetValue(c_atidentifier, value);
    }
    public string atUrl
    {
      get => ValueStr(c_aturl);
      set => SetValue(c_aturl, value);
    }
    public string atEncodingAnalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeEadId(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "eadid";
    private const string c_atidentifier = "identifier";
    private const string c_aturl = "url";
    private const string c_atencodinganalog = "encodinganalog";
    #endregion
  }
  public class NodeFileDesc : RawXml
  {
    private const string Name = nameof(NodeFileDesc);
    #region Attibute
    #endregion
    #region Node
    public NodeTitleStmt ndTitleStmt { get; private set; }
    #endregion
    #region Constructor
    public NodeFileDesc(RawXml parXml) : base(parXml)
    {
      ndTitleStmt = GetOrAdd<NodeTitleStmt>(c_ndtitlestmt);
    }
    #endregion
    #region Constant
    internal const string c_node = "filedesc";
    private const string c_ndtitlestmt = NodeTitleStmt.c_node;
    #endregion
  }
  public class NodeTitleStmt : RawXml
  {
    private const string Name = nameof(NodeTitleStmt);
    #region Attibute
    #endregion
    #region Node
    public NodeTitleProper ndTitleProper { get; private set; }
    #endregion
    #region Constructor
    public NodeTitleStmt(RawXml parXml) : base(parXml)
    {
      ndTitleProper = GetOrAdd<NodeTitleProper>(c_ndtitleproper);
    }
    #endregion
    #region Constant
    public const string c_node = "titlestmt";
    private const string c_ndtitleproper = NodeTitleProper.c_node;
    #endregion
  }
  public class NodeTitleProper : RawXml
  {
    private const string Name = nameof(NodeTitleProper);
    #region Attibute
    public string atEncodingAnalog
    {
      get => ValueStr(c_atencodinganalog);
      set => SetValue(c_atencodinganalog, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeTitleProper(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "titleproper";
    private const string c_atencodinganalog = "encodinganalog";
    #endregion
  }
  public class NodeProfileDesc : RawXml
  {
    private const string Name = nameof(NodeProfileDesc);
    #region Attibute
    #endregion
    #region Node
    public NodeCreation ndCreation { get; private set; }
    public NodeLangusage ndLangusage { get; private set; }
    #endregion
    #region Constructor
    public NodeProfileDesc(RawXml parXml) : base(parXml)
    {
      ndCreation = GetOrAdd<NodeCreation>(c_ndcreation);
      ndLangusage = GetOrAdd<NodeLangusage>(c_ndlangusage);
    }
    #endregion
    #region Constant
    internal const string c_node = "profiledesc";
    private const string c_ndcreation = NodeCreation.c_node;
    private const string c_ndlangusage = NodeLangusage.c_node;
    #endregion
  }
  public class NodeCreation : RawXml
  {
    private const string Name = nameof(NodeCreation);
    #region Attribute
    #endregion
    #region Node
    public NodeDate ndDate { get; private set; }
    #endregion
    #region Constructor

    public NodeCreation(RawXml parXml) : base(parXml)
    {
      ndDate = GetOrAdd<NodeDate>(c_nddate);
    }
    #endregion

    #region Constant
    internal const string c_node = "creation";
    private const string c_nddate = NodeDate.c_node;
    #endregion
  }
  public class NodeDate : RawXml
  {
    private const string Name = nameof(NodeDate);
    #region Attibute
    public string atNormal
    {
      get => ValueStr(c_atnormal);
      set => SetValue(c_atnormal, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeDate(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "date";
    private const string c_atnormal = "normal";
    #endregion
  }
  public class NodeLangusage : RawXml
  {
    private const string Name = nameof(NodeLangusage);
    #region Attribute
    #endregion
    #region Node
    public NodeLanguage ndLanguage { get; private set; }
    #endregion
    #region Constructor

    public NodeLangusage(RawXml parXml) : base(parXml)
    {
      ndLanguage = GetOrAdd<NodeLanguage>(c_ndlanguage);
    }
    #endregion
    #region Constant
    internal const string c_node = "langusage";
    private const string c_ndlanguage = NodeLanguage.c_node;

    #endregion
  }
  public class NodeLanguage : RawXml
  {
    private const string Name = nameof(NodeLanguage);
    #region Attibute
    public string atLangcode
    {
      get => ValueStr(c_atlangcode);
      set => SetValue(c_atlangcode, value);
    }
    #endregion
    #region Node
    #endregion
    #region Constructor
    public NodeLanguage(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    internal const string c_node = "language";
    private const string c_atlangcode = "langcode";
    #endregion
  }
  #endregion
  public class AtomEadDoc : RawXml
  {
    private const string Name = nameof(AtomEadDoc);
    #region Attibute
    #endregion
    #region Node
    public NodeEadHeader ndHeader { get; private set; }
    public NodeEadArchDesc ndArchDesc { get; private set; }
    #endregion
    #region Constructor
    public AtomEadDoc(string parFileName) : base(parFileName, hc4x_NodeType.XmlFileOpen)
    {
      ndHeader = GetOrAdd<NodeEadHeader>(c_ndheader);
      ndArchDesc = GetOrAdd<NodeEadArchDesc>(c_ndarchdesc);

    }
    public new void Close()
    {
      ndHeader?.Close();
      ndArchDesc?.Close();
      base.Close();
    }
    #endregion
    #region Constant
    public const string c_sector = "ead";
    private const string c_ndheader = NodeEadHeader.c_node;
    private const string c_ndarchdesc = NodeEadArchDesc.c_node;
    #endregion
  }
}
