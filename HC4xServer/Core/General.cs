using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using LibServer;

namespace HC4xServer.Core {
  public enum hc4x_Environment {
    None,
    Development,
    Test,
    Production
    }
  public enum hc4x_SiteArea {
    None,
    publicarea,
    privatearea,
    rest
    }
  public static class GearCore {
    private const string Name = nameof(GearCore);
    #region Method
    public static Task DefaultRedirect(HttpContext parContext, hc4x_SiteArea parArea, string parDefaultPageId) {
      Task retValue;
      string strUrl;
      NodeRoute objRoute;
      try {
        objRoute = RouteData(parContext);
        switch (parArea) {
          case hc4x_SiteArea.privatearea:
            strUrl = objRoute.privateRoute(parDefaultPageId);
            break;
          case hc4x_SiteArea.rest:
            strUrl = objRoute.restRoute(parDefaultPageId);
            break;
          default:
            strUrl = objRoute.publicRoute(parDefaultPageId);
            break;
          }
        parContext.Response.Redirect(strUrl);
        retValue = Task.CompletedTask;
        }
      catch (Exception Err) { retValue = DefaultErrorHandler(parContext, Err, Name, nameof(DefaultRedirect)); }
      return (retValue);
      }
    public static NodeRoute RouteData(HttpContext parContext) {
      string[] arKey;
      NodeRoute retValue;
      RouteValueDictionary objRoute;
      try {
        objRoute = parContext.Request.RouteValues;
        arKey = objRoute.Keys.ToArray();
        retValue = new NodeRoute();
        foreach (string itKey in arKey) retValue.Add(itKey, objRoute[itKey]);
        if (retValue.NoLang()) retValue.atLang = BrowserDefaultLang(parContext);
        }
      catch (Exception Err) { retValue = null; DefaultErrorHandler(parContext, Err, Name, nameof(DefaultRedirect)); }
      return (retValue);
      }
    public static string BrowserDefaultLang(HttpContext parContext) {
      string retValue;
      StringWithQualityHeaderValue objLang;
      try {
        objLang = parContext.Request.GetTypedHeaders().AcceptLanguage[0];
        retValue = objLang.Value.Value;
        if (retValue.StartsWith(c_pt_lang, StringComparison.InvariantCultureIgnoreCase))
          retValue = c_pt_lang;
        else
          retValue = c_default_lang;
        }
      catch (Exception Err) { retValue = string.Empty; DefaultErrorHandler(parContext, Err, Name, nameof(DefaultRedirect)); }
      return (retValue);
      }
    private static Task DefaultErrorHandler(HttpContext parContext, Exception parErr, string parClass, string parMethod) {
      parContext.Response.WriteAsync(string.Format("<p>Class:{0}</p>", parClass)
        + string.Format("<p>Method:{0}</p>", parMethod)
        + string.Format("<p>Err:{0}</p>", parErr.Message));
      return Task.CompletedTask;
      }
    #endregion
    #region Constant
    #region Layout
    public const string c_tagtitle = "title";
    public const string c_SectorHeadMeta = "HeadMeta";
    public const string c_SectorHeadLink = "HeadLink";
    public const string c_BodyHeaderFooter = "BodyHeaderFooter";
    public const string c_SectorBodyHeader = "BodyHeader";
    public const string c_SectorBodyFooter = "BodyFooter";
    public const string c_layoutpublic = "_LayoutPublic";
    //# public const string c_layoutprivate = "_LayoutPrivate";
    #endregion
    #region Lang
    public const string c_default_lang = "en";
    public const string c_pt_lang = "pt";
    #endregion
    #endregion
    }
  }
