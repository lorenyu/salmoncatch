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
/// Summary description for Objective
/// </summary>
public class Objective
{
    public Bitmap targetImage;
    public ImageDatabase imageDb;

    public int numImagesPerRow;
    public int numImagesPerCol;

    // TODO: Handle edge cases
    /// <summary>
    /// Width of adjusted component image.
    /// </summary>
    public int AdjustedComponentImageWidth
    {
        get
        {
            return (int)FAdjustedComponentImageWidth;
        }
    }
    /// <summary>
    /// Width of adjusted component image with subpixel floating point precision.
    /// </summary>
    public double FAdjustedComponentImageWidth
    {
        get
        {
            return (double)targetImage.Width / numImagesPerRow;
        }
    }

    // TODO: Handle edge cases
    /// <summary>
    /// Height of adjusted component image.
    /// </summary>
    public int AdjustedComponentImageHeight
    {
        get
        {
            return (int)FAdjustedComponentImageHeight;
        }
    }
    /// <summary>
    /// Height of adjusted component image with subpixel floating point precision.
    /// </summary>
    public double FAdjustedComponentImageHeight
    {
        get
        {
            return (double)targetImage.Height / numImagesPerCol;
        }
    }

	public Objective(Bitmap targetImage, int numImagesPerRow, int numImagesPerCol)
	{
        this.targetImage = targetImage;
        this.numImagesPerRow = numImagesPerRow;
        this.numImagesPerCol = numImagesPerCol;
        this.imageDb = new ImageDatabase();
	}
}
