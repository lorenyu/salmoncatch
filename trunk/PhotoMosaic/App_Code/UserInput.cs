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
using System.Drawing;

/// <summary>
/// A temporary "struct" used to parse the parameters received from the web form and
/// to do some initial input checking.
/// </summary>
public class UserInput
{
    public string userName;
    public Bitmap targetImage;
    public int numHorizontalImages;
    public int numVerticalImages;
    public bool isValid;

    public UserInput(Page page)
    {
        isValid = true;

        try
        {
            if (page.Request.Form["username"] != null)
            {
                // using 'simple'

                FileUpload targetImageUpload = (FileUpload)page.FindControl("targetImage");
                if (!targetImageUpload.HasFile) throw new Exception("No image uploaded.");
                HttpPostedFile targetImageFile = targetImageUpload.PostedFile;
                if (!targetImageFile.ContentType.StartsWith("image")) throw new Exception("Only images can be uploaded.");
                if (targetImageFile.ContentLength > Settings.MAX_IMAGE_UPLOAD_SIZE) throw new Exception("Max image upload size exceeded.");
                targetImage = new Bitmap(targetImageFile.InputStream);

                numHorizontalImages = 10;
                numVerticalImages = 10;

                if (page.Request.Form["imageSetType"] == "username")
                {
                    userName = page.Request.Form["username"];
                }
                else
                {
                    // TODO: get search stuff
                    isValid = false;
                }
            }
            else
            {
                // using 'black and white'
                userName = page.Request.Form["Username"];
                string targetImageUrl = page.Request.Form["TargetImageUrl"];
                WebUtil.GetBitmap(targetImageUrl);
                numHorizontalImages = int.Parse(page.Request.Form["NumHorizontalImages"]);
                numVerticalImages = int.Parse(page.Request.Form["NumVerticalImages"]);
            }
        }
        catch (Exception ex)
        {
            // TODO: error handling
            isValid = false;
            return;
        }

        // Extra error checking
        if (userName == "" ||
            numHorizontalImages < 1 ||
            numVerticalImages < 1)
        {
            isValid = false;
        }
    }

    /*
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
     * */
}
