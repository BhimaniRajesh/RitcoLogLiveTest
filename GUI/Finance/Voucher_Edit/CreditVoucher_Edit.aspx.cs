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
using WebX.Controllers;
using WebX.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class GUI_Finance_Voucher_Edit_CreditVoucher_Edit : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    static DataTable dt = new DataTable("table1");
    MyFunctions fn = new MyFunctions();
    string newdate = "";
    string Defaultdate = "", PBOV_TYP;
    string svrtax, tds, SBC, KKC, tdsrate = "", voucherno = "", brcd = "";
    string Financial_Year = "", fin_year = "", opertitle = "MANUAL CREDIT VOUCHER",chequeno,chequedt;
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

        if (!IsPostBack)
        {
            FillBusDiv();

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
            

            dt = new DataTable("table1");

            //string sql = "select  company_acccode,accdesc,credit,Narration from " + table_name + " a,webx_acctinfo b With(NOLOCK) where voucherno='" + voucherno + "'";
            //sql += " and a.acccode in (select acccode from webx_acctinfo where accdesc not like '%tds%' and acccode not in ('CLO0023','LCS0001','LCS0002','ACA0002')) and acccategory not in ('CASH','BANK ') and a.acccode = b.acccode  and isnull(voucher_cancel,'N')='N' order by srno";
            string sql = "select  b.company_acccode,b.accdesc,a.credit,a.Narration from WEBX_ACCcode_details a,webx_acctinfo b With(NOLOCK) where ISNULL(a.voucher_cancel,'N')='N' and a.acccode=b.acccode and a.voucherno='" + voucherno + "'";
            da = new SqlDataAdapter(sql, con);
            da.Fill(dt);

            grvcontrols.DataSource = dt;
            grvcontrols.DataBind();

            //sql = "select  sum(credit) from " + table_name + " With(NOLOCK) where voucherno='" + voucherno + "' ";
            //sql += " and acccode in (select acccode from webx_acctinfo where ";
            //sql += " acccode not in ('CLO0023','LCS0001','LCS0002') and acccategory not in ('CASH','BANK','TDS'))";
            //sql += " and isnull(voucher_cancel,'N')='N'";
            sql = "select  sum(credit) from WEBX_ACCcode_details With(NOLOCK) where voucherno='" + voucherno + "' and ISNULL(voucher_cancel,'N')='N'";

            hdntotamt.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString,CommandType.Text,sql).ToString();

            sql = "select acccode,accdesc from webx_acctinfo where accdesc like '%tds%'";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, sql);
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
                CASH_AMT = dr["CASH_AMT"].ToString();
                BANK_AMT = dr["BANK_AMT"].ToString();
                hdncashbankvalue.Value = dr["transmode"].ToString();
                if (ddlPayMode.SelectedValue != "Cash")
                {
                    txtChqNo.Text = dr["chqno"].ToString();
                    txtChqDate.Text = dr["ChqDate"].ToString();
                    HdnChqNo.Value = dr["chqno"].ToString();
                    HdnChqDate.Value = dr["ChqDate"].ToString();
                    HdnBankaccode.Value = dr["Pay_BANK"].ToString();
                    if (dr["OnAccount"].ToString().CompareTo("Y") == 0)
                    {
                        hdnonaccount.Value = "Y";
                        if (hdnonaccount.Value == "Y")
                        {
                            string ErrorMsg = "On Account Cheque Is Used In This Voucher So You Can't Edit This Credit Voucher";
                            Response.Redirect("~/GUI/ErrorPage.aspx?PageHead=" + "Credit Voucher Error" + "&ErrorMsg=" + ErrorMsg);
                        }
                    }
                    else
                        hdnonaccount.Value = "N";
                    if (dr["Depoflag"].ToString().CompareTo("Y") == 0)
                    {
                        hdndepoflag.Value = "Y";
                        if (hdndepoflag.Value == "Y")
                        {
                            sql = "SELECT COUNT(DISTINCT VOUCHERNO) FROM " + table_name + " WHERE CHQNO='" + HdnChqNo.Value + "' AND CHQDATE=CONVERT(DATETIME,'" + HdnChqDate.Value + "',103) AND ISNULL(VOUCHER_CANCEL,'N')='N' AND (COMMENT IS NULL OR COMMENT <> 'VOUCHER WAS EDITED') AND OPERTITLE='CHEQUE TRANSACTION'";
                            count = Convert.ToDouble(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, sql));
                            if (count >= 1)
                            {
                                string ErrorMsg = "Used Cheque In This Credit Voucher Is Deposited In Bank Through Cheque Deposit Voucher So You Can't Edit This Credit Voucher";
                                Response.Redirect("~/GUI/ErrorPage.aspx?PageHead=" + "Credit Voucher Edit Error" + "&ErrorMsg=" + ErrorMsg);
                            }
                        }
                    }
                    else
                        hdndepoflag.Value = "N";
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
            //string Sql = "SELECT ITEMS FROM SPLIT('" + lblvoucherno.Text + "','/') WHERE ITEMS IN(SELECT LOCCODE FROM WEBX_LOCATION WHERE ACTIVEFLAG='Y')";
            //brcd = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, Sql).ToString();

            brcd = HdnBrcd.Value;

            /*
            string[] arrbrcd;
            string Sql = "";
            arrbrcd = lblvoucherno.Text.Split('/');
            for (int i = 0; i < arrbrcd.Length; i++)
            {
                Sql = "select loccode from webx_location where loccode='" + arrbrcd[i].ToString() + "'";
                dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, Sql);
                while (dr.Read())
                {
                    brcd = dr["loccode"].ToString();
                    HdnBrcd.Value = brcd;
                }
                dr.Close();
            }
            */

            if (PBOV_Type == "C" || PBOV_Type == "P")
            {
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
                RAD_Customer.Checked = true;
                RAD_Vendor.Checked = false;
                RAD_Emp.Checked = false;
                Show_Pbov_list_Display();
                if (PBOV_Code.ToString() == "8888")
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
                RAD_Emp.Checked = true;
                RAD_Driver.Checked = false;
                RAD_Vehicle.Checked = false;
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
                RAD_Customer.Checked = true;
                Show_Pbov_list_Display();
                txtCode.Text = PBOV_Name;
            }

            decimal servicetax = 0, tdsvalue = 0;
            decimal SBCvalue = 0;
            decimal KKCvalue = 0;

            if(svrtax != "")
                servicetax = Convert.ToDecimal(svrtax);
            
            if (tds != "")
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
        }
        if (IsPostBack)
        {
            if (Svctax_yn.Checked == true)
            {
                DateTime curdt;
                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                curdt = Convert.ToDateTime(txtVoucherDate.Text, enGB);
                HdnServiceTaxRate.Value = Convert.ToString(TaxRateController.GetServiceTaxRate(curdt));
                HdnEduCessRate.Value = Convert.ToString(TaxRateController.GetEduCessRate(curdt));
                HdnHEduCessRate.Value = Convert.ToString(TaxRateController.GetHEduCessRate(curdt));
                HdnSBCRate.Value = Convert.ToString(TaxRateController.GetSwachhBharatCessRate(curdt));
                HdnKKCRate.Value = Convert.ToString(TaxRateController.GetKrushiKalyanCessRate(curdt));

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

                    txtSBCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnSBCRate.Value)) / 100).ToString("F0");
                    txtSBCess.Text = Convert.ToDecimal(txtSBCess.Text).ToString("F2");

                    txtKKCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnKKCRate.Value)) / 100).ToString("F0");
                    txtKKCess.Text = Convert.ToDecimal(txtKKCess.Text).ToString("F2");

                    txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text) + Convert.ToDecimal(txtServiceTax.Text) + Convert.ToDecimal(txtEduCess.Text) + Convert.ToDecimal(txtHEduCess.Text) + Convert.ToDecimal(txtSBCess.Text) + Convert.ToDecimal(txtKKCess.Text)).ToString("F0");
                    txtAmtApplL.Text = Convert.ToDecimal(txtAmtApplL.Text).ToString("F2");
                }
                else
                {
                    txtServiceTax.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnServiceTaxRate.Value)) / 100).ToString("F2");
                    txtEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnEduCessRate.Value)) / 100).ToString("F2");
                    txtHEduCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtServiceTax.Text) * Convert.ToDecimal(HdnHEduCessRate.Value)) / 100).ToString("F2");

                    txtSBCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnSBCRate.Value)) / 100).ToString("F2");
                    txtKKCess.Text = Convert.ToDecimal((Convert.ToDecimal(txtAmtAppl.Text) * Convert.ToDecimal(HdnKKCRate.Value)) / 100).ToString("F2");

                    txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text) + Convert.ToDecimal(txtServiceTax.Text) + Convert.ToDecimal(txtEduCess.Text) + Convert.ToDecimal(txtHEduCess.Text) + Convert.ToDecimal(txtSBCess.Text) + Convert.ToDecimal(txtKKCess.Text)).ToString("F2");
                }
            }
            if (Svctax_yn.Checked == false)
            {
                if (txtAmtAppl.Text == "")
                    txtAmtAppl.Text = "0.00";
                txtServiceTax.Text = "0.00";
                txtEduCess.Text = "0.00";
                txtHEduCess.Text = "0.00";
                txtSBCess.Text = "0.00";
                txtKKCess.Text = "0.00";
                txtAmtApplL.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmtAppl.Text)).ToString("F2");
            }
        }

        //con.Close();
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
        dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
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
    public void CASH_BANK_ACCOUNT()
    {   
        if (ddlPayMode.SelectedItem.Text == "Cash")
        {
            txtCashAmt.Text = txtNetPay.Text;
            txtCashAmt.Enabled = false;
            ddrCashcode.Enabled = true;
            txtChqAmt.Enabled = false;
            txtChqNo.Enabled = false;
            rdDiposited.Enabled = false;
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

            txtRecBank.Text = "";

            string strSql = "select top 1 isnull(banknm,'') as banknm from webx_chq_det where chqno='" + txtChqNo.Text.Trim() + "' and chqdt = convert(datetime,'" + txtChqDate.Text.Trim() + "',103)";
            txtRecBank.Text = Convert.ToString(SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, strSql));
            
            ddrBankaccode.Items.Clear();

            strSql = "select top 1 '' as asscodeval,'--Select--' as accdesc Union select acccode  as asscodeval,accdesc from webx_acctinfo where  ( ((bkloccode like 'All' or PATINDEx ('%" + HdnBrcd.Value + "%',bkloccode)>0) AND acccategory='BANK')) order by accdesc asc";
            dr = SqlHelper.ExecuteReader(SessionUtilities.ConnectionString, CommandType.Text, strSql);
            while (dr.Read())
            {
                ddrBankaccode.Items.Add(new ListItem(dr.GetValue(1).ToString(), dr.GetValue(0).ToString()));
            }
            dr.Close();

            if (hdndepoflag.Value == "Y")
            {
                rdDiposited.SelectedValue = "Y";
                ddrBankaccode.Enabled = true;
                hdndepoflag.Value = "";
            }
            if (hdndepoflag.Value == "N")
            {
                rdDiposited.SelectedValue = "N";
                ddrBankaccode.Enabled = false;
                hdndepoflag.Value = "";
            }
            if (hdnonaccount.Value == "Y")
            {
                Onaccount.Checked = true;
                //hdnonaccount.Value = "";
            }
            if (hdnonaccount.Value == "N")
            {
                Onaccount.Checked = false;
                hdnonaccount.Value = "";
            }

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

            if (hdndepoflag.Value == "Y")
            {
                rdDiposited.SelectedValue = "Y";
                ddrBankaccode.Enabled = true;
                hdndepoflag.Value = "";
            }
            if (hdndepoflag.Value == "N")
            {
                rdDiposited.SelectedValue = "N";
                ddrBankaccode.Enabled = false;
                hdndepoflag.Value = "";
            }
            
            ddrCashcode.CssClass = "blackfnt";
            ddrBankaccode.CssClass = "blackfnt";
            ddrBankaccode.SelectedValue = HdnBankaccode.Value;
        }
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        CASH_BANK_ACCOUNT();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Year = Session["FinYear"].ToString();
        string opertitle = "MANUAL CREDIT VOUCHER";
        string Financial_Year = Session["FinYear"].ToString();
        string Voucherno = lblvoucherno.Text.ToString();
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
            Chqno = txtChqNo.Text;
            CDate = txtChqDate.Text.Split('/');
            ChqDate = Convert.ToDateTime(CDate[1] + "/" + CDate[0] + "/" + CDate[2]).ToString("yyyy/MM/dd");
            string chque_yn_chk = "N";

            if (Chqno != HdnChqNo.Value && ChqDate != HdnChqDate.Value)
            {
                string sql_chk = "select chqamt,adjustamt,banknm from webx_chq_det where chqno='" + Chqno + "' and chqdt='" + ChqDate + "'";
                SqlCommand cmd_chk = new SqlCommand(sql_chk, con);
                SqlDataReader dr_chk = null;

                dr_chk = cmd_chk.ExecuteReader();
                
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
                    Response.End();
                }
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

            string sql_Acctrans = "", Acccode = "", Accdesc = "", Debit = "", Credit = "", Narration = "", Description = "";
            string Xml_Acccode_Details = "<root>";

            foreach (GridViewRow gridrow in grvcontrols.Rows)
            {
                Acccode = ((TextBox)gridrow.FindControl("txtAccCode")).Text.ToString();
                Credit = ((TextBox)gridrow.FindControl("txtAmt")).Text.ToString();
                Debit = "0.00";
                Narration = ((TextBox)gridrow.FindControl("txtNarration")).Text.ToString().Replace("'", "''");
                Description = ((TextBox)gridrow.FindControl("txtDescription")).Text.ToString().Replace("'", "''");
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
            Xml_Other_Details = Xml_Other_Details + "<Brcd>" + HdnBrcd.Value.Trim() + "</Brcd>";
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

            // END

            double LedgerDiff = 0;
            sql_Acctrans = "select sum(debit) - sum(credit) as Differance from webx_acctrans_" + fin_year + " where isnull(voucher_cancel,'N')='N' and voucherno='" + Voucherno + "'";
            cmd = new SqlCommand(sql_Acctrans, con, trans);
            SqlDataReader dr_count = cmd.ExecuteReader();
            while (dr_count.Read())
            {
                LedgerDiff = Convert.ToDouble(dr_count["Differance"].ToString().Trim());
            }
            dr_count.Close();

            if (LedgerDiff != 0)
            {
                throw new Exception("Debit Amount Should Be Equal To Payment Amount");
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
            Response.Redirect("Message.aspx?" + e1.Message);
            Response.End();
        }
        con.Close();
        Response.Redirect("./voucher_Done.aspx?VoucherType=Credit&VoucherNo=" + Voucherno + "&Defaultdate=" + txtVoucherDate.Text, false);
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
