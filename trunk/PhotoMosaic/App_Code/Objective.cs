using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Objective
/// </summary>
public class Objective
{
    private TargetImage target;
    /// <summary>
    /// Target image.
    /// </summary>
    public TargetImage Target
    {
        get
        {
            return target;
        }
    }

    private ImageDatabase imageDb;
    /// <summary>
    /// Database of adjusted component images
    /// </summary>
    public ImageDatabase ImageDatabase
    {
        get
        {
            return imageDb;
        }
    }

    private ResultImage resultImage;
    public ResultImage ResultImage
    {
        get
        {
            return resultImage;
        }
    }

    private int numImagesPerRow;
    /// <summary>
    /// Number of adjusted component images per row in result image
    /// </summary>
    public int NumImagesPerRow
    {
        get
        {
            return numImagesPerRow;
        }
    }

    private int numImagesPerCol;
    /// <summary>
    /// Number of adjusted component images per column in result image
    /// </summary>
    public int NumImagesPerCol
    {
        get
        {
            return numImagesPerCol;
        }
    }

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
            return (double)Target.Image.Width / NumImagesPerRow;
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
            return (double)Target.Image.Height / NumImagesPerCol;
        }
    }

	public Objective(TargetImage target, int numImagesPerRow, int numImagesPerCol)
	{
        this.target = target;
        this.numImagesPerRow = numImagesPerRow;
        this.numImagesPerCol = numImagesPerCol;
        this.imageDb = new ImageDatabase();
        this.resultImage = new ResultImage(target.Image.Width, target.Image.Height);
	}

    internal void makeResultImage()
    {
        throw new Exception("The method or operation is not implemented.");        
    }

    internal void makeListOfTargetRegions()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    internal void adjustImages()
    {
        throw new Exception("The method or operation is not implemented.");
    }

}
