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
/// Summary description for ImageProcessor
/// </summary>
public static class ImageProcessor
{
    public static Color CalculateMeanColor(Bitmap image, Rectangle region)
    {
        int totalR = 0;
        int totalG = 0;
        int totalB = 0;
        int numPixels = region.Width * region.Height;

        for (int x = region.X; x < region.X + region.Width && x < image.Width; x++)
        {
            for (int y = region.Y; y < region.Y + region.Height && y < image.Height; y++)
            {
                Color color = image.GetPixel(x, y);
                totalR += color.R;
                totalG += color.G;
                totalB += color.B;
            }
        }

        int meanR = totalR / numPixels;
        int meanG = totalG / numPixels;
        int meanB = totalB / numPixels;

        return Color.FromArgb(meanR, meanG, meanB);
    }

    public static Color CalculateMeanColor(Bitmap image)
    {
        return ImageProcessor.CalculateMeanColor(
            image,
            new Rectangle(new Point(), image.Size));
    }

    /// <summary>
    /// Calculates the Euclidean distance between two colors.
    /// Equivalent to calculating the RMS (Root Mean Squared)
    /// distance using the RGB values.
    /// 
    /// If c1 = r1 g1 b1 and c2 = r2 g2 b2 then this function returns
    /// sqrt{ (r1-r2)^2 + (g1-g2)^2 + (b1-b2)^2 }
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns></returns>
    public static double Distance(Color c1, Color c2)
    {
        return Math.Sqrt(
            Math.Pow((c1.R - c2.R), 2) +
            Math.Pow((c1.G - c2.G), 2) +
            Math.Pow((c1.B - c2.B), 2));
    }
}
