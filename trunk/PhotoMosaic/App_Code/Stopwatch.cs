using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Stopwatch
/// </summary>
public class Stopwatch
{
    private TimeSpan interval;
    private DateTime time;

	public Stopwatch()
	{
        time = DateTime.Now;
	}


    public string timeElapsed()
    {
        interval = DateTime.Now - time;
        return interval.Seconds.ToString();
    }
}
