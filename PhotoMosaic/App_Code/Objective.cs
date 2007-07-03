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
/// Summary description for Objective
/// </summary>
public class Objective
{
    private TargetImage target;
    public TargetImage Target
    {
        get
        {
            return target;
        }
    }

    private ImageDatabase imageDb;
    public ImageDatabase ImageDatabase
    {
        get
        {
            return imageDb;
        }
    }

	public Objective(TargetImage target)
	{
        this.target = target;
        this.imageDb = new ImageDatabase();
	}
}
