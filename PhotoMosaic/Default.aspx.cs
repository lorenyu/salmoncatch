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
//using FlickrNet;

public partial class _Default : System.Web.UI.Page 
{
    private static string FLICKR_API_KEY = "76689f3376d2752abacb6bac4c12f580";
    private static string FLICKR_API_SECRET = "a095ecfb543abc2d";

    public UserInput userInput;

    private Objective CreateObjective(UserInput userInput)
    {
        Objective result = new Objective(new TargetImage(new Bitmap(userInput.targetImageFilename)));

        List<Bitmap> images = FileManager.LoadComponentImages(userInput.componentImageDirectory);
        foreach (Bitmap image in images)
        {
            result.ImageDatabase.AddImage(image);
        }
        return result;
    }

    protected void AssembleButton_Click(object sender, EventArgs e)
    {
        //Flickr flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);

        debugLabel.Text = "Current Directory is " + System.IO.Directory.GetCurrentDirectory();

        Assembler assembler = new Assembler();

        userInput.targetImageFilename = DropDownList1.SelectedValue;

        Objective objective = CreateObjective(userInput);
        assembler.Assemble(objective);
    }
}
