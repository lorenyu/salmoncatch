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
        UserInput userInput;

        //Initalize struct
        userInput.targetImageFilename = userInput.componentImageDirectory = "";
        userInput.numHorizontalImages = userInput.numVerticalImages = 10;
        userInput.userName = userInput.userID = userInput.tempFrob  = "";
        userInput.flickr = null;
        
        userInput.targetImageFilename = DropDownList1.SelectedValue;
        userInput.componentImageDirectory = DropDownList2.SelectedValue;

        userInput.userName = UsernameTextBox.Text;
        
        try
        {
            FlickrUtil flickr = new FlickrUtil();
            userInput.flickr = flickr.NewFlickr();
            userInput.userID = flickr.GetUserID(userInput);
            flickr.getAllPublicPhotos(userInput);
            
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
            println(ex.StackTrace);
            return;
        }
    }

    private Objective CreateObjective(UserInput userInput)
    {
        // Obtain target image
        println("TargetImage at " + Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename));
        Bitmap targetImage = new Bitmap(Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename));

        Objective result = new Objective();
        result.targetImage = targetImage;
        result.numImagesPerRow = userInput.numHorizontalImages;
        result.numImagesPerCol = userInput.numVerticalImages;

        // Size of adjusted component images in result image
        Size adjustedComponentImageSize = new Size(result.AdjustedComponentImageWidth, result.AdjustedComponentImageHeight);

        // Get component images from directory
        println("Color directory at " + Path.Combine(Settings.COLORGENERATOR_PATH, userInput.componentImageDirectory));
        string[] filenames = Directory.GetFiles(Path.Combine(Settings.COLORGENERATOR_PATH, userInput.componentImageDirectory));
        List<ComponentImage> adjustedComponentImages = new List<ComponentImage>();
        foreach (string filename in filenames)
        {
            try
            {
                Bitmap componentImage = new Bitmap(filename);
                Bitmap adjustedComponentImage = new Bitmap(componentImage, adjustedComponentImageSize);
                adjustedComponentImages.Add(new ComponentImage(adjustedComponentImage));
                componentImage.Dispose();   // Dispose of pre-adjusted component image
            }
            catch
            {
                // Ignore exceptions thrown by the Bitmap constructor that occur
                // when we try to open a file in the color directory that is not
                // an image file (e.g. "Thumbs.db")
            }
        }
        result.imageDb = new ImageDatabase(adjustedComponentImages);

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

    protected void TestButton_Click(object sender, EventArgs e)
    {
        Objective result = new Objective();
        result.targetImage = new Bitmap(Path.Combine(Settings.IMAGES_PATH, DropDownList1.SelectedValue));
        try
        {
            result.numImagesPerRow = int.Parse(NumHorizontalImagesTextbox.Text);
            result.numImagesPerCol = int.Parse(NumVerticalImagesTextbox.Text);
        }
        catch
        {
        }
        Size adjustedComponentImageSize = new Size(result.AdjustedComponentImageWidth, result.AdjustedComponentImageHeight);



        Stopwatch timer;
        string[] filenames;
        List<Bitmap> images = new List<Bitmap>();
        List<Bitmap> resizedImages = new List<Bitmap>();;
        filenames = Directory.GetFiles(Path.Combine(Settings.COLORGENERATOR_PATH, DropDownList2.SelectedValue));

        timer = new Stopwatch();
        foreach (string filename in filenames)
        {
            try
            {
                images.Add(new Bitmap(filename));
            }
            catch
            {
            }
        }
        println("Time to load images: " + timer.timeElapsed());

        timer = new Stopwatch();
        foreach (Bitmap image in images)
        {
            resizedImages.Add(new Bitmap(image, adjustedComponentImageSize));
        }
        println("Time to resize images: " + timer.timeElapsed());

        timer = new Stopwatch();
        foreach (Bitmap resizedImage in resizedImages)
        {
            new ComponentImage(resizedImage);
        }
        println("Time to compute mean colors on resized images: " + timer.timeElapsed());

        timer = new Stopwatch();
        foreach (Bitmap image in images)
        {
            new ComponentImage(image);
        }
        println("Time to compute mean colors on original images: " + timer.timeElapsed());
    }
}
