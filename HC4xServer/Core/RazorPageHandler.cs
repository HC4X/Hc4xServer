using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LibServer;

namespace HC4xServer.Core {
  public class PageCore : AxisMundi {
    private const string Name = nameof(PageCore);
    #region Axis
    public new PageLauncher ndPage => (PageLauncher)base.ndPage;
    #endregion
    #region Attribute
    public string atRedirect { get; private set; }
    #endregion
    #region Node
    public BlazorServerService ndBlazorServer { get; private set; }
    #endregion
    #region Method
    public void RedirectTo(hc4x_SiteArea parArea, string parPageId) {
      switch (parArea) {
        case hc4x_SiteArea.privatearea:
          atRedirect = ndRoute.privateRoute(parPageId);
          break;
        case hc4x_SiteArea.publicarea:
          atRedirect = ndRoute.publicRoute(parPageId);
          break;
        case hc4x_SiteArea.rest:
          atRedirect = ndRoute.restRoute(parPageId);
          break;
        }
      }
    internal bool ActionGet() {
      bool retValue = false;
      RawPage objPage;
      ServerInterface objInterface;
      try {
        objInterface = ndPage.CurInterface();
        objPage = objInterface.ndPage;
        if (objPage == null) return (true);
        if (objPage.Init(this))
          retValue = objPage.ActionGet(ndRoute.atPageId);
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(ActionGet)); }
      return (retValue);
      }
    internal bool ActionPost() {
      bool retValue = false;
      RawPage objPage;
      ServerInterface objInterface;
      try {
        objInterface = ndPage.CurInterface();
        objPage = objInterface.ndPage;
        if (objPage.Init(this))
          retValue = objPage.ActionPost(ndRoute.atPageId);
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(ActionPost)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool Init() {
      bool retValue = false;
      HttpContext objContext;
      try {
        objContext = ndPage.HttpContext;
        ndBlazorServer = (BlazorServerService)objContext.RequestServices.GetService(typeof(BlazorServerService));
        //# if (ndServer == null) ndServer = new BlazorServerService();
        ndRoute = GearCore.RouteData(objContext);
        retValue = Init(ndBlazorServer);
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(Init)); }
      return (retValue);
      }
    public PageCore(PageLauncher parPage, hc4x_RequestMethod parMethod) : base(parPage, parMethod) { }
    #endregion
    }
  public class RestCore : AxisMundi {
    private const string Name = nameof(RestCore);
    #region Node
    public BlazorServerService ndServer { get; private set; }
    private ServerInterface ndCurInterface;
    #endregion
    #region Method
    internal async Task<bool> RestPost() {
      bool retValue = false;
      RawRest objRest;
      ServerInterface objInterface;
      try {
        objInterface = CurInterface();
        if (objInterface == null) {
          retValue = await GetResponse();
          return (retValue);
          }
        objRest = objInterface.ndRest;
        if (objRest != null) {
          if (objRest.Init(this))
            if (objRest.ActionPost(ndRoute.atPageId))
              retValue = objRest.ActionResponse();
          }
        else
          retValue = await GetResponse();
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(RestPost)); }
      return (retValue);
      }
    internal async Task<bool> RestGet() {
      bool retValue = false;
      RawRest objRest;
      ServerInterface objInterface;
      try {
        objInterface = CurInterface();
        if (objInterface == null) {
          retValue = await GetResponse();
          return (retValue);
          }
        objRest = objInterface.ndRest;
        if (objRest != null) {
          if (objRest.Init(this))
            if (objRest.ActionGet(ndRoute.atPageId))
              retValue = objRest.ActionResponse();
          }
        else
          retValue = await GetResponse();
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(RestGet)); }
      return (retValue);
      }
    internal async Task<bool> GetResponse() {
      bool retValue = false;
      try {
        retValue = await ExecuteRest(ndRoute.atPageId);
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    private ServerInterface CurInterface() {
      ServerInterface retValue;
      try {
        retValue = ndCurInterface;
        if (retValue == null) ndCurInterface = retValue = ndServer.GetInterface(hc4x_SiteArea.publicarea, ndRoute.atPageId);
        }
      catch (Exception Err) { retValue = null; ShowException(Err, Name, nameof(CurInterface)); }
      return (retValue);
      }
    #endregion
    #region Constructor
    public bool Init() {
      bool retValue = false;
      try {
        ndServer = (BlazorServerService)fwContext.RequestServices.GetService(typeof(BlazorServerService));
        //# if (ndServer == null) ndServer = new BlazorServerService();
        ndRoute = GearCore.RouteData(fwContext);
        retValue = Init(ndServer);
        }
      catch (Exception) { throw; }
      return (retValue);
      }
    public RestCore(HttpContext parContext, hc4x_RequestMethod parMethod) : base(parContext, parMethod) { }
    #endregion
    }
  }