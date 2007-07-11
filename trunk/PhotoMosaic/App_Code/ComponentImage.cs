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
/// Summary description for ComponentImage
/// </summary>
public class ComponentImage
{
	public ComponentImage(Bitmap image)
	{
        this.image = image;
        this.meanColor = ImageProcessor.CalculateMeanColor(image);
	}

    private Bitmap image;
    public Bitmap Image
    {
        get
        {
            return image;
        }
    }

    private Color meanColor;
    public Color MeanColor
    {
        get
        {
            return meanColor;
        }
    }
}
