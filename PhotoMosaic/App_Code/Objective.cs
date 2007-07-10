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
    public TargetImage Target
    {
        get
        {
            return target;
        }
    }

    private ImageDatabase imageDb;
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
    public int NumImagesPerRow
    {
        get
        {
            return numImagesPerRow;
        }
    }

    private int numImagesPerCol;
    public int NumImagesPerCol
    {
        get
        {
            return numImagesPerCol;
        }
    }

    // TODO: Handle edge cases
    public int AdjustedComponentImageWidth
    {
        get
        {
            return Target.Image.Width / NumImagesPerRow;
        }
    }

    // TODO: Handle edge cases
    public int AdjustedComponentImageHeight
    {
        get
        {
            return Target.Image.Height / NumImagesPerCol;
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
