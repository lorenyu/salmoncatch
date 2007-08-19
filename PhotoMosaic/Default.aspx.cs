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

        // Parse the input received from the web form
        UserInput input = new UserInput(Request.Form);

        // If there is input and the input is valid
        if (input.isValid)
        {
            User user = new User(input.userName);
            try
            {
                Objective objective = CreateObjective(user, input);
                Assembler assembler = new Assembler(objective);
                foreach (Bitmap image in objective.images)
                    image.Dispose();
                assembler.Assemble();
                Bitmap resultImage = assembler.GetResultImage();

                // TODO: either don't save, or save it in user dir
                //       for example: user.SaveImage(resultImage);
                string savePath = Path.Combine(Settings.IMAGES_PATH, Settings.RESULTIMAGE_FILENAME);
                resultImage.Save(savePath, ImageFormat.Png);
                debugImage.Width = resultImage.Width;
                debugImage.Height = resultImage.Height;
                debugImage.ImageUrl = Settings.IMAGES_URL + "/" + Settings.RESULTIMAGE_FILENAME;
            }
            catch (Exception ex)
            {
                // TODO: error handling
                return;
            }
        }
    }

    private Objective CreateObjective(User user, UserInput input)
    {
        Objective objective = new Objective();

        // Obtain target image
        objective.targetImage = WebUtil.GetBitmap(input.targetImageUrl);;

        // Specify number of regions in result image
        objective.numImagesPerRow = input.numHorizontalImages;
        objective.numImagesPerCol = input.numVerticalImages;

        // Quality of component image matching
        objective.quality = 3;

        // Scaling factor to determine size of result image
        // TODO: remove of magic numbers and possibly rename the instance variable
        objective.scalingFactor = (objective.numImagesPerRow + objective.numImagesPerCol) / 30;

        // Get component images
        objective.images = user.GetPublicPhotos();

        return objective;
    }
}
