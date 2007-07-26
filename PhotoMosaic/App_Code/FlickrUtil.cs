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

	public FlickrUtil()
	{
	}

    public Flickr NewFlickr()
    {
        return new Flickr(FLICKR_API_KEY);
    }

    public string GetUserID(UserInput userInput)
    {
        FoundUser user = userInput.flickr.PeopleFindByUsername(userInput.userName);
        return user.UserId;
    }

    public void getAllPublicPhotos(UserInput userInput)
    {
        Photos photos = userInput.flickr.PeopleGetPublicPhotos(userInput.userID);
        savePhotos(photos);
    }

    public void search(string search, UserInput userInput)
    {
        PhotoSearchOptions searchOptions = new PhotoSearchOptions();
        searchOptions.Tags = search;
        Photos photos = userInput.flickr.PhotosSearch(searchOptions);
        savePhotos(photos);
    }

    public void authenticateStep1(UserInput userInput)
    {
        userInput.flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
        userInput.tempFrob = userInput.flickr.AuthGetFrob();
        string flickrUrl = userInput.flickr.AuthCalcUrl(userInput.tempFrob, AuthLevel.Write);
        System.Diagnostics.Process.Start(flickrUrl);
    }

    public void authenticateStep2(UserInput userInput)
    {
        userInput.flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
        try
        {
            Auth auth = userInput.flickr.AuthGetToken(userInput.tempFrob);
            userInput.flickr.ApiToken = auth.Token;
        }
        catch (FlickrException ex)
        {
        }
    }

    private void savePhotos(Photos photos)
    {
        PhotoCollection allPhotos = photos.PhotoCollection;
        int i = 0;
        foreach (Photo photo in allPhotos)
        {
            //string dest = @"C:\Documents and Settings\AE\My Documents\Visual Studio 2005\WebSites\PhotoMosaic\images\flickr\img" + i + ".png";
            string dest = Settings.USER_DIR + @"\img" + i + ".png";
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
