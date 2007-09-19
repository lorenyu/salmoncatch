using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Collections.Generic;
using FlickrNet;
using System.IO;

/// <summary>
/// Summary description for DownloadImageThread
/// </summary>
public class DownloadImageThread
{
    private Photo[] photos;
    private List<Bitmap> bitmaps;

	public DownloadImageThread(Photo[] photos)
	{
        this.photos = photos;
	}

    public void BeginDownload()
    {
        bitmaps = new List<Bitmap>();
        foreach (Photo photo in photos)
        {
            Bitmap bitmap = WebUtil.GetBitmap(photo.ThumbnailUrl);
            bitmaps.Add(bitmap);
        }
    }

    public Photo[] Photos
    {
        get { return photos; }
    }

    public List<Bitmap> Result
    {
        get { return bitmaps; }
    }
}