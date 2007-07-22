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
        return Math.Sqrt(
            Math.Pow((c1.R - c2.R), 2) +
            Math.Pow((c1.G - c2.G), 2) +
            Math.Pow((c1.B - c2.B), 2));
    }
}
