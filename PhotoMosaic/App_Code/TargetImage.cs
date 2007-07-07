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
using System.Collections.Generic;

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

    /// <summary>
    /// Get and return list L of regions of TargetImage
    /// 
    /// Let N = horizontal resolution of finished image (number of AdjustedComponentImage's per row in ResultImage)
    /// Let M = vertical resolution of finished image (number of AdjustedComponentImage's per column in ResultImage)
    ///         in other words, the ResultImage will be an MxN matrix of AdjustedComponentImage's)
    /// Let dx = T.width / N (width of AdjustedComponentImage)
    /// Let dy = T.height / M (height of AdjustedComponentImage)
    /// 
    /// for (x = 0; x < T.width; x += dx)
    ///     for (y = 0; y < T.height; y += dy)
    ///         Rectangle region = new Rectangle(x, y, dx, dy)
    ///         L.Add(BitmapUtil.GetRegion(T, region))
    ///     end
    /// end
    /// </summary>
    public List<TargetImageRegion> GetRegions(int numImagesPerRow, int numImagesPerCol)
    {
        List<TargetImageRegion> result = new List<TargetImageRegion>();
        double dx = (double)Image.Width / numImagesPerRow;
        double dy = (double)Image.Height / numImagesPerCol;
        int aciWidth = (int)Math.Round(dx); // adjusted component image width
        int aciHeight = (int)Math.Round(dy); // adjusted component image height
        double epsilon = 0.9; // To be more robust against rounding errors with doubles.
        for (double x = 0; x < Image.Width + epsilon; x += dx)
        {
            for (double y = 0; y < Image.Height + epsilon; y += dy)
            {
                Region region = new Region(new Rectangle(
                    (int)Math.Round(x),
                    (int)Math.Round(y),
                    aciWidth,
                    aciHeight));
                result.Add(this.GetImageRegion(region));
            }
        }
        return result;
    }

    public TargetImageRegion GetImageRegion(Region region)
    {
        return new TargetImageRegion(this, region);
    }
}
