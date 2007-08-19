using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FlickrNet;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    public string userName;
    public Flickr flickr;
    public string userId;
    public string tempFrob;
    public string userDir;

	public User(string userName)
	{
        this.userName = userName;
        this.flickr = new Flickr(FlickrUtil.FLICKR_API_KEY, FlickrUtil.FLICKR_API_SECRET);

        FoundUser user = flickr.PeopleFindByUsername(userName);
        this.userId = user.UserId;

        this.userDir = Path.Combine(Settings.CACHE_DIR, userName);
        if (!Directory.Exists(userDir))
        {
            Directory.CreateDirectory(userDir);
        }
	}

    /// <summary>
    /// If cache is stale, download images from flickr.
    /// Otherwise obtain images from cache
    /// </summary>
    /// <returns></returns>
    public List<Bitmap> GetPublicPhotos()
    {
        // TODO: what if user removes images from his website?
        if (IsCacheStale())
        {
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.UserId = userId;
            PhotoCollection photos = Search(searchOptions);
            CachePhotos(photos);
            return GetImagesFromPhotos(photos);
        }
        else
        {
            
            string[] filenames = Directory.GetFiles(userDir);
            List<Bitmap> images = new List<Bitmap>();
            try
            {
                foreach (string filename in filenames)
                {
                    Bitmap image = new Bitmap(filename);
                    images.Add(image);
                }
            }
            catch
            {
                // Ignore exceptions thrown by the Bitmap constructor that occur
                // when we try to open a file in the color directory that is not
                // an image file (e.g. "Thumbs.db")
            }
            return images;
        }
    }

    private PhotoCollection Search(PhotoSearchOptions searchOptions)
    {
        searchOptions.PerPage = FlickrUtil.NUMBER_PHOTOS_PER_PAGE;
        PhotoCollection mergedCollection;

        // Get the first page of photos

        Photos photos = flickr.PhotosSearch(searchOptions);
        mergedCollection = photos.PhotoCollection;

        // Get the rest of the pages of photos, but stop at max_pages if there are too many

        int pages = (int)photos.TotalPages;
        if (pages > FlickrUtil.MAX_PAGES)
        {
            pages = FlickrUtil.MAX_PAGES;
        }
        for (int i = 1; i < pages; i++)
        {
            searchOptions.Page = i;
            photos = flickr.PhotosSearch(searchOptions);
            mergedCollection.AddRange(photos.PhotoCollection);
        }
        return mergedCollection;
    }

    public void Authenticate()
    {
        if (tempFrob == null)
        {
            tempFrob = flickr.AuthGetFrob();
            string flickrUrl = flickr.AuthCalcUrl(tempFrob, AuthLevel.Write);
            System.Diagnostics.Process.Start(flickrUrl);
        }
        else
        {
            try
            {
                Auth auth = flickr.AuthGetToken(tempFrob);
                flickr.ApiToken = auth.Token;
            }
            catch (FlickrException ex)
            {
                // TODO: error handling
            }
        }
    }

    private List<Bitmap> GetImagesFromPhotos(PhotoCollection photos)
    {
        List<Bitmap> bitmaps = new List<Bitmap>();
        foreach (Photo photo in photos)
        {
            string dest = Path.Combine(userDir, photo.PhotoId + ".png");
            bitmaps.Add(WebUtil.GetBitmap(photo.ThumbnailUrl));
        }
        return bitmaps;
    }

    public void SaveImage(Bitmap image, string name)
    {
        string dest = Path.Combine(userDir, name + ".png");
        image.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
    }

    private void CachePhotos(PhotoCollection photos)
    {
        int i = 0;
        foreach (Photo photo in photos)
        {
            SaveImage(WebUtil.GetBitmap(photo.ThumbnailUrl), i.ToString());
            i++;
        }
    }

    private bool IsCacheStale()
    {
        // TODO: return whether cache stored is before last flickr change
        return false;
    }
}
