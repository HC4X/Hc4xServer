using System;
using System.Threading.Tasks;
using LibServer;
using LibModel;
using HC4xServer.Core;

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
      }
    public int atStoneProductId {
      get => ValueInt(c_pkeystoneproduct);
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
    private const string c_node = "NodeSlab";
    #endregion
    }
  public class NodeProduct : RawXml {
    private const string Name = nameof(NodeProduct);
    #region Node
    internal NodeSlab ndSlab { get; private set; }
    #endregion
    #region Attribute
    public int atPkeyStoneProduct {
      get => ValueInt(c_pkeystoneproduct);
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
    internal static string sqlUserById(int parId) { return (string.Format("{0} = {1}", c_pkeystoneproduct, parId)); }
    #endregion
    #region Constructor
    public NodeProduct(RawXml parXml) : base(parXml) {
      ndSlab = GetOrAdd<NodeSlab>(c_nodeslab);
      }
    #endregion
    #region Constant
    private const string c_pkeystoneproduct = "pkeyStoneProduct";
    private const string c_pkeycustomer = "pkeyCustomer";
    private const string c_color = "color";
    private const string c_description = "description";
    private const string c_fichaTecnica = "fichaTecnicaProduct1";
    private const string c_apresentacao = "apresentacao";
    private const string c_node = "NodeProduct";
    private const string c_nodeslab = "NodeSlab";
    #endregion
    }
  public class SectorProduct : RawXml {
    private const string Name = nameof(SectorProduct);
    #region Node 
    internal NodeProduct ndProduct { get; private set; }
    #endregion
    #region Constructor
    public bool Init() { ndProduct = GetOrAdd<NodeProduct>(c_nodeproduct); return (true); }
    public SectorProduct(RawXml parXml) : base(parXml) { }
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
    public NodeProduct FindProductById(int parId) {
      NodeProduct retValue = null;
      RawTable objTable;
      try {
        objTable = scData.SelectCommand("*", "stoneproduct", NodeProduct.sqlUserById(parId), "");
        if (objTable.scRow.ndCount == 1) {
          retValue = GetOrAdd<NodeProduct>(nameof(NodeProduct));
          retValue.Update(objTable.scRow[0]);
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindProductById)); }
      return (retValue);
      }
    public bool SlabToXml() {
      bool retValue = false;
      RawTable objTable;
      RawRow[] arNode;
      try {
        objTable = scData.SelectCommand("*", "vw_stoneproductslab", string.Format("pkeycustomer={0}", atPkeyCustomer), "");
        arNode = objTable.scRow.ArrayNode();
        foreach (RawRow itNode in arNode) {

          }
        retValue = true;
        //for (int i = 0; i < objTable.scRow.ndCount; i++)
        //{
        //  arNodeView[i] = new NaturalView(axMundi);
        //  RawRow row = objTable.scRow[i];
        //  arNodeView[i].Update(objTable.scRow[i]);
        //}
        //int valorMaximo = arNodeView.Max(obj => obj.atPkeyStoneProduct);
        //NodeProduct[] arNodeProduct = new NodeProduct[valorMaximo];
        //for (int i = 0; i <  valorMaximo; i++) {
        //  arNodeProduct[i] = FindProductById((i+1));
        //}
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(SlabToXml)); }
      return (retValue);
      }
    public bool ProductToXml() { return (true); } //Preencher cada nodeproduct
    public bool CustomerToXml() {
      bool retValue = false;
      RawTable objTable;
      RawRow objRow;
      try {
        objTable = scData.SelectCommand("pkeyCustomer, namecustomer, razaoSocial, emailContact, site", "hc4xcustomer", string.Format("pkeycustomer={0}", atPkeyCustomer), "");
        objRow = objTable.scRow[0];
        atRazaoSocial = objRow.ValueStr(c_razaosocial);
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
  //#public class NaturalStone_XXX : DiceBase {
  //#  private const string Name = nameof(NaturalStone_XXX);
  //#  #region Axis
  //#  private new PageCore axMundi => (PageCore)base.axMundi;
  //#  #endregion
  //#  #region Method
  //#  public NodeProduct FindProductById(int parId) {
  //#    NodeProduct retValue = null;
  //#    RawTable objTable;
  //#    try {
  //#      objTable = scData.SelectCommand("*", "stoneproduct", NodeProduct.sqlUserById(parId), "");
  //#      if (objTable.scRow.ndCount == 1) {
  //#        retValue = new NodeProduct(axMundi);
  //#        retValue.Update(objTable.scRow[0]);
  //#        }
  //#      }
  //#    catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindProductById)); }
  //#    return (retValue);
  //#    }
  //#  #endregion
  //#  #region Constructor
  //#  public bool Init() { return (InitData()); }
  //#  public NaturalStone_XXX(PageCore parPageHandler) : base(parPageHandler, c_sector) { }
  //#  #endregion
  //#  #region Constant
  //#  private const string c_sector = "NaturalStoneSector";
  //#  #endregion
  //#  }
  public class HyperStone_Handler : RawPage {
    private const string Name = nameof(HyperStone_Handler);
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Node
    private NaturalStone scNatural { get; set; }
    #endregion
    #region RawPage
    public override bool ActionGet(string parPageId) {
      bool retValue = false;
      int pkeyCustomer = 3;
      try {
        if (scNatural.Init(pkeyCustomer))
          if (scNatural.CustomerToXml())
            if (scNatural.ProductToXml())
              retValue = scNatural.SlabToXml();
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionGet)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public override bool Init(AxisMundi parMundi) {
      bool retValue = false;
      try {
        if (!base.Init(parMundi)) return (retValue);
        switch (axMundi.atRequestMethod) {
          case RequestMethod.Post:
            scNatural = new NaturalStone(this);
            retValue = InitData();
            break;
          case RequestMethod.Get:
            scNatural = new NaturalStone(this);
            retValue = InitData();
            break;
          default:
            retValue = true;
            break;
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
      }
    #endregion
    }
  }
