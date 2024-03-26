using System;
using LibModel;

namespace DesignTest {
  /// <summary>Handles a single User</summary>
  public class HC4x_NodeUser : RawXml {
    private const string Name = nameof(HC4x_NodeUser);
    #region Attribute
    public int atId {
      get => ValueInt(c_pkey);
      set => SetValue(c_pkey, value);
      }
    public string atName {
      get => ValueStr(c_name);
      set => SetValue(c_name, value);
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
    #endregion
    #region Method
    public bool ValidPassword(string parPassWord) {
      bool retValue;
      try {
        retValue = (atPass == parPassWord);
        }
      catch (Exception Err) { throw; }
      return (retValue);
      }
    public bool AlterPassword(string parCurPass, string parNewPass, string parNewPassConfirm) { return (true); }
    public bool Update(string parUserName, string parUserDesc, string parEmail, string parPassWord) { return (true); }
    #endregion
    #region Constructor
    /// <summary>If atId == 0, load user's attibutes from parameters</summary>
    public bool Init(string parName, string parDesc, string parEmail, string parPassWord) { return (true); }
    /// <summary>If atId > 0, load user's attributes from the database</summary>
    public bool Init() { return (true); }
    public HC4x_NodeUser(RawXml parXml) : base(parXml) { }
    public HC4x_NodeUser() : base(c_node, hc4x_NodeType.XmlNodeName) { }
    #endregion
    #region Constant
    private const string c_email = "email";
    private const string c_pass = "passUser";
    private const string c_type = "typeUser";
    private const string c_name = "nameUser";
    private const string c_pkey = "pkeyUser";
    private const string c_node = "NodeUser";
    #endregion
    }
  /// <summary>Handles a User group</summary>
  public class HC4x_SectorUser : RawXml {
    private const string Name = nameof(HC4x_SectorUser);
    #region Attribute
    public int atGroupId { get; private set; }
    public string atGroupName { get; private set; }
    #endregion
    #region Method
    public HC4x_NodeUser FindUserOrGroup(string parUserName) {
      HC4x_NodeUser retValue = null;
      HC4x_NodeUser[] arNode;
      try {
        arNode = ArrayOf<HC4x_NodeUser>();
        foreach (HC4x_NodeUser itNode in arNode)
          //# if (itNode.atType == hc4x_UserType.User)
          if (itNode.atName == parUserName) {
            retValue = itNode;
            break;
            }
        }
      catch (Exception Err) { throw; }
      return (retValue);
      }
    public HC4x_NodeUser FindUser(string parUserName) {
      HC4x_NodeUser retValue = null;
      HC4x_NodeUser[] arNode;
      try {
        arNode = ArrayOf<HC4x_NodeUser>();
        foreach (HC4x_NodeUser itNode in arNode)
          if (itNode.atType == hc4x_UserType.User)
            if (itNode.atName == parUserName) {
              retValue = itNode;
              break;
              }
        }
      catch (Exception Err) { throw; }
      return (retValue);
      }
    public HC4x_NodeUser CreateUser(string parUserName, string parUserDesc, string parEmail, string parPassWord) {
      HC4x_NodeUser retValue;
      try {
        //# here: code to check if user exists. User can only be created if it does not exist
        retValue = new HC4x_NodeUser(); //# userId 0 indicates that the user does not exist
        retValue.Init(parUserName, parUserDesc, parEmail, parPassWord);
        //# here: code to insert the user into the database
        }
      catch (Exception Err) { throw; }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool Init() { return (true); }
    public HC4x_SectorUser(RawXml parXml) : base(parXml) { }
    #endregion
    }
  }
