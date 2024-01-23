using System;
using Microsoft.AspNetCore.Hosting;
using LibServer;
using LibModel;

namespace HC4xServer.Core {
  public class PageLauncher : RawPage {
    private const string Name = nameof(PageLauncher);
    #region Attribute
    public hc4x_SiteArea atSiteArea { get; protected set; }
    #endregion
    #region Node
    public new PageCore axMundi => (PageCore)base.axMundi;
    public BlazorServerService ndService => (BlazorServerService)axMundi.ndServerService;
    private RenderPage ndRenderPage { get; set; }
    #endregion
    #region Method
    public string GetHeaderSection(string parPageId) {
      string retValue;
      ServerInterface objInterface;
      try {
        objInterface = GetInterface(parPageId);
        if (objInterface == null) return (string.Empty);
        if (ndRenderPage != null)
          retValue = ndRenderPage.RenderHeader(objInterface);
        else
          retValue = objInterface.atHeader;
        }
      catch (Exception Err) { retValue = GearServer.ExceptionHtml(Err, Name, nameof(GetHeaderSection)); }
      return (retValue);
      }
    public string GetFooterSection(string parPageId) {
      string retValue;
      ServerInterface objInterface;
      try {
        objInterface = GetInterface(parPageId);
        if (objInterface == null) return (string.Empty);
        if (ndRenderPage != null)
          retValue = ndRenderPage.RenderFooter(objInterface);
        else
          retValue = objInterface.atFooter;
        }
      catch (Exception Err) { retValue = GearServer.ExceptionHtml(Err, Name, nameof(GetFooterSection)); }
      return (retValue);
      }
    private ServerInterface GetHeadMeta(string parInterface) {
      ServerInterface retValue;
      retValue = axMundi.ndCubeApp.rcInterface[parInterface];
      if (!retValue.Init()) retValue = null;
      return (retValue);
      }
    public ServerInterface RenderCurrent() {
      ServerInterface retValue;
      RawPage objPage;
      try {
        retValue = CurInterface();
        if (retValue != null)
          if ((objPage = retValue.ndPage) != null) objPage.ActionRender();
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(RenderCurrent)); }
      return (retValue);
      }
    public ServerInterface CurInterface() {
      ServerInterface retValue;
      try {
        retValue = ndCurInterface;
        if (retValue == null)
          retValue = ndCurInterface = GetInterface(atInterface);
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(CurInterface)); }
      return (retValue);
      }
    private ServerInterface EmptyInterface() { return (ndCurInterface = axMundi.EmptyInterface(hc4x_ModelLayout.InfoPage)); }
    public ServerInterface GetInterface(string parPageId) {
      ServerInterface retValue;
      try {
        if (atRedirecting) {
          retValue = EmptyInterface();
          return (retValue);
          }
        retValue = ndService.GetInterface(atSiteArea, parPageId);
        if (retValue == null) {
          atMessage = string.Format("Interface not Found: {0}", parPageId);
          retValue = axMundi.EmptyInterface(hc4x_ModelLayout.InfoPage);
          }
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(GetInterface)); }
      return (retValue);
      }
    public void ShowException(Exception parErr, string parClass, string parMethod) {
      atMessage = GearServer.ExceptionHtml(parErr, parClass, parMethod);
      if (ndCurInterface == null) {
        if (axMundi.ndCubeApp != null)
          GetInterface(axMundi.ndRoute.atPageId);
        }
      }
    private bool InitPublic() {
      bool retValue = false;
      try {
        ndRenderPage = ndCubeApp.rcInterface.PublicRenderPage();
        retValue = (ndRenderPage != null);
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(InitPublic)); }
      return (retValue);
      }
    private bool InitPrivate() {
      bool retValue = false;
      try {
        ndRenderPage = ndCubeApp.rcInterface.PrivateRenderPage();
        if (ndRenderPage != null) {
          if (InitData()) {
            if (!(retValue = axMundi.OpenDbSession()))
              axMundi.RedirectTo(hc4x_SiteArea.publicarea, "non-authenticate");
            }
          }
        }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(InitPrivate)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool InitGet() {
      bool retValue = false;
      try {
        if (!Init(new PageCore(this, hc4x_RequestMethod.Get))) return (retValue);
        if (retValue = axMundi.Init()) {
          if (InitSiteArea()) {
            atInterface = ndRoute.atPageId;
            if (ndRoute.atQueryStr.Length == 0)
              retValue = axMundi.ActionGet();
            else
              retValue = axMundi.ActionPost();
            }
          }
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(InitGet)); }
      return (retValue);
      }
    public bool InitPost() {
      bool retValue = false;
      try {
        if (!Init(new PageCore(this, hc4x_RequestMethod.Post))) return (retValue);
        if (axMundi.Init()) {
          if (InitSiteArea()) {
            atInterface = ndRoute.atPageId;
            retValue = axMundi.ActionPost();
            }
          }
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(InitPost)); }
      return (retValue);
      }
    private bool InitSiteArea() {
      bool retValue = false;
      string strFileExt;
      try {
        atSiteArea = ndRoute.SiteArea<hc4x_SiteArea>();
        switch (atSiteArea) {
          case hc4x_SiteArea.publicarea:
            if (!(retValue = InitPublic())) return (retValue);
            break;
          case hc4x_SiteArea.privatearea:
            if (!(retValue = InitPrivate())) return (retValue);
            break;
          default:
            strFileExt = GearPath.FileExtension(ndRoute.atPageId);
            if (!string.IsNullOrEmpty(strFileExt))
              throw new NodeException(string.Format("File Not Found: {0}", ndRoute.atPageId));
            else
              throw new NodeException(string.Format("Unreconiged Area: {0}", atSiteArea));
          }
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(InitSiteArea)); }
      return (retValue);
      }
    public PageLauncher() { } //# Init(new PageCore(this)); }
    #endregion
    }
  public class BlazorServerService : ServerService { //# ServerInformation
    private const string Name = nameof(BlazorServerService);
    #region Attribute
    public hc4x_Environment atEnvironment => GearBase.ParseEnum<hc4x_Environment>(ndEnvironment.EnvironmentName);
    #endregion
    #region Node
    public IWebHostEnvironment ndEnvironment => fwApp.Environment;
    #endregion
    #region Method
    public ServerInterface GetInterface(hc4x_SiteArea parSiteArea, string parPageId) {
      ServerInterface retValue;
      try {
        switch (parSiteArea) {
          case hc4x_SiteArea.None:
            retValue = axMundi.EmptyInterface(hc4x_ModelLayout.InfoPage);
            break;
          case hc4x_SiteArea.publicarea:
            retValue = ndCubeApp.rcInterface.PublicArea(parPageId);
            break;
          case hc4x_SiteArea.privatearea:
            retValue = ndCubeApp.rcInterface.PrivateArea(parPageId);
            break;
          default:
            retValue = ndCubeApp.rcInterface[parPageId];
            break;
          }
        }
      catch (Exception Err) { retValue = null; axMundi.ShowException(Err, Name, nameof(GetInterface)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public BlazorServerService() { }
    #endregion
    }
  }
