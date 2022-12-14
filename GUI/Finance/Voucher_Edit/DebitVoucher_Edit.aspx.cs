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
using WebX.Controllers;
using WebX.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class GUI_Finance_Voucher_Edit_DebitVoucher_Edit : System.Web.UI.Page
{   
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    SqlConnection con;
    DateFunction DAccess = new DateFunction();
    static DataTable dt = new DataTable("table1");
    string svrtax, tds, SBC, KKC, tdsrate = "", brcd = "", voucherno = "", PBOV_TYP;
    string Financial_Year = "", fin_year = "", opertitle = "MANUAL DEBIT VOUCHER", Defaultdate = "";
    MyFunctions fn = new MyFunctions();
    double NetAMount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {   
        con = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        con.Open();

        voucherno = Request.QueryString["voucherno"].ToString().Replace("'", "");

        Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        fin_year = Session["FinYear"].ToString();
        double fin_year_next = Convert.ToDouble(Financial_Year) + 1;
        fin_year = Financial_Year + "_" + fin_year_next.ToString().PadLeft(2, '0');

        string table_name = "webx_acctrans_" + fin_year; 

        if(!IsPostBack)
        {
            FillBusDiv();

            dt = new DataTable("table1");

            hdnroundoff.Value = "N";
            hdneditablsvctaxrate.Value = "N";

            double count = 0;
            count = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select count(*) FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Decimal Values Y/N'"));
            if (count > 0)
            {
                hdnroundoff.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Decimal Values Y/N'").ToString();
                if (hdnroundoff.Value == "Y")
                {
                    decimal amtdiff = 0;
                    amtdiff = Convert.ToDecimal(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "SELECT ABS(SUM(DEBIT)-ROUND(SUM(DEBIT),0)) FROM " + table_name + " WHERE ISNULL(VOUCHER_CANCEL,'N')='N' AND VOUCHERNO='" + voucherno + "'"));
                    if (amtdiff > 0)
                    {
                        hdnroundoff.Value = "N";
                    }
                }
            }
            count = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select count(*) FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Editable ServiceTax Rate Y/N'"));
            if (count > 0)
            {
                hdneditablsvctaxrate.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, "select RULE_Y_N FROM webx_modules_rules where Module_Name='Manual Vouchers' and RULE_DESC='Editable ServiceTax Rate Y/N'").ToString();
            }
            if (hdneditablsvctaxrate.Value == "Y")
            {
                trSvcTaxRate.Visible = true;
            }

            string sql = "select  b.company_acccode,b.accdesc,a.debit,a.Narration from WEBX_ACCcode_details a,webx_acctinfo b With(NOLOCK) where ISNULL(a.voucher_cancel,'N')='N' and a.acccode=b.acccode and a.voucherno='" + voucherno + "'";
            da = new SqlDataAdapter(sql, con);
            da.Fill(dt);

            grvcontrols.DataSource = dt;
            grvcontrols.DataBind();

            sql = "select  sum(debit) from WEBX_ACCcode_details With(NOLOCK) where voucherno='" + voucherno + "' and ISNULL(voucher_cancel,'N')='N'";
           
            hdntotamt.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, sql).ToString();

            sql = "select acccode,accdesc from webx_acctinfo where accdesc like '%tds%'";
            SqlCommand cmd1 = new SqlCommand(sql, con);
            dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                Tdssection.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();
            Tdssection.CssClass = "blackfnt";

            string Payment_account_Cash = "", Payment_account_Bank = "", PBOV_Type = "", PBOV_Code = "", PBOV_Name = "", CASH_AMT = "", BANK_AMT = "";

            sql = "exec usp_voucher_detail_Ver2 '" + voucherno + "','" + table_name + "'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sql);
            while (dr.Read())
            {
                lblvoucherno.Text = voucherno;
                txtVoucherDate.Text = dr["Transdate"].ToString();
                txtManualNo.Text = dr["Manual_Voucherno"].ToString();
                txtPreparedLoc.Text = dr["brcd"].ToString();
                HdnBrcd.Value = dr["brcd"].ToString();
                txtPreparedBy.Text = dr["Entryby"].ToString();
                txtAccLoc.Text = dr["Accounting_Brcd"].ToString();
                ddlBusinessDiv.SelectedValue = dr["Business_type"].ToString();
                txtPreparedFor.Text = dr["preparefor"].ToString();
                txtReferenceNo.Text = dr["Refno"].ToString();
                txtNarration.Text = dr["Narration"].ToString();
                svrtax = dr["servicetax"].ToString();
                tds = dr["tds"].ToString();
                SBC = dr["SBCess"].ToString();
                KKC = dr["KKCess"].ToString();
                tdsrate = dr["tdsrate"].ToString();
                txtSrvTaxNo.Text = dr["servicetaxno"].ToString();
                txtPanNo.Text = dr["panno"].ToString();
                Tdssection.SelectedValue = dr["tdsacccode"].ToString();
                ddlPayMode.SelectedValue = dr["transmode"].ToString();
                Payment_account_Cash = dr["Pay_cash"].ToString();
                Payment_account_Bank = dr["Pay_BANK"].ToString();
                PBOV_Type = dr["PBOV_Type"].ToString();
                PBOV_Code = dr["pbov_code"].ToString();
                PBOV_Name = dr["pbov_NAME"].ToString();
                CASH_AMT= dr["CASH_AMT"].ToString();
                BANK_AMT = dr["BANK_AMT"].ToString();
                hdncashbankvalue.Value = dr["transmode"].ToString();
                Old_CASH_AMT.Value = CASH_AMT.ToString();
                if (ddlPayMode.SelectedValue != "Cash")
                {
                    txtChqNo.Text = dr["chqno"].ToString();
                    txtChqDate.Text = dr["ChqDate"].ToString();
                    HdnChqNo.Value = dr["chqno"].ToString();
                    HdnChqDate.Value = dr["ChqDate"].ToString();
                    HdnBankaccode.Value = dr["Pay_BANK"].ToString();
                }

                HdnCashAmt.Value = CASH_AMT;
                HdnChqAmt.Value = BANK_AMT;
            }

            dr.Close();

            DateTime curdt;
            System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
            curdt = Convert.ToDateTime(txtVoucherDate.Text, enGB);
            HdnServiceTaxRate.Value = Convert.ToString(TaxRateController.GetServiceTaxRate(curdt));
            HdnEduCessRate.Value = Convert.ToString(TaxRateController.GetEduCessRate(curdt));
            HdnHEduCessRate.Value = Convert.ToString(TaxRateController.GetHEduCessRate(curdt));
            HdnSBCRate.Value = Convert.ToString(TaxRateController.GetSwachhBharatCessRate(curdt));
            HdnKKCRate.Value = Convert.ToString(TaxRateController.GetKrushiKalyanCessRate(curdt));

            brcd = HdnBrcd.Value;
           
            if (PBOV_Type == "C" || PBOV_Type == "P")
            {
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
                RAD_Customer.Checked = true; 
                RAD_Vendor.Checked = false;
                RAD_Emp.Checked = false;
                Show_Pbov_list_Display();
                if(PBOV_Code.ToString() == "8888")
                    txtCode.Text = PBOV_Name;
                else
                    Dr_Pbov_list.SelectedValue = PBOV_Code.ToString();
            }
            else if (PBOV_Type == "V")
            {
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
                RAD_Customer.Checked = false;
                RAD_Vendor.Checked = true;
                RAD_Emp.Checked = false;
                Show_Pbov_list_Display();
                if (PBOV_Code.ToString() == "8888")
                    txtCode.Text = PBOV_Name;
                else
                    Dr_Pbov_list.SelectedValue = PBOV_Code.ToString();
            }
            else if (PBOV_Type == "U" || PBOV_Type == "E")
            {
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
                RAD_Emp.Checked = true;
                RAD_Customer.Checked = false;
                RAD_Vendor.Checked = false;
                Show_Pbov_list_Display();
                if (PBOV_Code.ToString() == "8888")
                    txtCode.Text = PBOV_Name;
                else
                    Dr_Pbov_list.SelectedValue = PBOV_Code.ToString();
            }
            else if (PBOV_Type == "D")
            {
                RAD_Driver.Checked = true;
                RAD_Vehicle.Checked = false;
                RAD_Emp.Checked = false;
                RAD_Customer.Checked = false;
                RAD_Vendor.Checked = false;
                Show_Pbov_list_Display();
                if (PBOV_Code.ToString() == "8888")
                    txtCode.Text = PBOV_Name;
                else
                    Dr_Pbov_list.SelectedValue = PBOV_Code.ToString();
            }
            else if (PBOV_Type == "L")
            {
                RAD_Vehicle.Checked = true;
                RAD_Driver.Checked = false;
                RAD_Emp.Checked = false;
                RAD_Customer.Checked = false;
                RAD_Vendor.Checked = false;

                Show_Pbov_list_Display();
                if (PBOV_Code.ToString() == "8888")
                    txtCode.Text = PBOV_Name;
                else
                    Dr_Pbov_list.SelectedValue = PBOV_Code.ToString();
            }
            else if (PBOV_Type == "M")
            {
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
                RAD_Emp.Checked = false;
                RAD_Vendor.Checked = false;
                RAD_Customer.Checked = true;
                Show_Pbov_list_Display();
                txtCode.Text = PBOV_Name;
            }

            decimal servicetax = 0;
            decimal tdsvalue = 0;
            decimal SBCvalue = 0;
            decimal KKCvalue = 0;

            if(svrtax != "")
                servicetax = Convert.ToDecimal(svrtax);
            if(tds != "")
                tdsvalue = Convert.ToDecimal(tds);
            if (SBC != "" && SBC != "0")
                SBCvalue = Convert.ToDecimal(SBC);
            if (KKC != "" && KKC != "0")
                KKCvalue = Convert.ToDecimal(KKC);

            if (servicetax > 0)
            {
                hdnsvctax.Value = svrtax;
                if (hdneditablsvctaxrate.Value.CompareTo("Y") == 0)
                {
                    Svctax_yn.Checked = true;
                    txtSvrTaxRate.Text = Convert.ToString((Convert.ToDecimal(100) * Convert.ToDecimal(servicetax)) / Convert.ToDecimal(hdntotamt.Value));
                    txtSvrTaxRate.Text = Convert.ToDecimal(txtSvrTaxRate.Text).ToString("F2");
                    HdnServiceTaxRate.Value = txtSvrTaxRate.Text;
                }
            }
            if (servicetax == 0)
            {
                hdnsvctax.Value = "0.00";
                if (hdneditablsvctaxrate.Value.CompareTo("Y") == 0)
                {
                    Svctax_yn.Checked = false;
                    txtSvrTaxRate.Text = "0.00";
                }
            }

            if (SBCvalue > 0)
            {
                HdnSBCess.Value = SBC;
                txtSBCess.Text = Convert.ToString(SBCvalue);
            }
            else if (SBCvalue == 0)
            {
                HdnSBCess.Value = "0.00";
                txtSBCess.Text = "0.00";
            }
            if (KKCvalue > 0)
            {
                HdnKKCess.Value = KKC;
                txtKKCess.Text = Convert.ToString(KKCvalue);
            }
            else if (KKCvalue == 0)
            {
                HdnKKCess.Value = "0.00";
                txtKKCess.Text = "0.00";
            }

            if (tdsvalue > 0)
            {
                HdnTdsAmt.Value = tds;
                txtTDSAmt.Text = HdnTdsAmt.Value;

                HdnTDSRate.Value = tdsrate;
                txtTDSRate.Text = HdnTDSRate.Value;
            }
            
            CASH_BANK_ACCOUNT();
            
            if (ddlPayMode.SelectedValue == "Cash" || ddlPayMode.SelectedValue == "Both")
             {
                 ddrCashcode.SelectedValue = Payment_account_Cash.ToString();
             }
            if (ddlPayMode.SelectedValue == "Bank" || ddlPayMode.SelectedValue == "Both")
             {
                 ddrBankaccode.SelectedValue = Payment_account_Bank.ToString();
             }
            txtCashAmt.Text = CASH_AMT;
            txtChqAmt.Text = BANK_AMT;
        }

        con.Close();

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
        txtSrvTaxNo.Attributes.Add("onchange", "CheckTax()");
        txtPanNo.Attributes.Add("onblur", "CheckPan(this)");
        txtCashAmt.Attributes.Add("onblur", " CheckCashBank()");
        txtChqAmt.Attributes.Add("onblur", " CheckCashBank()");

    }
    private void FillBusDiv()
    {
        string strSql = "Select top 1 '-1' as CodeId ,'--Select One--' as CodeDesc Union select CodeId,CodeDesc from webx_master_general where codetype = 'BUT' and statuscode = 'Y'";
        cmd = new SqlCommand(strSql, con);

        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ddlBusinessDiv.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
        }
        dr.Close();
        ddlBusinessDiv.SelectedIndex = 1;
        ddlBusinessDiv.CssClass = "blackfnt";
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
        else
        {
            maxrows = 5;
        }
        for (int i = 0; i < maxrows; i++)
        {
            dt.Rows.Add();
        }

        grvcontrols.DataSource = dt;
        grvcontrols.DataBind();
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
    protected void txtAmtApplL_TextChanged(object sender, EventArgs e)
    {}
    public void CASH_BANK_ACCOUNT()
    {   
        if (ddlPayMode.SelectedItem.Text == "Cash")
        {
            txtCashAmt.Text = txtNetPay.Text;
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
            string strSql = " select top 1 '' as asscodeval,'--Select--' as accdesc Union  select acccode  as asscodeval,accdesc from webx_acctinfo where  acccategory='CASH'";
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
            txtChqAmt.Text = txtNetPay.Text;

            txtCashAmt.Text = "";

            ddrBankaccode.Enabled = true;
            txtChqDate.Enabled = true;

            txtChqNo.Text = HdnChqNo.Value;
            txtChqDate.Text = HdnChqDate.Value;

            ddrBankaccode.Items.Clear();

            string strSql = "select top 1 '' as asscodeval,'--Select--' as accdesc Union select acccode  as asscodeval,accdesc from webx_acctinfo where  ( ((bkloccode like 'All' or PATINDEx ('%" + HdnBrcd.Value + "%',bkloccode)>0) AND acccategory='BANK')) order by accdesc asc";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrBankaccode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();
            ddrBankaccode.CssClass = "blackfnt";
            ddrBankaccode.SelectedValue = HdnBankaccode.Value;

        }
        else if (ddlPayMode.SelectedItem.Text == "Both")
        {
            txtCashAmt.Enabled = true;
            txtChqAmt.Enabled = true;
            txtChqNo.Enabled = true;
            txtChqDate.Enabled = true;
            ddrBankaccode.Enabled = true;
            ddrCashcode.Enabled = true;

            txtChqNo.Text = HdnChqNo.Value;
            txtChqDate.Text = HdnChqDate.Value;

            txtChqAmt.Text = HdnChqAmt.Value;
            txtCashAmt.Text = HdnCashAmt.Value;

            ddrCashcode.Items.Clear();
            string strSql = " select top 1 '' as asscodeval,'--Select--' as accdesc Union  select acccode  as asscodeval,accdesc from webx_acctinfo where  acccategory='CASH'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrCashcode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();
            
            ddrBankaccode.Items.Clear();

            strSql = "select top 1 '' as asscodeval,'--Select--' as accdesc Union select acccode  as asscodeval,accdesc from webx_acctinfo where  ( ((bkloccode like 'All' or PATINDEx ('%" + HdnBrcd.Value + "%',bkloccode)>0) AND acccategory='BANK')) order by accdesc asc";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrBankaccode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();

            ddrCashcode.CssClass = "blackfnt";
            ddrBankaccode.CssClass = "blackfnt";
            ddrBankaccode.SelectedValue = HdnBankaccode.Value;
        }
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        CASH_BANK_ACCOUNT();
    }
    public void Show_Pbov_list_Display()
    {
        string sql_display_pbov = "";
        if (RAD_Customer.Checked == true)
        {
            sql_display_pbov = "select custcd as code , custnm+':'+custcd  as name from webx_custhdr WITH(NOLOCK) where CUSTCD is not null and    PATINDEx ('%" + HdnBrcd.Value + "%',custloc)>0 order by CUSTNM ";
        }
        else if (RAD_Vendor.Checked == true)
        {
            sql_display_pbov = "select vendorCode as code , vendorname+':'+vendorCode as name from webx_VENDOR_HDR WITH(NOLOCK) where vendorcode in ( select vendorcode from webx_VENDOR_det WITH(NOLOCK)  where vendorCode is not null and    PATINDEx ('%" + HdnBrcd.Value + "%',vendorbrcd)>0 ) order by vendorname";
        }
        else if (RAD_Emp.Checked == true)
        {
            if (SessionUtilities.Client.CompareTo("Ritco") == 0)
                sql_display_pbov = "select userid as code ,name+':'+userid as name  from webx_master_users  WITH(NOLOCK) where status='100' and BranchCode='" + HdnBrcd.Value + "' order by name";
            else
                sql_display_pbov = "select userid as code ,name+':'+userid as name  from webx_master_users  WITH(NOLOCK) where status='100' order by name,BranchCode";
        }
        else if (RAD_Driver.Checked == true)
        {
            sql_display_pbov = "select Driver_id as code ,Driver_Name + ':' + convert(varchar,Manual_Driver_Code) as name from webx_fleet_drivermst WITH(NOLOCK) where PATINDEx ('%" + HdnBrcd.Value + "%',Driver_Location)>0 and ActiveFlag='Y' order by Driver_Name,Driver_Location";
        }
        else if (RAD_Vehicle.Checked == true)
        {
            sql_display_pbov = "select Vehno as code ,Vehno as name from webx_vehicle_hdr WITH(NOLOCK) where PATINDEx ('%" + HdnBrcd.Value + "%',Conrtl_branch)>0 and ActiveFlag='Y' order by Vehno";
        }

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {   
        string Year = Session["FinYear"].ToString();
        string Financial_Year = Session["FinYear"].ToString().Substring(2,2);
        string Voucherno = lblvoucherno.Text.ToString() ;
        string[] VDate = txtVoucherDate.Text.Split('/');
        string EntryBy = Session["empcd"].ToString(), Transtype = "", OppaccountDesc = "", OppaccountCode = "";
        
        string VoucherDate = Convert.ToDateTime(VDate[1] + "/" + VDate[0] + "/" + VDate[2]).ToString("yyyy/MM/dd");
        string sql_Acccode = "";
        string[] CDate;
        string ChqDate="",Chqno="";

        con.Open();

        if (ddlPayMode.SelectedItem.Text != "Cash")
        {
            Chqno = txtChqNo.Text;
            CDate = txtChqDate.Text.Split('/');
            ChqDate = Convert.ToDateTime(CDate[1] + "/" + CDate[0] + "/" + CDate[2]).ToString("yyyy/MM/dd");

            string sql_chk = "select count(*) from webx_chq_det where chqno='" + Chqno + "' and chqdt='" + ChqDate + "' and voucherno <> '" + Voucherno + "'";
            double cnt = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, sql_chk));
            
            string chque_yn_chk = "N";
            if (cnt > 0)
            {
                chque_yn_chk = "Y";
            }
            if (chque_yn_chk == "Y")
            {   
                string ErrorMsg = "Duplicate Cheque Entered";
                Response.Redirect("~/GUI/ErrorPage.aspx?PageHead=" + "Cheque Entry Error" + "&ErrorMsg=" + ErrorMsg);
            }
        }
        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
        {
            SqlDataReader dtr;
            double intCASH_OPNDEBIT = 0;
            double intCASH_OPNCredit = 0;
            string str = "";
            string openBalance = "";
            string finyear = Session["FinYear"].ToString();
            string curr_year = DateTime.Now.ToString("yyyy");

            string finyearstar = "";
            if (finyear == curr_year)
            {
                finyearstar = "01 Apr " + curr_year;
            }
            else
            {
                finyearstar = "01 Apr " + finyear;
            }

            str = "select BANK_OPNDEBIT=isNull(sum(case when acccategory='BANK' then debit else 0 end),0.00),BANK_OPNCredit=isNull(sum(case when acccategory='BANK' then credit else 0 end),0.00),CASH_OPNDEBIT=isNull(sum(case when acccategory='CASH' then debit else 0 end),0.00),CASH_OPNCredit=isNull(sum(case when acccategory='CASH' then credit else 0 end),0.00) from webx_acctrans_" + fin_year + " M,webx_acctinfo D where m.acccode=d.acccode and ( acccategory='CASH' OR ((bkloccode like '%" + txtPreparedLoc.Text + "%' or bkloccode='ALL' ) AND acccategory='BANK')) and m.brcd='" + txtPreparedLoc.Text + "' and Voucher_cancel='N' And convert(varchar,transdate,106) between Convert(datetime,'" + finyearstar + "',106) and convert(datetime,'" + fn.Mydate1(txtVoucherDate.Text) + "',106) Union select BANK_OPNDEBIT=(case when acccategory='BANK' then isnull(o.opendebit_i,0) else 0 end),BANK_OPNCredit=(case when acccategory='BANK' then isnull(o.opencredit_i,0) else 0 end),CASH_OPNDEBIT=(case when acccategory='CASH' then isnull(o.opendebit_i,0) else 0 end),CASH_OPNCredit=(case when acccategory='CASH' then isnull(o.opencredit_i,0) else 0 end) from webx_acctinfo D with(NOLOCK) left outer join webx_acctopening_" + fin_year + "  o with(NOLOCK) on D.acccode=o.acccode where o.brcd='" + txtPreparedLoc.Text + "' and ( acccategory='CASH' OR ((bkloccode like '%" + txtPreparedLoc.Text + "%' or bkloccode='ALL' ) AND acccategory='BANK'))";
            Response.Write("<br>str -" + str);
            dtr = DAccess.getreader(str);
            while (dtr.Read())
            {
                string CASH_OPNDEBIT = dtr["CASH_OPNDEBIT"].ToString();
                intCASH_OPNDEBIT = intCASH_OPNDEBIT + Convert.ToDouble(dtr["CASH_OPNDEBIT"]);
                string CASH_OPNCredit = dtr["CASH_OPNCredit"].ToString();
                intCASH_OPNCredit = intCASH_OPNCredit + Convert.ToDouble(dtr["CASH_OPNCredit"]);
                if (intCASH_OPNDEBIT > intCASH_OPNCredit)
                {
                    openBalance = Convert.ToString(intCASH_OPNDEBIT - intCASH_OPNCredit) + " " + "Dr";
                }
                else
                {
                    openBalance = Convert.ToString(intCASH_OPNCredit - intCASH_OPNDEBIT) + " " + "Cr";
                }
            }
            dtr.Close();

            double Old_AMT = 0, New_AMT = 0;
            Old_AMT = Convert.ToDouble(Old_CASH_AMT.Value.ToString());
            New_AMT = Convert.ToDouble(txtNetPay.Text);
            double curr_val = 0;
            if (New_AMT > Old_AMT)
            {
                curr_val = New_AMT - Old_AMT;
            }

            double open_val = Convert.ToDouble(openBalance.Substring(0, openBalance.Length - 3));
            string open_val_str = (openBalance.Substring(openBalance.Length - 3, 3)).Trim();

            if (open_val > 0 && curr_val > 0)
            {
                if (open_val >= curr_val && open_val_str == "Dr")
                { }
                else
                {
                    string ErrorMsg = "Transactions halted !!!,No sufficient 'CASH IN HAND' for this Location " + Session["brcd"].ToString();
                    Response.Redirect("~/GUI/ErrorPage.aspx?PageHead=" + "Fund Transfer" + "&ErrorMsg=" + ErrorMsg);
                }
            }
        }
        if (ddlPayMode.SelectedItem.Text == "Cash")
        {
            Transtype = "CASH PAYMENT";
        }
        if (ddlPayMode.SelectedItem.Text == "Bank")
        {
            Transtype = "BANK PAYMENT";
        }
        if (ddlPayMode.SelectedItem.Text == "Both")
        {
            Transtype = "BOTH PAYMENT";
        }

        //sql_Acccode = "select top 1 accdesc from webx_acctinfo WITH(NOLOCK) where Acccode='" + OppaccountCode + "'";
        //OppaccountDesc = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, sql_Acccode).ToString();
        
        /*
        cmd = new SqlCommand(sql_Acccode, con);
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            OppaccountDesc = dr["accdesc"].ToString().Trim();
        }
        dr.Close();
        */

        if (txtServiceTax.Text == "")
        {
            txtServiceTax.Text = "0.00";
        }
        if (txtEduCess.Text == "")
        {
            txtEduCess.Text = "0.00";
        }
        if (txtHEduCess.Text == "")
        {
            txtHEduCess.Text = "0.00";
        }
        if (txtTDSRate.Text == "")
        {
            txtTDSRate.Text = "0.00";
        }
        if (txtTDSAmt.Text == "")
        {
            txtTDSAmt.Text = "0.00";
        }

        SqlTransaction trans;
        trans = con.BeginTransaction();
        try
        {
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

            if (RAD_Customer.Checked)
                PBOV_typ = "P";
            else if (RAD_Vendor.Checked)
                PBOV_typ = "V";
            else if (RAD_Emp.Checked)
                PBOV_typ = "E";
            else if (RAD_Driver.Checked)
                PBOV_typ = "D";
            else if (RAD_Vehicle.Checked)
                PBOV_typ = "L";

            string sql_Acctrans = "", Acccode = "",Accdesc = "",Debit = "", Credit = "", Narration = "", Description = "";
            string Xml_Acccode_Details = "<root>";

            foreach (GridViewRow gridrow in grvcontrols.Rows)
            {
                Acccode = ((TextBox)gridrow.FindControl("txtAccCode")).Text.ToString();
                Debit = ((TextBox)gridrow.FindControl("txtAmt")).Text.ToString();

                Credit = "0.00";
                Narration = ((TextBox)gridrow.FindControl("txtNarration")).Text.ToString().Replace("'", "''");

                if (Acccode != "")
                {
                    sql_Acccode = "select Acccode,Accdesc from webx_acctinfo WITH(NOLOCK) where Company_Acccode='" + Acccode + "'";
                    cmd = new SqlCommand(sql_Acccode, con, trans);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Acccode = dr["Acccode"].ToString().Trim();
                        Accdesc = dr["Accdesc"].ToString().Trim();
                        Accdesc = Accdesc.Replace("<", "&lt;");
                        Accdesc = Accdesc.Replace(">", "&gt;");
                        Accdesc = Accdesc.Replace("\"", "&quot;");
                        Accdesc = Accdesc.Replace("'", "&apos;");
                    }
                    dr.Close();
                    
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

            string svrcheck = "N", tdscheck = "N";

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

            string Xml_Other_Details = "<root><Other>";
            Xml_Other_Details = Xml_Other_Details + "<Finyear>" + Session["FinYear"].ToString().Trim() + "</Finyear>";
            Xml_Other_Details = Xml_Other_Details + "<Transdate>" + VoucherDate.Trim() + "</Transdate>";
            Xml_Other_Details = Xml_Other_Details + "<Transtype>" + Transtype.Trim() + "</Transtype>";
            Xml_Other_Details = Xml_Other_Details + "<Voucherno>" + Voucherno.Trim() + "</Voucherno>";
            Xml_Other_Details = Xml_Other_Details + "<Brcd>" + HdnBrcd.Value.Trim() + "</Brcd>";
            Xml_Other_Details = Xml_Other_Details + "<Entryby>" + txtPreparedBy.Text.Trim() + "</Entryby>";
            Xml_Other_Details = Xml_Other_Details + "<Opertitle>" + "MANUAL DEBIT VOUCHER" + "</Opertitle>";
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
            Xml_Other_Details = Xml_Other_Details + "<pbov_typ>" + PBOV_typ.Trim() + "</pbov_typ>";
            Xml_Other_Details = Xml_Other_Details + "<Svrtax_yn>" + svrcheck.Trim() + "</Svrtax_yn>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_yn>" + tdscheck.Trim() + "</Tds_yn>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_Acccode>" + Tdssection.SelectedValue.ToString().Trim() + "</Tds_Acccode>";
            Xml_Other_Details = Xml_Other_Details + "<Tds_rate>" + txtTDSRate.Text.Trim() + "</Tds_rate>";
            Xml_Other_Details = Xml_Other_Details + "<bankAcccode>" + ddrBankaccode.SelectedValue.ToString().Trim() + "</bankAcccode>";
            Xml_Other_Details = Xml_Other_Details + "<recbanknm></recbanknm>";
            Xml_Other_Details = Xml_Other_Details + "<Onaccount_YN></Onaccount_YN>";
            Xml_Other_Details = Xml_Other_Details + "<Deposited></Deposited>";
            Xml_Other_Details = Xml_Other_Details + "<Paymode>" + ddlPayMode.SelectedValue.ToString().Trim() + "</Paymode>";
            Xml_Other_Details = Xml_Other_Details + "<SvrTaxRate>" + HdnServiceTaxRate.Value.Trim() + "</SvrTaxRate>";
            Xml_Other_Details = Xml_Other_Details + "<EduCessRate>" + HdnEduCessRate.Value.Trim() + "</EduCessRate>";
            Xml_Other_Details = Xml_Other_Details + "<HEduCessRate>" + HdnHEduCessRate.Value.Trim() + "</HEduCessRate>";
            Xml_Other_Details = Xml_Other_Details + "<SBCess>" + txtSBCess.Text.Trim() + "</SBCess>";
            Xml_Other_Details = Xml_Other_Details + "<SBCessRate>" + HdnSBCRate.Value.Trim() + "</SBCessRate>";
            Xml_Other_Details = Xml_Other_Details + "<KKCess>" + txtKKCess.Text.Trim() + "</KKCess>";
            Xml_Other_Details = Xml_Other_Details + "<KKCessRate>" + HdnKKCRate.Value.Trim() + "</KKCessRate>";
            Xml_Other_Details = Xml_Other_Details + "</Other></root>";

            Xml_Acccode_Details = RemoveSpecialCharactorsForXML(Xml_Acccode_Details);
            Xml_Other_Details = RemoveSpecialCharactorsForXML(Xml_Other_Details);
            
            string sql = "usp_Update_ManualAccountDetails_Ver2";
            cmd = new SqlCommand(sql, con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Xml_Acccode_Details", SqlDbType.Text).Value = Xml_Acccode_Details.Replace("&", "&amp;").Trim();
            cmd.Parameters.Add("@Xml_Other_Details", SqlDbType.Text).Value = Xml_Other_Details.Replace("&", "&amp;").Trim();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();

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
                throw new Exception("Error : Debit Amount Is Not Equal To Payment Amount");
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
            //Response.End();
        }
        con.Close();
        Response.Redirect("./voucher_Done.aspx?VoucherType=Debit&VoucherNo=" + Voucherno + "&Defaultdate=" + txtVoucherDate.Text, false);
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
