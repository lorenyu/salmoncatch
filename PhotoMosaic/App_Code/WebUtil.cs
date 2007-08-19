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
using System.Net;
using System.IO;

/// <summary>
/// Summary description for WebUtil
/// </summary>
public static class WebUtil
{
    public static Bitmap GetBitmap(String url)
    {
        WebClient web = new WebClient();
        byte[] bytes = web.DownloadData(url);
        MemoryStream stream = new MemoryStream(bytes);
        return new Bitmap(stream);
    }
}
