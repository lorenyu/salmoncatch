using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// Summary description for FileManager
/// </summary>
public static class FileManager
{
    /// <summary>
    /// Loads Images from current working directory.
    /// </summary>
    /// <returns></returns>
    public static List<Bitmap> LoadComponentImages()
    {
        return FileManager.LoadComponentImages(Directory.GetCurrentDirectory());
    }

    public static List<Bitmap> LoadComponentImages(string directory)
    {
        List<Bitmap> result = new List<Bitmap>();
        string[] filenames = Directory.GetFiles(directory);
        foreach (string filename in filenames)
        {
            result.Add(new Bitmap(filename));
        }
        return result;
    }
}
