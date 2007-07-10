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
/// Summary description for ResultImage
/// </summary>
public class ResultImage
{
    private Bitmap b;
    private Graphics g;
    private int x;
    private int y;

	public ResultImage(int width, int height)
	{
        this.b = new Bitmap(width, height);
        this.g = Graphics.FromImage(b);
        x = 0;
        y = 0;
    }

    internal void addNextImage(ComponentImage componentImage)
    {
        g.DrawImage(componentImage.Image, x, y);
        x += componentImage.Image.Width;
        if(x > b.Width){
           y += componentImage.Image.Height;
           x = 0;
        }
    }
}

