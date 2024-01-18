using System;
using System.Threading.Tasks;
using LibServer;
using LibModel;
using HC4xServer.Core;
using System.Linq;

namespace HC4x_Server.HCStone {
  public class SlabUpload : RawPage {
    private const string Name = nameof(SlabUpload);
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region RawPage
    public override bool ActionGet(string parPageId) { return true; }
    public override bool ActionPost(string parPageId) {
      bool retValue = false;
      string strWwwPath;
      NodeFormFile[] arFormFile;
      Task<bool> objTask;
      try {
        arFormFile = axRequest.FileKey();
        strWwwPath = GearPath.Combine(axMundi.atWebPath, "Upload");
        foreach (NodeFormFile itFile in arFormFile) {
          strWwwPath = itFile.GetSafeName(strWwwPath);
          objTask = Task.Run(() => itFile.SaveLocalServer(strWwwPath));
          if (!objTask.Result) break;
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionPost)); }
      return (retValue);
      }
    #endregion
    }
  public class NodeSlab : RawXml {
    private const string Name = nameof(NodeSlab);
    #region Attribute
    public int atStoneSlabId {
      get => ValueInt(c_pkeystoneslab);
      set => SetValue(c_pkeystoneslab, value);
      }
    public int atStoneProductId {
      get => ValueInt(c_pkeystoneproduct);
      set => SetValue(c_pkeystoneproduct, value);
      }
    public float atArea {
      get => ValueFlt(c_area);
      set => SetValue(c_area, value);
      }
    public float atThickness {
      get => ValueFlt(c_thickness);
      set => SetValue(c_thickness, value);
      }
    public float atWeight {
      get => ValueFlt(c_weight);
      set => SetValue(c_weight, value);
      }
    public string atQuality {
      get => ValueStr(c_quality);
      set => SetValue(c_quality, value);
      }
    public string atBundle {
      get => ValueStr(c_bundle);
      set => SetValue(c_bundle, value);
      }
    public string atUrl {
      get => ValueStr(c_url);
      set => SetValue(c_url, value);
      }
    #endregion
    #region Constructor
    public NodeSlab(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant 
    private const string c_pkeystoneslab = "pkeyStoneSlab";
    private const string c_pkeystoneproduct = "pkeyStoneProduct";
    private const string c_area = "area";
    private const string c_thickness = "thickness";
    private const string c_weight = "weight";
    private const string c_quality = "quality";
    private const string c_bundle = "bundle";
    private const string c_url = "url";
    internal const string c_node = "NodeSlab";
    #endregion
    }
  public class NodeProduct : RawXml {
    private const string Name = nameof(NodeProduct);
    #region Attribute
    public string atPkeyStoneProduct {
      get => ValueStr(c_pkeystoneproduct);
      set => SetValue(c_pkeystoneproduct, value);
      }
    public int atPkeyCustomer {
      get => ValueInt(c_pkeycustomer);
      set => SetValue(c_pkeycustomer, value);
      }
    public string atColor {
      get => ValueStr(c_color);
      set => SetValue(c_color, value);
      }
    public string atDescription {
      get => ValueStr(c_description);
      set => SetValue(c_description, value);
      }
    public string atFichaTecnica {
      get => ValueStr(c_fichaTecnica);
      set => SetValue(c_fichaTecnica, value);
      }
    public string atApresentacao {
      get => ValueStr(c_apresentacao);
      set => SetValue(c_apresentacao, value);
      }
    #endregion
    #region Method
    public bool SlabAdd(NodeSlab parSlab) { NodeAdd(parSlab); return true; }
    public NodeSlab CreateSlab() { return (NodeCreate<NodeSlab>(NodeSlab.c_node)); }
    #endregion
    #region Constructor
    public NodeProduct(RawXml parXml) : base(parXml) { }
    #endregion
    #region Constant
    private const string c_pkeystoneproduct = "pkeyStoneProduct";
    private const string c_pkeycustomer = "pkeyCustomer";
    private const string c_color = "color";
    private const string c_description = "description";
    private const string c_fichaTecnica = "fichaTecnicaProduct1";
    private const string c_apresentacao = "apresentacao";
    internal const string c_node = "NodeProduct";
    private const string c_nodeslab = "NodeSlab";
    #endregion
    }
  public class SectorProduct : RawXml {
    private const string Name = nameof(SectorProduct);
    #region Node 
    internal SectorNode scProduct { get; private set; }
    #endregion
    #region Method
    public bool SlabAdd(NodeSlab parSlab) { NodeAdd(parSlab); return true; }
    public NodeProduct GetProduct(string parKey) { return (scProduct.ValueAs<NodeProduct>(parKey)); }

    public NodeSlab GetSlab(string parKey) { return (scProduct.ValueAs<NodeSlab>(parKey)); }

    public bool ProductAdd(NodeProduct parProduct) { NodeAdd(parProduct); scProduct.Add(parProduct.atPkeyStoneProduct, parProduct); return (true); }
    public NodeProduct CreateProduct() { return (NodeCreate<NodeProduct>(NodeProduct.c_node)); }
    public NodeSlab CreateSlab() { return (NodeCreate<NodeSlab>(NodeSlab.c_node)); }
    #endregion
    #region Constructor
    public SectorProduct(RawXml parXml) : base(parXml) { scProduct = new SectorNode(); }
    #endregion
    #region Constant
    private const string c_nodeproduct = "NodeProduct";
    private const string c_sector = "SectorProduct";
    #endregion
    }
  public class NaturalStone : WebXml {
    private const string Name = nameof(NaturalStone);
    #region Axis
    private ServerData scData => ndStoneHandler.scData;
    #endregion
    #region Node 
    internal HyperStone_Handler ndStoneHandler { get; private set; }
    internal SectorProduct scProduct { get; private set; }
    #endregion
    #region Attribute
    public int atPkeyCustomer {
      get => ValueInt(c_pkeycustomer);
      set => SetValue(c_pkeycustomer, value);
      }
    public string atNameCustomer {
      get => ValueStr(c_namecustomer);
      set => SetValue(c_namecustomer, value);
      }
    public string atEmailCustomer {
      get => ValueStr(c_emailcontact);
      set => SetValue(c_emailcontact, value);
      }
    public string atRazaoSocial {
      get => ValueStr(c_razaosocial);
      set => SetValue(c_razaosocial, value);
      }
    public string atCnpjCpf {
      get => ValueStr(c_cnpjCpf);
      set => SetValue(c_cnpjCpf, value);
      }
    public string atSite {
      get => ValueStr(c_site);
      set => SetValue(c_site, value);
      }
    public string atDescCustomer {
      get => ValueStr(c_desccustomer);
      set => SetValue(c_desccustomer, value);
      }
    public string atLogoCustomer {
      get => ValueStr(c_logocustomer);
      set => SetValue(c_logocustomer, value);
      }
    public string atFolderCustomer {
      get => ValueStr(c_foldercustomer);
      set => SetValue(c_foldercustomer, value);
      }
    public string atNameContact {
      get => ValueStr(c_namecontact);
      set => SetValue(c_namecontact, value);
      }
    #endregion
    #region Method
    public int FindProductIdBySlab(int parPkeySlab) {
      int retValue = 0;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("pkeyStoneProduct", "vw_stoneproductslab", $"pkeyStoneSlab={parPkeySlab}", "");
        if (objTable.scRow.ndCount == 1) {
          retValue = objTable.scRow[0].ValueInt("pkeyStoneProduct");
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindProductIdBySlab)); }
      return (retValue);
    }
    public bool SlabToXml(int parPkeyStoneProduct, hc4x_Environment environment) { //ALTERAR AQUI TAMBEEEEEM
      bool retValue = false;
      NodeProduct objProduct;
      NodeSlab objSlab;
      RawTable objTable;
      RawRow[] arNode;
      try {
        objTable = scData.SelectCommand("*", "vw_stoneproductslab", string.Format("pkeystoneproduct={0}", parPkeyStoneProduct), "");
        arNode = objTable.scRow.ArrayNode();
        foreach (RawRow itNode in arNode)
        {
          objProduct = scProduct.GetProduct(itNode.ValueStr("pkeyStoneProduct"));
          objSlab = objProduct.CreateSlab();
          objSlab.atStoneProductId = itNode.ValueInt("pkeyStoneProduct");
          objSlab.atStoneSlabId = itNode.ValueInt("pkeyStoneSlab");
          if (environment == hc4x_Environment.Development)
          {
            objSlab.atUrl = "https://dev.hypercube4x.com/" + itNode.ValueStr("url");
          }
          else
            objSlab.atUrl = "https://hypercube4x.com/" + itNode.ValueStr("url");
          objSlab.atWeight = itNode.ValueFlt("weight");
          objSlab.atArea = itNode.ValueFlt("area");
          objSlab.atThickness = itNode.ValueFlt("thickness");
          objSlab.atQuality = itNode.ValueStr("quality");
          objSlab.atBundle = itNode.ValueStr("bundle");
          objProduct.SlabAdd(objSlab);
        }
        retValue = true;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(SlabToXml)); }
      return (retValue);
    }
    public bool OnlySlabToXml(int parPkeyStoneSlab, hc4x_Environment environment) {
      bool retValue = false;
      NodeSlab objSlab;
      RawTable objTable;
      RawRow[] arNode;
      try
      {
        objTable = scData.SelectCommand("*", "stoneslab", string.Format("pkeystoneslab={0}", parPkeyStoneSlab), "");
        arNode = objTable.scRow.ArrayNode();
        objSlab = scProduct.CreateSlab();
        foreach (RawRow itNode in arNode)
        {
          objSlab.atStoneProductId = itNode.ValueInt("pkeyStoneProduct");
          objSlab.atStoneSlabId = itNode.ValueInt("pkeyStoneSlab");
          if (environment == hc4x_Environment.Development)
          {
            objSlab.atUrl = "https://dev.hypercube4x.com/" + itNode.ValueStr("url");
          }
          else
            objSlab.atUrl = "https://hypercube4x.com/" + itNode.ValueStr("url");
          objSlab.atWeight = itNode.ValueFlt("weight");
          objSlab.atArea = itNode.ValueFlt("area");
          objSlab.atThickness = itNode.ValueFlt("thickness");
          objSlab.atQuality = itNode.ValueStr("quality");
          objSlab.atBundle = itNode.ValueStr("bundle");
          scProduct.SlabAdd(objSlab);
        }
        retValue = true;
        string x = atXmlDocument;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(OnlySlabToXml)); }
      return (retValue);
    }
    public bool SlabToXml(hc4x_Environment environment) {
      bool retValue = false;
      NodeProduct objProduct;
      NodeSlab objSlab;
      RawTable objTable;
      RawRow[] arNode;
      try {
        objTable = scData.SelectCommand("*", "vw_stoneproductslab", string.Format("pkeycustomer={0}", atPkeyCustomer), "");
        arNode = objTable.scRow.ArrayNode();
        foreach (RawRow itNode in arNode) {
          objProduct = scProduct.GetProduct(itNode.ValueStr("pkeyStoneProduct"));
          objSlab = objProduct.CreateSlab();
          
          objSlab.atStoneProductId = itNode.ValueInt("pkeyStoneProduct");
          objSlab.atStoneSlabId = itNode.ValueInt("pkeyStoneSlab");
          if (environment == hc4x_Environment.Development)
          {
            objSlab.atUrl = "https://dev.hypercube4x.com/" + itNode.ValueStr("url");
          }
          else
            objSlab.atUrl = "https://hypercube4x.com/" + itNode.ValueStr("url");
          objSlab.atWeight = itNode.ValueFlt("weight");
          objSlab.atArea = itNode.ValueFlt("area");
          objSlab.atThickness = itNode.ValueFlt("thickness");
          objSlab.atQuality = itNode.ValueStr("quality");
          objSlab.atBundle = itNode.ValueStr("bundle");
          objProduct.SlabAdd(objSlab);
          }
        retValue = true;
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(SlabToXml)); }
      return (retValue);
      }
    public bool ProductToXml() {
      bool retValue = true;
      NodeProduct objProduct;
      RawTable objTable;
      RawRow[] arNode;
      try {
        objTable = scData.SelectCommand("pkeyStoneProduct,pkeyCustomer,color,description,fichaTecnica,apresentacao", "stoneproduct", string.Format("pkeyCustomer={0}", atPkeyCustomer), "");
        arNode = objTable.scRow.ArrayNode();
        foreach (RawRow itNode in arNode) {
          objProduct = scProduct.CreateProduct();
          objProduct.atPkeyStoneProduct = itNode.ValueStr("pkeyStoneProduct");
          objProduct.atPkeyCustomer = atPkeyCustomer;
          objProduct.atColor = itNode.ValueStr("color");
          objProduct.atDescription = itNode.ValueStr("description");
          objProduct.atFichaTecnica = itNode.ValueStr("fichaTecnica");
          objProduct.atApresentacao = itNode.ValueStr("apresentacao");
          scProduct.ProductAdd(objProduct);
          }
        retValue = true;
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(CustomerToXml)); }
      return (retValue);
      }
    public bool ProductToXml(int parPkeyStoneProduct) {
      bool retValue = true;
      NodeProduct objProduct;
      RawTable objTable;
      RawRow[] arNode;
      try
      {
        objTable = scData.SelectCommand("pkeyStoneProduct,pkeyCustomer,color,description,fichaTecnica,apresentacao", "stoneproduct", $"pkeyCustomer = {atPkeyCustomer=3}  AND pkeyStoneProduct={parPkeyStoneProduct}", "");
        arNode = objTable.scRow.ArrayNode();
        foreach (RawRow itNode in arNode)
        {
          objProduct = scProduct.CreateProduct();
          objProduct.atPkeyStoneProduct = itNode.ValueStr("pkeyStoneProduct");
          objProduct.atPkeyCustomer = atPkeyCustomer;
          objProduct.atColor = itNode.ValueStr("color");
          objProduct.atDescription = itNode.ValueStr("description");
          objProduct.atFichaTecnica = itNode.ValueStr("fichaTecnica");
          objProduct.atApresentacao = itNode.ValueStr("apresentacao");
          scProduct.ProductAdd(objProduct);
        }
        retValue = true;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(CustomerToXml)); }
      return (retValue);
    }
    public bool CustomerToXml() {
      bool retValue = false;
      RawTable objTable;
      RawRow objRow;
      try {
        objTable = scData.SelectCommand("pkeyCustomer, namecustomer, razaoSocial, emailContact, site", "hc4xcustomer", string.Format("pkeycustomer={0}", atPkeyCustomer), "");
        objRow = objTable.scRow[0];
        atRazaoSocial = objRow.ValueStr(c_razaosocial);
        atNameCustomer = objRow.ValueStr(c_namecustomer);
        atEmailCustomer = objRow.ValueStr(c_emailcontact);
        atSite = objRow.ValueStr(c_site);
        retValue = true;
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(CustomerToXml)); }
      return (retValue);
      }
    public bool CustomerToXml(int parPkeyCustomer)
    {
      bool retValue = false;
      RawTable objTable;
      RawRow objRow;
      try
      {
        if(parPkeyCustomer == 0)
          parPkeyCustomer = 3;
        
        objTable = scData.SelectCommand("pkeyCustomer, namecustomer, razaoSocial, emailContact, site", "hc4xcustomer", string.Format("pkeycustomer={0}", parPkeyCustomer), "");
        objRow = objTable.scRow[0];
        atRazaoSocial = objRow.ValueStr(c_razaosocial);
        atNameCustomer = objRow.ValueStr(c_namecustomer);
        atEmailCustomer = objRow.ValueStr(c_emailcontact);
        atSite = objRow.ValueStr(c_site);
        retValue = true;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(CustomerToXml)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public bool Init(int parkeyCustomer) { atPkeyCustomer = parkeyCustomer; return (true); }
    public NaturalStone(HyperStone_Handler parStoneHandler) : base(parStoneHandler.axMundi, c_node) {
      ndStoneHandler = parStoneHandler;
      scProduct = GetOrAdd<SectorProduct>(c_sectorproduct);
      }
    #endregion
    #region Constant
    private const string c_pkeycustomer = "pkeycustomer";
    private const string c_namecustomer = "namecustomer";
    private const string c_emailcontact = "emailContact";
    private const string c_razaosocial = "razaoSocial";
    private const string c_cnpjCpf = "cnpjCpf";
    private const string c_site = "site";
    private const string c_desccustomer = "descCustomer";
    private const string c_logocustomer = "logoCustomer";
    private const string c_foldercustomer = "folderCustomer";
    private const string c_namecontact = "nameContact";
    private const string c_node = "NaturalStone";
    private const string c_sectorproduct = "SectorProduct";
    #endregion
    }
  public class HyperStone_Handler : RawRest {
    private const string Name = nameof(HyperStone_Handler);
    #region Axis
    private new RestCore axMundi => (RestCore)base.axMundi;
    #endregion
    #region Node
    private NaturalStone scNatural { get; set; }
    #endregion
    #region RawRest
    public override bool ActionResponse() { return (axResponse.ResponseXml(scNatural)); }
    public override bool ActionPost(string parPageId) { return CreateXml(0); }
    public override bool ActionGet(string parPageId)
    {
      string strQuery;
      string[] arQuery;
      strQuery = axRequest.QueryString();
      arQuery = GearAux.Split(axRequest.QueryString(), "/").Select(s => s.Trim()).ToArray();

      int[] intArgs = arQuery.Select(s => GearBase.ParseInt(s)).ToArray();

      return CreateXml(intArgs);
    }

    private bool CreateXml(params int[] intArgs) {
      bool retValue = false;
      try {
        if(intArgs.Length == 0) {
          return retValue;
        }
        else {
          if (intArgs.Length == 1)  {
            if (scNatural.Init(intArgs[0]))
              if (scNatural.CustomerToXml())
                if (scNatural.ProductToXml())
                  retValue = scNatural.SlabToXml(axMundi.ndServer.atEnvironment);
          }
          else if (intArgs.Length == 2) { 
            if (scNatural.CustomerToXml(intArgs[0]))
              if (scNatural.ProductToXml(intArgs[1]))
              retValue = scNatural.SlabToXml(intArgs[1], axMundi.ndServer.atEnvironment);
          }
          else if(intArgs.Length == 3) {
            return retValue;
          }
          else if (intArgs.Length == 4) {
            if (intArgs[1] == 0) {
              intArgs[1] = scNatural.FindProductIdBySlab(intArgs[3]);
            }
            if (scNatural.CustomerToXml(intArgs[0]))
              if (scNatural.ProductToXml(intArgs[1]))
                retValue = scNatural.OnlySlabToXml(intArgs[3], axMundi.ndServer.atEnvironment);
           }
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(CreateXml)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public override bool Init(AxisMundi parMundi) {
      bool retValue = false;
      try {
        if (!base.Init(parMundi)) return (retValue);
        //#switch (axMundi.atRequestMethod) {
        //#  case RequestMethod.Post:
        //#    scNatural = new NaturalStone(this);
        //#    retValue = InitData();
        //#    break;
        //#  case RequestMethod.Get:
        scNatural = new NaturalStone(this);
        retValue = InitData();
        //#  break;
        //#default:
        //#  retValue = true;
        //#  break;
        //#}
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
      }
    #endregion
    }
  }
