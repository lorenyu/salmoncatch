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
        this.scalingFactor = 2.0;
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
            switch (value)
            {
                case LevelOfDetail.LOW: PixelsPerRegion = 18;
                    break;
                case LevelOfDetail.MEDIUM: PixelsPerRegion = 12;
                    break;
                case LevelOfDetail.HIGH: PixelsPerRegion = 6;
                    break;
                case LevelOfDetail.HIGHEST: PixelsPerRegion = 3;
                    break;
                case LevelOfDetail.DEFAULT: PixelsPerRegion = 12;
                    break;
            }
        }
    }

    private int PixelsPerRegion
    {
        set
        {
            numImagesPerRow = targetImage.Width / value;
            numImagesPerCol = targetImage.Height / value;
        }
    }
}
