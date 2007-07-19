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

        for (double x = 0; x < image.Width + epsilon; x += dx)
        {
            for (double y = 0; y < image.Height + epsilon; y += dy)
            {
                Rectangle region = new Rectangle(
                    (int)Math.Round(x),
                    (int)Math.Round(y),
                    aciWidth,
                    aciHeight);
                ComponentImage ci = objective.imageDb.FindBestMatch(objective.targetImage, region);
                g.DrawImageUnscaledAndClipped(ci.Image, region);
            }
        }

        return result;
    }
}
