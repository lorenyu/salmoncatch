using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public partial class result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase)) return;

        // TODO: Don't need this.  Just use Server.MapPath([virtual path])
        Settings.APPLICATION_PATH = this.Request.PhysicalApplicationPath;

        // Parse the input received from the web form
        UserInput input = new UserInput(this);

        if (input.isValid)
        {
            try
            {
                User user = new User(input.userName);
                Objective objective = CreateObjective(user, input);
                Assembler assembler = new Assembler(objective);
                foreach (Bitmap image in objective.images)
                    image.Dispose();
                assembler.Assemble();
                Bitmap resultImage = assembler.GetResultImage();

                // TODO: either don't save, or save it in user dir
                //       for example: user.SaveImage(resultImage);
                string savePath = Path.Combine(Server.MapPath(Settings.CACHE_URL), Session.SessionID + ".png");
                resultImage.Save(savePath, ImageFormat.Png);
                debugImage.Width = resultImage.Width;
                debugImage.Height = resultImage.Height;
                debugImage.ImageUrl = Settings.CACHE_URL + "/" + Session.SessionID + ".png";
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
        Objective objective = new Objective(input.targetImage, user.GetPublicPhotos());

        // Scaling factor to determine size of result image
        // TODO: remove of magic numbers and possibly rename the instance variable
        objective.scalingFactor = (objective.numImagesPerRow + objective.numImagesPerCol) / 20;

        return objective;
    }
}
