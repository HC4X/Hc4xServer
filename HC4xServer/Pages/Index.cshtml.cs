using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using HC4xServer.Core;

namespace HC4x_Server.Pages {
  public class IndexModel : PageLauncher {
    private const string Name = nameof(IndexModel);
    #region Generated
    private readonly ILogger<IndexModel> _logger;
    #endregion
    #region Attribute
    #endregion
    #region Node
    #endregion
    #region Async
    public IActionResult OnGet() {
      IActionResult retValue = null;
      string strRedirect;
      try {
        if (!InitGet()) return (retValue);
        strRedirect = axMundi.atRedirect;
        if (!string.IsNullOrWhiteSpace(strRedirect)) {
          retValue = Redirect(strRedirect);
          }
        else
          atMessage = axMundi.atMessage;
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(OnGet)); }
      return (retValue);
      }
    public IActionResult OnPost() {
      IActionResult retValue = null;
      string strRedirect;
      try {
        if (!InitPost()) return (retValue);
        strRedirect = axMundi.atRedirect;
        if (!string.IsNullOrWhiteSpace(strRedirect)) {
          retValue = Redirect(strRedirect);
          }
        else
          atMessage = axMundi.atMessage;
        }
      catch (Exception Err) { ShowException(Err, Name, nameof(OnPost)); }
      return (retValue);
      }
    #endregion
    #region Method
    public string GetHeadMeta() { return (GetHeaderSection(GearCore.c_SectorHeadMeta)); }
    public string GetHeadLink() { return (GetHeaderSection(GearCore.c_SectorHeadLink)); }
    public string GetBodyHeader() { return (GetHeaderSection(GearCore.c_BodyHeaderFooter)); }
    public string GetBodyFooter() { return (GetFooterSection(GearCore.c_BodyHeaderFooter)); }
    #endregion
    #region Constructor
    public IndexModel(ILogger<IndexModel> logger) { _logger = logger; }
    #endregion
    }
  }