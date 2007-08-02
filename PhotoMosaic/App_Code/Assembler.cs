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
using System.Drawing.Imaging;

public class Assembler
{
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
    /// <param name="objective"></param>
    public Bitmap Assemble(Objective objective)
    {
        Bitmap result = new Bitmap(objective.targetImage.Width, objective.targetImage.Height);
        Graphics g = Graphics.FromImage(result);

        Bitmap image = objective.targetImage;

        double dx = objective.FAdjustedComponentImageWidth;
        double dy = objective.FAdjustedComponentImageHeight;
        int aciWidth = objective.AdjustedComponentImageWidth;
        int aciHeight = objective.AdjustedComponentImageHeight;
        double epsilon = 0.9; // To be more robust against rounding errors with doubles.

        Stopwatch.nearestNeighborStopwatch.Reset();
        for (double x = 0; x < image.Width + epsilon; x += dx)
        {
            for (double y = 0; y < image.Height + epsilon; y += dy)
            {
                Rectangle region = new Rectangle(
                    (int)x,
                    (int)y,
                    aciWidth,
                    aciHeight);
                if (region.Width <= 0 || region.Height <= 0)
                    continue;
                Color regionMeanColor = ImageProcessor.CalculateMeanColor(objective.targetImage, region);
                Stopwatch.nearestNeighborStopwatch.Start();
                ComponentImage ci = objective.imageDb.FindBestMatch(regionMeanColor);
                Stopwatch.nearestNeighborStopwatch.Stop();

                bool adjustComponentImageColor = true;
                if (adjustComponentImageColor)
                {
                    int width = ci.Image.Width;
                    int height = ci.Image.Height;
                    ImageAttributes imageAttributes = ImageProcessor.ImageAttributesFromColorTransform(ci.MeanColor, regionMeanColor);
                    g.DrawImage(ci.Image,
                        region,
                        0, 0, width, height,
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }
                else
                {
                    g.DrawImageUnscaledAndClipped(ci.Image, region);
                }
            }
        }

        return result;
    }
}
