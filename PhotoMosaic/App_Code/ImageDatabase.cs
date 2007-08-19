using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

/// <summary>
/// Summary description for ImageDatabase
/// </summary>
public class ImageDatabase
{
    #region Old Code

    public ImageDatabase(List<ComponentImage> adjustedComponentImages)
    {
        this.cis = adjustedComponentImages.ToArray();
    }

    ComponentImage[] cis; // for linear search

    /// <summary>
    /// Returns the adjusted component image in the database that
    /// best matches the specified region of the target image.
    /// Returns null if image database is empty.
    /// </summary>
    /// <param name="image">The target image</param>
    /// <param name="region"></param>
    /// <returns></returns>
    public ComponentImage FindBestMatch(Color color)
    {
        // linear search
        int minDistanceSquared = int.MaxValue;
        ComponentImage bestImage = null;

        foreach (ComponentImage ci in cis)
        {
            int distanceSquared = ColorUtil.DistanceSquared(color, ci.MeanColor);
            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                bestImage = ci;
            }
        }
        return bestImage;
    }

    #endregion
}
