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
using System.Threading;

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
    private DateTime lastDownloadDate;


	public User(string userName)
	{
        this.userName = userName;
        this.flickr = new Flickr(FlickrUtil.FLICKR_API_KEY, FlickrUtil.FLICKR_API_SECRET);
        this.lastDownloadDate = new DateTime(0001, 1, 1);

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

            //TODO: Come up with better cache solution 
            //searchOptions.MinUploadDate = lastDownloadDate;
            PhotoCollection photos = Search(searchOptions);
            //lastDownloadDate = DateTime.Now;
            List<Bitmap> images = GetImagesFromPhotos(photos);
            CacheImages(images);
            return images;
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
        //search by default starts on page 1, the next page is page 2
        for (int i = 2; i < pages+1; i++)
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

    //private List<Bitmap> GetImagesFromPhotos(PhotoCollection photos)
    //{
    //    List<Bitmap> bitmaps = new List<Bitmap>();
    //    Stopwatch s = new Stopwatch();
    //    s.Start();
    //    foreach (Photo photo in photos)
    //    {
    //        string dest = Path.Combine(userDir, photo.PhotoId + ".png");
    //        bitmaps.Add(WebUtil.GetBitmap(photo.ThumbnailUrl));
    //    }
    //    s.Stop();
    //    return bitmaps;
    //}

    private List<Bitmap> GetImagesFromPhotos(PhotoCollection photos)
    {
        Stopwatch s = new Stopwatch();
        //TODO: Remove stopwatch
        s.Start();
        List<Bitmap> bitmaps = new List<Bitmap>();
        int ImagesPerThread = photos.Length / FlickrUtil.SIMULTANEOUS_DOWNLOADS;
        Thread[] threads = new Thread[FlickrUtil.SIMULTANEOUS_DOWNLOADS];
        DownloadImageThread[] downloadImageThread = new DownloadImageThread[FlickrUtil.SIMULTANEOUS_DOWNLOADS];
        Photo[] PhotoArray = photos.ToPhotoArray();

        int begin = 0;
        int end = ImagesPerThread + PhotoArray.Length % FlickrUtil.SIMULTANEOUS_DOWNLOADS;
        for (int i = 0; i < FlickrUtil.SIMULTANEOUS_DOWNLOADS; i++)
        {
            Photo[] subArray = new Photo[end - begin];
            Array.Copy(PhotoArray, begin, subArray, 0, end - begin);
            downloadImageThread[i] = new DownloadImageThread(subArray);
            threads[i] = new Thread(new ThreadStart(downloadImageThread[i].BeginDownload));
            begin = end;
            end += ImagesPerThread;
        }
        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        foreach (DownloadImageThread imageThread in downloadImageThread)
        {
            bitmaps.AddRange(imageThread.GetResult());
        }
        //TODO: Remove stopwatch
        s.Stop();
        return bitmaps;
    }

    public void SaveImage(Bitmap image, string name)
    {
        string dest = Path.Combine(userDir, name + ".png");
        image.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
    }

    private void CacheImages(List<Bitmap> images)
    {
        for (int i = 0; i < images.Count; i++)
        {
            SaveImage(images[i], i.ToString());
        }
    }

    private bool IsCacheStale()
    {
        // TODO: return whether cache stored is before last flickr change
        return true;
    }
}
