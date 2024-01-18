using System;
using HC4x_Server.Logic;
using HC4xServer.Core;
using System.Threading.Tasks;
using HC4xServer.Logic;
using LibServer;
using LibModel;
using System.Text;

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
    internal bool RegisterCustomer() {
      bool retValue = false;
      PostCustomer objPostCustomer;
      HC4x_NodeCustomer objCustomer;
      int pkeyAppUser;
      try {
        objPostCustomer = axRequest.FormKeyVal<PostCustomer>();
        objCustomer = scCustomer.FindCustomerByPkeyUser(axSession.atPkUser);
        if (!ValidateCPFCNPJForm(objPostCustomer.atCnpjCpf))
        {
          retValue = GetHC4xCustomerCategorys();

          return retValue;
        }
        if (objCustomer == null) {
          objCustomer = objPostCustomer.CreateNodeCustomer();
          pkeyAppUser = scCustomer.FindAppUserById(axSession.atPkUser);
          objCustomer.atPkeyAppUser = pkeyAppUser == -1 ? scCustomer.dbInsertHc4xAppUser(axSession.atPkUser) : pkeyAppUser;
          objCustomer.atNameCustomer = objPostCustomer.atNameCustomer;
          objCustomer.atRazaoSocial = objPostCustomer.atRazaoSocial;
          if (ValidateCustomerCnpjCpf(objPostCustomer.atCnpjCpf))
          {
            objCustomer.atCnpjCpf = objPostCustomer.atCnpjCpf;
          }
          else
          {
            GetHC4xCustomerCategorys();
            return retValue;
          }
          objCustomer.atCustomerCategory = scCustomer.FindPkeyCustomerCategoryByCategory(objPostCustomer.atCustomerCategory);
          objCustomer.atNameContact = objPostCustomer.atNameContact;
          objCustomer.atEmailContact = objPostCustomer.atEmailContact;
          objCustomer.atDescCustomer = objPostCustomer.atDescCustomer;
          objCustomer.atSite = objPostCustomer.atSite;
          objCustomer.atPkeyCustomer = scCustomer.dbInsertHc4xCustomer(objCustomer);
          if (objCustomer.atPkeyCustomer >= 0)
            GetHC4xCustomer(objCustomer);
            retValue = ndCurInterface.EvalForm(objCustomer);
            atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Success,"Empresa cadastrada com sucesso !");
        }
        else {
          UpdateCustomerForm(objCustomer);
          GetHC4xCustomer(objCustomer);
          retValue = ndCurInterface.EvalForm(objCustomer);
        }
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(RegisterCustomer)); }
      return (retValue);
    }
    internal bool ValidateCustomerCnpjCpf(string parCnpjCpf) { 
      if (!ContainsNumbers(parCnpjCpf)) {
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, "Somente números no campo CNPJ / CPF");
        return false;
      }
      if (parCnpjCpf.Length == 11 && IsValidCPF(parCnpjCpf)) {
        return true;
      }
      else if (parCnpjCpf.Length == 11) {
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, "CPF Inválido");
        return false;
      }
      if (parCnpjCpf.Length == 14 && IsValidCNPJ(parCnpjCpf)) {
        return true;
      }
      else if (parCnpjCpf.Length == 14) {
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, "CNPJ Inválido");
        return false;
      }
      return false;
    }
    internal bool ValidateCPFCNPJForm(string parCnpjCpf) {
      if (!ContainsNumbers(parCnpjCpf))
      {
        atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, "Somente números no campo CPF / CNPJ");
        return false;
      }

      if (parCnpjCpf.Length == 11) {
        return true;
      }

      if (parCnpjCpf.Length == 14) {
        return true;
      }
      string type = (parCnpjCpf.Length < 14) ? "CPF" : "CNPJ";
      atMessage = scCustomer.GetAlertByType(hc4x_TypeAlert.Danger, $"{type} informado incorretamente");
      return false;
    }


    internal bool UpdateUserForm(HC4x_NodeUser parUser) {
      bool retValue = false;
      HC4x_UserHandler objUserHander = new HC4x_UserHandler();
      RawMailMessage objMail;
      PostUser objPostUser;
      objPostUser = axRequest.FormKeyVal<PostUser>();

      if (!string.IsNullOrEmpty(objPostUser.atName))
        parUser.atDesc = objPostUser.atName;
      if (!string.IsNullOrEmpty(objPostUser.atMail) && objPostUser.atMail != parUser.atEmail) {
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
    public bool ContainsNumbers(string input) {
      foreach (char c in input)
      {
        if (Char.IsDigit(c))
        {
          return true;
        }
      }
      return false;
    }
    internal bool UpdateCustomerForm(HC4x_NodeCustomer parCustomer) {
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
    public override bool ActionRender() {
      bool retValue = false;
      try
      {
        if (ndCurInterface.EvalForm(ndUser))
          retValue = base.ActionRender();
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionRender)); }
      return (retValue);
    }
    public override bool ActionGet(string parPageId) {
      bool retValue = false;
      ndUser = scUser.FindUserById(axSession.atPkUser);
      ndCustomer = scCustomer.FindCustomerByPkeyUser(axSession.atPkUser);
      switch (parPageId) {
        case c_register_customer:
          retValue = GetHC4xCustomer(ndCustomer);
          break;
        case c_area_cliente:
          retValue = GetImgUser();
          break;
        default:
          return retValue;
      }
      return retValue;
    }
    public override bool ActionPost(string parPageId) {
      bool retValue = false;
      ndUser = scUser.FindUserById(axSession.atPkUser);
      ndCustomer = scCustomer.FindCustomerByPkeyUser(axSession.atPkUser);
      try {
        switch (parPageId) {
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
    public bool AreaUser(){
      bool retValue = false;
      try {
        if (axRequest.FileKey() != null && axRequest.FileKey().Length > 0)
          UpdateProfilePic();

        retValue = UpdateUserForm(ndUser);
        ndCurInterface.EvalForm(ndUser);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(AreaUser)); }
      return (retValue);
    }
    public bool UpdateProfilePic()  {
      bool retValue = false;
      string strWwwPath;
      NodeFormFile[] arFormFile;
      string imagePath;
      Task<bool> objTask;
      try {
        arFormFile = axRequest.FileKey();
        strWwwPath = GearPath.Combine(axMundi.atWebPath, "upload-image-profile");
        foreach (NodeFormFile itFile in arFormFile) {
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
    internal bool GetImgUser() {
      bool retValue = false;
      string atPath = scUser.FindImgUser(ndUser.atId);
      if (atPath != "")  {
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
    internal bool GetHC4xCustomer(HC4x_NodeCustomer parCustomer) {
      bool retValue = false;
      try {
        
        if(parCustomer == null) {
          retValue = GetHC4xCustomerCategorys();
        }
        else {
           GetHC4xCustomerCategory(parCustomer);  
           retValue = ndCurInterface.EvalForm(parCustomer);
        }
       }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(ActionGet)); }
      return retValue;
    }
    internal bool GetHC4xCustomerCategorys() {
      bool retValue = true;
      try {
        //hc4xCategorys = scCustomer.FindCustomerCategory();
        //htmlBuilder = new StringBuilder();

        //foreach (string categoria in hc4xCategorys) {
        //  htmlBuilder.AppendFormat("<option value='{0}'>{0}</option>", categoria);
        //}
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(GetHC4xCustomerCategorys)); }
      return retValue;
    }
    internal bool GetHC4xCustomerCategory(HC4x_NodeCustomer parCustomer) {
      bool retValue = false;
      StringBuilder htmlBuilder;
      string categoria;
      try {
        htmlBuilder = new StringBuilder();

        if (parCustomer == null) {
          retValue = GetHC4xCustomerCategorys();
        }
        else {
          categoria = scCustomer.FindDescCustomerCategoryById(parCustomer.atCustomerCategory);
          htmlBuilder.AppendFormat("<option value='{0}' selected>{0}</option>", categoria);
          retValue = ndCurInterface.SetForm(ndCurInterface.atForm.Replace("{0}",htmlBuilder.ToString()));
        }
      }
      catch(Exception Err) { axMundi.ShowException(Err, Name, nameof(GetHC4xCustomerCategory)); }
      return retValue;
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
    internal static bool IsValidCPF(string cpf) {
      if (cpf.Length != 11)
        return false;

      for (int i = 0; i < 10; i++)
      {
        if (cpf == new string(i.ToString()[0], 11))
          return false;
      }

      int[] multiplierFirstPart = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplierSecondPart = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

      string tempCpf = cpf.Substring(0, 9);
      tempCpf += CalculateDigit(tempCpf, multiplierFirstPart);
      tempCpf += CalculateDigit(tempCpf, multiplierSecondPart);

      return cpf == tempCpf;
    }
    internal static bool IsValidCNPJ(string cnpj) {
      if (cnpj.Length != 14)
        return false;

      for (int i = 0; i < 10; i++)
      {
        if (cnpj == new string(i.ToString()[0], 14))
          return false;
      }

      int[] multiplierFirstPart = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplierSecondPart = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      string tempCnpj = cnpj.Substring(0, 12);
      tempCnpj += CalculateDigit(tempCnpj, multiplierFirstPart);
      tempCnpj += CalculateDigit(tempCnpj, multiplierSecondPart);

      return cnpj == tempCnpj;
    }

    private static int CalculateDigit(string cpf, int[] multipliers)
    {
      int sum = 0;

      for (int i = 0; i < multipliers.Length; i++)
      {
        sum += int.Parse(cpf[i].ToString()) * multipliers[i];
      }

      int result = sum % 11;

      return (result < 2) ? 0 : 11 - result;
    }
    #endregion
    #region Constant
    private const string c_area_cliente = "area-usuario";
    private const string c_register_customer = "register-customer";
    #endregion
  }
}