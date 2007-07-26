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

/// <summary>
/// Contains file paths, urls, and other settings.
/// Urls are virtual paths relative to the website directory, while
/// Paths are absolute physical paths.
/// The physical path of the website directory is given by APPLICATION_PATH.
/// Thus a url of "images/target.png" corresponds to the physical path obtained
/// by combining APPLICATION_PATH and the relative path of the url.
/// </summary>
public static class Settings
{
    /// <summary>
    /// The physical path of the website directory.
    /// Thus a url of "images/target.png" corresponds to the physical path obtained
    /// by combining APPLICATION_PATH and the relative path of the url.
    /// </summary>
    public static string APPLICATION_PATH;

    public static string USER_URL = "FakeUser";
    public static string USER_DIR
    {
        get
        {
            
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Settings.CACHE_DIR);
                dir.CreateSubdirectory(USER_URL);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            return Path.Combine(Settings.CACHE_DIR, USER_URL);
        }
    }
    public static string CACHE_URL = "cache"; 
    public static string CACHE_DIR
    {
        get
        {
            return Path.Combine(Settings.IMAGES_PATH, CACHE_URL);
        }
    }
    public static string IMAGES_URL = "images";    
    public static string IMAGES_PATH
    {
        get
        {
            return Path.Combine(Settings.APPLICATION_PATH, IMAGES_URL);
        }
    }
    public static string COLORGENERATOR_PATH
    {
        get
        {
            return Path.Combine(Settings.APPLICATION_PATH, @"..\ColorGenerator");
        }
    }
    public static string RESULTIMAGE_FILENAME = "resultimage.png";
}
