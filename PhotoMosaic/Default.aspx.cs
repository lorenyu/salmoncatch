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

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AssembleButton_Click(object sender, EventArgs e)
    {
        Assembler assembler = new Assembler();
        ImageMap m = new ImageMap();
        //Objective objective = new Objective(new TargetImage(new Bitmap()));
        //assembler.Assemble(objective);
    }
}