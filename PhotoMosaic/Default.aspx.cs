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
using System.Timers;
using System.Drawing.Imaging;

public partial class _Default : System.Web.UI.Page 
{
    private static string FLICKR_API_KEY = "76689f3376d2752abacb6bac4c12f580";
    private static string FLICKR_API_SECRET = "a095ecfb543abc2d";

    public static _Default page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = this;
        println();
        println("Page loaded");
        Settings.APPLICATION_PATH = this.Request.PhysicalApplicationPath;
        println("Application path at " + Settings.APPLICATION_PATH);
    }

    protected void AssembleButton_Click(object sender, EventArgs e)
    {
        Stopwatch totalTimer;
        Stopwatch objectiveTimer;
        Stopwatch assembleObjectiveTimer;

        // Create instance of UserInput and populate it with user entered data on web page
        UserInput userInput = new UserInput();
        userInput.targetImageFilename = DropDownList1.SelectedValue;
        userInput.componentImageDirectory = DropDownList2.SelectedValue;
        try
        {
            userInput.numHorizontalImages = int.Parse(NumHorizontalImagesTextbox.Text);
            userInput.numVerticalImages = int.Parse(NumVerticalImagesTextbox.Text);

            totalTimer = new Stopwatch();

            objectiveTimer = new Stopwatch();
            Objective objective = CreateObjective(userInput);
            objectiveTime.Text = objectiveTimer.timeElapsed();

            debugLabel.Text += "<br/>\nTIWidth = " + objective.targetImage.Width;
            debugLabel.Text += "<br/>\nTIHeight = " + objective.targetImage.Height;
            debugLabel.Text += "<br/>\nACIWidth = " + objective.AdjustedComponentImageWidth.ToString();
            debugLabel.Text += "<br/>\nACIHeight = " + objective.AdjustedComponentImageHeight.ToString();

            Assembler assembler = new Assembler();
            assembleObjectiveTimer = new Stopwatch();
            Bitmap resultImage = assembler.Assemble(objective);
            assembleObjectiveTime.Text = assembleObjectiveTimer.timeElapsed();

            string savePath = Path.Combine(Settings.IMAGES_PATH, Settings.RESULTIMAGE_FILENAME);
            resultImage.Save(savePath, ImageFormat.Png);
            debugImage.ImageUrl = Settings.IMAGES_URL + "/" + Settings.RESULTIMAGE_FILENAME;

            totalTime.Text = totalTimer.timeElapsed();
        }
        catch (Exception ex)
        {
            // Error handling
            println(ex.Message);
            return;
        }
    }

    private Objective CreateObjective(UserInput userInput)
    {
        // Obtain target image
        println("TargetImage at " + Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename));
        Bitmap targetImage = new Bitmap(Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename));
        Objective result = new Objective(
            targetImage,
            userInput.numHorizontalImages,
            userInput.numVerticalImages
        );

        // Size of adjusted component images in result image
        Size adjustedComponentImageSize = new Size(result.AdjustedComponentImageWidth, result.AdjustedComponentImageHeight);

        // Get component images from directory
        println("Color directory at " + Path.Combine(Settings.COLORGENERATOR_PATH, userInput.componentImageDirectory));
        string[] filenames = Directory.GetFiles(Path.Combine(Settings.COLORGENERATOR_PATH, userInput.componentImageDirectory));
        foreach (string filename in filenames)
        {
            try
            {
                Bitmap componentImage = new Bitmap(filename);
                Bitmap adjustedComponentImage = new Bitmap(componentImage, adjustedComponentImageSize);
                result.imageDb.AddImage(adjustedComponentImage);
                componentImage.Dispose(); // Dispose of pre-adjusted component image
            }
            catch
            {
            }
        }

        debugImage.Width = targetImage.Width;
        debugImage.Height = targetImage.Height;
        debugImage.ImageUrl = Settings.IMAGES_URL + "/" + userInput.targetImageFilename;

        return result;
    }

    public static void println() { println(""); }
    public static void println(string str)
    {
        page.debugLabel.Text += str + "<br/>\n";
    }
}
