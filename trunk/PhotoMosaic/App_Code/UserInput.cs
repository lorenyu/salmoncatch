using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FlickrNet;
using System.Collections.Specialized;

/// <summary>
/// A temporary "struct" used to parse the parameters received from the web form and
/// to do some initial input checking.
/// </summary>
public class UserInput
{
    public string userName;
    public string targetImageUrl;
    public int numHorizontalImages;
    public int numVerticalImages;
    public bool isValid;

    public UserInput(NameValueCollection webForm)
    {
        isValid = true;

        try
        {
            userName = webForm["Username"];
            targetImageUrl = webForm["TargetImageUrl"];
            numHorizontalImages = int.Parse(webForm["NumHorizontalImages"]);
            numVerticalImages = int.Parse(webForm["NumVerticalImages"]);
        }
        catch (Exception ex)
        {
            // TODO: error handling
            isValid = false;
            return;
        }

        if (userName == "" ||
            targetImageUrl == "" ||
            numHorizontalImages < 1 ||
            numVerticalImages < 1)
        {
            isValid = false;
        }
    }
}
