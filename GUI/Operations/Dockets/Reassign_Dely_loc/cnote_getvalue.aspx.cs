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

public partial class GUI_admin_AirportMaster_dyn_combo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string result ="";
        string dno = Request.QueryString.Get("dno").Replace("'","''") ;
        //string rtype = Request.QueryString.Get("rtype").Replace("'", "''");
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();
        SqlDataReader drchq;
        string sql = "select  destcd,reassign_destcd from webx_master_docket d with(NOLOCK) where  d.dockno='" + dno + "'";
        
        SqlCommand cmd = new SqlCommand(sql, conn);
        drchq = cmd.ExecuteReader();
        Int16 cnt = 0;
        if  (drchq.Read())
        {
            result = Convert.ToString(drchq["destcd"]) + "|" + Convert.ToString(drchq["reassign_destcd"]);
        }
        
        else
        {
            result = "fail";
        }

        drchq.Close();
        Response.Write(result);
    }
}
