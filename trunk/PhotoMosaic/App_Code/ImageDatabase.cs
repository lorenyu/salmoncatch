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
    ComponentImageTree images;

	public ImageDatabase(List<ComponentImage> adjustedComponentImages)
	{
        this.images = new ComponentImageTree(adjustedComponentImages);
	}

    /// <summary>
    /// More generalized version of FindBestMatch.
    /// 
    /// Currently not implemented, so use FindBestMatch for now.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public ComponentImage ComponentImageLookup(ImageQuery query)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    /// <summary>
    /// Returns the adjusted component image in the database that
    /// best matches the specified region of the target image.
    /// Returns null if image database is empty.
    /// </summary>
    /// <param name="image">The target image</param>
    /// <param name="region"></param>
    /// <returns></returns>
    public ComponentImage FindBestMatch(Bitmap image, Rectangle region)
    {
        Color regionMeanColor = ImageProcessor.CalculateMeanColor(image, region);
        return images.NearestNeighbor(regionMeanColor);
    }
}
