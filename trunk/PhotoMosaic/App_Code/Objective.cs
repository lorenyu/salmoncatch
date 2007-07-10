using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Objective
/// </summary>
public class Objective
{
    private TargetImage target;
    public TargetImage Target
    {
        get
        {
            return target;
        }
    }

    private ImageDatabase imageDb;
    public ImageDatabase ImageDatabase
    {
        get
        {
            return imageDb;
        }
    }

    private ResultImage resultImage;
    public ResultImage ResultImage
    {
        get
        {
            return resultImage;
        }
    }

	public Objective(TargetImage target)
	{
        int finalWidth= 400;
        int finalHeight = 400;
        this.target = target;
        this.imageDb = new ImageDatabase();
        this.resultImage = new ResultImage(finalWidth, finalHeight);
	}

    internal void makeResultImage()
    {
        throw new Exception("The method or operation is not implemented.");        
    }

    internal void makeListOfTargetRegions()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    internal void adjustImages()
    {
        throw new Exception("The method or operation is not implemented.");
    }

}

/*
 * Code Graveyard
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
*/