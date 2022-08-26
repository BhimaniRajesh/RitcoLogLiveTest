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

public partial class GUI_admin_CustomerMaster_ListAll : System.Web.UI.Page
{
    SqlConnection cn;  // = new SqlConnection(ConfigurationManager.AppSettings["dbConn"].ToString());
    public static string chkval;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
            
            
            bindGrid();
        }
    }
    protected void pgChange(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    public void bindGrid()
    {
        cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();
            string sql;

            sql = "select GRPCD,GRPNM FROM webx_GRPMST ORDER BY GRPNM";
            
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Sr. No", typeof(int));
            dt.Columns["Sr. No"].AutoIncrement = true;
            dt.Columns["Sr. No"].AutoIncrementSeed = 1;
            dt.Columns["Sr. No"].AutoIncrementStep = 1;
            adp.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
          cn.Close();
         }
        
    }



