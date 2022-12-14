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
using Microsoft.ApplicationBlocks.Data;

public partial class GUI_Finance_Journal_Voucher_JournalVoucher : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    static DataTable dt = new DataTable("table1");
    MyFunctions fn = new MyFunctions();
    DateFunction DAccess = new DateFunction();
    public string Financial_Year = "", fin_year = "", opertitle = "MANUAL JOURNAL VOUCHER", Defaultdate = "";
    string voucherno = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        con.Open();

        Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        fin_year = Session["FinYear"].ToString();
        double fin_year_next = Convert.ToDouble(Financial_Year) + 1;
        fin_year = Financial_Year + "_" + fin_year_next.ToString().PadLeft(2, '0');
        Defaultdate = Request.QueryString["Defaultdate"];

        string table_name = "webx_acctrans_" + fin_year;

        voucherno = Request.QueryString["voucherno"].ToString().Replace("'", "");
        
        if (!IsPostBack)
        {
            dt = new DataTable("table1");

            hdnroundoff.Value = "N";

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

            /*
            string sql = "select oppacccode=b.company_acccode from " + table_name + " a,webx_acctinfo b with(nolock) ";
            sql += " where a.voucherno='" + voucherno + "' and a.oppacccode=b.acccode ";
            sql += " group by b.company_acccode having count(*) > 1 ";
            */

            string sql = "exec FindOppCode '" + voucherno + "','" + table_name + "'";
            HdnOppcode.Value = SqlHelper.ExecuteScalar(SessionUtilities.ConnectionString, CommandType.Text, sql).ToString();

            sql = "select company_acccode,accdesc,debit,credit,Narration from " + table_name + " a,webx_acctinfo b With(NOLOCK) where voucherno='" + voucherno + "'";
            sql += " and a.acccode in (select acccode from webx_acctinfo where  a.acccode = b.acccode) and voucher_cancel='N' order by srno";

            dt = SqlHelper.ExecuteDataset(SessionUtilities.ConnectionString, CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                grvcontrols.DataSource = dt;
                grvcontrols.DataBind();
            }
            
            string Payment_account_Cash = "", Payment_account_Bank = "", PBOV_Type = "", PBOV_Code = "", PBOV_Name = "", CASH_AMT = "", BANK_AMT = "";

            sql = "exec usp_voucher_detail_Ver1 '" + voucherno + "','" + table_name + "'";
            DataTable dt_vcdtl = SqlHelper.ExecuteDataset(SessionUtilities.ConnectionString, CommandType.Text, sql).Tables[0];

            lblvoucherno.Text = voucherno;
            txtVoucherDate.Text = dt_vcdtl.Rows[0]["Transdate"].ToString();
            txtManualNo.Text = dt_vcdtl.Rows[0]["Manual_Voucherno"].ToString();
            HdnBrcd.Value = dt_vcdtl.Rows[0]["brcd"].ToString();
            HdnEmpcd.Value = dt_vcdtl.Rows[0]["entryby"].ToString();
            lblpreparedfor.Text = dt_vcdtl.Rows[0]["brcd"].ToString() + " : " + fn.GetLocation(dt_vcdtl.Rows[0]["brcd"].ToString());
            lblpreparedby.Text = dt_vcdtl.Rows[0]["entryby"].ToString() + " : " + fn.GetEmpName(dt_vcdtl.Rows[0]["entryby"].ToString());
            txtPreparedFor.Text = dt_vcdtl.Rows[0]["preparefor"].ToString();
            txtRefNo.Text = dt_vcdtl.Rows[0]["Refno"].ToString();
            txtNarration.Text = dt_vcdtl.Rows[0]["Narration"].ToString();
            PBOV_Type = dt_vcdtl.Rows[0]["PBOV_Type"].ToString();
            PBOV_Code = dt_vcdtl.Rows[0]["pbov_code"].ToString();
            PBOV_Name = dt_vcdtl.Rows[0]["pbov_NAME"].ToString(); 
            
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
        }

        txtManualNo.Attributes.Add("onblur", "Manualbillblur('" + txtManualNo.ClientID.ToString() + "')");
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
    public void Show_Pbov_list_Display()
    {
        string sql_display_pbov = "";
        if (RAD_Customer.Checked == true)
        {
            sql_display_pbov = "select custcd as code , custnm+':'+custcd  as name from webx_custhdr WITH(NOLOCK) where CUSTCD is not null and    PATINDEx ('%" + HdnBrcd.Value + "%',custloc)>0 order by CUSTNM ";
        }
        else if (RAD_Vendor.Checked == true)
        {
            sql_display_pbov = "select vendorCode as code , vendorname+':'+vendorCode as name from webx_VENDOR_HDR WITH(NOLOCK) where Active='Y' and vendorcode in ( select vendorcode from webx_VENDOR_det WITH(NOLOCK)  where vendorCode is not null and    PATINDEx ('%" + HdnBrcd.Value + "%',vendorbrcd)>0 ) order by vendorname";
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
        
        SqlCommand cmd_pbov = new SqlCommand(sql_display_pbov, con);
        SqlDataReader dr_pbov;
        dr_pbov = cmd_pbov.ExecuteReader();
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
    protected void grvcontrols_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {   
            ((TextBox)e.Row.FindControl("txtAccCode")).Attributes.Add("onblur", "AccBlur('" + ((TextBox)e.Row.FindControl("txtAccCode")).ClientID.ToString() + "')");
            ((Button)e.Row.FindControl("btnAccCode")).Attributes.Add("onclick", "popuplist('AccCode','" + ((TextBox)e.Row.FindControl("txtAccCode")).ClientID.ToString() + "','none')");

            TextBox txtAccCode = ((TextBox)e.Row.FindControl("txtAccCode"));
            HtmlInputRadioButton rad_Particular = ((HtmlInputRadioButton)e.Row.FindControl("rad_Particular"));
            if (txtAccCode.Text.CompareTo(HdnOppcode.Value) == 0)
                rad_Particular.Checked = true;
            else
                rad_Particular.Checked = false;

			hdnTotDebitAmt.Value ="0.00";
            hdnTotCreditAmt.Value = "0.00";

            foreach (GridViewRow gridrow in grvcontrols.Rows)
            {
                string txtCreditAmt = ((TextBox)gridrow.FindControl("txtCreditAmt")).Text.ToString();
                string txtDebitAmt = ((TextBox)gridrow.FindControl("txtDebitAmt")).Text.ToString();
               
                if (txtCreditAmt == "")
                    txtCreditAmt = "0.00";
                if (txtDebitAmt == "")
                    txtDebitAmt = "0.00";

                hdnTotDebitAmt.Value = Convert.ToString(Convert.ToDouble(hdnTotDebitAmt.Value) + Convert.ToDouble(txtDebitAmt));
                hdnTotCreditAmt.Value = Convert.ToString(Convert.ToDouble(hdnTotCreditAmt.Value) + Convert.ToDouble(txtCreditAmt));
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtTotDebitAmt = ((TextBox)e.Row.FindControl("txtTotDebitAmt"));
            TextBox txtTotCreditAmt = ((TextBox)e.Row.FindControl("txtTotCreditAmt"));

            txtTotDebitAmt.Text = hdnTotDebitAmt.Value;
            txtTotCreditAmt.Text = hdnTotCreditAmt.Value;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Year = Session["FinYear"].ToString();
        string Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        string Voucherno = lblvoucherno.Text;
        string[] VDate = txtVoucherDate.Text.Split('/');
        string EntryBy = Session["empcd"].ToString(), Transtype = "Journal", OppaccountDesc = "", OppaccountCode = "", OppaccountDesc_mode = "", OppaccountCode_mode = "";
        string sql_Acccode = "";
        string VoucherDate = Convert.ToDateTime(VDate[1] + "/" + VDate[0] + "/" + VDate[2]).ToString("yyyy/MM/dd");

        string PBOV_code = "", PBOV_Name = "", PBOV_TYP = "";
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
            PBOV_TYP = "P";
        else if (RAD_Vendor.Checked)
            PBOV_TYP = "V";
        else if (RAD_Emp.Checked)
            PBOV_TYP = "E";
        else if (RAD_Driver.Checked)
            PBOV_TYP = "D";
        else if (RAD_Vehicle.Checked)
            PBOV_TYP = "L";

        string sql_Acctrans = "", Acccode = "", Debit = "", Credit = "", Narration = "", Description = "";
        string Narration1 = "";

        foreach (GridViewRow gridrow in grvcontrols.Rows)
        {
            Acccode = ((TextBox)gridrow.FindControl("txtAccCode")).Text.ToString();
            Description = ((TextBox)gridrow.FindControl("txtDescription")).Text.ToString();
            Narration = ((TextBox)gridrow.FindControl("txtNarration")).Text.ToString();
            
            if (Acccode != "")
            {
                if (((HtmlInputRadioButton)gridrow.FindControl("rad_Particular")).Checked)
                {
                    OppaccountCode = Acccode;
                    OppaccountDesc = Description;
                }
            }
        }

        SqlTransaction trans;
        trans = con.BeginTransaction();
        try
        {
            string Xml_Acccode_Details = "<root>";
            foreach (GridViewRow gridrow in grvcontrols.Rows)
            {
                Acccode = ((TextBox)gridrow.FindControl("txtAccCode")).Text.ToString();
                Debit = ((TextBox)gridrow.FindControl("txtDebitAmt")).Text.ToString();
                Credit = ((TextBox)gridrow.FindControl("txtCreditAmt")).Text.ToString();
                Narration = ((TextBox)gridrow.FindControl("txtNarration")).Text.ToString();
                Description = ((TextBox)gridrow.FindControl("txtDescription")).Text.ToString();

                if (Acccode != "")
                {
                    sql_Acccode = "select Acccode,accdesc from webx_acctinfo WITH(NOLOCK) where Company_Acccode='" + Acccode + "'";
                    cmd = new SqlCommand(sql_Acccode, con, trans);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    string Accdesc = "";
                    while (dr.Read())
                    {
                        Acccode = dr["Acccode"].ToString().Trim();
                        Accdesc = dr["accdesc"].ToString().Trim();
                    }
                    dr.Close();

                    Xml_Acccode_Details = Xml_Acccode_Details + "<Acccode>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Acccode>" + Acccode.Trim() + "</Acccode>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Accdesc>" + Accdesc.Trim() + "</Accdesc>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Debit>" + Debit.Trim() + "</Debit>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Credit>" + Credit.Trim() + "</Credit>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<Narration>" + Narration.Trim() + "</Narration>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<PartyCode></PartyCode>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "<PartyName></PartyName>";
                    Xml_Acccode_Details = Xml_Acccode_Details + "</Acccode>";
                }
            }

            Xml_Acccode_Details = Xml_Acccode_Details + "</root>";

            string Xml_Other_Details = "<root><Other>";
            Xml_Other_Details = Xml_Other_Details + "<Finyear>" + Year.Trim() + "</Finyear>";
            Xml_Other_Details = Xml_Other_Details + "<Transdate>" + fn.Mydate1(txtVoucherDate.Text.Trim()).Trim() + "</Transdate>";
            Xml_Other_Details = Xml_Other_Details + "<Transtype>Journal</Transtype>";
            Xml_Other_Details = Xml_Other_Details + "<Voucherno>" + Voucherno.Trim() + "</Voucherno>";
            Xml_Other_Details = Xml_Other_Details + "<Brcd>" + HdnBrcd.Value.Trim() + "</Brcd>";
            Xml_Other_Details = Xml_Other_Details + "<Entryby>" + Session["empcd"].ToString().Trim() + "</Entryby>";
            Xml_Other_Details = Xml_Other_Details + "<Opertitle>MANUAL JOURNAL VOUCHER</Opertitle>";
            Xml_Other_Details = Xml_Other_Details + "<ManualNo>" + txtManualNo.Text.Trim() + "</ManualNo>";
            Xml_Other_Details = Xml_Other_Details + "<preparefor>" + txtPreparedFor.Text.Trim() + "</preparefor>";
            Xml_Other_Details = Xml_Other_Details + "<Refno>" + txtRefNo.Text.Trim() + "</Refno>";
            Xml_Other_Details = Xml_Other_Details + "<Narration>" + txtNarration.Text.Trim() + "</Narration>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_code>" + PBOV_code.Trim() + "</pbov_code>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_name>" + PBOV_Name.Trim() + "</pbov_name>";
            Xml_Other_Details = Xml_Other_Details + "<pbov_typ>" + PBOV_TYP.Trim() + "</pbov_typ>";
            Xml_Other_Details = Xml_Other_Details + "<OppAccount>" + OppaccountCode.Trim() + "</OppAccount>";
            Xml_Other_Details = Xml_Other_Details + "<Jv_Type>Normal JV</Jv_Type>";
            Xml_Other_Details = Xml_Other_Details + "</Other></root>";

            Xml_Acccode_Details = RemoveSpecialCharactorsForXML(Xml_Acccode_Details);
            Xml_Other_Details = RemoveSpecialCharactorsForXML(Xml_Other_Details);

            string sql = "usp_Update_ManualJVAccountDetails_Ver2";
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
            
            LedgerDiff = Convert.ToDouble(SqlHelper.ExecuteScalar(trans,CommandType.Text,sql_Acctrans));
            
            if (LedgerDiff != 0)
            {
                throw new Exception("Error : Debit Amount Not Equal To Credit Amount");
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
            msg = msg.Replace('\r', ' ');
            trans.Rollback();
            Response.Redirect("Message.aspx?" + msg);
            Response.End();
        }
        con.Close();
        Response.Redirect("./voucher_Done.aspx?VoucherType=Journal&VoucherNo=" + Voucherno + "&Defaultdate=" + txtVoucherDate.Text, false);         
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
