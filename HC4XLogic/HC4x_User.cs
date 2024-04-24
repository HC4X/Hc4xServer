using HC4xServer.Core;
using HC4xServer.Logic;
using LibModel;
using LibServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HC4x_Server.Logic
{
  /// <summary>Handles a single User</summary>
  public class HC4x_NodeUser : WebXml
  {
    #region Name
    private const string Name = nameof(HC4x_NodeUser);
    #endregion
    #region Attribute
    public int atId
    {
      get => ValueInt(c_pkey);
      set => SetValue(c_pkey, value);
    }
    public int atSelfUser
    {
      get => ValueInt(c_self);
      set => SetValue(c_self, value);
    }
    public string atName
    {
      get => ValueStr(c_name);
      set => SetValue(c_name, value);
    }
    public string atDesc
    {
      get => ValueStr(c_desc);
      set => SetValue(c_desc, value);
    }
    public string atImg
    {
      get => ValueStr(c_imguser);
      set => SetValue(c_imguser, value);
    }
    public string atPass
    {
      get => ValueStr(c_pass);
      set => SetValue(c_pass, value);
    }
    public string atEmail
    {
      get => ValueStr(c_email);
      set => SetValue(c_email, value);
    }
    public hc4x_UserType atType
    {
      get => ValueEnum<hc4x_UserType>(c_type);
      set => SetValue(c_type, value);
    }
    public hc4x_UserStatus atStatus
    {
      get => ValueEnum<hc4x_UserStatus>(c_self);
      set => SetValue(c_self, (int)value);
    }
    public string atConfirmCode => atName.Substring(0, 8);
    #endregion
    #region Method
    public bool ValidPassword(string parPassWord) { return (atPass == parPassWord); }
    public bool AlterPassword(string parCurPass, string parNewPass, string parNewPassConfirm)
    {
      bool retValue = false;
      try
      {
        if (ValidPassword(parCurPass))
        {
          if (parNewPass == parNewPassConfirm)
          {
            retValue = true;
          }
          else
          {
            retValue = false;
          }
        }
      }
      catch (Exception) { throw; }
      return (retValue);
    }
    public bool ConfirmCode(string parCode)
    {
      bool retValue = false;
      try
      {
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
    private const string c_imguser = "imgUser";
    private const string c_node = "NodeUser";
    #endregion
  }
  /// <summary>Handles a User group</summary
  public class HC4x_NodeAppUser : WebXml
  {
    #region Name
    private const string Name = nameof(HC4x_NodeAppUser);
    #endregion
    #region Attribute
    public int atPkeyAppUser
    {
      get => ValueInt(c_pkeyappuser);
      set => SetValue(c_pkeyappuser, value);
    }
    public int atPkeyUser
    {
      get => ValueInt(c_pkeyuser);
      set => SetValue(c_pkeyuser, value);
    }
    public int atPkeyApp
    {
      get => ValueInt(c_pkeyapp);
      set => SetValue(c_pkeyapp, value);
    }
    #endregion
    #region Constructor
    public HC4x_NodeAppUser(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Constant
    private const string c_pkeyappuser = "pkeyAppUser";
    private const string c_pkeyapp = "pkeyApp";
    private const string c_pkeyuser = "pkeyUser";
    private const string c_node = "NodeAppUser";
    #endregion
  }
  public class HC4x_NodeCustomer : WebXml
  {
    #region Name
    private const string Name = nameof(HC4x_NodeCustomer);
    #endregion
    #region Attribute
    public int atPkeyCustomer
    {
      get => ValueInt(c_pkeycustomer);
      set => SetValue(c_pkeycustomer, value);
    }

    public string atLogoCustomer
    {
      get => ValueStr(c_logocustomer);
      set => SetValue(c_logocustomer, value);
    }
    public int atPkeyCustomerCategory
    {
      get => ValueInt(c_pkeycustomercategory);
      set => SetValue(c_pkeycustomercategory, value);
    }
    public int atPkeyAppUser
    {
      get => ValueInt(c_pkeyappuser);
      set => SetValue(c_pkeyappuser, value);
    }
    public string atNameCustomer
    {
      get => ValueStr(c_namecustomer);
      set => SetValue(c_namecustomer, value);
    }
    public string atRazaoSocial
    {
      get => ValueStr(c_razaosocial);
      set => SetValue(c_razaosocial, value);
    }
    public string atCnpjCpf
    {
      get => ValueStr(c_cnpjcpf);
      set => SetValue(c_cnpjcpf, value);
    }
    public string atNameContact
    {
      get => ValueStr(c_namecontact);
      set => SetValue(c_namecontact, value);
    }
    public string atEmailContact
    {
      get => ValueStr(c_emailcontact);
      set => SetValue(c_emailcontact, value);
    }
    public string atSite
    {
      get => ValueStr(c_site);
      set => SetValue(c_site, value);
    }
    public string atDescCustomer
    {
      get => ValueStr(c_desccustomer);
      set => SetValue(c_desccustomer, value);
    }
    #endregion
    #region Constructor
    public HC4x_NodeCustomer(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Constant
    private const string c_pkeycustomer = "pkeyCustomer";
    private const string c_pkeycustomercategory = "pkeyCustomerCategory";
    private const string c_pkeyappuser = "pkeyAppUser";
    private const string c_namecustomer = "nameCustomer";
    private const string c_razaosocial = "razaoSocial";
    private const string c_cnpjcpf = "cnpjCpf";
    private const string c_namecontact = "nameContact";
    private const string c_emailcontact = "emailContact";
    private const string c_site = "site";
    private const string c_desccustomer = "descCustomer";
    private const string c_logocustomer = "logoCustomer";
    private const string c_node = "NodeCustomer";
    #endregion
  }
  public class HC4x_SectorCustomer : DiceBase
  {
    #region Name
    private const string Name = nameof(HC4x_SectorCustomer);
    #endregion
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Static
    internal static bool ValidCPF(string parCPForCNPJ)
    {
      string cpf = parCPForCNPJ.Replace(".", "").Replace("-", "");

      if (cpf.Length != 11)
        return false;

      if (new string(cpf[0], cpf.Length) == cpf)
        return false;

      for (int i = 0, soma = 0, resto; i < 9; i++)
      {
        soma += int.Parse(cpf[i].ToString()) * (10 - i);
        if (i == 8)
        {
          resto = soma % 11;
          if (resto < 2) resto = 0;
          else resto = 11 - resto;
          if (resto != int.Parse(cpf[9].ToString())) return false;

          soma = 0;
        }
      }

      for (int i = 0, soma = 0, resto; i < 10; i++)
      {
        soma += int.Parse(cpf[i].ToString()) * (11 - i);
        if (i == 9)
        {
          resto = soma % 11;
          if (resto < 2) resto = 0;
          else resto = 11 - resto;
          if (resto != int.Parse(cpf[10].ToString())) return false;
        }
      }

      return true;
    }
    internal static bool ValidCNPJ(string parCPForCNPJ)
    {
      string cnpj = parCPForCNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

      if (cnpj.Length != 14)
        return false;

      if (new string(cnpj[0], cnpj.Length) == cnpj)
        return false;

      int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      int soma, resto;
      string digito, tempCnpj;

      tempCnpj = cnpj.Substring(0, 12);
      soma = 0;

      for (int i = 0; i < 12; i++)
        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

      resto = (soma % 11);
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;

      digito = resto.ToString();
      tempCnpj += digito;
      soma = 0;

      for (int i = 0; i < 13; i++)
        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

      resto = (soma % 11);
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;

      digito += resto.ToString();

      return cnpj.EndsWith(digito);
    }
    #endregion
    #region Method
    //esse método pode ser estático
    public string GetAlertByType(hc4x_TypeAlert parTypeAlert, string text)
    {
      string retValue = "";
      try
      {
        switch (parTypeAlert)
        {
          case hc4x_TypeAlert.Success:
            retValue = $"<div class=\"alert alert-success alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n<strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Info:
            retValue = $"<div class=\"alert alert-info alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n<strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Warning:
            retValue = $"<div class=\"alert alert-warning alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Danger:
            retValue = $"<div class=\"alert alert-danger alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Primary:
            retValue = $"<div class=\"alert alert-primary alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Secundary:
            retValue = $"<div class=\"alert alert-secundary alert-dismissible\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(GetAlertByType)); }
      return retValue;
    }
    internal bool UpdateImg(object parObject)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        HC4x_NodeCustomer parCustomer = parObject as HC4x_NodeCustomer;
        if (!string.IsNullOrEmpty(parCustomer.atLogoCustomer))
        {
          sqlCommand = string.Format("UPDATE hc4xcustomer " + "SET logoCustomer = \"{0}\" WHERE pkeyCustomer = {1}; ", parCustomer.atLogoCustomer, parCustomer.atPkeyCustomer);
          iRow = scData.ExecuteCommand(sqlCommand);
          retValue = (iRow == 1);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateImg)); }
      return retValue;
    }
    internal int FindAppUserById(int parId)
    {
      RawTable objTable;
      int iIdentity = -1;
      try
      {
        objTable = scData.SelectCommand("pkeyAppUser", "hc4xappuser", string.Format("pkeyUser={0}", parId), "");
        if (objTable.scRow.ndCount == 1)
          iIdentity = objTable[0].ValueInt("pkeyAppUser");
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindAppUserById)); }
      return iIdentity;
    }
    internal int FindPkeyCustomerCategoryByCategory(string parCategory)
    {
      int retValue = -1;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("pkeyCustomerCategory", "hc4xcustomercategory", string.Format("descCustomerCategory = \"{0}\"", parCategory), "");
        retValue = objTable[0].ValueInt("pkeyCustomerCategory");
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindPkeyCustomerCategoryByCategory)); }
      return retValue;
    }
    internal bool UpdateCustomerData(HC4x_NodeCustomer parCustomer)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        sqlCommand =
          "UPDATE hc4xcustomer " +
          $"SET nameCustomer = \"{parCustomer.atNameCustomer}\", " +
          $"razaoSocial = \"{parCustomer.atRazaoSocial}\", " +
          $"cnpjCpf = \"{parCustomer.atCnpjCpf}\", " +
          $"nameContact = \"{parCustomer.atNameContact}\", " +
          $"emailContact = \"{parCustomer.atEmailContact}\", " +
          $"site = \"{parCustomer.atSite}\"," +
          $"descCustomer =  \"{parCustomer.atDescCustomer}\"," +
          $"pkeyCustomerCategory =  {parCustomer.atPkeyCustomerCategory} " +
          $"WHERE pkeyCustomer = {parCustomer.atPkeyCustomer};";

        iRow = scData.ExecuteCommand(sqlCommand);
        retValue = iRow == 1;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateCustomerData)); }
      return retValue;
    }
    internal string FindDescCustomerCategoryById(int parIdCategory)
    {
      string retValue = "";
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("descCustomerCategory", "hc4xcustomercategory", string.Format("pkeyCustomerCategory = {0}", parIdCategory), "");
        retValue = objTable[0].ValueStr("descCustomerCategory");
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindDescCustomerCategoryById)); }
      return retValue;
    }
    public HC4x_NodeCustomer FindCustomerByPkeyUser(int parPkeyUser)
    {
      HC4x_NodeCustomer retValue = null;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("hc4xcustomer.*", "hc4xcustomer INNER JOIN hc4xappuser ON hc4xappuser.pkeyAppUser = hc4xcustomer.pkeyAppUser", $"hc4xappuser.pkeyUser = {parPkeyUser}", "");
        if (objTable.scRow.ndCount == 1)
        {
          retValue = new HC4x_NodeCustomer(axMundi);
          retValue.Update(objTable.scRow[0]);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindCustomerByPkeyUser)); }
      return (retValue);
    }
    internal HC4x_NodeCustomer dbInsertCustomer(HC4x_NodeCustomer objCustomer)
    {
      int iIndentity;
      NodeTable objTable;
      try
      {
        objTable = ndCubeApp.scTable["hc4x_customer_insert"];
        iIndentity = scData.InsertCommand("hc4xcustomer", objTable.scField, objCustomer);
        if (iIndentity > 0) objCustomer.atPkeyCustomer = iIndentity;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(HC4x_NodeCustomer)); }
      return (objCustomer);
    }
    internal HC4x_NodeAppUser dbInsertAppUser(HC4x_NodeAppUser objAppUser)
    {
      int iIndentity;
      NodeTable objTable;
      try
      {
        objTable = ndCubeApp.scTable["hc4x_appuser_insert"];
        iIndentity = scData.InsertCommand("hc4xappuser", objTable.scField, objAppUser);
        if (iIndentity > 0) objAppUser.atPkeyAppUser = iIndentity;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(HC4x_NodeCustomer)); }
      return (objAppUser);
    }
    #endregion
    #region Constructor
    public bool Init() { return (InitData()); }
    public HC4x_SectorCustomer(PageCore parPageHandler) : base(parPageHandler, c_sector) { }
    #endregion
    #region Constant
    private const string c_sector = "CustomerSector";
    #endregion
  }

  public class HC4x_StoneProduct : WebXml
  {
    #region Name
    private const string Name = nameof(HC4x_StoneProduct);
    #endregion
    #region Attribute
    public int atId
    {
      get => ValueInt(c_pkey);
      set => SetValue(c_pkey, value);
    }
    public string atDescription
    {
      get => ValueStr(c_description);
      set => SetValue(c_description, value);
    }
    public string atProductCover
    {
      get => ValueStr(c_productcover);
      set => SetValue(c_productcover, value);
    }
    #endregion
    #region Constructor
    public HC4x_StoneProduct(PageCore parMundi) : base(parMundi, c_node) { }
    #endregion
    #region Constant
    private const string c_node = "NodeStoneProduct";
    private const string c_description = "description";
    private const string c_productcover = "productCover";
    private const string c_pkey = "pkeyStoneProduct";
    #endregion
  }
  public class HC4x_SectorStoneProduct : DiceBase
  {
    #region Name
    private const string Name = nameof(HC4x_SectorStoneProduct);
    #endregion
    #region Constructor
    public bool Init() { return (InitData()); }
    public HC4x_SectorStoneProduct(PageCore parPageHandler) : base(parPageHandler, c_sector) { }
    #endregion
    #region Constant
    private const string c_sector = "StoneProductSector";
    #endregion
  }
  public class HC4x_SectorUser : DiceBase
  {
    #region Name
    private const string Name = nameof(HC4x_SectorUser);
    #endregion
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Attribute
    public int atGroupId { get; private set; }
    public string atGroupName { get; private set; }
    #endregion
    #region Method
    internal string FindImgUser(int parId)
    {
      string retValue = "";
      RawTable objTable;

      try
      {
        objTable = scData.SelectCommand("imgUser", "hc4xuser", $"pkeyUser = {parId}", "");
        if (!string.IsNullOrEmpty(objTable.scRow[0].ValueStr("imgUser")))
        {
          retValue = objTable.scRow[0].ValueStr("imgUser");
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindImgUser)); }
      return retValue;
    }
    internal string GetAlertByType(hc4x_TypeAlert parTypeAlert, string text)
    {
      string retValue = "";
      try
      {
        switch (parTypeAlert)
        {
          case hc4x_TypeAlert.Success:
            retValue = $"<div class=\"alert alert-success alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n<strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Info:
            retValue = $"<div class=\"alert alert-info alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n<strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Warning:
            retValue = $"<div class=\"alert alert-warning alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Danger:
            retValue = $"<div class=\"alert alert-danger alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Primary:
            retValue = $"<div class=\"alert alert-primary alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
          case hc4x_TypeAlert.Secundary:
            retValue = $"<div class=\"alert alert-secundary alert-dismissible m-0\">\r\n  <button class=\"btn-close\" data-bs-dismiss=\"alert\"></button>\r\n  <strong>{text}</strong>\r\n</div>";
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(GetAlertByType)); }
      return retValue;
    }
    internal bool UpdateGroup(HC4x_NodeUser parUser)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        sqlCommand = string.Format("UPDATE hc4xuser " + "SET selfUser = {0} " + "WHERE pkeyUser = {1}", (int)parUser.atStatus, parUser.atId);
        iRow = scData.ExecuteCommand(sqlCommand);
        retValue = (iRow == 1);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateGroup)); }
      return (retValue);
    }

    internal bool UpdatePatchUser(HC4x_NodeUser parUser, string parNewPassword)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        sqlCommand = string.Format("UPDATE hc4xuser " + "SET passUser = \"{0}\" " + "WHERE pkeyUser = {1}", parNewPassword, parUser.atId);
        iRow = scData.ExecuteCommand(sqlCommand);
        retValue = (iRow == 1);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateGroup)); }
      return (retValue);
    }
    internal bool UpdateUserData(HC4x_NodeUser parUser)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        sqlCommand = string.Format("UPDATE hc4xuser " + "SET selfUser = {0} , email = \"{1}\", descUser = \"{2}\" WHERE pkeyUser = {3};", (int)parUser.atStatus, parUser.atEmail, parUser.atDesc, parUser.atId);
        iRow = scData.ExecuteCommand(sqlCommand);
        retValue = (iRow == 1);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateUserData)); }
      return retValue;
    }
    internal bool UpdateImg(object parObject)
    {
      bool retValue = false;
      int iRow;
      string sqlCommand;
      try
      {
        HC4x_NodeUser parUser = parObject as HC4x_NodeUser;
        if (!string.IsNullOrEmpty(parUser.atImg))
        {
          sqlCommand = string.Format("UPDATE hc4xuser " + "SET imgUser = \"{0}\" WHERE pkeyUser = {1}; ", parUser.atImg, parUser.atId);
          iRow = scData.ExecuteCommand(sqlCommand);
          retValue = (iRow == 1);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateImg)); }
      return retValue;
    }
    public HC4x_NodeUser FindUserOrGroup(string parEmail)
    {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserOrGroup(parEmail), "");
        if (objTable.scRow.ndCount > 0)
        {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserOrGroup)); }
      return (retValue);
    }
    public HC4x_NodeUser FindUserById(int parId) { return (FindUserById(parId.ToString())); }
    public HC4x_NodeUser FindUserById(string parId)
    {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserById(parId), "");
        if (objTable.scRow.ndCount == 1)
        {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserById)); }
      return (retValue);
    }
    public HC4x_NodeUser FindUserByMail(string parEmail)
    {
      HC4x_NodeUser retValue = null;
      RawTable objTable;
      try
      {
        objTable = scData.SelectCommand("*", "hc4xuser", HC4x_NodeUser.sqlUserByMail(parEmail), "");
        if (objTable.scRow.ndCount == 1)
        {
          retValue = new HC4x_NodeUser(axMundi);
          retValue.Update(objTable.scRow[0]);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(FindUserByMail)); }
      return (retValue);
    }
    public bool UpdateUser(HC4x_NodeUser parUser)
    {
      bool retValue = false;
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateUser)); }
      return (retValue);
    }
    public HC4x_NodeUser dbInsertUser(HC4x_NodeUser parUser)
    {
      int iIndentity;
      NodeTable objTable;
      try
      {
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
  public class HC4x_UserHandler : RawPage
  {
    #region Name
    private const string Name = nameof(HC4x_UserHandler);
    #endregion
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Attribute
    #endregion
    #region Node
    private HC4x_SectorUser scUser { get; set; }
    private HC4x_SectorStoneProduct scStoneProduct { get; set; }
    #endregion
    #region Method
    internal RawMailMessage? RegisterMail(HC4x_NodeUser parUser, hc4x_SiteArea parSiteArea, string parEmlId, string parRedirect)
    {
      string strUrl;
      string eBookUrl;
      RackInterface objRack;
      RawMailMessage retValue;
      try
      {
        objRack = axMundi.ndCubeApp.rcInterface;
        eBookUrl = "https://download.hypercube4x.com/pt/EBook_HyperStone.pdf";
        switch (parSiteArea)
        {
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
        retValue.AddBodyPlain(string.Format(retValue.atHeader, strUrl, eBookUrl));
      }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(RegisterMail)); }
      return (retValue);
    }
    internal bool GetDynamicStones(string parKeyCustomer)
    {
      bool retValue = false;
      string strUrl;
      string strContentPage;
      RawTable objTable;
      RawRow[] arNode;
      try
      {
        objTable = scData.SelectCommand("pkeyStoneProduct, description, productCover", "stoneproduct", "pkeyCustomer = " + parKeyCustomer, "");
        arNode = objTable.scRow.ArrayNode();
        strUrl = "hc4x://newscene=HyperStone/url=" + axRequest.EncodedUrl(axRequest.atBaseUrl + "/rest/pt/hcstone-slabxml/0/") + "{hc4x-key:pkeyStoneProduct}";
        strContentPage = ndCurInterface.atContentPage.Replace("{hc4x-key:url_template}", strUrl);
        ndCurInterface.SetContentPage(strContentPage);
        retValue = ndCurInterface.EvalContent(arNode);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(GetDynamicStones)); }
      return (retValue);
    }
    internal bool ConfirmUserEMail()
    {
      bool retValue = false;
      string strQuery;
      string[] arQuery;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        strQuery = axRequest.QueryString();
        arQuery = GearAux.SyntaxSplit(strQuery, true);
        objUser = scUser.FindUserById(arQuery[0]);
        if (objUser == null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Warning, "Usuário não encontrado");
          return (retValue);
        }
        switch (objUser.atStatus)
        {
          case hc4x_UserStatus.Admin:
          case hc4x_UserStatus.User:
            atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Success, "Código Validado com sucesso!");
            retValue = true;
            break;
          case hc4x_UserStatus.Anonymous:
          case hc4x_UserStatus.Waiting:
            if (objUser.ConfirmCode(arQuery[1]))
            {
              objUser.atStatus = hc4x_UserStatus.User;
              if (retValue = scUser.UpdateGroup(objUser))
                atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Success, "Código Validado com sucesso!");
            }
            else
              atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Secundary, "Código inválido");
            break;
          default:
            atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Info, "O usuário encontra-se desativado. Favor reativar o usuário antes de confirmar o código");
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ConfirmUserEMail)); }
      return (retValue);
    }
    internal bool RegisterUser()
    {
      bool retValue = false;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      RawMailMessage objMail;
      hc4x_PasswordLevel passwordLevel;
      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        passwordLevel = GetPasswordLevel(objPostUser.atPass);
        if (passwordLevel == hc4x_PasswordLevel.Weak || passwordLevel == hc4x_PasswordLevel.Normal)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Senha fraca! Utilize caracteres especiais como por exemplo: !@#");
          return (retValue);
        }
        if (objPostUser.atPass != objPostUser.atPass_2)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Senha não confere com a repetição !");
          return (retValue);
        }
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser != null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Info, "Usuário já existe no banco de dados !");
        }
        else
        {
          objUser = objPostUser.CreateNodeUser();
          if (scUser.dbInsertUser(objUser) != null)
          {
            objMail = RegisterMail(objUser, hc4x_SiteArea.publicarea, "confirm-email_eml", "confirm-email");
            if (objMail != null) axResponse.SendMail(objMail);
            retValue = true;
          }
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Success, "Usuário cadastrado com sucesso !");
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(RegisterUser)); }
      return (retValue);
    }
    internal bool ValidateUserPass()
    {
      bool retValue = false;
      string strCrypto;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser == null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Usuário ou senha inválidos");
          return (retValue);
        }
        strCrypto = objPostUser.EncryptUTF8(objPostUser.atPass);
        if (!objUser.ValidPassword(strCrypto))
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Usuário ou senha inválidos");
          return (retValue);
        }
        switch (objUser.atStatus)
        {
          case hc4x_UserStatus.None:
            break;
          case hc4x_UserStatus.Admin:
          case hc4x_UserStatus.User:
            if (axSession.SetCredentials(objUser.atId, objUser.atSelfUser, objUser.atEmail))
              axMundi.RedirectTo(hc4x_SiteArea.privatearea, "index");
            retValue = true;
            break;
          case hc4x_UserStatus.Anonymous:
          case hc4x_UserStatus.Waiting:
            atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Warning, "Seu cadastro não está validado. Você só pode logar depois que validar o seu email." + string.Format("<a href={0}>Clique Aqui</a>", "/publicarea/pt/newmailcode/"));
            break;
          case hc4x_UserStatus.Suspended:
          case hc4x_UserStatus.Canceled:
            atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Seu cadastro encontra-se suspenso ou cancelado. Deseja reativar?");
            break;
          default:
            atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Info, "Seu login não pode ser validado. Entre em contato com o administrador do sistema");
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ValidateUserPass)); }
      return (retValue);
    }
    internal bool ForgotPass()
    {
      bool retValue = false;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      RawMailMessage objMail;
      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser == null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Warning, "Usuário não encontrado!");
          return retValue;
        }
        else
        {
          objMail = ResetPassword(objUser, hc4x_SiteArea.publicarea, "confirm-forgot_eml", "reset_pass");
          if (objMail != null) axResponse.SendMail(objMail);
          retValue = true;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ForgotPass)); }
      return retValue;
    }
    internal RawMailMessage? ResetPassword(HC4x_NodeUser parUser, hc4x_SiteArea parSiteArea, string parEmlId, string parRedirect)
    {
      string strUrl;
      RackInterface objRack;
      RawMailMessage retValue;
      try
      {
        objRack = axMundi.ndCubeApp.rcInterface;
        switch (parSiteArea)
        {
          case hc4x_SiteArea.publicarea:
            retValue = objRack.PublicArea<RawMailMessage>(parEmlId);
            break;
          case hc4x_SiteArea.privatearea:
            retValue = objRack.PrivateArea<RawMailMessage>(parEmlId);
            break;
          default:
            return (null);
        }
        strUrl = ndRoute.FullPublicRoute(parRedirect, parUser.atId.ToString(), true);
        retValue.atSubject = retValue.atTitle;
        retValue.AddTo(parUser.atEmail, parUser.atDesc);
        retValue.AddBodyPlain(string.Format(retValue.atHeader, strUrl));
      }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(RegisterMail)); }
      return (retValue);
    }
    internal bool ResetPass()
    {
      bool retValue = false;
      string strQuery;
      string[] arQuery;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        strQuery = axRequest.QueryString();
        arQuery = GearAux.SyntaxSplit(strQuery, true);
        if (arQuery.Length == 0)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Entrada inválida");
          return retValue;
        }
        objUser = scUser.FindUserById(arQuery[0]);
        if (objUser == null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Usuário não encontrado");
          return (retValue);
        }
        objPostUser = axRequest.FormKeyVal<PostUser>();
        if (string.IsNullOrWhiteSpace(objPostUser.atPass))
        {
          return (retValue);
        }

        var passwordLevel = GetPasswordLevel(objPostUser.atPass);

        if (passwordLevel == hc4x_PasswordLevel.Weak || passwordLevel == hc4x_PasswordLevel.Normal)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Senha fraca! Utilize caracteres especiais como por exemplo: !@#");
          return (retValue);
        }

        if (objPostUser.atPass != objPostUser.atPass_2)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Danger, "Senha não confere com a repetição da senha");
          return (retValue);
        }

        if (objUser.AlterPassword(objUser.atPass, objPostUser.atPass, objPostUser.atPass_2))
        {
          string newPass = objPostUser.EncryptUTF8(objPostUser.atPass);
          if (scUser.UpdatePatchUser(objUser, newPass))
            retValue = true;
        }

      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ResetPass)); }
      return (retValue);
    }
    //refatorar pois estão no lugar errado os métodos abaixo
    public hc4x_PasswordLevel GetPasswordLevel(string password)
    {
      hc4x_PasswordLevel level;

      switch (true)
      {
        case var _ when !ContainsNumbers(password):
          level = hc4x_PasswordLevel.Weak;
          break;
        case var _ when ContainsNumbers(password) && !ContainsSpecialCharacters(password):
          level = hc4x_PasswordLevel.Normal;
          break;
        case var _ when ContainsSpecialCharacters(password) && ContainsNumbers(password):
          level = hc4x_PasswordLevel.Strong;
          break;
        default:
          level = hc4x_PasswordLevel.None;
          break;
      }
      return level;
    }
    public bool ContainsNumbers(string input)
    {
      foreach (char c in input)
      {
        if (Char.IsDigit(c))
        {
          return true;
        }
      }
      return false;
    }
    public bool ContainsSpecialCharacters(string input)
    {
      Regex regex = new Regex("[!@#$%^&*(),.?\":{ }|<>]");
      return regex.IsMatch(input);
    }
    public bool GetInfoDownloadPage()
    {
      bool retValue = false;
      string atDownloadFolderPath;
      HC4x_DeployInfo objDeployInfo;
      string strContentPage;
      string languageCode;
      try
      {
        languageCode = axMundi.ndBlazorServer.ndCubeApp.scLocale.GetNode(0).ValueStr("LanguageCode");
        objDeployInfo = new HC4x_DeployInfo();
        atDownloadFolderPath = GearPath.Combine($@"{axMundi.atWebPath}\Download", "deployinfo.json");
        using (StreamReader r = new StreamReader(atDownloadFolderPath))
        {
          string jsonString = r.ReadToEnd();
          objDeployInfo = JsonSerializer.Deserialize<HC4x_DeployInfo>(jsonString);
        }
        if (objDeployInfo != null)
        {
          strContentPage = ndCurInterface.atContentPage
          .Replace("{hc4x-key:Version}", objDeployInfo.Version)
          .Replace("{hc4x-key:Data}", objDeployInfo.Date);

          if (languageCode == "pt-BR")
            strContentPage = strContentPage
            .Replace("{hc4x-key:Nodepackage64}", objDeployInfo.NodePackage[0].Url)
            .Replace("{hc4x-key:Nodepackage86}", objDeployInfo.NodePackage[1].Url);
          else
            strContentPage = strContentPage
            .Replace("{hc4x-key:Nodepackage64}", objDeployInfo.NodePackage[2].Url)
            .Replace("{hc4x-key:Nodepackage86}", objDeployInfo.NodePackage[3].Url);
          retValue = ndCurInterface.SetContentPage(strContentPage);

        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(GetInfoDownloadPage)); }
      return retValue;
    }
    internal bool ResendConfirmation()
    {
      bool retValue = false;
      PostUser objPostUser;
      HC4x_NodeUser objUser;
      RawMailMessage objMail;

      try
      {
        objPostUser = axRequest.FormKeyVal<PostUser>();
        objUser = scUser.FindUserByMail(objPostUser.atEmail);
        if (objUser == null)
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Warning, "Usuário não existente no banco de dados, realize o cadastro");
          return (retValue);
        }
        else if (objUser.atStatus == hc4x_UserStatus.Waiting)
        {
          objMail = RegisterMail(objUser, hc4x_SiteArea.publicarea, "confirm-email_eml", "confirm_sucess");
          if (objMail != null) axResponse.SendMail(objMail);
          retValue = true;
        }
        else
        {
          atMessage = scUser.GetAlertByType(hc4x_TypeAlert.Success, "Usuário já confirmado, o código de confirmação não é mais necessário!");
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ResendConfirmation)); }
      return (retValue);
    }
    #endregion
    #region RawPage
    public override bool ActionGet(string parPageId)
    {
      try
      {
        switch (parPageId)
        {
          case c_hyper_stone:
            return GetDynamicStones("3");
          case c_download:
            return GetInfoDownloadPage();
          default:
            return true;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionGet)); };
      return true;
    }
    public override bool ActionPost(string parPageId)
    {
      bool retValue = false;
      try
      {
        switch (parPageId)
        {
          case c_page_login:
            retValue = ValidateUserPass();
            break;
          case c_page_registration:
            if (retValue = RegisterUser())
            {
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "registration_success");
            }
            break;
          case c_page_validemail:
            if (retValue = ConfirmUserEMail())
            {
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "confirm_sucess");
            }
            break;
          case c_forgot_pass:
            if (retValue = ForgotPass())
            {
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "send_success");
            }
            break;
          case c_page_newmailcode:
            if (retValue = ResendConfirmation())
            {
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "registration_success");
            }
            break;
          case c_reset_pass:
            if (retValue = ResetPass())
            {
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "alter_pass_success");
            }
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionPost)); }
      return (retValue);
    }
    public override bool Init(AxisMundi parMundi)
    {
      bool retValue = false;
      try
      {
        if (!base.Init(parMundi)) return (retValue);
        switch (axMundi.atRequestMethod)
        {
          case hc4x_RequestMethod.Post:
            scUser = new HC4x_SectorUser(axMundi);
            retValue = scUser.Init();
            break;
          case hc4x_RequestMethod.Get:
            if (axMundi.axRequest.QueryString().Length > 0 || ndCurInterface.atModelLayout == hc4x_ModelLayout.ContentPage.ToString())
            {
              scUser = new HC4x_SectorUser(axMundi);
              scStoneProduct = new HC4x_SectorStoneProduct(axMundi);
              retValue = scUser.Init() && scStoneProduct.Init();
            }
            retValue = true;
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
    #region Constant
    private const string c_page_login = "login";
    private const string c_page_registration = "registration";
    private const string c_page_validemail = "confirm-email";
    private const string c_forgot_pass = "forgot-pass";
    private const string c_page_newmailcode = "newmailcode";
    private const string c_reset_pass = "reset_pass";
    private const string c_hyper_stone = "hyper_stone";
    private const string c_download = "download";
    #endregion
  }
  public class HC4x_DeployInfo
  {
    public string id { get; set; }
    public string Version { get; set; }
    public string Date { get; set; }
    public string Status { get; set; }
    public string CheckUpdate { get; set; }
    public string ServerUrl { get; set; }
    public List<HC4x_NodePackage> NodePackage { get; set; }
  }
  public class HC4x_NodePackage
  {
    public string id { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
  }
}