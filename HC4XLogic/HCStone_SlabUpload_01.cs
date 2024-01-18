using System;
using HC4xServer.Core;
using LibServer;
using LibModel;
using System.Threading.Tasks;

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
    #region Constructor
    #endregion
    }
  public class NodeSlab : WebXml {
    #region Attribute
    public int atId {
      get => ValueInt(c_pkey);
      set => SetValue(c_pkey, value);
      }
    public float atArea {
      get => ValueFlt(c_area);
      set => SetValue(c_area, value);
      }
    public int atStoneProductId {
      get => ValueInt(c_pkeyStoneProduct);
      set => SetValue(c_pkeyStoneProduct, value);
      }
    public string atUrl {
      get => ValueStr(c_url);
      set => SetValue(c_url, value);
      }
    public string atQuality {
      get => ValueStr(c_quality);
      set => SetValue(c_quality, value);
      }
    public float atThickness {
      get => ValueFlt(c_thickness);
      set => SetValue(c_thickness, value);
      }
    public float atWeight {
      get => ValueFlt(c_weight);
      set => SetValue(c_weight, value);
      }
    public string atBundle {
      get => ValueStr(c_bundle);
      set => SetValue(c_bundle, value);
      }
    #endregion
    #region Constructor
    public NodeSlab(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Constant
    private const string c_pkey = "pkeyStoneSlab";
    private const string c_pkeyStoneProduct = "pkeyStoneProduct";
    private const string c_area = "area";
    private const string c_thickness = "thickness";
    private const string c_weight = "weight";
    private const string c_quality = "quality";
    private const string c_bundle = "bundle";
    private const string c_url = "url";
    public const string c_node = "NodeSlab";
    #endregion
    }
  public class NodeProduct : WebXml {
    private const string Name = nameof(NodeProduct);
    #region Attribute

    public int atId {
      get => ValueInt(c_pkey_stone_product);
      set => SetValue(c_pkey_stone_product, value);
      }
    public int atPkeyCustomer {
      get => ValueInt(c_pkey_customer);
      set => SetValue(c_pkey_customer, value);
      }
    public string atDescription {
      get => ValueStr(c_description);
      set => SetValue(c_description, value);
      }
    public string atColor {
      get => ValueStr(c_color);
      set => SetValue(c_color, value);
      }
    public string atFichaTecnica {
      get => ValueStr(c_ficha_tecnica);
      set => SetValue(c_ficha_tecnica, value);
      }
    public string atApresentacao {
      get => ValueStr(c_apresentacao);
      set => SetValue(c_apresentacao, value);
      }
    #endregion
    #region Node
    public SectorNode scSlab { get; private set; }
    #endregion
    #region Constructor
    public NodeProduct(PageCore parMundi) : base(parMundi, c_node) { scSlab = SectorOf<NodeSlab>(NodeSlab.c_node, ""); }
    #endregion
    #region Constant
    private const string c_pkey_stone_product = "pkeyStoneProduct";
    private const string c_pkey_customer = "pkeyCustomer";
    private const string c_color = "color";
    private const string c_description = "description";
    private const string c_ficha_tecnica = "fichaTecnica";
    private const string c_apresentacao = "apresentacao";
    public const string c_node = "NodeProduct";
    #endregion
    }
  public class SectorBlock : WebXml {
    #region Attribute
    #endregion
    #region Constructor
    public SectorBlock(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Constant
    private const string c_node = "SectorBlock";
    #endregion
    }
  public class SectorProduct : WebXml {
    private const string Name = nameof(SectorProduct);
    #region Attribute
    #endregion
    #region Node
    public SectorNode scProduct { get; private set; }
    #endregion
    #region Constructor
    public SectorProduct(PageCore parMundi) : base(parMundi, c_node) { scProduct = SectorOf<NodeProduct>(NodeProduct.c_node, ""); }
    #endregion
    #region Constant
    internal const string c_node = "SectorProduct";
    #endregion
    }
  public class NaturalStone : WebXml {
    private const string Name = nameof(NaturalStone);
    #region Attribute
    public int atId {
      get => ValueInt(c_pkey_customer);
      set => SetValue(c_pkey_customer, value);
      }
    public DateTime atUpdate {
      get => ValueDateTime(c_update);
      set => SetValue(c_update, value);
      }
    public string atName {
      get => ValueStr(c_name);
      set => SetValue(c_name, value);
      }
    public string atRazaoSocial {
      get => ValueStr(c_razao_social);
      set => SetValue(c_razao_social, value);
      }
    public string atCnpjCpf {
      get => ValueStr(c_cnpj_cpf);
      set => SetValue(c_cnpj_cpf, value);
      }
    public string atNameContact {
      get => ValueStr(c_name_contact);
      set => SetValue(c_name_contact, value);
      }

    public string atEmailContact {
      get => ValueStr(c_email_contact);
      set => SetValue(c_email_contact, value);
      }

    public string atSite {
      get => ValueStr(c_site);
      set => SetValue(c_site, value);
      }
    public string atDescCustomer {
      get => ValueStr(c_desc_customer);
      set => SetValue(c_desc_customer, value);
      }
    public string atLogoCustomer {
      get => ValueStr(c_logo_customer);
      set => SetValue(c_logo_customer, value);
      }
    public string atFolderCustomer {
      get => ValueStr(c_folder_customer);
      set => SetValue(c_folder_customer, value);
      }
    #endregion
    #region Node
    public SectorProduct scProduct { get; private set; }
    #endregion
    #region Constructor
    public NaturalStone(PageCore parMundi) : base(parMundi, c_node) { scProduct = GetOrAdd<SectorProduct>(SectorProduct.c_node); }
    #endregion
    #region Constant
    private const string c_name = "namecustomer";
    private const string c_email_contact = "emailContact";
    private const string c_update = "atUpdate";
    private const string c_pkey_customer = "pkeyCustomer";
    private const string c_razao_social = "razaoSocial";
    private const string c_cnpj_cpf = "cnpjCpf";
    private const string c_site = "site";
    private const string c_desc_customer = "descCustomer";
    private const string c_logo_customer = "logoCustomer";
    private const string c_folder_customer = "folderCustomer";
    private const string c_name_contact = "nameContact";
    private const string c_node = "NaturalStone";
    #endregion
    }
  public class HyperStone_Handler : RawPage {
    private const string Name = nameof(HyperStone_Handler);
    #region Node
    public NaturalStone ndNaturalStone { get; private set; }
    #endregion
    #region RawPage
    public override bool ActionGet(string parPageId) {
      bool retValue = false;
      NodeProduct[] arNode;
      try {
        ndNaturalStone = GearXml.ParseFile<NaturalStone>("Caminho completo para o arquivo xml.");
        arNode = ndNaturalStone.scProduct.scProduct.ArrayOf<NodeProduct>();
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionGet)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    #endregion
    }
  }
