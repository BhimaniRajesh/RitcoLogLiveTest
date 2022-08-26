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

public partial class popupbranch : System.Web.UI.Page
{
    public static string strQSParam;
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            hdnstrQSParam.Value = HttpUtility.UrlDecode(Request.QueryString.ToString());
            cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["dbConn"].ToString());
            //SqlConnection cn = new SqlConnection(comon.common.cnstr);
            string sql = "Select loccode,locname from webx_Location where activeflag='Y' order by locname";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //dt.Columns.Add("Select", typeof(RadioButton));
            //dt.Columns["Select"].AutoIncrement = true;
            GridView1.DataSource = dt;
            GridView1.DataBind(); 
  
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (strQSParam != "2")
            {
                ((RadioButton)(e.Row.FindControl("radselect"))).Attributes.Add("onclick", "SelectLocation(this),checkone(this)");
            }
            else 
            {
                ((RadioButton)(e.Row.FindControl("radselect"))).Attributes.Add("onclick", "dataenryloc()");
            }

        }
    }
}
