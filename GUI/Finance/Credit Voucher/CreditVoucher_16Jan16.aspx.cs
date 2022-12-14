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
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.Text;
using WebX.Controllers;
using WebX.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class GUI_Finance_Accounts_Debit_Voucher_DebitVoucher : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    DataTable dt = new DataTable("table1");
    MyFunctions fn = new MyFunctions();
    string newdate = "", Financial_Year = "", fin_year = "";
    string Defaultdate = "", PBOV_TYP;
    protected void Page_Load(object sender, EventArgs e)
    {   
        newdate = Convert.ToString(Request.QueryString["Vdate"]);
        con = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        con.Open();

        Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        double fin_year_next = Convert.ToDouble(Financial_Year) + 1;
        fin_year = Financial_Year + "_" + fin_year_next.ToString().PadLeft(2, '0');

        Defaultdate = Request.QueryString["Defaultdate"];
        if (Defaultdate == "")
        {
            Defaultdate = "today";
        }
        if (!IsPostBack)
        {
            hdnroundoff.Value = "N";
            hdneditablsvctaxrate.Value = "N";

            double count = 0;
            count = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select count(*) FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Decimal Values Y/N'"));
            if (count > 0)
            {
                hdnroundoff.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Decimal Values Y/N'").ToString();
            }
            count = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select count(*) FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Editable ServiceTax Rate Y/N'"));
            if (count > 0)
            {
                hdneditablsvctaxrate.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Editable ServiceTax Rate Y/N'").ToString();
            }

            //hdnroundoff.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Decimal Values Y/N'").ToString();
            //hdneditablsvctaxrate.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Editable ServiceTax Rate Y/N'").ToString();

            if (hdneditablsvctaxrate.Value == "Y")
            {
                trSvcTaxRate.Visible = true;
            }
            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add();    
            }
            txtRows.Text = "5";
            grvcontrols.DataSource = dt;
            grvcontrols.DataBind();
         
            FillBusDiv();

            if (Defaultdate != "today")
            {
                txtVoucherDate.Text = Defaultdate;
            }
            else
            {
                txtVoucherDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            txtVoucherDate.CssClass = "blackfnt";

            SqlDataReader dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, "select acccode,accdesc from webx_acctinfo where accdesc like '%tds%'");
            while (dr.Read())
            {
                Tdssection.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();

            Tdssection.CssClass = "blackfnt";
            txtPreparedLoc.Text = Session["brcd"].ToString();
            txtAccLoc.Text = Session["brcd"].ToString();
            txtPreparedBy.Text = Session["empcd"].ToString();
            Show_Pbov_list_Display();
        }
        if (Svctax_yn.Checked == true)
        {
            DateTime curdt;
            System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
            curdt = Convert.ToDateTime(txtVoucherDate.Text, enGB);
            HdnServiceTaxRate.Value = Convert.ToString(TaxRateController.GetServiceTaxRate(curdt));
            HdnEduCessRate.Value = Convert.ToString(TaxRateController.GetEduCessRate(curdt));
            HdnHEduCessRate.Value = Convert.ToString(TaxRateController.GetHEduCessRate(curdt));

            if (Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"), enGB) > Convert.ToDateTime("31/05/2015", enGB))
                txtVoucherDate.Enabled = false;

            if (hdneditablsvctaxrate.Value.CompareTo("Y") == 0)
            {
                HdnServiceTaxRate.Value = txtSvrTaxRate.Text;
            }
            if (hdnroundoff.Value.CompareTo("Y") == 0)
            {
                txtServiceTax.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnServiceTaxRate.Value)) / 100).ToString("F0");
                txtServiceTax.Text = Convert.ToDecimal(txtServiceTax.Text).ToString("F2");
                txtEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnEduCessRate.Value)) / 100).ToString("F0");
                txtEduCess.Text = Convert.ToDecimal(txtEduCess.Text).ToString("F2");
                txtHEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnHEduCessRate.Value)) / 100).ToString("F0");
                txtHEduCess.Text = Convert.ToDecimal(txtHEduCess.Text).ToString("F2");
                txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text) + Convert.ToDecimal(txtServiceTax.Text) + Convert.ToDecimal(txtEduCess.Text) + Convert.ToDecimal(txtHEduCess.Text)).ToString("F0");
                txtAmtApplL.Text = Convert.ToDecimal(txtAmtApplL.Text).ToString("F2");
            }
            else
            {   
                txtServiceTax.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnServiceTaxRate.Value)) / 100).ToString("F2");
                txtEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnEduCessRate.Value)) / 100).ToString("F2");
                txtHEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnHEduCessRate.Value)) / 100).ToString("F2");
                txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text) + Convert.ToDecimal(txtServiceTax.Text) + Convert.ToDecimal(txtEduCess.Text) + Convert.ToDecimal(txtHEduCess.Text)).ToString("F2");
            }
        }
        if (Svctax_yn.Checked == false)
        {
            txtVoucherDate.Enabled = true;
            if (txtAmtAppl.Text == "")
                txtAmtAppl.Text = "0.00";
            txtServiceTax.Text = "0.00";
            txtEduCess.Text = "0.00";
            txtHEduCess.Text = "0.00";
            txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text)).ToString("F2");
        }

        btnPreparedLoc.Attributes.Add("onclick", "popuplist('LocCode','" + txtPreparedLoc.ClientID.ToString() + "','none')");
        txtPreparedLoc.Attributes.Add("onblur", "LocBlur('" + txtPreparedLoc.ClientID.ToString() + "')");
        txtAccLoc.Attributes.Add("onblur", "LocBlur('" + txtAccLoc.ClientID.ToString() + "')");
        txtManualNo.Attributes.Add("onblur", "Manualbillblur('" + txtManualNo.ClientID.ToString() + "')");
        btnAccLoc.Attributes.Add("onclick", "popuplist('LocCode','" + txtAccLoc.ClientID.ToString() + "','none')");
        txtServiceTax.Attributes.Add("onblur", "GetTot()");
        txtEduCess.Attributes.Add("onblur", "GetTot()");
        txtHEduCess.Attributes.Add("onblur", "GetTot()");
        txtTDSRate.Attributes.Add("onblur", "GetTot()");
        txtTDSAmt.Attributes.Add("onblur", "GetTot()");
        txtPanNo.Attributes.Add("onchange", "CheckPan(this)");
        txtCashAmt.Attributes.Add("onblur", " CheckCashBank()");
        txtChqAmt.Attributes.Add("onblur", " CheckCashBank()");
        txtVoucherDate.Attributes.Add("onblur", "javascript:MIn_Max_Date_Validation(this,'07','../../images/Date_Control_Rule_Check.aspx')");
    }
    public void Show_Pbov_list_Display()
    {
        string sql_display_pbov = "";
        if (RAD_Customer.Checked == true)
        {
            sql_display_pbov = "select custcd as code , custnm+':'+custcd  as name from webx_custhdr WITH(NOLOCK) where CUSTCD is not null and PATINDEx ('%" + Session["brcd"].ToString() + "%',custloc)>0 order by CUSTNM ";
        }
        else if (RAD_Vendor.Checked == true)
        {
            sql_display_pbov = "select vendorCode as code , vendorname+':'+vendorCode as name from webx_VENDOR_HDR WITH(NOLOCK) where vendorcode in ( select vendorcode from webx_VENDOR_det WITH(NOLOCK)  where vendorCode is not null and    PATINDEx ('%" + Session["brcd"].ToString() + "%',vendorbrcd)>0 ) order by vendorname";
        }
        else if (RAD_Emp.Checked == true)
        {
            if (SessionUtilities.Client.CompareTo("Ritco") == 0)
                sql_display_pbov = "select userid as code ,name+':'+userid as name  from webx_master_users  WITH(NOLOCK) where status='100' and BranchCode='" + SessionUtilities.CurrentBranchCode + "' order by name";
            else
                sql_display_pbov = "select userid as code ,name+':'+userid as name  from webx_master_users  WITH(NOLOCK) where status='100' order by name,BranchCode";
        }
        else if (RAD_Driver.Checked == true)
        {
            sql_display_pbov = "select Driver_id as code ,Driver_Name + ':' + convert(varchar,Manual_Driver_Code) as name from webx_fleet_drivermst WITH(NOLOCK) where PATINDEx ('%" + Session["brcd"].ToString() + "%',Driver_Location)>0 and ActiveFlag='Y' order by Driver_Name,Driver_Location";
        }
        else if (RAD_Vehicle.Checked == true)
        {
            sql_display_pbov = "select Vehno as code ,Vehno as name from webx_vehicle_hdr WITH(NOLOCK) where PATINDEx ('%" + Session["brcd"].ToString() + "%',Conrtl_branch)>0 and ActiveFlag='Y' order by Vehno";
        }
        //SqlCommand cmd_pbov = new SqlCommand(sql_display_pbov, con);
        //SqlDataReader dr_pbov;
        //dr_pbov = cmd_pbov.ExecuteReader();
        //Dr_Pbov_list.Items.Clear();
        SqlDataReader dr_pbov = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sql_display_pbov);
        Dr_Pbov_list.Items.Clear();
        Dr_Pbov_list.CssClass = "blackfnt";
        Dr_Pbov_list.Items.Add(new ListItem("Select", ""));
        while (dr_pbov.Read())
        {
            Dr_Pbov_list.Items.Add(new ListItem(dr_pbov.GetValue(1).ToString(), dr_pbov.GetValue(0).ToString()));
        }
        dr_pbov.Close();
        Dr_Pbov_list.Items.ToString().ToUpper();
    }
    public void Show_Pbov_list(object sender, EventArgs e)
    {
        Show_Pbov_list_Display();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Int16 maxrows = 0;
        if (txtRows.Text.ToString().CompareTo("") != 0)
        {
            try
            {
                maxrows = Convert.ToInt16(txtRows.Text);
            }
            catch (Exception ex)
            {
                txtRows.Text = "";
                txtRows.Focus();
            }
        }

        for (int i = 0; i < maxrows; i++)
        {
            dt.Rows.Add();
        }

        grvcontrols.DataSource = dt;
        grvcontrols.DataBind();
    }
    private void FillBusDiv()
    {
        string strSql = "Select top 1 '-1' as CodeId ,'--Select One--' as CodeDesc Union select CodeId,CodeDesc from webx_master_general where codetype = 'BUT' and statuscode = 'Y'";
        //cmd = new SqlCommand(strSql, con);
        //dr = cmd.ExecuteReader();
        dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
        while (dr.Read())
        {
            ddlBusinessDiv.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
        }
        dr.Close();
        ddlBusinessDiv.SelectedIndex = 1;
        ddlBusinessDiv.CssClass = "blackfnt";
    }

    protected void grvcontrols_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((TextBox)e.Row.FindControl("txtAccCode")).Attributes.Add("onblur", "AccBlur('" + ((TextBox)e.Row.FindControl("txtAccCode")).ClientID.ToString() + "')");
            ((Button)e.Row.FindControl("btnAccCode")).Attributes.Add("onclick", "popuplist('AccCode','" + ((TextBox)e.Row.FindControl("txtAccCode")).ClientID.ToString() + "','none')");
            ((TextBox)e.Row.FindControl("txtAmt")).Attributes.Add("onblur", "GetTot()");
        }
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayMode.SelectedItem.Text == "Cash")
        {

            txtCashAmt.Enabled = false;
            ddrCashcode.Enabled = true;

            txtChqAmt.Enabled = false;
            txtChqNo.Enabled = false;
            txtChqAmt.Text = "0.00";
            txtChqNo.Text = "";
            txtChqAmt.Text = "";
            txtChqNo.Text = "";

            ddrBankaccode.SelectedValue = "";
            ddrBankaccode.Enabled = false;
            txtChqDate.Text = "";
            txtChqDate.Enabled = false;

            ddrCashcode.Items.Clear();
            txtCashAmt.Text = Hnd_totalAmount.Value.ToString();
            string strSql = " select top 1 '' as asscodeval,'--Select--' as accdesc Union  select acccode as asscodeval,accdesc from webx_acctinfo where  acccategory='CASH'";
            //cmd = new SqlCommand(strSql, con);
            //dr = cmd.ExecuteReader();
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrCashcode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();
            ddrCashcode.CssClass = "blackfnt";
        }
        else if (ddlPayMode.SelectedItem.Text == "Bank")
        {
            txtCashAmt.Enabled = false;
            txtCashAmt.Text = "0.00";
            ddrCashcode.SelectedValue = "";
            ddrCashcode.Enabled = false;

            txtChqAmt.Enabled = false;
            txtChqNo.Enabled = true;
            txtChqAmt.Text = Hnd_totalAmount.Value.ToString();

            txtCashAmt.Text = "";

            ddrBankaccode.Enabled = true;
            txtChqDate.Enabled = true;

            ddrBankaccode.Items.Clear();

            string strSql = "select top 1 '' as asscodeval,'--Select--' as accdesc Union select acccode as asscodeval,accdesc from webx_acctinfo where  ( ((bkloccode like 'All' or PATINDEx ('%" + Session["brcd"].ToString() + "%',bkloccode)>0) AND acccategory='BANK')) order by accdesc asc";
            //cmd = new SqlCommand(strSql, con);
            //dr = cmd.ExecuteReader();
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrBankaccode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();

            ddrBankaccode.CssClass = "blackfnt";

        }
        else if (ddlPayMode.SelectedItem.Text == "Both")
        {
            txtCashAmt.Enabled = true;
            txtChqAmt.Enabled = true;
            txtChqNo.Enabled = true;
            txtChqDate.Enabled = true;
            ddrBankaccode.Enabled = true;
            ddrCashcode.Enabled = true;

            ddrCashcode.Items.Clear();
            string strSql = " select top 1 '' as asscodeval,'--Select--' as accdesc Union  select acccode as asscodeval,accdesc from webx_acctinfo where  acccategory='CASH'";
            //cmd = new SqlCommand(strSql, con);
            //dr = cmd.ExecuteReader();
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrCashcode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();

            ddrBankaccode.Items.Clear();
            string strSql1 = "select top 1 '' as asscodeval,'--Select--' as accdesc Union select acccode asscodeval,accdesc from webx_acctinfo where  ( ((bkloccode like 'All' or PATINDEx ('%" + Session["brcd"].ToString() + "%',bkloccode)>0) AND acccategory='BANK')) order by accdesc asc";
            //cmd = new SqlCommand(strSql1, con);
            //dr = cmd.ExecuteReader();
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql1);
            while (dr.Read())
            {
                ddrBankaccode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();
            ddrCashcode.CssClass = "blackfnt";
            ddrBankaccode.CssClass = "blackfnt";
        }
    }
    protected void txtAmtApplL_TextChanged(object sender, EventArgs e)
    { }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Year = Session["FinYear"].ToString();
        string opertitle = "MANUAL CREDIT VOUCHER";
        string Financial_Year = Session["FinYear"].ToString();
        //string Voucherno = fn.NextVoucherno(Session["brcd"].ToString(), Financial_Year.Substring(2,2));
        string Voucherno = "";
        string[] VDate = txtVoucherDate.Text.Split('/');
        string EntryBy = Session["empcd"].ToString(), Transtype = "", OppaccountDesc = "", OppaccountCode = "";
        string sql_Acccode = "";
        string VoucherDate = Convert.ToDateTime(VDate[1] + "/" + VDate[0] + "/" + VDate[2]).ToString("yyyy/MM/dd");
        string finyear = Session["FinYear"].ToString();
        string[] CDate;
        string ChqDate, Chqno = "", Str_Onaccount = "N", OppaccountDesc_mode = "", OppaccountCode_mode = "";
        double adjustamt = 0;

        if (RAD_Customer.Checked)
            PBOV_TYP = "P";
        else if (RAD_Vendor.Checked)
            PBOV_TYP = "V";
        else if (RAD_Emp.Checked)
            PBOV_TYP = "E";
        else if (RAD_Driver.Checked)
            PBOV_TYP = "D";
        else if (RAD_Vehicle.Checked)
            PBOV_TYP = "L";

        if (ddlPayMode.SelectedItem.Text != "Cash")
        {
            Chqno = "'" + txtChqNo.Text + "'";
            CDate = txtChqDate.Text.Split('/');
            ChqDate = Convert.ToDateTime(CDate[1] + "/" + CDate[0] + "/" + CDate[2]).ToString("yyyy/MM/dd");

            string sql_chk = "select chqamt,adjustamt,banknm from webx_chq_det where chqno=" + Chqno + " and chqdt='" + ChqDate + "'";
            SqlCommand cmd_chk = new SqlCommand(sql_chk, con);
            SqlDataReader dr_chk = null;

            dr_chk = cmd_chk.ExecuteReader();
            string chque_yn_chk = "N";
            if (dr_chk.Read())
            {
                chque_yn_chk = "Y";
            }
            dr_chk.Close();

            if (chque_yn_chk == "Y")
            {
                Response.Write("<br><br><br><font class='blackboldfnt' color='red' ><B>Error : Duplicate Cheque Entered </b></font>");
                string msg = "Error : Duplicate Cheque Entered";
                Response.Redirect("Message.aspx?" + msg);
                //Response.End();
            }
        }
        if (ddlPayMode.SelectedItem.Text == "Cash")
        {
            Transtype = "CASH RECEIPT";
        }
        else
        {
            Transtype = "BANK RECEIPT";
            OppaccountCode = "ACA0002";
            OppaccountDesc = "CHEQUES IN HAND";
            if (rdDiposited.SelectedItem.Text == "Yes")
            {
                OppaccountCode = ddrBankaccode.SelectedValue.ToString();
                OppaccountDesc = ddrBankaccode.SelectedItem.Text.ToString();
            }
            adjustamt = Convert.ToDouble(txtChqAmt.Text);
            if (Onaccount.Checked)
            {
                Str_Onaccount = "Y";
                adjustamt = 0;
            }
        }
        if (ddlPayMode.SelectedItem.Text == "Both")
        {
            Transtype = "BOTH RECEIPT";
        }

        sql_Acccode = "select accdesc from webx_acctinfo WITH(NOLOCK) where Acccode='" + OppaccountCode + "'";
        cmd = new SqlCommand(sql_Acccode, con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            OppaccountDesc = dr["accdesc"].ToString().Trim();
        }
        dr.Close();

        SqlTransaction trans;
        trans = con.BeginTransaction();
        try
        {
            Voucherno = fn.NextVoucherno(Session["brcd"].ToString(), Financial_Year.Substring(2, 2));
            string PBOV_code = "", PBOV_Name = "", PBOV_typ = "";
            if (Dr_Pbov_list.SelectedValue == "" || Dr_Pbov_list.SelectedValue == "8888")
            {
                if (txtCode.Text != "")
                {
                    PBOV_code = "8888";
                    PBOV_Name = txtCode.Text;
                }
                else
                {
                    PBOV_code = "8888";
                    PBOV_Name = "Walk in Customer";
                }
                //PBOV_code = "8888";
                //PBOV_Name = txtCode.Text;
            }
            else
            {
                PBOV_code = Dr_Pbov_list.SelectedValue.ToString();
                string[] PBOV_Name_arr = Dr_Pbov_list.SelectedItem.Text.ToString().Split(':');
                PBOV_Name = PBOV_Name_arr[0].ToString();
            }

            string sql_Acctrans = "", Acccode = "", Debit = "", Credit = "", Narration = "", Description = "";
            string Xml_Acccode_Details = "<root>";

            int i = 0;
            foreach (GridViewRow gridrow in grvcontrols.Rows)
            {
                Acccode = ((TextBox)gridrow.FindControl("txtAccCode")).Text.ToString();
                Credit = ((TextBox)gridrow.FindControl("txtAmt")).Text.ToString();
                Debit = "0.00";
                Narration = ((TextBox)gridrow.FindControl("txtNarration")).Text.ToString().Replace("'", "''");
                Description = ((TextBox)gridrow.FindControl("txtDescription")).Text.ToString().Replace("'", "''");
                if (Acccode != "")
                {
                    i = i + 1;
                    sql_Acccode = "select Acccode,accdesc from webx_acctinfo WITH(NOLOCK) where Company_Acccode='" + Acccode + "'";
                    cmd = new SqlCommand(sql_Acccode, con, trans);

                    dr = cmd.ExecuteReader();
                    string Accdesc = "";
                    while (dr.Read())
                    {
                        Acccode = dr["Acccode"].ToString().Trim();
                        Accdesc = dr["Accdesc"].ToString().Trim();
                        Accdesc = Accdesc.Replace("<", "&lt;");
                        Accdesc = Accdesc.Replace(">", "&gt;");
                        Accdesc = Accdesc.Replace("\"", "&quot;");
                        Accdesc = Accdesc.Replace("'", "&apos;");
                    }
                    if (i == 1)
                    {
                        OppaccountDesc_mode = Accdesc;
                        OppaccountCode_mode = Acccode;
                    }
                    dr.Close();

                    if (Str_Onaccount == "Y" && Acccode != "CDA0001")
                    {
                        Response.Write("<br><br><br><center><font class='blackboldfnt' color='red'><b>Error : On Account Cheque Facility is not Availble for :" + Accdesc + "</b></font></center>");
                        throw new Exception("Error : On Account Cheque Facility is not Availble for : " + Accdesc + ". This Facility Available only for ledger : Billed Debtors");
                    }

                    Xml_Acccode_Details = Xml_Acccode_Details + "<Acccode>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Acccode>" + Acccode.Trim() + "</Acccode>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Accdesc>" + Accdesc.Trim() + "</Accdesc>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Debit>" + Debit.Trim() + "</Debit>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Credit>" + Credit.Trim() + "</Credit>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Narration>" + Narration.Trim() + "</Narration>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "</Acccode>";
                }
            }

            Xml_Acccode_Details = Xml_Acccode_Details + "</root>";

            // Entry for Service Tax and Tds Details In Acctrans Table

            string svrcheck = "N", tdscheck = "N";
            string BankCode = "";

            if (Svctax_yn.Checked)
            {
                svrcheck = "Y";
            }
            if (TDS_yn.Checked)
            {
                tdscheck = "Y";
            }
            if (txtCashAmt.Text == "")
            {
                txtCashAmt.Text = "0";
            }
            if (txtChqAmt.Text == "")
            {
                txtChqAmt.Text = "0";
            }
            if (HdnServiceTaxRate.Value == "")
            {
                HdnServiceTaxRate.Value = "0";
            }
            if (HdnEduCessRate.Value == "")
            {
                HdnEduCessRate.Value = "0";
            }
            if (HdnHEduCessRate.Value == "")
            {
                HdnHEduCessRate.Value = "0";
            }

            string DepoFlag = "N";
            if (ddlPayMode.SelectedItem.Text != "Cash")
            {
                BankCode = "ACA0002";
                if (rdDiposited.SelectedItem.Text == "Yes")
                {
                    DepoFlag = "Y";
                    BankCode = ddrBankaccode.SelectedValue.ToString();
                }
            }
            // ADD Entry in Acctrans Table

            string Xml_Other_Details = "<root><Other>";
            Xml_Other_Details = Xml_Other_Details + "<Finyear>" + Session["FinYear"].ToString().Trim() + "</Finyear>";
            Xml_Other_Details = Xml_Other_Details + "<Transdate>" + VoucherDate.Trim() + "</Transdate>";
            Xml_Other_Details = Xml_Other_Details + "<Transtype>" + Transtype.Trim() + "</Transtype>";
            Xml_Other_Details = Xml_Other_Details + "<Voucherno>" + Voucherno.Trim() + "</Voucherno>";
            Xml_Other_Details = Xml_Other_Details + "<Brcd>" + Session["brcd"].ToString().Trim() + "</Brcd>";
            Xml_Other_Details = Xml_Other_Details + "<Entryby>" + txtPreparedBy.Text.Trim() + "</Entryby>";
            Xml_Other_Details = Xml_Other_Details + "<Opertitle>" + "MANUAL CREDIT VOUCHER" + "</Opertitle>";
            Xml_Other_Details = Xml_Other_Details + "<ManualNo>" + txtManualNo.Text.Trim() + "</ManualNo>";
            Xml_Other_Details = Xml_Other_Details + "<BusinessType>" + ddlBusinessDiv.SelectedValue.Trim() + "</BusinessType>";
            Xml_Other_Details = Xml_Other_Details + "<PreparedAtLoc>" + txtPreparedLoc.Text.Trim() + "</PreparedAtLoc>";
            Xml_Other_Details = Xml_Other_Details + "<Acclocation>" + txtAccLoc.Text.Trim() + "</Acclocation>";
            Xml_Other_Details = Xml_Other_Details + "<preparefor>" + txtPreparedFor.Text.Trim() + "</preparefor>";
            Xml_Other_Details = Xml_Other_Details + "<Refno>" + txtReferenceNo.Text.Trim() + "</Refno>";
            Xml_Other_Details = Xml_Other_Details + "<Narration>" + txtNarration.Text.Trim() + "</Narration>";
            Xml_Other_Details = Xml_Other_Details + "<Panno>" + txtPanNo.Text.Trim() + "</Panno>";
            Xml_Other_Details = Xml_Other_Details + "<Svrtaxno>" + txtSrvTaxNo.Text.Trim() + "</Svrtaxno>";
            Xml_Other_Details = Xml_Other_Details + "<chqno>" + txtChqNo.Text.Trim() + "</chqno>";
            Xml_Other_Details = Xml_Other_Details + "<chqamt>" + Convert.ToDouble(txtChqAmt.Text.Trim()) + "</chqamt>";
            Xml_Other_Details = Xml_Other_Details + "<chqdt>" + fn.Mydate1(txtChqDate.Text) + "</chqdt>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_code>" + PBOV_code.Trim() + "</pbov_code>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_name>" + PBOV_Name.Trim() + "</pbov_name>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_typ>" + PBOV_TYP.Trim() + "</pbov_typ>";
            Xml_Other_Details = Xml_Other_Details + "<Svrtax_yn>" + svrcheck.Trim() + "</Svrtax_yn>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_yn>" + tdscheck.Trim() + "</Tds_yn>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_Acccode>" + Tdssection.SelectedValue.ToString().Trim() + "</Tds_Acccode>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_rate>" + txtTDSRate.Text.Trim() + "</Tds_rate>";
            Xml_Other_Details = Xml_Other_Details + "<bankAcccode>" + BankCode.Trim() + "</bankAcccode>";
            Xml_Other_Details = Xml_Other_Details + "<recbanknm>" + txtRecBank.Text.Trim() + "</recbanknm>";
            Xml_Other_Details = Xml_Other_Details + "<Onaccount_YN>" + Str_Onaccount.Trim() + "</Onaccount_YN>";
            Xml_Other_Details = Xml_Other_Details + "<Deposited>" + DepoFlag.Trim() + "</Deposited>";
            Xml_Other_Details = Xml_Other_Details + "<Paymode>" + ddlPayMode.SelectedValue.ToString().Trim() + "</Paymode>";
            Xml_Other_Details = Xml_Other_Details + "<SvrTaxRate>" + HdnServiceTaxRate.Value.Trim() + "</SvrTaxRate>";
            Xml_Other_Details = Xml_Other_Details + "<EduCessRate>" + HdnEduCessRate.Value.Trim() + "</EduCessRate>";
            Xml_Other_Details = Xml_Other_Details + "<HEduCessRate>" + HdnHEduCessRate.Value.Trim() + "</HEduCessRate>";
            Xml_Other_Details = Xml_Other_Details + "</Other></root>";

            Xml_Acccode_Details = RemoveSpecialCharactorsForXML(Xml_Acccode_Details);
            Xml_Other_Details = RemoveSpecialCharactorsForXML(Xml_Other_Details);

            string sql = "usp_Generate_ManualAccountDetails_Ver2";
            cmd = new SqlCommand(sql, con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Xml_Acccode_Details", SqlDbType.Text).Value = Xml_Acccode_Details.Replace("&", "&amp;").Trim();
            cmd.Parameters.Add("@Xml_Other_Details", SqlDbType.Text).Value = Xml_Other_Details.Replace("&", "&amp;").Trim();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();

            // END

            double LedgerDiff = 0;
            sql_Acctrans = "select sum(debit) - sum(credit) as Differance from webx_acctrans_" + fin_year + " where voucher_cancel='N' and voucherno='" + Voucherno + "'";
            cmd = new SqlCommand(sql_Acctrans, con, trans);
            SqlDataReader dr_count = cmd.ExecuteReader();
            while (dr_count.Read())
            {
                LedgerDiff = Convert.ToDouble(dr_count["Differance"].ToString().Trim());
            }
            dr_count.Close();

            if (LedgerDiff != 0)
            {
                throw new Exception("Credit Amount Should Be Equal To Payment Amount");
            }
            else
            {
                trans.Commit();
            }
        }
        catch (Exception e1)
        {
            Response.Write("<br><br><br><center><font class='blackboldfnt' color='red'><b>Error : " + e1.Message + "</b></font></center>");
            string msg = e1.Message.ToString();
            msg = msg.Replace('\n', ' ');
            trans.Rollback();
            Response.Redirect("Message.aspx?" + msg);
        }

        con.Close();
        Response.Redirect("./voucher_Done.aspx?VoucherType=Credit&VoucherNo=" + Voucherno + "&Defaultdate=" + txtVoucherDate.Text, false);
    }
    public static string RemoveSpecialCharactorsForXML(string inputString)
    {
        string specialChr = "☺☻♥♦♣♠•◘○'‘’¢©÷·¶±€£®§™¥°÷×¾¢¡¿☼♀♂♪♫◄√↑↓→←∟↕↔ \n”";
        foreach (char c in specialChr.ToCharArray())
        {
            inputString = inputString.Replace(c, ' ');
        }
        inputString = inputString.Replace("–", "-");
        return inputString.Replace("&", "&amp;");
    }
}
