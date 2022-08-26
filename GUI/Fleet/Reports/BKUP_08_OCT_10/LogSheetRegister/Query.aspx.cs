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
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;

public partial class GUI_Fleet_Reports_LogSheetReg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pagetitle = System.Configuration.ConfigurationManager.AppSettings["PageTitle"];
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.MinValue);

        Common objCommon = new Common();
        objCommon.doCacheRoutine();
        this.Title = pagetitle;
   
        if (!IsPostBack)
        {
            BindDropDown("usp_locations_all", "LOCNAME", "LOCCODE", ddlLocation);
        }
      
        //btnShow.Attributes.Add("OnClick", "javascript:window.open('Result.aspx?FromDt=" + txtDateFrom.Text + "&ToDate=" + txtDateTo.Text + "&Vehicle=" + txtVehicle.Text + "&Loc=" + ddlLocation.SelectedValue + "','','scrollbars=yes,resizable=yes,statusbar=yes,width=1000,height=600');return true;");

    }
    public void BindDropDown(string strProc, string text, string value, DropDownList d)
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        string STR = strProc;
        conn.Open();
        SqlCommand cmd = new SqlCommand(STR, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                d.Items.Add(new ListItem(Convert.ToString(dr[text]), Convert.ToString(dr[value])));
            }
        }
        dr.Close();
        conn.Close();
    }
    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    TextBox txtDateFrom = (TextBox)this.DT.FindControl("txtDateFrom");
    //    TextBox txtDateTo = (TextBox)this.DT.FindControl("txtDateTo");
    //    TextBox txtVehicle = (TextBox)this.popUpVehicle.FindControl("tb_TextBox");
    //    Response.Redirect("Result.aspx?FromDt=" + txtDateFrom.Text + "&ToDate=" + txtDateTo.Text + "&Vehicle=" + txtVehicle.Text + "&Loc=" + ddlLocation.SelectedValue);
    //}
}
