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
/// Contains comparison methods and a distance metric for class Color
/// </summary>
public static class ColorUtil
{
    public static int CompareR(Color c1, Color c2)
    {
        return c1.R.CompareTo(c2.R);
    }

    public static int CompareG(Color c1, Color c2)
    {
        return c1.R.CompareTo(c2.R);
    }

    public static int CompareB(Color c1, Color c2)
    {
        return c1.B.CompareTo(c2.B);
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
        return Math.Sqrt(DistanceSquared(c1, c2));
    }

    public static int DistanceSquared(Color c1, Color c2)
    {
        int dr = c1.R - c2.R;
        int dg = c1.G - c2.G;
        int db = c1.B - c2.B;
        return (dr * dr) + (dg * dg) + (db * db);
    }

    /// <summary>
    /// Distance squared from a Color to a ColorRegion
    /// 
    /// Let c be a color, and let r = [[r1,r2],[g1,g2],[b1,b2]] be a color region.
    /// Let p be the closest color in r to c.
    /// This function returns (c[R] - p[R])^2 + (c[G] - p[G])^2 + (c[B] - p[B])^2
    /// 
    /// To calculate this we find p[R], p[G], and p[B] separately.
    /// 
    /// For example, to find p[R], we observe that
    /// p[R] = c[R]     if c[R] is in the closed interval [r1,r2],
    /// p[R] = r1       if c[R] is less than r1, and
    /// p[R] = r2       if c[R] is greater than r2.
    /// 
    /// Similar results hold for p[G] and p[B].
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <param name="region"></param>
    /// <returns></returns>
    public static int DistanceSquared(Color c, ComponentImageTree.ColorRegion region)
    {
        int dr = 0; // c[R] - p[R]
        int dg = 0; // c[G] - p[G]
        int db = 0; // c[B] - p[B]

        // compute dr
        if (c.R < region.minR)
            dr = c.R - region.minR;
        else if (c.R > region.maxR)
            dr = c.R - region.maxR;

        // do the same for dg and db
        if (c.G < region.minG)
            dg = c.G - region.minG;
        else if (c.G > region.maxG)
            dg = c.G - region.maxG;

        if (c.B < region.minB)
            db = c.B - region.minB;
        else if (c.B > region.maxB)
            db = c.B - region.maxB;

        return (dr * dr) + (dg * dg) + (db * db);
    }
}
