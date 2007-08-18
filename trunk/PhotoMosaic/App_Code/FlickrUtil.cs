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
    private static int NUMBER_PHOTOS_PER_PAGE = 100;
    private static int MAX_PAGES = 1;

    public static Flickr NewFlickr()
    {
        return new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
    }

    public static string GetUserID(UserInput userInput)
    {
        FoundUser user = userInput.flickr.PeopleFindByUsername(userInput.userName);
        return user.UserId;
    }

    public static void GetAllPublicPhotos(UserInput userInput)
    {
        PhotoSearchOptions searchOptions = new PhotoSearchOptions();
        searchOptions.UserId = userInput.userID;
        PhotoCollection collection = Search(userInput, searchOptions);
        savePhotos(collection);
    }

    public static void SearchByTag(UserInput userInput, string tag)
    {
        PhotoSearchOptions searchOptions = new PhotoSearchOptions();
        searchOptions.Tags = tag;
        PhotoCollection collection = Search(userInput, searchOptions);
        savePhotos(collection);
    }

    private static PhotoCollection Search(UserInput userInput, PhotoSearchOptions searchOptions)
    {
        searchOptions.PerPage = NUMBER_PHOTOS_PER_PAGE;
        Photos photos = userInput.flickr.PhotosSearch(searchOptions);
        PhotoCollection mergedCollection = new PhotoCollection();
        int pages = (int)photos.TotalPages;
        if (pages > MAX_PAGES)
        {
            pages = MAX_PAGES;
        }
        for (int i = 0; i < pages; i++)
        {
            searchOptions.Page = i;
            photos = userInput.flickr.PhotosSearch(searchOptions);
            mergedCollection.AddRange(photos.PhotoCollection);
        }
        return mergedCollection;
    }


    public static void authenticateStep1(UserInput userInput)
    {
        userInput.flickr = new Flickr(FLICKR_API_KEY, FLICKR_API_SECRET);
        userInput.tempFrob = userInput.flickr.AuthGetFrob();
        string flickrUrl = userInput.flickr.AuthCalcUrl(userInput.tempFrob, AuthLevel.Write);
        System.Diagnostics.Process.Start(flickrUrl);
    }

    public static void authenticateStep2(UserInput userInput)
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

    private static void savePhotos(PhotoCollection allPhotos)
    {
        int i = 0;
        
        foreach (Photo photo in allPhotos)
        {
            string dest = Settings.USER_DIR + @"\img" + i + ".png";
            GetImageFromURL(photo.ThumbnailUrl).Save(dest, System.Drawing.Imaging.ImageFormat.Png);
            i++;
        }
    }

    public static Image GetImageFromURL(string strURL)
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
