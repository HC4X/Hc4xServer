using System;
using HC4x_Server.Logic;
using HC4xServer.Core;
using System.Threading.Tasks;
using HC4xServer.Logic;
using LibServer;
using LibModel;

namespace HC4x_Server.PrivateArea
{
  internal class UserArea : RawPage {
    private const string Name = nameof(UserArea);
    #region Axis
    private new PageCore axMundi => (PageCore)base.axMundi;
    #endregion
    #region Node
    private HC4x_SectorUser scUser { get; set; }
    private HC4x_SectorCustomer scCustomer { get; set; }
    private HC4x_NodeUser ndUser { get; set; }
    private HC4x_NodeCustomer ndCustomer { get; set; }
    #endregion   
    #region Method
    internal bool ValidCPFCNPJ(string parCPForCNPJ) {
      bool retValue = true;
      if (parCPForCNPJ.Length == 14)
        retValue = HC4x_SectorCustomer.ValidCPF(parCPForCNPJ);
      else
        retValue = HC4x_SectorCustomer.ValidCNPJ(parCPForCNPJ);
      return retValue;
    }

    internal bool AreaUser()
    {
      bool retValue = false;
      try
      {
        if (axRequest.FileKey() != null && axRequest.FileKey().Length > 0)
          UpdateProfilePic();

        retValue = UpdateUserForm(ndUser);
        ndCurInterface.EvalForm(ndUser);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(AreaUser)); }
      return (retValue);
    }
    internal bool UpdateProfilePic()
    {
      bool retValue = false;
      string strWwwPath;
      NodeFormFile[] arFormFile;
      string imagePath;
      Task<bool> objTask;
      try
      {
        arFormFile = axRequest.FileKey();
        strWwwPath = GearPath.Combine(axMundi.atWebPath, "upload-image-profile");
        foreach (NodeFormFile itFile in arFormFile)
        {
          strWwwPath = itFile.GetSafeName(strWwwPath);
          imagePath = GearPath.Combine("/upload-image-profile", GearPath.FileName(itFile.GetSafeName(strWwwPath)));
          ndUser.atImg = imagePath.Replace("\\", "/");
          objTask = Task.Run(() => itFile.SaveLocalServer(strWwwPath));
          if (!objTask.Result) break;
        }
        retValue = scUser.UpdateImg(ndUser);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(UpdateProfilePic)); }
      return (retValue);
    }
    internal bool GetImgUser()
    {
      bool retValue = false;
      string atPath = scUser.FindImgUser(ndUser.atId);
      if (atPath != "")
      {
        retValue = true;
      }
      else
      {
        ndUser.atImg = "/images/ico/user-default.png";
      }
      retValue = scUser.UpdateImg(ndUser);
      ndCurInterface.EvalForm(ndUser);
      return retValue;
    }
    internal bool RegisterCustomer()
    {
      bool retValue = false;
      PostCustomer objPostCustomer;
      HC4x_NodeCustomer objCustomer;
      HC4x_NodeAppUser objAppUser;
      try
      {
        objPostCustomer = axRequest.FormKeyVal<PostCustomer>();
        objCustomer = new HC4x_NodeCustomer(axMundi);
        objAppUser = new HC4x_NodeAppUser(axMundi);
        if (ValidCPFCNPJ(objPostCustomer.atCnpjCpf))  {
          objCustomer.atCnpjCpf = objPostCustomer.atCnpjCpf;
        }
        else
        {
          scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, $"{objPostCustomer.atCnpjCpf} não é válido !");
          return retValue;
        }
        objAppUser.atPkeyUser = axSession.atPkUser;
        objAppUser.atPkeyApp = 4;
        objCustomer.atPkeyAppUser = scCustomer.dbInsertAppUser(objAppUser).atPkeyAppUser;
        objCustomer.atRazaoSocial = objPostCustomer.atRazaoSocial;
        objCustomer.atNameCustomer = objPostCustomer.atNameCustomer;
        objCustomer.atPkeyCustomerCategory = scCustomer.FindPkeyCustomerCategoryByCategory(objPostCustomer.atCustomerCategory);
        objCustomer.atNameContact = objPostCustomer.atNameContact;
        objCustomer.atEmailContact = objPostCustomer.atEmailContact;
        objCustomer.atSite = objPostCustomer.atSite;
        objCustomer.atDescCustomer = objPostCustomer.atDescCustomer;
        retValue = scCustomer.dbInsertCustomer(objCustomer).atPkeyCustomer > 0;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(RegisterCustomer)); }
      return (retValue);
    }
    internal bool UpdateUserForm(HC4x_NodeUser parUser)
    {
      bool retValue = false;
      HC4x_UserHandler objUserHander = new HC4x_UserHandler();
      RawMailMessage objMail;
      PostUser objPostUser;
      objPostUser = axRequest.FormKeyVal<PostUser>();

      if (!string.IsNullOrEmpty(objPostUser.atName))
        parUser.atDesc = objPostUser.atName;
      if (!string.IsNullOrEmpty(objPostUser.atMail) && objPostUser.atMail != parUser.atEmail)
      {
        parUser.atSelfUser = (int)hc4x_UserStatus.Waiting;
        parUser.atEmail = objPostUser.atMail;
        objUserHander.Init(axMundi);
        objMail = objUserHander.RegisterMail(parUser, hc4x_SiteArea.publicarea, "confirm-email_eml", "confirm-email");
        if (objMail != null) axResponse.SendMail(objMail);
      }
      if (retValue = scUser.UpdateUserData(parUser))
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Success, "Dados do usuário alterados com sucesso !");
      return retValue;
    }
    internal bool UpdateCustomerForm(HC4x_NodeCustomer parCustomer)
    {
      bool retValue = false;
      PostCustomer objPostCustomer;
      objPostCustomer = axRequest.FormKeyVal<PostCustomer>();
      parCustomer.atNameCustomer = objPostCustomer.atNameCustomer;
      parCustomer.atRazaoSocial = objPostCustomer.atRazaoSocial;
      parCustomer.atCnpjCpf = objPostCustomer.atCnpjCpf;
      parCustomer.atNameContact = objPostCustomer.atNameContact;
      parCustomer.atEmailContact = objPostCustomer.atEmailContact;
      parCustomer.atSite = objPostCustomer.atSite;
      parCustomer.atDescCustomer = objPostCustomer.atDescCustomer;

      if (retValue = scCustomer.UpdateCustomerData(parCustomer))
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Success, "Dados do cliente alterados com sucesso !");
      return retValue;
    }
    #endregion
    #region RawPage
    public override bool ActionRender()
    {
      bool retValue = false;
      try
      {
        if (ndCurInterface.EvalForm(ndUser))
          retValue = base.ActionRender();
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionRender)); }
      return (retValue);
    }
    public override bool ActionGet(string parPageId)
    {
      bool retValue = false;
      ndUser = scUser.FindUserById(axSession.atPkUser);
      ndCustomer = scCustomer.FindCustomerByPkeyUser(axSession.atPkUser);
      switch (parPageId)
      {
        case c_area_cliente:
          retValue = GetImgUser();
          break;
        case c_register_customer:
          if (ndCustomer == null)
            retValue = true;
          else retValue = ndCurInterface.EvalForm(ndCustomer);
          break;
        default:
          retValue = true; 
          break;
      }
      return retValue;
    }
    public override bool ActionPost(string parPageId)
    {
      bool retValue = false;
      ndUser = scUser.FindUserById(axSession.atPkUser);
      ndCustomer = scCustomer.FindCustomerByPkeyUser(axSession.atPkUser);
      try
      {
        switch (parPageId)
        {
          case c_area_cliente:
            retValue = AreaUser();
            break;
          case c_register_customer:
            retValue = RegisterCustomer();
            break;
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionPost)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public override bool Init(AxisMundi parMundi)
    {
      bool retValue = false;
      try
      {
        if (base.Init(parMundi))
        {
          scUser = new HC4x_SectorUser(axMundi);
          scCustomer = new HC4x_SectorCustomer(axMundi);
          retValue = scUser.Init() && scCustomer.Init();
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(Init)); }
      return (retValue);
    }
    #endregion
    #region Constant
    private const string c_area_cliente = "area-usuario";
    private const string c_register_customer = "register-customer";
    #endregion
  }
}