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
//using FlickrNet;

public partial class _Default : System.Web.UI.Page 
{
    private static string FLICKR_API_KEY = "76689f3376d2752abacb6bac4c12f580";
    private static string FLICKR_API_SECRET = "a095ecfb543abc2d";

    protected void AssembleButton_Click(object sender, EventArgs e)
    {
        //Flickr flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);

        debugLabel.Text = "Current Directory is " + System.IO.Directory.GetCurrentDirectory();

        Assembler assembler = new Assembler();
        //Objective objective = new Objective(new TargetImage(new Bitmap(TargetImageLocationTextbox.Text)));
        //ImportManager.ImportImagesToDatabase(objective.ImageDatabase, ImportManager.ImportMethod.FileSystem);
        //assembler.Assemble(objective);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
