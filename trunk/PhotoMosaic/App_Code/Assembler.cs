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
        Color color;
        ImageQuery imageQuery = new ImageQuery();
        ComponentImage componentImage;
        FinishedImage finishedImage = new FinishedImage(objective.Target.Image.Height, objective.Target.Image.Width);

        //Graphics graphics = Graphics.FromImage(objective.Target.Image);
        
        for (int i = 0; i < objective.Target.Image.Height; i++)
        {
            for (int j = 0; j < objective.Target.Image.Width; j++)
            {
                color = objective.Target.Image.GetPixel(i,j);
                componentImage = objective.ImageDatabase.ComponentImageLookup(imageQuery);
                finishedImage.addImage(componentImage);
            }
        }
    }
}
