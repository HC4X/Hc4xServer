using System;
using LibServer;

namespace HC4x_Server.Render {
  public class PublicAreaRender : RenderPage {
    private const string Name = nameof(PublicAreaRender);
    #region Method
    public override string RenderHeader(ServerInterface parInterface) { return (base.RenderHeader(parInterface)); }
    public override string RenderFooter(ServerInterface parInterface) { return (base.RenderFooter(parInterface)); }
    #endregion
    #region Constructor
    public PublicAreaRender() { }
    #endregion
    }
  public class PrivateAreaRender : RenderPage {
    private const string Name = nameof(PrivateAreaRender);
    #region Axis
    private AxisSession axSession => axMundi.axSession;
    #endregion
    #region Method
    public override string RenderHeader(ServerInterface parInterface) {
      string retValue;
      if (parInterface.atId == "BodyHeaderFooter")
        retValue = string.Format(parInterface.atHeader, axSession.atEmailUser);
      else
        retValue = base.RenderHeader(parInterface);
      return (retValue);
      }
    public override string RenderFooter(ServerInterface parInterface) { return (base.RenderFooter(parInterface)); }
    #endregion
    #region Constructor
    public PrivateAreaRender() { }
    #endregion
    }
  }
