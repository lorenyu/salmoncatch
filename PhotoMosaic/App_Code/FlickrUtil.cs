using FlickrNet;
using System;
using System.Drawing;
using System.Collections;
using System.Net;
using System.Web;
using System.Text;
using System.IO;


/// <summary>
/// Summary description for FlickrUtil
/// </summary>
public class FlickrUtil
{
    private static string FLICKR_API_KEY = "76689f3376d2752abacb6bac4c12f580";
    private static string FLICKR_API_SECRET = "a095ecfb543abc2d";

    private Photos photos;
    private string tempFrob;
    private Flickr flickr;
    private string userID;

    private string screenName;
    public string ScreenName
    {
        get
        {
            return screenName;
        }
    }

	public FlickrUtil()
	{
        flickr = new Flickr(FLICKR_API_KEY);
	}

    public void enterUserName(string name)
    {
        this.screenName = name;
        FoundUser user = flickr.PeopleFindByUsername(this.screenName);
        userID = user.UserId;
    }

    public void getAllPublicPhotos()
    {
        photos = flickr.PeopleGetPublicPhotos(userID);
        savePhotos();
    }

    public void search(string search)
    {
        PhotoSearchOptions searchOptions = new PhotoSearchOptions();
        searchOptions.Tags = search;
        photos = flickr.PhotosSearch(searchOptions);
        savePhotos();
    }

    public void authenticateStep1()
    {
        flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
        tempFrob = flickr.AuthGetFrob();
        string flickrUrl = flickr.AuthCalcUrl(tempFrob, AuthLevel.Write);
        System.Diagnostics.Process.Start(flickrUrl);
    }

    public void authenticateStep2()
    {
        flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
        try
        {
            Auth auth = flickr.AuthGetToken(tempFrob);
            flickr.ApiToken = auth.Token;
        }
        catch (FlickrException ex)
        {
        }
    }

    private void savePhotos()
    {
        PhotoCollection allPhotos = photos.PhotoCollection;
        int i = 0;
        foreach (Photo photo in allPhotos)
        {
            //string dest = @"C:\Documents and Settings\AE\My Documents\Visual Studio 2005\WebSites\PhotoMosaic\images\flickr\img" + i + ".png";
            string dest = Settings.USER_DIR + @"img" + i + ".png";
            GetImageFromURL(photo.ThumbnailUrl).Save(dest, System.Drawing.Imaging.ImageFormat.Png);
            i++;
        }
    }

    private Image GetImageFromURL(string strURL)
    {
        Image retVal = null;
        try
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strURL);
            request.Timeout = 5000;
            request.ReadWriteTimeout = 20000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            retVal = Image.FromStream(response.GetResponseStream());
        }
        catch (Exception)
        {
            retVal = null;
        }
        return retVal;
    }
}
