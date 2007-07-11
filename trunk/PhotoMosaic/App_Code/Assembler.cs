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
        //for loop around image
        //  find best image
        //objective.ResultImage.addNextImage(componentImage);

        Bitmap result = new Bitmap(objective.Target.Image.Width, objective.Target.Image.Height);
        Graphics g = Graphics.FromImage(result);

        Bitmap image = objective.Target.Image;

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
                ComponentImage ci = objective.ImageDatabase.FindBestMatch(objective.Target.Image, region);
                g.DrawImageUnscaledAndClipped(ci.Image, region);
            }
        }

        result.Save(@"C:\Documents and Settings\Loren Yu\Desktop\SVNSalmonCatch\PhotoMosaic\images\resultimage.png", System.Drawing.Imaging.ImageFormat.Png);
    }
}
