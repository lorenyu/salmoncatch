using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// A struct containing the parameters received from the web form.
/// </summary>
public struct UserInput
{
    public string targetImageFilename;
    public string componentImageDirectory;
    public int numHorizontalImages;
    public int numVerticalImages;
}
