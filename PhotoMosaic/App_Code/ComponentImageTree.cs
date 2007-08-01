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
    public enum ColorComponent { R = 0, G = 1, B = 2 }

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

    public class ColorRegion
    {
        public byte[] min;
        public byte[] max;

        public byte minR { get { return min[(int)ColorComponent.R]; } }
        public byte minG { get { return min[(int)ColorComponent.G]; } }
        public byte minB { get { return min[(int)ColorComponent.B]; } }
        public byte maxR { get { return max[(int)ColorComponent.R]; } }
        public byte maxG { get { return max[(int)ColorComponent.G]; } }
        public byte maxB { get { return max[(int)ColorComponent.B]; } }

        public ColorRegion()
        {
            this.min = new byte[3];
            this.max = new byte[3];
        }

        public ColorRegion(ColorRegion region) : this()
        {
            Array.Copy(region.min, this.min, this.min.Length);
            Array.Copy(region.max, this.max, this.max.Length);
        }

        static public ColorRegion MaxColorRegion()
        {
            ColorRegion result = new ColorRegion();
            result.max[(int)ColorComponent.R] = byte.MaxValue;
            result.max[(int)ColorComponent.G] = byte.MaxValue;
            result.max[(int)ColorComponent.B] = byte.MaxValue;
            return result;
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

    #endregion

    #region Nearest Neighbor Logic

    public ComponentImage NearestNeighbor(Color c)
    {
        int minDistanceSquared;
        return NearestNeighbor(out minDistanceSquared, c, this.root, ColorRegion.MaxColorRegion(), int.MaxValue);
    }

    /// <summary>
    /// 
    /// Let c[i] be the ith color component of the color c, where i = R, G, or B.
    /// or example, black[R] = 0, white[G] = 255, and cyan[B] = 255.
    /// 
    /// 
    /// Let c be the target color.
    /// Let t be the color of the root node of the tree.
    /// Let i be the dimension (color component) along which the root node splits the space.
    /// Let r be the the region (initially infinite) in which to search.
    /// Let m be the maximum allowed distance (initially infinite) between the target color
    ///     and the nearest neighbor.
    /// 
    /// 
    /// Let n_{r,d} be the nearest neighbor that lies within region r and has a distance at most
    ///     m from c (n_{r,d} may not exist, in which case n = null).
    /// Let d be the distance between the target color c and the nearest neighbor n_{r,d}.
    ///     (n_{r,d} may not exist, in which case d = m).
    /// 
    /// Recursive algorithm: NearestNeighbor(c, t, r, m) returns (n_{r,m}, d_{r,m})
    /// 
    /// If tree is empty, return closest object as null and minimum distance as infinity
    /// 
    /// Otherwise,
    /// If the c[i] is less than or equal to t[i], then c lies in the left side of the tree,
    /// so we'll search there first as an initial approximation of where the nearest neighbor
    /// ought to be.  Otherwise, c lies in the right side, and we'll search there first.
    /// 
    /// Let t1 be the root of the side of the tree that c lies in, and let t2 be the root of
    /// the other side.  Also, let r1 be the region represented by the side containing c while
    /// r2 represents the other region.
    /// 
    /// Let m1 = m, and recursively call NearestNeighbor(c, t1, r1, m1), which returns (n1, d1).
    ///
    /// If the distance between c and r2 is greater than or equal to d1, then there could not
    /// be a color closer to c than m2, so we stop looking and return (n1, d1).
    /// 
    /// Otherwise,
    /// Let m2 = min( d1, distance(c,t) ), and recursively call
    /// NearestNeighbor(c, t2, r2, m2), which returns (n2, d2).
    /// 
    /// If d1 is smaller than distance(c,t) and d2, return (n1, d1).
    /// If distance(c,t) is smaller than d1 and d2, return (t, distance(c,t)).
    /// If d2 is smaller than d1 and distance(c,t), return (n2, d2).
    /// 
    /// <param name="minDistanceSquared">d_{r,m}^2</param>
    /// <param name="target">c</param>
    /// <param name="tree">t</param>
    /// <param name="region">r</param>
    /// <param name="maxDistanceSquared">m^2</param>
    /// <returns>n_{r,m}</returns>
    private ComponentImage NearestNeighbor(out int minDistanceSquared, Color target, Node tree, ColorRegion region, int maxDistanceSquared)
    {
        if (tree == null)
        {
            minDistanceSquared = int.MaxValue;
            return null;
        }

        ColorComponent axis = tree.axis; // i
        byte rootAxisValue = AxisValue(tree.componentImage.MeanColor, axis); // t[i]

        Node tree1; // t1
        Node tree2; // t2
        ColorRegion region1 = new ColorRegion(region); // r1
        ColorRegion region2 = new ColorRegion(region); // r2

        if (AxisValue(target, axis) <= AxisValue(tree.componentImage.MeanColor, axis))
        {
            tree1 = tree.left;
            tree2 = tree.right;
            region1.max[(int)axis] = rootAxisValue;
            region2.min[(int)axis] = rootAxisValue;
        }
        else
        {
            tree1 = tree.right;
            tree2 = tree.left;
            region1.min[(int)axis] = rootAxisValue;
            region2.max[(int)axis] = rootAxisValue;
        }

        // First recursive call
        int minDistanceSquared1; // d1^2
        ComponentImage nearest1 = NearestNeighbor(out minDistanceSquared1, target, tree1, region1, maxDistanceSquared);

        if (ColorUtil.DistanceSquared(target, region2) > minDistanceSquared1)
        {
            minDistanceSquared = minDistanceSquared1;
            return nearest1;
        }

        int distanceToRootSquared = ColorUtil.DistanceSquared(target, tree.componentImage.MeanColor);

        // Second recursive call
        int minDistanceSquared2; // d2^2
        ComponentImage nearest2 = NearestNeighbor(out minDistanceSquared2, target, tree2, region2, Math.Min(minDistanceSquared1, distanceToRootSquared));

        // Return nearest of n1, t, and n2
        if (minDistanceSquared1 <= distanceToRootSquared)
        {
            if (minDistanceSquared1 <= minDistanceSquared2)
            {
                minDistanceSquared = minDistanceSquared1;
                return nearest1;
            }
            else
            {
                minDistanceSquared = minDistanceSquared2;
                return nearest2;
            }
        }
        else if (distanceToRootSquared <= minDistanceSquared2)
        {
            minDistanceSquared = distanceToRootSquared;
            return tree.componentImage;
        }
        else
        {
            minDistanceSquared = minDistanceSquared2;
            return nearest2;
        }
    }

    #endregion

    private byte AxisValue(Color c, ColorComponent axis)
    {
        switch (axis)
        {
            case ColorComponent.R: return c.R;
            case ColorComponent.G: return c.G;
            case ColorComponent.B: return c.B;
            default: throw new Exception("Color component " + axis.ToString() + " undefined.");
        }
    }

    private byte AxisValue(ComponentImage ci, ColorComponent axis)
    {
        return AxisValue(ci.MeanColor, axis);
    }
}
