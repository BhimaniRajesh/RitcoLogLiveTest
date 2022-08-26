using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.Security;

public partial class Finance_Billing_Billgeneration_start : System.Web.UI.Page
{
    string BIll_Business_Type_Wise_YN = "", BIll_POD_Approval_YN = "", Mindate = "", AlloW_BackDate = "";
    MyFunctions fn = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["brcd"] = "PRHO";
        //Session["empcd"] = "p1438";
        //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["dbConnection"]);
          Button1.Attributes.Add("onclick", "javascript:return OnSubmit()");
        
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        
        conn.Open();
       
        
        if (!IsPostBack)
        {
            fn.Fill_Customer_Dataset();
            
            SqlDataReader drpaybas;
            SqlDataReader drtrnmod;
            string sql1 = "select codeID,codedesc from Webx_Master_General WITH(NOLOCK)  where codetype='trn'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            drtrnmod = cmd1.ExecuteReader();

            Cbotrnmod.Items.Clear();
            Cbotrnmod.Items.Add(new ListItem("All-(Without Road/Express)", "13"));
            
            while (drtrnmod.Read())
            {
                Cbotrnmod.Items.Add(new ListItem(drtrnmod.GetValue(1).ToString(), drtrnmod.GetValue(0).ToString()));

            }

            drtrnmod.Close();
            sql1 = "select RUleID+'~'+RULE_value+'~'+rule_Y_N from Webx_Modules_Rules WITH(NOLOCK) where Module_Name in ('Bill Generation')";
            cmd1 = new SqlCommand(sql1, conn);
            drtrnmod = cmd1.ExecuteReader();
            BIll_Business_Type_Wise_YN = "N";
            string[] DR_Value_Arr = null;
            if (drtrnmod.Read())
            {
                DR_Value_Arr = drtrnmod.GetValue(0).ToString().Split('~');
                if (DR_Value_Arr[0] == "RULE02")
                {
                    Hnd_BIll_POD_Approval_YN.Value = DR_Value_Arr[2].ToString();
                    BIll_POD_Approval_YN = DR_Value_Arr[2].ToString();
                     
                }
                else if (DR_Value_Arr[0] == "RULE01")
                {
                    Hnd_BIll_Business_Type_Wise_YN.Value = DR_Value_Arr[2].ToString();
                    BIll_Business_Type_Wise_YN = DR_Value_Arr[2].ToString();
                }
                /*BIll_Business_Type_Wise_YN = drtrnmod.GetValue(0).ToString();
                BIll_POD_Approval_YN = drtrnmod.GetValue(1).ToString();
                Hnd_BIll_Business_Type_Wise_YN.Value = drtrnmod.GetValue(0).ToString();
                Hnd_BIll_POD_Approval_YN.Value = drtrnmod.GetValue(1).ToString();*/
            }
            drtrnmod.Close();
            if (BIll_Business_Type_Wise_YN == "Y")
            {
                Businesstype_tr.Visible = true;
                sql1 = "select codeID,codedesc from Webx_Master_General WITH(NOLOCK) where codetype='BUT'";
                cmd1 = new SqlCommand(sql1, conn);
                drtrnmod = cmd1.ExecuteReader();

                Cb_Businesstype.Items.Clear();
                Cb_Businesstype.Items.Add(new ListItem("Select", ""));
                while (drtrnmod.Read())
                {
                    Cb_Businesstype.Items.Add(new ListItem(drtrnmod.GetValue(1).ToString(), drtrnmod.GetValue(0).ToString()));

                }
                drtrnmod.Close();
            }
            
            conn.Close();
            if (Session["Client"].ToString() == "RCPL")
            {
                Cbopaybas.Items.Add(new ListItem("BOD", "P08"));
                Cbopaybas.Items.Add(new ListItem("National", "P09"));
            }
        }
        
       
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox txtDateFrom = (TextBox)this.DT.FindControl("txtDateFrom");
        TextBox txtDateTo = (TextBox)this.DT.FindControl("txtDateTo");
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

        string party_detail = Txt_Customer.Text.ToString();
        string[] party_arr;
        party_arr = party_detail.Split('~');
        string custcd = party_arr[1].ToString();
        string custnm = party_arr[0].ToString();
        string Final_str = "custcd=" + custcd.ToString();
        string RPTDocketDel = "Y";
        RPTDocketDel = RPTDocketDelievred.SelectedValue.ToString();

        Final_str = Final_str + "&custnm=" + custnm.ToString();
        Final_str = Final_str + "&trnmod=" + Cbotrnmod.SelectedItem.Value.Trim();
        Final_str = Final_str + "&fromtdate=" + dtFrom.ToString();
        Final_str = Final_str + "&todate=" + dtTo.ToString();
        if (Hnd_BIll_Business_Type_Wise_YN.Value == "Y")
        {
            Final_str = Final_str + "&Businesstype=" + Cb_Businesstype.SelectedValue.ToString();
            Final_str = Final_str + "&Businesstype_desc=" + Cb_Businesstype.SelectedItem.Text.ToString();
        }
        else
        {
            Final_str = Final_str + "&Businesstype=";
            Final_str = Final_str + "&Businesstype_desc=";
        }
        Final_str = Final_str + "&BIll_POD_Approval_YN=" + BIll_POD_Approval_YN;
        Final_str = Final_str + "&DocketDelivery=" + RPTDocketDel.ToString();
		Final_str = Final_str + "&DateType=" + ddlDateType.SelectedValue.ToString();
        //Final_str = Final_str + "&Docketnos=" + Docketnos.Text.ToString();
        Session["Docketnos"] = Docketnos.Text.ToString();

        Response.Redirect("genbillsdone.aspx?" + Final_str);
    }
    
}
