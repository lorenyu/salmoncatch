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
    public static Stopwatch nearestNeighborStopwatch = new Stopwatch();

    private DateTime lastStartTime;
    private bool isTiming = false;
    private int currentTimeElapsed = 0;

    public Stopwatch()
    {
    }

    public Stopwatch(bool startRightAway)
    {
        if (startRightAway)
        {
            Start();
        }
    }

    public void Start()
    {
        if (!isTiming)
        {
            isTiming = true;
            lastStartTime = DateTime.Now;
        }
    }

    public void Stop()
    {
        if (isTiming)
        {
            TimeSpan time = DateTime.Now - lastStartTime;
            currentTimeElapsed += time.Milliseconds;
            isTiming = false;
        }
    }

    public void Reset()
    {
        if (!isTiming)
        {
            currentTimeElapsed = 0;
        }
    }

    public int timeElapsed()
    {
        if (isTiming)
        {
            TimeSpan time = DateTime.Now - lastStartTime;
            return currentTimeElapsed + time.Milliseconds;
        }
        else
        {
            return currentTimeElapsed;
        }
    }
}
