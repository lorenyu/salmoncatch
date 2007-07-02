using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Drawing;

/// <summary>
/// Summary description for TargetImage
/// </summary>
public class TargetImage
{
	public TargetImage(Bitmap image)
	{
        this.image = image;
	}

    private Bitmap image;
    public Bitmap Image
    {
        get
        {
            return image;
        }
    }
}
