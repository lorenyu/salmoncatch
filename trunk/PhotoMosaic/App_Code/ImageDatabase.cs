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
    /// <summary>
    /// Temporary way of storing images
    /// </summary>
    private List<ComponentImage> images;

	public ImageDatabase()
	{
        this.images = new List<ComponentImage>();
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

    public void AddImage(Bitmap image)
    {
        this.images.Add(new ComponentImage(image));
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
        double minDistance = double.MaxValue;
        ComponentImage bestCi = null;

        foreach (ComponentImage ci in this.images)
        {
            Color regionMeanColor = ImageProcessor.CalculateMeanColor(image, region);
            double distance = ImageProcessor.Distance(regionMeanColor, ci.MeanColor);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestCi = ci;
            }
        }

        return bestCi;
    }
}
