using System;
using HC4x_Server.Logic;
using HC4xServer.Core;
using LibServer;

namespace HC4xServer.Logic {
  public class PostCustomer : WebKeyValue {
    private const string Name = nameof(PostCustomer);
    //# completar
    }
  public class PostUser : WebKeyValue {
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
    #endregion
    #region Method
    public HC4x_NodeUser CreateNodeUser() {
      HC4x_NodeUser retValue;
      try {
        retValue = new HC4x_NodeUser(axMundi) {
          atName = Guid.NewGuid().ToString(),
          atEmail = atEmail,
          atPass = EncryptUTF8(atPass),
          atDesc = "[Self Registered User]",
          atStatus = hc4x_UserStatus.Waiting
          };
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(CreateNodeUser)); }
      return (retValue);
      }
    public string EncryptUTF8(string parText) {
      string retValue;
      try {
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
    #endregion
    }
  }
