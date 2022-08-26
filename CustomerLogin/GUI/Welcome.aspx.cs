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

public partial class GUI_Welcome : System.Web.UI.Page
{
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        string pagetitle = System.Configuration.ConfigurationManager.AppSettings["PageTitle"];
        lblWelcome.Text = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote"];

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);

        Common objCommon = new Common();
        objCommon.doCacheRoutine();
        this.Title = pagetitle;

        lblUpdates_head.Text = Session["Client"].ToString() + " - Just In";
        lblNews_head.Text = Session["Client"].ToString() + " News";
        lblTracklineHeading.Text = Session["DocketCalledAs"].ToString() + " Tracking";

        lbldocknt.Text = Session["DocketCalledAs"].ToString() + " No." + "<br><i>" + "[if Multiple Separated By comma.]" + "<i>";

        conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        conn.Open();

        string sql_update = "select updates,color from webx_Customer_News_updates with(NOLOCK)  where id='U'";
        SqlCommand cmd = new SqlCommand(sql_update, conn);
        SqlDataReader dr = cmd.ExecuteReader();
        string st_update = "", st_color = "#000000";
        while (dr.Read())
        {

            if (dr["updates"] != null)
            {
                st_update = dr["updates"].ToString();
            }
            if (dr["color"] != null)
            {
                st_color = dr["color"].ToString();
            }
        }
        dr.Close();
        lblUpdates.Text = st_update;
        int red = 0, green = 0, blue = 0;
        //red=Convert.ToInt16(st_color.Substring(1,2));
        //green=Convert.ToInt16(st_color.Substring(3,2));
        //blue=Convert.ToInt16(st_color.Substring(5,2));

        //  lblUpdates.ForeColor = System.Drawing.Color.FromArgb(red, green, blue);

        lblUpdates.ForeColor = System.Drawing.Color.FromName(st_color);





        conn.Close();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        string docket_list = txtdktno.Text.ToString();
        string Doc_type = "DKT";
        string custcd = "All";// custcode.Text.ToString();
        string final;
        final = "?RO=All";
        final += "&LO=All";
        final += "&RO_TO=All";
        final += "&LO_TO=All";
        final += "&DateType=";
        final += "&fromdt=";
        final += "&todt=";
        final += "&st_Docnolist=" + docket_list;
        final += "&St_Manualnolist=";
        final += "&st_Vehnolist=";
        final += "&st_Freetext=";
        final += "&st_doctyp=" + Doc_type;
        final += "&str_doctyp=DOCKET";

        final += "&strPaybas=All";
        final += "&str_Paybas=All";

        final += "&strMode=All";
        final += "&str_Mode=All";

        final += "&strType=All";
        final += "&str_Type=All";

        //final += "&pagecnt=" + "1";
        //final += "&DWN_XLS=" + "N";


       // if (st_doctyp == "DKT" || st_doctyp == "THC")
        Response.Redirect("./Cust_Tracking/Result.aspx" + final);
    }
}
