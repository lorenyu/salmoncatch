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
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    private static string FLICKR_API_KEY = "76689f3376d2752abacb6bac4c12f580";
    private static string FLICKR_API_SECRET = "a095ecfb543abc2d";

    public UserInput userInput = new UserInput();

    private Objective CreateObjective(UserInput userInput)
    {
        // Obtain target image
        TargetImage targetImage = new TargetImage(Resources.Resource.LorenTargetImage1);
        Objective result = new Objective(targetImage);

        // Calculate size of adjusted component images in result image
        int adjustedComponentImageWidth = targetImage.Image.Width / userInput.numHorizontalImages; // TODO: Handle edge cases
        int adjustedComponentImageHeight = targetImage.Image.Height / userInput.numVerticalImages; // TODO: Handle edge cases
        Size adjustedComponentImageSize = new Size(adjustedComponentImageWidth, adjustedComponentImageHeight);

        // Get component images from directory
        string[] filenames = Directory.GetFiles(userInput.componentImageDirectory);
        foreach (string filename in filenames)
        {
            Bitmap componentImage = new Bitmap(filename);
            Bitmap adjustedComponentImage = new Bitmap(componentImage, adjustedComponentImageSize);
            result.ImageDatabase.AddImage(adjustedComponentImage);
            componentImage.Dispose();
        }

        debugImage.Width = targetImage.Image.Width;
        debugImage.Height = targetImage.Image.Height;
        debugImage.ImageUrl = userInput.targetImageFilename;

        return result;
    }

    protected void AssembleButton_Click(object sender, EventArgs e)
    {
        debugLabel.Text = "Current Directory is " + System.IO.Directory.GetCurrentDirectory();

        // Create instance of UserInput and populate it with user entered data on web page
        userInput.targetImageFilename = DropDownList1.SelectedValue;
        userInput.componentImageDirectory = DropDownList2.SelectedValue;
        try
        {
            userInput.numHorizontalImages = int.Parse(NumHorizontalImagesTextbox.Text);
            userInput.numVerticalImages = int.Parse(NumVerticalImagesTextbox.Text);

            debugLabel.Text += "<br/>\nTargetImage at " + userInput.targetImageFilename;

            Objective objective = CreateObjective(userInput);
            //Assembler assembler = new Assembler();
            //assembler.Assemble(objective);
        }
        catch (Exception ex)
        {
            // Error handling
            debugLabel.Text += "<br/>\n" + ex.Message;
            return;
        }
    }
}
