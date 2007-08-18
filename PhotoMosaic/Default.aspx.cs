using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Net;
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
        Settings.APPLICATION_PATH = this.Request.PhysicalApplicationPath;

        // Create instance of UserInput and populate it with user entered data on web page

        // TODO: UserInput userInput = new UserInput(Request.Form);
        UserInput userInput = new UserInput(Request.Form);

        // TODO: More robust error checking
        if (userInput.isValid)
        {
            try
            {
                FlickrUtil flickr = new FlickrUtil();

                // TODO: shouldn't this also download if folder is not there?
                if (userInput.redownload)
                {
                    userInput.flickr = flickr.NewFlickr();
                    userInput.userID = flickr.GetUserID(userInput);
                    flickr.getAllPublicPhotos(userInput);
                }

                if (!Settings.USE_DROP_DOWN_TARGET)
                {
                    WebClient wc = new WebClient();
                    byte[] originalData = wc.DownloadData(userInput.targetURL);
                    MemoryStream stream = new MemoryStream(originalData);
                    Bitmap targetImage = new Bitmap(stream);
                    userInput.targetImageFilename = "downloadedPhoto.Png";
                    string path = Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename);
                    targetImage.Save(path, ImageFormat.Png);
                }

                Objective objective = CreateObjective(userInput);
                Assembler assembler = new Assembler();
                Bitmap resultImage = assembler.Assemble(objective);

                string savePath = Path.Combine(Settings.IMAGES_PATH, Settings.RESULTIMAGE_FILENAME);
                resultImage.Save(savePath, ImageFormat.Png);
                debugImage.ImageUrl = Settings.IMAGES_URL + "/" + Settings.RESULTIMAGE_FILENAME;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

    private Objective CreateObjective(UserInput userInput)
    {
        // Obtain target image
        Bitmap targetImage = new Bitmap(Path.Combine(Settings.IMAGES_PATH, userInput.targetImageFilename));

        Objective result = new Objective();
        result.targetImage = targetImage;
        result.numImagesPerRow = userInput.numHorizontalImages;
        result.numImagesPerCol = userInput.numVerticalImages;

        // Size of adjusted component images in result image
        Size adjustedComponentImageSize = new Size(result.AdjustedComponentImageWidth, result.AdjustedComponentImageHeight);

        // Get component images from directory
        string[] filenames = Directory.GetFiles(Settings.COLORGENERATOR_PATH);
        List<ComponentImage> adjustedComponentImages = new List<ComponentImage>();
        foreach (string filename in filenames)
        {
            try
            {
                Bitmap componentImage = new Bitmap(filename);
                Bitmap adjustedComponentImage = ImageProcessor.ScaleAndClipImage(componentImage, adjustedComponentImageSize);
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
        return result;
    }
}
