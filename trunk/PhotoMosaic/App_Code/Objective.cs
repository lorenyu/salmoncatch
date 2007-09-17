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
/// A struct that specifies the constraints for creating the photomosaic
/// </summary>

public enum LevelOfDetail
{
    LOWEST,
    LOW,
    MEDIUM,
    HIGH,
    HIGHEST,
    DEFAULT
}

public enum AssembleQuality
{
    LOW,
    MEDIUM,
    HIGH,
    HIGHEST,
    DEFAULT
}

public class Objective
{
    public Objective(Bitmap targetImage, List<Bitmap> images)
    {
        this.targetImage = targetImage;
        this.images = images;

        this.LevelOfDetail = LevelOfDetail.MEDIUM;
        this.quality = AssembleQuality.DEFAULT;
        this.scalingFactor = 1.0;
    }

    public Bitmap targetImage;
    public List<Bitmap> images;
    public int numImagesPerRow;
    public int numImagesPerCol;
    public AssembleQuality quality;
    public double scalingFactor;

    public LevelOfDetail LevelOfDetail
    {
        set
        {
            int resolution = targetImage.Width * targetImage.Height;
            
            switch (value)
            {
                case LevelOfDetail.LOWEST: DesiredComponentImageCount = 10 * 10;
                    break;
                case LevelOfDetail.LOW: DesiredComponentImageCount = 25 * 25;
                    break;
                case LevelOfDetail.MEDIUM: DesiredComponentImageCount = 50 * 50;
                    break;
                case LevelOfDetail.HIGH: DesiredComponentImageCount = 75 * 75;
                    break;
                case LevelOfDetail.HIGHEST: DesiredComponentImageCount = 100 * 100;
                    break;
                case LevelOfDetail.DEFAULT: goto case LevelOfDetail.MEDIUM;
                    break;
            }
        }
    }

    private int DesiredComponentImageCount
    {
        set
        {
            double aspectRatio = (double)targetImage.Width / targetImage.Height;
            numImagesPerRow = (int)Math.Sqrt(value * aspectRatio);
            numImagesPerCol = (int)(numImagesPerRow / aspectRatio);
        }
    }
}
