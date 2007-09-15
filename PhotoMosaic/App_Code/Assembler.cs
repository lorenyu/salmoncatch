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
using System.Collections.Generic;

public class Assembler
{
    int quality;
    int numCols;
    int numRows;
    int minDistanceBetweenDuplicates;
    double defaultScalingFactor;
    Bitmap original;
    List<Bitmap> cis;
    int ciWidth;
    int ciHeight;

    // Result of assemble
    ComponentImage[,] grid;
    // TODO: this doesn't seem like the most efficient way
    Color[,] regionMeanColors;

    public Assembler(Objective objective)
    {
        if (objective.images.Count <= 0) throw new Exception("No images to assemble.");

        original = objective.targetImage;
        switch (objective.quality)
        {
            case AssembleQuality.LOW: quality = 1;
                break;
            case AssembleQuality.MEDIUM: quality = 2;
                break;
            case AssembleQuality.HIGH: quality = 3;
                break;
            case AssembleQuality.HIGHEST: quality = 4;
                break;
            case AssembleQuality.DEFAULT: quality = 3;
                break;
        }
        numRows = objective.numImagesPerCol;
        numCols = objective.numImagesPerRow;
        defaultScalingFactor = objective.scalingFactor;
        minDistanceBetweenDuplicates = 2; // TODO: make this a parameter

        ciWidth = (int)((double)defaultScalingFactor * original.Width / numCols);
        ciHeight = (int)((double)defaultScalingFactor * original.Height / numRows);
        Size ciSize = new Size(ciWidth, ciHeight);

        cis = ImageProcessor.ScaleAndClipImages(objective.images, ciSize);
    }

    public void Assemble()
    {
        // Resize images for fast comparison
        // For now we use simple low quality resizing

        // Resize component images
        Size compareSize = new Size(quality, quality);
        List<Bitmap> thumbs = ImageProcessor.ResizeImages(cis, compareSize);

        // Resize target image
        // TODO: later we can add a nice resize method to ImageProcessor
        Size newSize = new Size(quality * numCols, quality * numRows);
        Bitmap targetImage = ImageProcessor.ResizeTiny(original, newSize);

        // Create M x N matrix to store the resulting mapping from regions to component images
        grid = new ComponentImage[numRows, numCols];
        regionMeanColors = new Color[numRows, numCols];

        Rectangle region = new Rectangle(new Point(0,0), compareSize);
        for (int i = 0; i < numRows; i++, region.Y += quality)
        {
            region.X = 0;
            for (int j = 0; j < numCols; j++, region.X += quality)
            {
                grid[i, j] = FindClosestImage(thumbs, targetImage, region, i, j);
                regionMeanColors[i, j] = ImageProcessor.CalculateMeanColor(targetImage, region);   
            }
        }
    }

    public Bitmap GetResultImage()
    {
        return GetResultImage(defaultScalingFactor);
    }

    public Bitmap GetResultImage(double scalingFactor)
    {
        Bitmap result = new Bitmap(numCols * ciWidth, numRows * ciHeight);

        Graphics g = Graphics.FromImage(result);
        Rectangle region = new Rectangle(0, 0, ciWidth, ciHeight);
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                region.X = j * ciWidth;
                region.Y = i * ciHeight;

                ComponentImage ci = grid[i, j];
                ImageAttributes imageAttributes = ImageProcessor.ImageAttributesFromColorTransform(ci.MeanColor, regionMeanColors[i, j]);
                g.DrawImage(ci.Image,
                    region,
                    0, 0, ciWidth, ciHeight,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
        }
        return result;
    }

    public ComponentImage FindClosestImage(List<Bitmap> images, Bitmap target, Rectangle region, int row, int col)
    {
        // linear search
        long minDistance = long.MaxValue;
        int best = -1;

        for (int i = 0; i < images.Count; i++)
        {
            if (IsImageNearby(this.cis[i], row, col)) continue;

            long rmsDistance = ImageProcessor.DistanceSquared(images[i], target, region);
            if (rmsDistance < minDistance)
            {
                minDistance = rmsDistance;
                best = i;
            }
        }

        if (best == -1) throw new Exception("Not enough component images given constraints.");

        return new ComponentImage(this.cis[best]);
    }

    private bool IsImageNearby(Bitmap image, int row, int col)
    {
        int d = minDistanceBetweenDuplicates;
        int startCol = Math.Max(col - d, 0);
        int endCol = Math.Min(col + d, numCols - 1);

        // Check previous rows
        for (int i = Math.Max(row - d, 0); i < row; i++)
        {
            for (int j = startCol; j <= endCol; j++)
            {
                if (grid[i, j].Image == image)
                {
                    return true;
                }
            }
        }

        // Check current row before current cell
        for (int j = startCol; j < col; j++)
        {
            if (grid[row, j].Image == image)
            {
                return true;
            }
        }

        return false;
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
    /// <param name="objective"></param>
    /*
    public Bitmap AssembleResultImage(Objective objective)
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
                ComponentImage ci = objective.imageDb.FindBestMatch(regionMeanColor);

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
     * */
}
