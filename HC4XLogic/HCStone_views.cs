using HC4xServer.Core;
using LibModel;
using LibServer;
using System;

namespace HC4x_Server.HCStone
{
  public class view_stone_catalog
  {
    private const string Name = nameof(view_stone_catalog);
    #region Axis
    private PageCore axMundi { get; set; }
    private AxisRequest axRequest => axMundi.axRequest;
    private ServerApplication ndCubeApp => axMundi.ndCubeApp;
    private ServerData scData => ndCubeApp.scData;
    #endregion
    #region Method
    public bool public_catalog(ServerInterface parInterface, string parKeyCustomer)
    {
      bool retValue = false;
      string strUrl;
      try
      {
        strUrl = "hc4x://newscene=HyperStone/url=" + axRequest.EncodedUrl(axRequest.atBaseUrl + "/rest/pt/hcstone-slabxml/0/") + "{hc4x-key:pkeyStoneProduct}";
        retValue = render_catalog(parInterface,
          "pkeyStoneProduct, description, " +
          "productCover",
          "stoneproduct", "pkeyCustomer = " + parKeyCustomer,
          "",
          strUrl);
        //# objTable = scData.SelectCommand("pkeyStoneProduct, description, productCover", "stoneproduct", "pkeyCustomer = " + parKeyCustomer, "");
        //# arNode = objTable.scRow.ArrayNode();
        //# strUrl = "hc4x://newscene=HyperStone/url=" + axRequest.EncodedUrl(axRequest.atBaseUrl + "/0/") + "{hc4x-key:pkeyStoneProduct}";
        //# strContentPage = parInterface.atContentPage.Replace("{hc4x-key:url_template}", strUrl);
        //# parInterface.SetContentPage(strContentPage);
        //# retValue = parInterface.EvalContent(arNode);
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(public_catalog)); }
      return (retValue);
    }
    private bool render_catalog(ServerInterface parInterface, string parFields, string parTable, string parWhere, string parOrderBy, string parUrl)
    {
      bool retValue = false;
      string strContentPage;
      RawTable objTable;
      RawRow[] arNode;
      try
      {
        objTable = scData.SelectCommand(parFields, parTable, parWhere, parOrderBy);
        arNode = objTable.scRow.ArrayNode();
        strContentPage = parInterface.atContentPage.Replace("{hc4x-key:url_template}", parUrl);
        retValue = parInterface.SetContentPage(strContentPage);
        parInterface.EvalContent(arNode);
        var x = parInterface.atContentPage;
      }
      catch (Exception Err) { axMundi.ShowException(Err, Name, nameof(render_catalog)); }
      return (retValue);
    }
    #endregion
    #region Constructor
    public view_stone_catalog(PageCore parCore) { axMundi = parCore; }
    public void Close() { axMundi = null; }
    #endregion
  }
}
