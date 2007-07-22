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
/// Implementation of a three-dimensional color-based kd-tree, or RGB-tree.
/// Each of the three dimensions corresponds to the r, g, or b axis.
/// </summary>
public class ComponentImageTree
{
    enum ColorComponent { R = 0, G = 1, B = 2 }

    // A tree node
    private class Node
    {
        public ComponentImage componentImage;
        /// <summary>
        /// The color component used to compare component images at this node.
        /// Geometrically, it is axis along which the node divides the tree
        /// e.g. If axis == Axis.R, then the node divides the tree with a plane
        /// orthogonal to the R-axis (or equivalently, a plane parallel to the BG-plane).
        /// </summary>
        public ColorComponent axis;
        /// <summary>
        /// The part of the tree that contains nodes with the axis color component less than
        /// this component image's axis color component.
        /// </summary>
        public Node left;
        /// <summary>
        /// The part of the tree that contains nodes with the axis color component greater than
        /// or equal to this component image's axis color component.
        /// </summary>
        public Node right;

        public Node(ComponentImage componentImage, ColorComponent colorComponent)
        {
            this.componentImage = componentImage;
            this.axis = colorComponent;
        }
    }

    private class IndexedImage
    {
        public ComponentImage componentImage;
        public int redIndex;
        public int greenIndex;
        public int blueIndex;

        public void SetIndex(int index, ColorComponent axis)
        {
            switch (axis)
            {
                case ColorComponent.R: redIndex = index; break;
                case ColorComponent.G: greenIndex = index; break;
                case ColorComponent.B: blueIndex = index; break;
                default: throw new Exception("Color component " + axis.ToString() + " undefined.");
            }
        }

        public int GetIndex(ColorComponent axis)
        {
            switch (axis)
            {
                case ColorComponent.R: return redIndex;
                case ColorComponent.G: return greenIndex;
                case ColorComponent.B: return blueIndex;
                default: throw new Exception("Color component " + axis.ToString() + " undefined.");
            }
        }

    }

    private IndexedImage[] images;

    // The root of the tree
    private Node root;

    #region Constructor Logic

    /// <summary>
    /// NOTE: Takes O(n log n) time, where n is the number of component images
    /// </summary>
    /// <param name="componentImages"></param>
    public ComponentImageTree(List<ComponentImage> componentImages)
	{
        images = new IndexedImage[componentImages.Count];
        for (int i = 0; i < images.Length; i++)
        {
            images[i] = new IndexedImage();
            images[i].componentImage = componentImages[i];
        }

        IndexedImage[] imagesSortedByR = new IndexedImage[componentImages.Count];
        IndexedImage[] imagesSortedByG = new IndexedImage[componentImages.Count];
        IndexedImage[] imagesSortedByB = new IndexedImage[componentImages.Count];
        IndexedImage[] imagesTemp = new IndexedImage[componentImages.Count];

        int[] colorCounts = new int[byte.MaxValue + 1];;

        SortByColor(images, imagesSortedByR, colorCounts, ColorComponent.R);
        SortByColor(images, imagesSortedByG, colorCounts, ColorComponent.G);
        SortByColor(images, imagesSortedByB, colorCounts, ColorComponent.B);

        root = ConstructTree(
            imagesSortedByR,
            imagesSortedByG,
            imagesSortedByB,
            imagesTemp,
            0, componentImages.Count, ColorComponent.R);
	}

    /// <summary>
    /// 
    /// Takes an array of component images, and recursively constructs a balanced 3D kd-tree
    /// of the component images in the range [begin, end) of that array.
    /// 
    /// Notation:
    /// Let the array of component images be given by ( X_0, X_1, ... , X_{n-1} ).
    /// Let a = begin,
    ///     b = end,
    ///     k = end - begin.
    /// Let R = ( R_a, ... , R_{b-1} ) be the images ( X_a, ... , X_{b-1} ) sorted by the red
    /// component of the mean colors of x_a, ... , x_{b-1}, respectively.
    /// Similarly, let G = ( G_a, ... , G_{b-1} ) be the same range of images sorted by the
    /// green component of the mean colors of x_a, ... , x_{b-1}, and
    /// let B = ( B_a, ... , B_{b-1} ) be the images sorted by the blue component.
    /// 
    /// Let Rindex_i = Rindex( X_i ) denote the index of X_i in R, and similarly,
    /// let Gindex_i and Bindex_i denote the index of X_i in G and B, respectively.
    /// 
    /// Algorithm:
    /// 
    /// Base case: If k = 0, return null
    /// 
    /// Recursion: If we are splitting on the red color axis, then:
    /// 
    ///                Take the median image in R, given by R_{floor(i_m)}, where
    ///                i_m = (a+b-1)/2, the index of the image with the lower median value
    ///                of red in R.
    /// 
    ///                Remove the median image from R and then split R into R1 and R2,
    ///                where R1 contains the images before the median image and R2
    ///                contains the images after.
    /// 
    ///                We also remove the median image from G, and split G into G1 and G2,
    ///                where G1 contains the images whose index in R is less than i_m,
    ///                and G2 contains the images whose index in R is greater than i_m,
    ///                while keeping G1 and G2 sorted by the green component of the images.
    ///                To do this, we simply iterate through G and add the image to G1 or G2
    ///                depending on whether its index in R is less than or greater than i_m.
    ///                Since G is already sorted, G1 and G2 will be sorted as well.
    /// 
    ///                Similarly, split B into B1 and B2 after removing the median image.
    /// 
    ///                Recurse with R1, G1, and B1 with a different color axis,
    ///                and set the resulting tree to be the left child of the median image.
    /// 
    ///                Recurse with R2, G2, and B2 with a different color axis,
    ///                and set the resulting tree to be the right child of the median image.
    /// 
    ///                Return the median image as the root of the tree.
    /// 
    ///            Splitting on a different axis is analagous.
    /// 
    /// </summary>
    /// <param name="imagesSortedByR"></param>
    /// <param name="imagesSortedByG"></param>
    /// <param name="imagesSortedByB"></param>
    /// <param name="imagesTemp"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <param name="axis">The color axis along which to split the images.</param>
    /// <returns></returns>
    private Node ConstructTree(IndexedImage[] imagesSortedByR, IndexedImage[] imagesSortedByG,
        IndexedImage[] imagesSortedByB, IndexedImage[] imagesTemp, int begin, int end,
        ColorComponent axis)
    {
        if (begin >= end)
            return null;

        IndexedImage[] imagesSortedByAxis;
        switch (axis)
        {
            case ColorComponent.R: imagesSortedByAxis = imagesSortedByR; break;
            case ColorComponent.G: imagesSortedByAxis = imagesSortedByG; break;
            case ColorComponent.B: imagesSortedByAxis = imagesSortedByB; break;
            default: throw new Exception("Color component " + axis.ToString() + " undefined.");
        }

        // Find the lower median
        int medianIndex = (begin + end - 1) / 2;            // median index = i_m
        int medianAxisIndex = imagesSortedByAxis[medianIndex].GetIndex(axis);

        // Create node whose axis color component lies at the median of [begin, end)
        Node result = new Node(imagesSortedByAxis[medianIndex].componentImage, axis);

        // Split the arrays in place while keeping each subarray sorted

        // Split array R
        if (axis != ColorComponent.R)
        {
            int i1 = begin;
            int i2 = medianIndex + 1;
            for (int i = begin; i < end; i++)
            {
                int axisIndex = imagesSortedByR[i].GetIndex(axis);
                if (axisIndex < medianAxisIndex)
                {
                    imagesSortedByR[i1++] = imagesSortedByR[i];
                }
                else if (axisIndex > medianAxisIndex)
                {
                    imagesTemp[i2++] = imagesSortedByR[i];
                }
                else // axisIndex == medianAxisIndex
                {
                    imagesTemp[medianIndex] = imagesSortedByR[i];
                }
            }
            Array.Copy(imagesTemp, medianIndex, imagesSortedByR, medianIndex, end - medianIndex);
        }

        // Split array G
        if (axis != ColorComponent.G)
        {
            int i1 = begin;
            int i2 = medianIndex + 1;
            for (int i = begin; i < end; i++)
            {
                int axisIndex = imagesSortedByG[i].GetIndex(axis);
                if (axisIndex < medianAxisIndex)
                {
                    imagesSortedByG[i1++] = imagesSortedByG[i];
                }
                else if (axisIndex > medianAxisIndex)
                {
                    imagesTemp[i2++] = imagesSortedByG[i];
                }
                else // axisIndex == medianAxisIndex
                {
                    imagesTemp[medianIndex] = imagesSortedByG[i];
                }
            }
            Array.Copy(imagesTemp, medianIndex, imagesSortedByG, medianIndex, end - medianIndex);
        }

        // Split array B
        if (axis != ColorComponent.B)
        {
            int i1 = begin;
            int i2 = medianIndex + 1;
            for (int i = begin; i < end; i++)
            {
                int axisIndex = imagesSortedByB[i].GetIndex(axis);
                if (axisIndex < medianAxisIndex)
                {
                    imagesSortedByB[i1++] = imagesSortedByB[i];
                }
                else if (axisIndex > medianAxisIndex)
                {
                    imagesTemp[i2++] = imagesSortedByB[i];
                }
                else // axisIndex == medianAxisIndex
                {
                    imagesTemp[medianIndex] = imagesSortedByB[i];
                }
            }
            Array.Copy(imagesTemp, medianIndex, imagesSortedByB, medianIndex, end - medianIndex);
        }

        // Recurse.
        ColorComponent nextAxis = (ColorComponent)(((int)axis + 1) % 3);
        result.left = ConstructTree(
            imagesSortedByR, imagesSortedByG, imagesSortedByB, imagesTemp,
            begin, medianIndex, nextAxis);
        result.right = ConstructTree(
            imagesSortedByR, imagesSortedByG, imagesSortedByB, imagesTemp,
            medianIndex + 1, end, nextAxis);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="images"></param>
    /// <param name="dest"></param>
    /// <param name="colorCounts">An array used to store the counts used by this algorithm.
    ///     Passed in so the same buffer can be used across calls</param>
    /// <param name="axis"></param>
    private void SortByColor(IndexedImage[] images, IndexedImage[] dest, int[] colorCounts, ColorComponent axis)
    {
        Array.Clear(colorCounts, 0, colorCounts.Length);
        foreach (IndexedImage image in images)
        {
            colorCounts[AxisValue(image.componentImage, axis)]++;
        }
        for (int i = 1; i < colorCounts.Length; i++)
        {
            colorCounts[i] += colorCounts[i - 1];
        }
        foreach (IndexedImage image in images)
        {
            int index = --colorCounts[AxisValue(image.componentImage, axis)];
            dest[index] = image;
            dest[index].SetIndex(index, axis);
        }
    }

    private byte AxisValue(ComponentImage ci, ColorComponent axis)
    {
        switch (axis)
        {
            case ColorComponent.R: return ci.MeanColor.R;
            case ColorComponent.G: return ci.MeanColor.G;
            case ColorComponent.B: return ci.MeanColor.B;
            default: throw new Exception("Color component " + axis.ToString() + " undefined.");
        }
    }

    #endregion

    public ComponentImage NearestNeighbor(Color c)
    {
        double minDistance = double.MaxValue;
        ComponentImage bestImage = null;

        foreach (IndexedImage image in images)
        {
            double distance = ColorUtil.Distance(c, image.componentImage.MeanColor);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestImage = image.componentImage;
            }
        }
        return bestImage;
    }
}
