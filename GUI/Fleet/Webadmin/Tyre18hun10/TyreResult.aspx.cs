using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class admin_TyreResult : System.Web.UI.Page
{
      string  Tyre="";


    protected void Page_Load(object sender, EventArgs e)
    {
        Tyre = Request.QueryString["Tyre"];
        lblDocNo.Text = Tyre;
    }
}
