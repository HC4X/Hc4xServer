using System;
using System.Threading.Tasks;
using HC4xServer.Core;
using HC4xServer.Logic;
using LibServer;
using LibModel;

namespace HC4x_Server.Logic {
  /// <summary>Handles a single User</summary>
  public class HC4x_NodeUser : WebXml {
    private const string Name = nameof(HC4x_NodeUser);
    #region Attribute
    public int atId {
      get => ValueInt(c_pkey);
      set => SetValue(c_pkey, value);
      }
    public int atSelfUser {
      get => ValueInt(c_self);
      set => SetValue(c_self, value);
      }
    public string atName {
      get => ValueStr(c_name);
      set => SetValue(c_name, value);
      }
    public string atDesc {
      get => ValueStr(c_desc);
      set => SetValue(c_desc, value);
      }
    public string atPass {
      get => ValueStr(c_pass);
      set => SetValue(c_pass, value);
      }
    public string atEmail {
      get => ValueStr(c_email);
      set => SetValue(c_email, value);
      }
    public hc4x_UserType atType {
      get => ValueEnum<hc4x_UserType>(c_type);
      set => SetValue(c_type, value);
      }
    public hc4x_UserStatus atStatus {
      get => ValueEnum<hc4x_UserStatus>(c_self);
      set => SetValue(c_self, (int)value);
      }
    public string atConfirmCode => atName.Substring(0, 8);
    #endregion
    #region Method
    public bool ValidPassword(string parPassWord) { return (atPass == parPassWord); }
    public bool AlterPassword(string parCurPass, string parNewPass, string parNewPassConfirm) {
      bool retValue = false;
      try {
        if (ValidPassword(parCurPass)) {
          if (parNewPass == parNewPassConfirm) {
            retValue = true;
            }
          else {
            retValue = false;
            }
          }
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    public bool ConfirmCode(string parCode) {
      bool retValue = false;
      try {
        retValue = atConfirmCode == parCode;
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    #endregion
    #region Constructor
    public HC4x_NodeUser(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Static
    internal static string sqlUserOrGroup(string parEmail) { return (string.Format("{0} = '{1}'", c_email, parEmail)); }
    internal static string sqlUserByMail(string parEmail) { return (string.Format("{0} = {1} and {2} = '{3}'", c_type, (int)hc4x_UserType.User, c_email, parEmail)); }
    internal static string sqlUserById(string parId) { return (string.Format("{0} = {1}", c_pkey, GearBase.ParseInt(parId))); }
    #endregion
    #region Constant
    private const string c_email = "email";
    private const string c_pass = "passUser";
    private const string c_type = "typeUser";
    private const string c_desc = "descUser";
    private const string c_name = "nameUser";
    private const string c_self = "selfUser";
    private const string c_pkey = "pkeyUser";
    private const string c_node = "NodeUser";
    #endregion
    }
  /// <summary>Handles a User group</summary>
  public class HC4x_SectorUser : DiceBase {
    private const string Name = nameof(HC4x_SectorUser);
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Attribute
    public int atGroupId { get; private set; }
    public string atGroupName { get; private set; }
    #endregion
    #region Method
    internal bool UpdateGroup(HC4x_NodeUser parUser) {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try {
        sqlCommand = string.Format("UPDATE hc4xuser " + "SET selfUser = {0} " + "WHERE pkeyUser = {1}", (int)parUser.atStatus, parUser.atId);
        iRow = scData.ExecuteCommand(sqlCommand);
        retValue = (iRow == 1);
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateGroup)); }
      return (retValue);
      }
    public HC4x_NodeUser FindUserOrGroup(string parEmail) {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserOrGroup(parEmail), "");
        if (objTable.scRow.ndCount > 0) {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserOrGroup)); }
      return (retValue);
      }
    public HC4x_NodeUser FindUserById(string parId) {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserById(parId), "");
        if (objTable.scRow.ndCount == 1) {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserById)); }
      return (retValue);
      }
    public HC4x_NodeUser FindUserByMail(string parEmail) {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserByMail(parEmail), "");
        if (objTable.scRow.ndCount == 1) {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserByMail)); }
      return (retValue);
      }
    public bool UpdateUser(HC4x_NodeUser parUser) {
      bool retValue = false;
      try {
        throw new NotImplementedException();
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateUser)); }
      return (retValue);
      }
    public HC4x_NodeUser dbInsertUser(HC4x_NodeUser parUser) {
      int iIndentity;
      NodeTable objTable;
      try {
        objTable = ndCubeApp.scTable["hc4x_user_insert"];
        iIndentity = scData.InsertCommand("hc4xuser", objTable.scField, parUser);
        if (iIndentity > 0) parUser.atId = iIndentity;
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(dbInsertUser)); }
      return (parUser);
      }
    #endregion
    #region Constructor
    public bool Init() { return (InitData()); }
    public HC4x_SectorUser(PageCore parPageHandler) : base(parPageHandler, c_sector) { }
    //# public HC4x_SectorUser(PageCore parPageHandler, RawXml parXml) : base(parPageHandler, parXml) { }
    #endregion
    #region Constant
    private const string c_sector = "UserSector";
    #endregion
    }
  public class HC4x_UserHandler : RawPage {
    private const string Name = nameof(HC4x_UserHandler);
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Attribute
    #endregion
    #region Node
    private HC4x_SectorUser scUser { get; set; }
    #endregion
    #region Method
    internal RawMailMessage? RegisterMail(HC4x_NodeUser parUser, hc4x_SiteArea parSiteArea, string parEmlId, string parRedirect) {
      string strUrl;
      RackInterface objRack;
      RawMailMessage retValue;
      try {
        objRack = axMundi.ndCubeApp.rcInterface;
        switch (parSiteArea) {
          case hc4x_SiteArea.publicarea:
            retValue = objRack.PublicArea<RawMailMessage>(parEmlId);
            break;
          case hc4x_SiteArea.privatearea:
            retValue = objRack.PrivateArea<RawMailMessage>(parEmlId);
            break;
          default:
            return (null);
          }
        strUrl = ndRoute.FullPublicRoute("confirm-email", parUser.atId + ";" + parUser.atConfirmCode, true);
        retValue.atSubject = retValue.atTitle;
        retValue.AddTo(parUser.atEmail, parUser.atDesc);
        retValue.AddBodyPlain(string.Format(retValue.atHeader, strUrl));
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(RegisterMail)); }
      return (retValue);
      }
    internal bool AlterUserPass() {
      bool retValue = false;
      try {

        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(AlterUserPass)); }
      return (retValue);
      }
    internal bool ConfirmUserEMail() {
      bool retValue = false;
      string strQuery;
      string[] arQuery;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      try {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        strQuery = axRequest.QueryString();
        arQuery = GearAux.SyntaxSplit(strQuery, true);
        objUser = scUser.FindUserById(arQuery[0]);
        if (objUser == null) {
          atMessage = "Usuário não encontrado";
          return (retValue);
          }
        switch (objUser.atStatus) {
          case hc4x_UserStatus.Admin:
          case hc4x_UserStatus.User:
            atMessage = "Código Validado com sucesso!";
            retValue = true;
            break;
          case hc4x_UserStatus.Anonymous:
          case hc4x_UserStatus.Waiting:
            if (objUser.ConfirmCode(arQuery[1])) {
              objUser.atStatus = hc4x_UserStatus.User;
              if (retValue = scUser.UpdateGroup(objUser))
                atMessage = "Código Validado com sucesso!";
              }
            else
              atMessage = "Código inválido";
            break;
          default:
            atMessage = "O usuário encontra-se desativado. Favor reativar o usuário antes de confirmar o código";
            break;
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ConfirmUserEMail)); }
      return (retValue);
      }
    internal bool RegisterUser() {
      bool retValue = false;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      RawMailMessage objMail;
      try {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        //# Além do "required" no campo senha do form tem que verificar do lado server se a senha foi passada
        if (string.IsNullOrWhiteSpace(objPostUser.atPass)) {
          atMessage = "Senha não pode ser vazia";
          return (retValue);
          }
        if (objPostUser.atPass != objPostUser.atPass_2) {
          atMessage = "Senha não confere com a repetição da senha";
          return (retValue);
          }
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser != null) {
          atMessage = "Usuário já existe no banco de dados";
          }
        else {
          objUser = objPostUser.CreateNodeUser();
          if (scUser.dbInsertUser(objUser) != null) {
            objMail = RegisterMail(objUser, hc4x_SiteArea.publicarea, "confirm-email_eml", "confirm-email"); //#, $"{objUser.atId};{objUser.atName.Substring(0, 8)}");//mandar o emaill para o usuário
            if (objMail != null) axResponse.SendMailAsync(objMail);
            retValue = true;
            }
          atMessage = "Usuário cadastrado com sucesso!";
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(RegisterUser)); }
      return (retValue);
      }
    internal bool ValidateUserPass() {
      bool retValue = false;
      string strCrypto;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      try {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser == null) {
          atMessage = "Usuário ou senha inválidos";
          return (retValue);
          }
        strCrypto = objPostUser.EncryptUTF8(objPostUser.atPass);
        if (!objUser.ValidPassword(strCrypto)) {
          atMessage = "Usuário ou senha inválidos";
          return (retValue);
          }
        switch (objUser.atStatus) {
          case hc4x_UserStatus.None:
            //# Não é preciso fazer nada
            break;
          case hc4x_UserStatus.Admin:
          case hc4x_UserStatus.User:
            if (axSession.SetCredentials(objUser.atId, objUser.atSelfUser, objUser.atEmail))
              axMundi.RedirectTo(hc4x_SiteArea.privatearea, "index");
            retValue = true;
            break;
          case hc4x_UserStatus.Anonymous:
          case hc4x_UserStatus.Waiting:
            atMessage = "Seu cadastro não está validado. Você só pode logar depois que validar o seu email." + string.Format("<a href={0}>Clique Aqui</a>", "/publicarea/pt/confirm-email/" + objUser.atId);
            break;
          case hc4x_UserStatus.Suspended:
          case hc4x_UserStatus.Canceled:
            atMessage = "Seu cadastro encontra-se suspenso ou cancelado. Deseja reativar?";
            break;
          default:
            //# erro de programação. Por exemplo, o usuário está em um grupo que não está listado em hc4x_UserStatus
            atMessage = "Seu login não pode ser validado. Entre em contato com o administrador do sistema";
            break;
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ValidateUserPass)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool Init(PageCore parMundi) {
      bool retValue = false;
      try {
        if (base.Init(parMundi)) {
          scUser = new HC4x_SectorUser(axMundi);
          retValue = scUser.Init();
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
      }
    #endregion
    }
  }