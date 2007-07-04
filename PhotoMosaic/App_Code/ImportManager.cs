using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Summary description for ResourceManager
/// </summary>
public static class ImportManager
{
    public enum ImportMethod { FileSystem, Web };

    public static void ImportImagesToDatabase(ImageDatabase imageDb, ImportMethod method)
    {
        if (method != ImportMethod.FileSystem) return;

        List<Bitmap> images = FileManager.LoadComponentImages();
        foreach (Bitmap image in images)
        {
            imageDb.AddImage(image);
        }
    }
}
