using System;
using LibModel;

namespace DesignTest {
  internal static class RegExpTest {
    private static string strPattern = "{(hc4x-key):(\\w+)}";
    private static string strForm = "<label for=\"fname\">First name:</label>"
                     + "<input type=\"text\" id=\"fname\" name=\"fname\" value=\"{hc4x-key:email}\">"
                     + "<label for=\"lname\">Last name:</label>"
                     + "<input type=\"text\" id=\"lname\" name=\"lname\" value=\"{hc4x-key:lname}\">"
                     + "<input type=\"submit\" value=\"Submit\">";
    private static RawKeyValue ndKeyValue;
    internal static bool EvalExpression() {
      RawRegExp objRegExp;
      MatchRegExp[] arRegExp;
      loadDataset();
      objRegExp = GearRegExp.MultiLineIgnoreCase(strForm, strPattern);
      arRegExp = objRegExp.ArrayNode();
      foreach (MatchRegExp itRegExp in arRegExp) {
        string strValue = ndKeyValue.ValueStr(itRegExp.GroupStr(2));
        strForm = strForm.Replace(itRegExp.atContent, strValue);
        }
      return (true);
      }
    private static void loadDataset() {
      ndKeyValue = new RawKeyValue();
      ndKeyValue.Add("email", "raphael@hypercube4x.com");
      ndKeyValue.Add("lname", "Raphael Macedo");
      }
    }
  }
