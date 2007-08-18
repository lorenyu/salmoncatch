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
/// A struct containing the parameters received from the web form.
/// </summary>
public class UserInput
{
    public UserInput(NameValueCollection webForm)
    {
        isValid = true;

        userName = webForm["Username"];
        targetURL = webForm["TargetImageUrl"];

        try
        {
            numHorizontalImages = int.Parse(webForm["NumHorizontalImages"]);
            numVerticalImages = int.Parse(webForm["NumVerticalImages"]);
        }
        catch (Exception ex)
        {
            // TODO: Handle exception
            isValid = false;
            return;
        }

        // TODO: refactor these into settings
        redownload = true;

        // TODO: I don't think these should be static.
        Settings.USER_URL = userName;

        if (userName == "" || targetURL == "") isValid = false;
    }

    public string targetImageFilename;
    public int numHorizontalImages;
    public int numVerticalImages;

    public string userName;
    public string userID;
    public Flickr flickr;
    public string tempFrob;

    public string targetURL;
    public bool redownload;
    public bool isValid;
}
