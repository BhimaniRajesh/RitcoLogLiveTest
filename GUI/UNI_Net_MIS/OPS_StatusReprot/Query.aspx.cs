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
public partial class GUI_UNI_Net_MIS_OPS_StatusReprot_Query : System.Web.UI.Page
{
    SqlConnection conn;
    public static int chgV = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         MySQLDataSource_GeneralMaster.ConnectionString = Session["SqlProvider"].ToString().Trim();
        MySQLDataSource_GeneralMaster1.ConnectionString = Session["SqlProvider"].ToString().Trim();
        MySQLDataSource_GeneralMaster2.ConnectionString = Session["SqlProvider"].ToString().Trim();
        MySQLDataSource_GeneralMaster3.ConnectionString = Session["SqlProvider"].ToString().Trim();
       

        conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        conn.Open();
        if (!IsPostBack)
        {
            cboMode.SelectedValue = "All";
            cboPaybas.SelectedValue = "All";
            cboType.SelectedValue = "All";
            cboBUTType.SelectedValue = "All";
           // DllVendTy.SelectedValue = "All";

        }
        conn.Close();

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        conn.Open();
        string brcd = Session["brcd"].ToString();
        string sqlstr = "select loc_level from webx_location where loccode='" + brcd + "'";
        // Response.Write("<br> sqlstr : " + sqlstr);
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        SqlDataReader dread = cmd.ExecuteReader();
        dread.Read();
        int loclevel = Convert.ToInt32(dread.GetValue(0).ToString());
        string loc_level = Convert.ToString(dread.GetValue(0).ToString());
        //Response.Write("<br> sqlstr : " + loclevel);
        dread.Close();

        string minLV = "", maxLV = "";

        sqlstr = "select max(codeid),min(codeid) from webx_master_general where codetype='HRCHY' and codeid<>'1'  and  codeid>=" + loclevel + " ";
        cmd = new SqlCommand(sqlstr, conn);
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            minLV = dr[1].ToString();
            maxLV = dr[0].ToString();
        }
        dr.Close();
        string Str_VAL = "", STR_LV = "";
        string MAxStr_VAL = "", MAxSTR_LV = "";

        for (int i = Convert.ToInt32(minLV); i <= Convert.ToInt32(maxLV); i++)
        {
            DropDownList DL = (DropDownList)Fromlc.FindControl("DL_" + i);

            if (DL.SelectedValue.ToString() != "All")
            {
                if (Str_VAL == "")
                    Str_VAL = DL.SelectedValue.ToString();
                else
                    Str_VAL = Str_VAL + "," + DL.SelectedValue.ToString();

                MAxStr_VAL = DL.SelectedValue.ToString();

                if (STR_LV == "")
                    STR_LV = i.ToString();
                else
                    STR_LV = STR_LV + "," + i.ToString();
                MAxSTR_LV = i.ToString();
            }


        }


        string st_paybasis = cboPaybas.SelectedValue.ToString();
        string st_trnmod = cboMode.SelectedValue.ToString();
        string st_type = cboType.SelectedValue.ToString();
        string st_busttype = cboBUTType.SelectedValue.ToString();

        string str_paybasis = cboPaybas.SelectedItem.ToString();
        string str_trnmod = cboMode.SelectedItem.ToString();
        string str_type = cboType.SelectedItem.ToString();
        string str_busttype = cboBUTType.SelectedItem.ToString();

        string st_custcd = txtCustomer.Text.ToString();// custcd.SelectedValue.ToString();
        conn.Close();

        TextBox txtDateFrom = (TextBox)DT.FindControl("txtDateFrom");
        TextBox txtDateTo = (TextBox)DT.FindControl("txtDateTo");

        RadioButtonList radDate = (RadioButtonList)DT.FindControl("radDate");
        string report_mode = "";
        RadioButtonList rb2 = new RadioButtonList();
        rb2 = (RadioButtonList)radDate;
        if ((rb2.Items[0].Selected))
        {
            report_mode = "Date";
        }
        if ((rb2.Items[1].Selected))
        {
            report_mode = "Week";
        }
        if ((rb2.Items[2].Selected))
        {
            report_mode = "Today";
        }

        string strrightnow, strrightnow1;
        System.DateTime righnow = System.DateTime.Today;
        strrightnow = righnow.ToString("dd/MM/yyyy");
        strrightnow1 = righnow.ToString("dd/MM/yyyy");
        if (report_mode == "Date")
        {
            strrightnow = txtDateFrom.Text.ToString();
            strrightnow1 = txtDateTo.Text.ToString(); //Request.QueryString.Get("txtDateTo");
        }
        else if (report_mode == "Week")
        {
            strrightnow = righnow.AddDays(-7).ToString("dd/MM/yyyy");
            strrightnow1 = righnow.ToString("dd/MM/yyyy");
        }
        else if (report_mode == "Today")
        {
            strrightnow = righnow.ToString("dd/MM/yyyy");
            strrightnow1 = righnow.ToString("dd/MM/yyyy");

        }
        else if (report_mode == "Tilldate")
        {
            strrightnow = "01/04/1990";
            strrightnow1 = righnow.ToString("dd/MM/yyyy");

        }
        if (strrightnow == "")
        {
            strrightnow = righnow.ToString("dd/MM/yyyy");
        }

        if (strrightnow1 == "")
        {
            strrightnow1 = righnow.ToString("dd/MM/yyyy");
        }

        string[] strArrDtFrom = strrightnow.Split('/');
        string[] strArrDtTo = strrightnow1.Split('/');

        string dtFrom = Convert.ToDateTime(strArrDtFrom[1] + "/" + strArrDtFrom[0] + "/" + strArrDtFrom[2]).ToString("dd MMM yy");
        string dtTo = Convert.ToDateTime(strArrDtTo[1] + "/" + strArrDtTo[0] + "/" + strArrDtTo[2]).ToString("dd MMM yy");

    

        string final;

        if (MAxStr_VAL == "")
            MAxStr_VAL = "All";
       
        if (MAxSTR_LV == "")
            MAxSTR_LV = "1";

        string DTTYPE = DT_TYPE.SelectedValue.ToString();
        string DTTYPE_str = DT_TYPE.SelectedItem.ToString();
    
        string strcustcd = txtCustomer.Text.ToString();
        string DocketLsit = txtsysno.Text.ToString();

        string st_status = DLLTYP.SelectedValue.ToString();
        string str_status = DLLTYP.SelectedItem.ToString();

        string st_status_sub = DLLTYP_D.SelectedValue.ToString();
        string str_status_sub = DLLTYP_D.SelectedItem.ToString();

        string strdays = "";
        if (st_status == "1")
        {
            st_status_sub = DLLTYP_D.SelectedValue.ToString();
            str_status_sub = DLLTYP_D.SelectedItem.ToString();

            strdays = DLLDAYS_D.SelectedValue.ToString();
        }
        else if (st_status == "2")
        {
            st_status_sub = DLLTYP_U.SelectedValue.ToString();
            str_status_sub = DLLTYP_U.SelectedItem.ToString();
            strdays = DLLDAYS_U.SelectedValue.ToString();
        }

        string DWN_XLS = "";
        if (rptfrt.Items[0].Selected)
        {
            DWN_XLS = "0";
        }
        else if (rptfrt.Items[1].Selected)
        {
            DWN_XLS = "1";
        }

        string DLLRPTTYP_st = DLLRPTTYP.SelectedValue.ToString();
        string pgs = pgSize.SelectedValue.ToString();
            final = "?st_paybasis=" + st_paybasis;
            final += "&st_trnmod=" + st_trnmod;
            final += "&st_type=" + st_type;
            final += "&st_busttype=" + st_busttype;
            final += "&str_paybasis=" + str_paybasis;
            final += "&str_trnmod=" + str_trnmod;
            final += "&str_type=" + str_type;
            final += "&str_busttype=" + str_busttype;
            final += "&dtFrom=" + dtFrom;
            final += "&dtTo=" + dtTo;
            final += "&MAxStr_VAL=" + MAxStr_VAL;
            final += "&MAxSTR_LV=" + MAxSTR_LV;
            final += "&DTTYPE=" + DTTYPE;
            final += "&DTTYPE_str=" + DTTYPE_str;
            final += "&strcustcd=" + strcustcd;
            final += "&DocketLsit=" + DocketLsit;
            final += "&st_status=" + st_status;
            final += "&str_status=" + str_status;
            final += "&st_status_sub=" + st_status_sub;
            final += "&str_status_sub=" + str_status_sub;
            final += "&strdays=" + strdays;
            final += "&Psize=" + pgs;
            final += "&PGNO=1";
            final += "&DWN_XLS=" + DWN_XLS;
            final += "&DLLRPTTYP_st=" + DLLRPTTYP_st;
            if (DLLRPTTYP_st == "1")
                Response.Redirect("Result.aspx" + final);
            else
                Response.Redirect("Result_RS.aspx" + final);
    }
}
