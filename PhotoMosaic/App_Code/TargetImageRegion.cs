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
/// Summary description for TargetImageRegion
/// </summary>
public class TargetImageRegion
{
    private TargetImage targetImage;
    private TargetImage TargetImage
    {
        get
        {
            return targetImage;
        }
    }

    private Region region;
    public Region Region
    {
        get
        {
            return region;
        }
    }

	public TargetImageRegion(TargetImage image, Region region)
	{
        this.targetImage = image;
        this.region = region;
	}
}
