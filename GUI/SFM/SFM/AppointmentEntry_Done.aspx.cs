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
using System.Data.SqlClient;
public partial class SFM_AppointmentEntry_Done : System.Web.UI.Page
{
    public static SqlConnection conn;
    public string AppontmentId;
    protected void Page_Load(object sender, EventArgs e)
    {
        conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        AppontmentId = Request.QueryString.Get("AppontmentId");
    }
}
