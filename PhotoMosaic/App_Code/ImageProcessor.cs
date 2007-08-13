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

    public static ImageAttributes ImageAttributesFromColorTransform(Color sourceColor, Color targetColor)
    {
        float dr = (targetColor.R - sourceColor.R) / 255.0f;
        float dg = (targetColor.G - sourceColor.G) / 255.0f;
        float db = (targetColor.B - sourceColor.B) / 255.0f;

        ColorMatrix transform = new ColorMatrix(new float[][] {
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {dr, dg, db, 0, 1}});
        ImageAttributes imageAttributes = new ImageAttributes();
        imageAttributes.SetColorMatrix(transform);
        return imageAttributes;
    }

    public static Bitmap ClipImage(Bitmap image, Size size)
    {
        Bitmap result = new Bitmap(size.Width, size.Height);

        int x, y, w, h;

        int w0 = image.Size.Width;
        int h0 = image.Size.Height;
        int w1 = size.Width;
        int h1 = size.Height;

        w = w0;
        h = h0;
        x = (w1 - w0) / 2;
        y = (h1 - h0) / 2;

        Graphics g = Graphics.FromImage(result);
        g.DrawImageUnscaledAndClipped(image, new Rectangle(x, y, w, h));
        return result;
    }

    public static Bitmap ScaleAndClipImage(Bitmap image, Size size)
    {
        Bitmap result = new Bitmap(size.Width, size.Height);

        int x, y, w, h;

        int w0 = image.Size.Width;
        int h0 = image.Size.Height;
        int w1 = size.Width;
        int h1 = size.Height;

        if (w0 * h1 >= w1 * h0)
        {
            h = h1;
            w = h1 * (w0 / h0);
        }
        else
        {
            w = w1;
            h = w1 * (h0 / w0);
        }

        x = (w1 - w) / 2;
        y = (h1 - h) / 2;

        Graphics g = Graphics.FromImage(result);
        g.DrawImage(image, x, y, w, h);
        return result;
    }
}
