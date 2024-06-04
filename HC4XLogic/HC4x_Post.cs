using HC4x_Server.Logic;
using HC4xServer.Core;
using LibServer;
using System;

namespace HC4xServer.Logic
{
  public class PostCustomer : WebKeyValue
  {
    private const string Name = nameof(PostCustomer);
    #region Axis
    public new PageCore axMundi => (PageCore)base.axMundi;
    public AxisSession axSession => axMundi.axSession;
    #endregion
    #region Attribute
    public int atPkeyCustomer => ValueInt(c_pkeycustomer);
    public string atCustomerCategory => ValueStr(c_customerCategory);
    public int atPkeyAppUser
    {
      get => ValueInt(c_pkeyappuser);
      set => SetValue(c_pkeyappuser, value);
    }
    public string atNameCustomer => ValueStr(c_namecustomer);
    public string atRazaoSocial => ValueStr(c_razaosocial);
    public string atCnpjCpf => ValueStr(c_cnpjcpf);
    public string atNameContact => ValueStr(c_namecontact);
    public string atEmailContact => ValueStr(c_emailcontact);
    public string atSite => ValueStr(c_site);
    public string atDescCustomer => ValueStr(c_desccustomer);
    public string atLogoCustomer => ValueStr(c_logocustomer);
    #endregion
    #region Method
    public HC4x_NodeCustomer CreateNodeCustomer()
    {
      HC4x_NodeCustomer retValue;
      try
      {
        retValue = new HC4x_NodeCustomer(axMundi)
        {
          atNameCustomer = atNameCustomer,
          atRazaoSocial = atRazaoSocial,
          atCnpjCpf = atCnpjCpf,
          atNameContact = atNameContact,
          atEmailContact = atEmailContact,
          atSite = atSite,
          atDescCustomer = atDescCustomer,
          atPkeyAppUser = atPkeyAppUser,
        };
      }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(CreateNodeCustomer)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const string c_pkeycustomer = "pkeyCustomer";
    private const string c_customerCategory = "CustomerCategory";
    private const string c_pkeyappuser = "pkeyAppUser";
    private const string c_namecustomer = "nameCustomer";
    private const string c_razaosocial = "razaoSocial";
    private const string c_cnpjcpf = "cnpjCpf";
    private const string c_namecontact = "nameContact";
    private const string c_emailcontact = "emailContact";
    private const string c_site = "site";
    private const string c_desccustomer = "descCustomer";
    private const string c_logocustomer = "logoCustomer";
    #endregion
  }
  public class PostUser : WebKeyValue
  {
    private const string Name = nameof(PostUser);
    #region Axis
    public new PageCore axMundi => (PageCore)base.axMundi;
    public AxisSession axSession => axMundi.axSession;
    #endregion
    #region Attribute
    public string atEmail => ValueStr(c_email);
    public string atPass => ValueStr(c_pass);
    public string atPass_2 => ValueStr(c_pass_2);
    public string atNewPass => ValueStr(c_new_pass);
    public string atNewPassConf => ValueStr(c_new_pass_conf);
    public string atCode => ValueStr(c_code);
    public string atName => ValueStr(c_name);
    public string atMail => ValueStr(c_mail);
    public string atRememberMe => ValueStr(c_remember_me);
    #endregion
    #region Method
    public HC4x_NodeUser CreateNodeUser()
    {
      HC4x_NodeUser retValue;
      try
      {
        retValue = new HC4x_NodeUser(axMundi)
        {
          atName = Guid.NewGuid().ToString(),
          atEmail = atEmail,
          atPass = EncryptUTF8(atPass),
          atDesc = "",
          atStatus = hc4x_UserStatus.Waiting,
          atImg = "/images/ico/user-default.png",
        };
      }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(CreateNodeUser)); }
      return (retValue);
    }
    public string EncryptUTF8(string parText)
    {
      string retValue;
      try
      {
        retValue = axSession.EncryptUTF8(parText);
      }
      catch (Exception Err) { retValue = string.Empty; axMundi.ShowException(Err, Name, nameof(PostUser)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const string c_new_pass = "user_new_pass";
    private const string c_new_pass_conf = "user_new_pass_conf";
    private const string c_code = "user_code";
    private const string c_pass_2 = "user_pass_2";
    private const string c_pass = "user_pass";
    private const string c_email = "user_email";
    private const string c_name = "descUser";
    private const string c_mail = "email";
    private const string c_remember_me = "rememberMe";
    #endregion
  }
}
