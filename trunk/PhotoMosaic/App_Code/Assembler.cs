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
/// Summary description for Constructor
/// </summary>
public class Assembler
{
	public Assembler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void Assemble(Objective objective)
    {
/*      objective.adjustImages();
        objective.makeListOfTargetRegions();
        objective.makeResultImage();
*/
        Bitmap image = objective.Target.Image;

        //for loop around image
        //  find best image
        //objective.ResultImage.addNextImage(componentImage);

        double dx = objective.FAdjustedComponentImageWidth;
        double dy = objective.FAdjustedComponentImageHeight;
        int aciWidth = objective.AdjustedComponentImageWidth;
        int aciHeight = objective.AdjustedComponentImageHeight;
        double epsilon = 0.9; // To be more robust against rounding errors with doubles.
        for (double x = 0; x < image.Width + epsilon; x += dx)
        {
            for (double y = 0; y < image.Height + epsilon; y += dy)
            {
                Region region = new Region(new Rectangle(
                    (int)Math.Round(x),
                    (int)Math.Round(y),
                    aciWidth,
                    aciHeight));
                // Do something with region
            }
        }
    }
}
