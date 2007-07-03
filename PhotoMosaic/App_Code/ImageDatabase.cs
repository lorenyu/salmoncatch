using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Summary description for ImageDatabase
/// </summary>
public class ImageDatabase
{
	public ImageDatabase()
	{
        this.images = new List<Bitmap>();
	}

    public ComponentImage ComponentImageLookup(ImageQuery query)
    {
        return new ComponentImage();
    }

    public void AddImage(Bitmap image)
    {
        this.images.Add(image);
    }

    /// <summary>
    /// Temporary way of storing images
    /// </summary>
    private List<Bitmap> images;
}
