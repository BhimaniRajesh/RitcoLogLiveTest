using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class GUI_Finance_Vendor_BA_payment_BA_Payment_ver1_Result_BA_BillEntry : System.Web.UI.Page
{
    public int intTotalRecords;
    SqlConnection conn;
    public static string dkt_call = "";
    MyFunctions fn = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
            conn.Open();

            string SQL_TDSACC = "select acccode,accdesc from vw_tds_payable ";
            tdsacccode.Items.Add(new ListItem("-- Select --", ""));
            SqlCommand cmd1 = new SqlCommand(SQL_TDSACC, conn);
            SqlDataReader dread1 = cmd1.ExecuteReader();
            {

                while (dread1.Read())
                {
                    tdsacccode.Items.Add(new ListItem(dread1.GetValue(1).ToString(), dread1.GetValue(0).ToString()));

                }

            }
            dread1.Close();

            transdate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            vendorbilldt.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            duedt.Text = System.DateTime.Today.AddDays(15).ToString("dd/MM/yyyy");
            conn.Close();

            BindGrid();
        }
        dkt_call = Session["DocketCalledAs"].ToString();
       
     

    }
    protected void pgChange(object sender, GridViewPageEventArgs e)
    {

        dgDocket.PageIndex = e.NewPageIndex;

         BindGrid();

    }
    public void BindGrid()
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();

       string brcd = Session["brcd"].ToString();
       string fromdt = Request.QueryString["fromdt"].ToString();
       string todt = Request.QueryString["todt"].ToString();
       string RPTTYP = Request.QueryString["RPTTYP"].ToString();
       string str_mode = Request.QueryString["str_mode"].ToString();
       string VendoCode = Request.QueryString["VendoCode"].ToString();
       string VendoName = Request.QueryString["VendoName"].ToString();

       lblVend.Text = VendoName;
       lblDockDt.Text = fromdt + " - " + todt;

       if (RPTTYP.ToUpper() == "BKG")
           lblTyp.Text = "Booking";
       else if (RPTTYP.ToUpper() == "DLY")
           lblTyp.Text = "Delivery";

       if (str_mode.ToUpper() == "1")
           lblTyp1.Text = "All - (W/o - Road)";
       else if (str_mode.ToUpper() == "2")
           lblTyp1.Text = "Road";


       string SQL_grid = "exec WebxNet_BApay_Show_Dockets '" + fromdt + "','" + todt + "','" + VendoCode + "','" + brcd + "','" + str_mode + "','" + RPTTYP  + "'";

       SqlCommand sqlcmd12 = new SqlCommand(SQL_grid, conn);
       SqlDataAdapter da = new SqlDataAdapter(sqlcmd12);

       DataSet ds = new DataSet();

       da.Fill(ds);
       dgDocket.DataSource = ds;
       intTotalRecords = ds.Tables[0].Rows.Count;
       dgDocket.DataBind();
       conn.Close();

       
    }

    protected void dgDocket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label dkt_colname = (Label)e.Row.FindControl("lblColname");
            /// Label lblColDockdt = (Label)e.Row.FindControl("lblColDockdt");

            dkt_colname.Text = dkt_call + " No.";
            ///lblColDockdt.Text = dkt_call + " Date";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldockno = (Label)e.Row.FindControl("lbldockno");
            Label lblBill_MRNo = (Label)e.Row.FindControl("lblBill_MRNo");
            Label lblBill_MRNoStatus = (Label)e.Row.FindControl("lblBill_MRNoStatus");
            
            string MDockets = lbldockno.Text.ToString();

            //SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
            //conn.Open();

            //String SQL_status = "exec usp_bapayment_validdkt '" + MDockets + "'";
            //SqlCommand sqlcmd21 = new SqlCommand(SQL_status, conn);
            //SqlDataReader dr21;
            //dr21 = sqlcmd21.ExecuteReader();

            //String status_doc = "", attech_doc = "", ded = "";
            //// double ded = 0;
            //while (dr21.Read())
            //{
            //    status_doc = dr21.GetValue(1).ToString();// dr21["OP_status"].ToString();
            //    attech_doc = dr21.GetValue(0).ToString();
            //    ded = dr21.GetValue(3).ToString();

            //    if (ded == "")
            //    {
            //        ded = "0";
            //    }
            //}
            //dr21.Close();


            if (lblBill_MRNoStatus.Text.ToString() == "Yes")
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[0].Enabled = false;

                for (int ri = 0; ri < 22; ri++)
                {
                    e.Row.Cells[ri].BackColor = System.Drawing.Color.LightGray;
                }
               
            }

            //lblBill_MRNo.Text = status_doc;

            //e.Row.Cells[9].Text = status_doc;
            //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[10].Text = attech_doc;
            //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[21].Text = ded;
            //e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Center;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();

        string brcd = Session["brcd"].ToString();
      
        string todt = Request.QueryString["todt"].ToString();
        string RPTTYP = Request.QueryString["RPTTYP"].ToString();
        string str_mode = Request.QueryString["str_mode"].ToString();
        string VendoCode = Request.QueryString["VendoCode"].ToString();
        string VendoName = Request.QueryString["VendoName"].ToString();


        string AcctTable = "webx_acctinfo";
        string dkt_call = Session["DocketCalledAs"].ToString();
        string Year = Session["FinYear"].ToString().Substring(2, 2);
        //string brcd = Session["brcd"].ToString();
        //string abc = Year.Substring(2, 2);
        string empcd = Session["empcd"].ToString();

        string CloseYear = Convert.ToString((Convert.ToInt16(Year) + 1));
        if (CloseYear.Length == 1)
        {
            CloseYear = "0" + CloseYear;
        }
        string YearString = Convert.ToString(Year) + "_" + Convert.ToString(CloseYear);
        string yearSuffix = YearString;
        string finyearstart = "01 APR " + Year;
        //Year = Year;
        string mAcctTransTable = "webx_acctrans_" + yearSuffix;
      //  string AcctOpenTable = "WEBX_acctopening_" + yearSuffix;


        ///////////////////////////Vendor PanCard/Service Tax No
        string VendorCodeName = "", str_PAN_NO = "", str_SERVTAXNO = "";
        string sqlstr = "select vendorcode+' : '+ vendorname,isnull(PAN_NO,'') as PAN_NO,isnull(SERVTAXNO,'') as SERVTAXNO from webx_vendor_hdr where vendorcode='" + VendoCode + "'";
        // Response.Write("<br> sqlstr : " + sqlstr);
        SqlCommand cmd11 = new SqlCommand(sqlstr, conn);
        SqlDataReader dread11 = cmd11.ExecuteReader();
        while (dread11.Read())
        {
            str_PAN_NO = Convert.ToString(dread11.GetValue(1).ToString());
            str_SERVTAXNO = Convert.ToString(dread11.GetValue(2).ToString());
            VendorCodeName = Convert.ToString(dread11.GetValue(0).ToString());
        }
        dread11.Close();

        ///////////////////////////For Next BA Bill Code

        string sql_billno = "Exec sp_get_next_be_code '" + brcd + "'";
        SqlCommand cmd = new SqlCommand(sql_billno, conn);
        string BillNo = (string)cmd.ExecuteScalar();

        double othChrg = Convert.ToDouble(oth_amt.Text);
        double DEDUCTION = Convert.ToDouble(oth_ded.Text);
        string transdate1 = transdate.Text.ToString();
        double servicetaxrate = Convert.ToDouble(svtax_rate.Text);
        double servicetax = Convert.ToDouble(svrcamt.Text);
        double othertax = Convert.ToDouble(cessamt.Text);
        double tdsrate = Convert.ToDouble(tds_rate.Text);
        double tds = Convert.ToDouble(tds_chrg.Text);
        string tdsacccode1 = tdsacccode.SelectedValue.ToString();
        string tdsgrpcode = tdsacccode1;
        string tdsgrpdesc = tdsacccode.SelectedItem.ToString();
        string tdsaccdesc = tdsacccode.SelectedItem.ToString();
        string tdsfor1 = tdsfor.SelectedValue.ToString();
        double TotalNetpayable1 = Convert.ToDouble(TotalNetpayable.Text);
        string PayMode = "";// cboModeOfTransaction.SelectedValue.ToString();
        double appramt = TotalNetpayable1;
        double TotalNetCommision = 0, Totalcommision = 0;
        Totalcommision = Convert.ToDouble(txtNetCommission.Text);
        TotalNetCommision = Totalcommision - DEDUCTION + othChrg;

        if (Totalcommision < 0 || Totalcommision == 0)
        {

            //reduredt
            Response.Redirect("ErrorPage.aspx");
        }

        SqlTransaction trans;
        string transtyp1 = "Journal";
        trans = conn.BeginTransaction();
        try
        {

            string[] transdate1_arr = transdate1.Split('/');

            string trdt = Convert.ToDateTime(transdate1_arr[1] + "/" + transdate1_arr[0] + "/" + transdate1_arr[2]).ToString("dd MMM yy");


            string billdt = trdt;
            string[] VENDORBILLDT_arr = vendorbilldt.Text.ToString().Split('/');
            string VENDORBILLDT = Convert.ToDateTime(VENDORBILLDT_arr[1] + "/" + VENDORBILLDT_arr[0] + "/" + VENDORBILLDT_arr[2]).ToString("dd MMM yy");

            double SVCTAX = Convert.ToDouble(svrcamt.Text);
            double cess_amt = Convert.ToDouble(cessamt.Text);
            string REMARK = txtremarks.Text.ToString();
            string PAYDT = "null";
            string VOUCHERNO = "null";
            double OTHERDED = Convert.ToDouble(oth_ded.Text);
            double SVCTAXDED = 0;
            double OTHERCHRG = 0;
            string VoucherNo = fn.NextVoucherno(brcd, Year);

            double NETAMT = Convert.ToDouble(TotalNetpayable.Text);
            string AcctOTHERDED = "", AcctSVCTAXDED = "";
            string betype = "BA Payment";
            double colamt = appramt;
            double Debit = appramt;
            double Credit = appramt;
            string acccode = "";

            string Bookcode = "BA Commision";
            double Currbalance = 0;

            string DocNo = "", Docsf = "", Deptcode = "", Oppaccount = "", Payto = "";

            string[] VendorCodeName_arr = VendorCodeName.Split(':');
            string PBOV_code = VendorCodeName_arr[0].ToString().Trim();
            string PBOV_Name = VendorCodeName_arr[1].ToString().Trim();
            string PBOV_typ = "V";


            string Voucher_Cancel = "N";
          //  string opertitle = "Manual Voucher";
            string transNo = "", PBOV_CODE = "", PBOV_NAME = "", PBOV_TYP = "", docNo = "";
            string cessaccdesc = "", cessacccode = "", serviceacccode = "", serviceaccdesc = "";
            string vehExpAcccode = "VOC0001";

            if (Session["Client"].ToString() == "RLL" || Session["Client"].ToString() == "RCPL" || Session["Client"].ToString() == "ASL")
            {
                if (RPTTYP.ToUpper() == "DLY")
                {
                    if (str_mode == "1")
                    {
                        vehExpAcccode = "PE105";
                    }
                    else
                    {
                        vehExpAcccode = "PE107";
                        if (Session["Client"].ToString() == "RCPL" || Session["Client"].ToString() == "ASL")
                        {
                            vehExpAcccode = "PE105";
                        }
                    }
                }
                else
                {
                    if (str_mode == "1")
                    {
                        vehExpAcccode = "PE104";
                    }
                    else
                    {
                        vehExpAcccode = "PE106";
                        if (Session["Client"].ToString() == "RCPL" || Session["Client"].ToString() == "ASL")
                        {
                            vehExpAcccode = "PE104";
                        }
                    }
                }
            }
            string vehExpAccdesc = fn.getAccountDesc(AcctTable, vehExpAcccode);


            double totdiscount = 0;
            string[] Duedt_arr = duedt.Text.ToString().Split('/');
            string Duedt = Convert.ToDateTime(Duedt_arr[1] + "/" + Duedt_arr[0] + "/" + Duedt_arr[2]).ToString("dd MMM yy");

            string Entryby = empcd;
            string entryfor = empcd;
           // double OTHERDED = Convert.ToDouble(oth_ded.Text);
            ////////////////////////Vendor Header Entry 

            string sql_billentry = "INSERT INTO WEBX_VENDORBILL_HDR " +
                  "(BILLNO, BILLSF, BRCD, BILLDT, VENDORCODE, VENDORNAME, VENDORBILLDT,VENDORBILLNO, PCAMT, ADVAMT, OTHAMT, SVCTAX, DUEDT,ENTRYBY, ENTRYDT, REMARK, PAYDT, VOUCHERNO, OTHERDED, TDSRATE, TDS,SVCTAXDED, OTHERCHRG, NETAMT,tdsfor,cessamt,tdsgrpcode,tdsgrpdesc,betype,tdsacccode,tdsaccdesc,discount,pendamt,servicetaxrate) " +
                  "VALUES     " +
                  "('" + BillNo + "','.','" + brcd + "','" + billdt + "','" + VendoCode + "','" + PBOV_Name + "','" + VENDORBILLDT + "','" + vendorbillno.Text.ToString() + "','" + Totalcommision + "','0','" + othChrg + "','" + SVCTAX + "','" + Duedt + "','" + Entryby + "',getdate(),'" + REMARK + "',getdate(),'" + VOUCHERNO + "','" + OTHERDED + "','" + tdsrate + "','" + tds + "','" + SVCTAXDED + "','" + OTHERCHRG + "','" + NETAMT + "','" + tdsfor1 + "','" + cess_amt + "','" + tdsgrpcode + "','" + tdsgrpdesc + "','" + betype + "','" + tdsacccode1 + "','" + tdsaccdesc + "','" + totdiscount + "','" + NETAMT + "','" + servicetaxrate + "')";
            // Response.Write("<br>sql_othertax2 : " + sql_billentry);
            SqlCommand sqlcmd4 = new SqlCommand(sql_billentry, conn);
            sqlcmd4.Transaction = trans;
            sqlcmd4.ExecuteNonQuery();

            //////////////////////Vendor Details Entry

            string docketlist = "";
            foreach (GridViewRow gridrow in dgDocket.Rows)
            {
                CheckBox chk = (CheckBox)gridrow.FindControl("chkOrderNo");

                Label lbldkt = (Label)gridrow.FindControl("dktval");
                Label lbldktno = (Label)gridrow.FindControl("lbldockno");               


                string dktsf = ".";// lbldktsf.Text.ToString();
                string dktno = lbldktno.Text.ToString();
                Label lblSlabrate = (Label)gridrow.FindControl("lblSlabrate");
                Label lblCommossion = (Label)gridrow.FindControl("lblCommossion");

                if (lblSlabrate.Text == "")
                {
                    lblSlabrate.Text = "0.00";
                }
                if (lblCommossion.Text == "")
                {
                    lblCommossion.Text = "0.00";
                }
                              
                double st_commission = Convert.ToDouble(lblCommossion.Text);
                double st_slabrate = Convert.ToDouble(lblSlabrate.Text);

                string docType = RPTTYP.ToUpper();
                if (chk.Checked == true)
                {
                    string sql_bapayment_docket = "insert into webx_bapayment_docket with (ROWLOCK) (Dockno,docksf,voucherno,transdate,Commission,slabrate,docType,BENO,Activeflag) values('" + dktno + "','" + dktsf + "','" + VoucherNo + "','" + trdt + "','" + st_commission + "','" + st_slabrate + "','" + docType + "','" + BillNo + "','Y')";
                    Response.Write("<br> sql_bapayment_docket : " + sql_bapayment_docket);
                    SqlCommand sqlcmd_docket = new SqlCommand(sql_bapayment_docket, conn);
                    sqlcmd_docket.Transaction = trans;
                    sqlcmd_docket.ExecuteNonQuery();

                    if (st_commission > 0 || st_slabrate > 0)
                    {
                        string sql_billentry_det = "INSERT INTO WEBX_VENDORBILL_DET " +
                            "(BILLNO, BILLSF, DOCNO, DOCSF, PCAMT, OTHAMT, SVCTAX, CESS,hedu_cess, ADVPAID,OTHDED, SVCTAXDED, voucherNo, paydt,netpay,tdsded,tds_rate,svctaxrate,discount)" +
                            "select '" + BillNo + "','.','" + dktno + "','" + dktsf + "','" + Totalcommision + "','" + othChrg + "',svctax,cess,hedu_cess,'0','0','" + SVCTAXDED + "',null,null,'" + st_commission + "','0','" + tdsrate + "','" + servicetaxrate + "','0' from webx_master_docket_charges where dockno='" + dktno + "'";
                        //"select '" + BillNo + "','.','" + dktno + "','" + dktsf + "','" + PCAMT + "','" + OTHAMT + "','" + st_svctax + "','" + st_cess + "','" + ADVPAID + "','" + OTHDED + "','" + SVCTAXDED + "',null,null,'" + st_commission + "','" + tdsded + "','" + tdsrate + "','" + servicetaxrate + "','" + discount + "' from webx_master_docket_charges where dockno='" + dktno + "'";
                        Response.Write("<br> sql_billentry_det : " + sql_billentry_det);
                        SqlCommand sqlcmd_sql_billentry_det = new SqlCommand(sql_billentry_det, conn);
                        sqlcmd_sql_billentry_det.Transaction = trans;
                        sqlcmd_sql_billentry_det.ExecuteNonQuery();
                    }
                    if (docketlist == "")
                    {
                        docketlist = lbldktno.Text.ToString();
                    }
                    else
                    {
                        docketlist = docketlist + "," + lbldktno.Text.ToString();
                    }

                }
            }


            ////////////////////////BA Accounting Process

            string expacccode = "LCL0002";
            string expaccount = fn.getAccountDesc(AcctTable, expacccode);

            string expaccdesc = expaccount;

            string voucher_status = "Approved";

            string v_approve_reject_by = "";
            //string v_approve_reject_branch = "";
            string v_closed_by = empcd;
            string v_closed_branch = brcd;
            string v_approve_reject_branch = brcd;
            string prepareByLoc = brcd;
            string oppacccode = "";
            string v_approve_reject_dt = "null";

           

            string panno = str_PAN_NO;
            string servicetaxNo = str_SERVTAXNO;

            cessacccode = "ALS0010";
            cessaccdesc = fn.getAccountDesc(AcctTable, cessacccode);
            serviceacccode = "ALS0009";
            serviceaccdesc = fn.getAccountDesc(AcctTable, serviceacccode);


           string Narration = "When Bill Entry for BA is generated : " + BillNo;
           string opertitle = "BA Bill Entry ";
            string TransNo = "1";
             Currbalance = 0;
            double Bookamt = 0;
             docNo = BillNo;
            if (TotalNetpayable1 > 0)
            {
                Bookamt = TotalNetpayable1 + DEDUCTION + othChrg;
            }


            if (TotalNetCommision > 0)
            {
                Debit = TotalNetCommision;
                Credit = 0;
                acccode = vehExpAcccode;
                Oppaccount = expaccdesc;
                oppacccode = expacccode;

                //       finyear,           Transdate,  Voucherno,           Acccode,           chqno,          chqdate,        Debit,          Credit,         Narration,          Transtype,          Currbalance,            docno,      brcd,            Deptcode,          Oppaccount,       Entryby,    Entrydt,Voucher_Cancel,autoentry,oppacccode,voucher_status,v_approve_reject_by,v_approve_reject_branch,v_closed_by,     v_closed_branch,v_approve_reject_dt,v_closed_dt,Payto,            entryfor,       prepareByLoc,           PBOV_code,          PBOV_Name,          PBOV_typ,           panno,          servicetaxNo
                string sql_Bookamt = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,Null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','" + panno + "','" + servicetaxNo + "','" + opertitle + "' ";
                Response.Write("<br> sql_colamt : " + sql_Bookamt);

                SqlCommand sqlcmd = new SqlCommand(sql_Bookamt, conn);
                sqlcmd.Transaction = trans;
                sqlcmd.ExecuteNonQuery();


                Debit = 0;
                Credit = TotalNetCommision;
                acccode = expacccode;
                Oppaccount = vehExpAccdesc;
                oppacccode = vehExpAcccode;

                string sql_Bookamt1 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','" + panno + "','" + servicetaxNo + "','" + opertitle + "' ";
                /// Response.Write("<br> sql_colamt : " + sql_Bookamt1);
                Response.Write("<br> sql_colamt : " + sql_Bookamt1);
                sqlcmd = new SqlCommand(sql_Bookamt1, conn);
                sqlcmd.Transaction = trans;
                sqlcmd.ExecuteNonQuery();

            }

            if (tds > 0)
            {
                Debit = tds;
                Credit = 0;
                acccode = expacccode;
                Oppaccount = tdsaccdesc;
                oppacccode = tdsacccode1;


                string sql_tds1 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br> sql_tds1 : " + sql_tds1);
                SqlCommand sqlcmd1 = new SqlCommand(sql_tds1, conn);
                sqlcmd1.Transaction = trans;
                sqlcmd1.ExecuteNonQuery();

                Debit = 0;
                Credit = tds;
                acccode = tdsacccode1;
                Oppaccount = expaccount;
                oppacccode = expacccode;


                string sql_tds2 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br> sql_tds2 : " + sql_tds2);
                sqlcmd1 = new SqlCommand(sql_tds2, conn);
                sqlcmd1.Transaction = trans;
                sqlcmd1.ExecuteNonQuery();

            }

            if (servicetax > 0)
            {
                Debit = servicetax;
                Credit = 0;
                acccode = expacccode;
                Oppaccount = serviceaccdesc;
                oppacccode = serviceacccode;


                string sql_servicetax1 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br> sql_servicetax1 : " + sql_servicetax1);
                SqlCommand sqlcmd2 = new SqlCommand(sql_servicetax1, conn);
                sqlcmd2.Transaction = trans;
                sqlcmd2.ExecuteNonQuery();


                Debit = 0;
                Credit = servicetax;
                acccode = serviceacccode;
                Oppaccount = expaccount;
                oppacccode = expacccode;


                string sql_servicetax2 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br> sql_servicetax2 : " + sql_servicetax2);
                sqlcmd2 = new SqlCommand(sql_servicetax2, conn);
                sqlcmd2.Transaction = trans;
                sqlcmd2.ExecuteNonQuery();
            }

            if (othertax > 0)
            {
                Debit = othertax;
                Credit = 0;
                acccode = expacccode;
                Oppaccount = cessaccdesc;
                oppacccode = cessacccode;


                string sql_othertax1 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br> sql_othertax1 : " + sql_othertax1);
                SqlCommand sqlcmd3 = new SqlCommand(sql_othertax1, conn);
                sqlcmd3.Transaction = trans;
                sqlcmd3.ExecuteNonQuery();


                Debit = 0;
                Credit = othertax;
                acccode = cessacccode;
                Oppaccount = expaccount;
                oppacccode = expacccode;


                string sql_othertax2 = "exec WEBX_acctrans_insertData_BA_Payment_ver1 '" + yearSuffix + "','" + trdt + "','" + VoucherNo + "','" + acccode + "',Null,null,'" + Debit + "','" + Credit + "','" + Narration + "','" + transtyp1 + "','" + Currbalance + "','" + docNo + "','" + brcd + "','" + Deptcode + "','" + Oppaccount + "','" + Entryby + "','','N','Y','" + oppacccode + "','Closed','" + v_approve_reject_by + "','" + v_approve_reject_branch + "','" + v_closed_by + "','" + v_closed_branch + "','','','" + Payto + "','" + entryfor + "','" + prepareByLoc + "','" + PBOV_code + "','" + PBOV_Name + "','" + PBOV_typ + "','null','null','" + opertitle + "' ";
                Response.Write("<br>sql_othertax2 : " + sql_othertax2);
                sqlcmd3 = new SqlCommand(sql_othertax2, conn);
                sqlcmd3.Transaction = trans;


                sqlcmd3.ExecuteNonQuery();
            }





           trans.Commit();
            //trans.Rollback();
            conn.Close();
          //  Response.Write("<br><br><br>" + abc12);
            Response.Redirect("Done_BA_BillEntry.aspx?" + BillNo);

        }
        catch (Exception e1)
        {
            Response.Write("<br><br><br><font class='blackboldfnt' >Error : " + e1.Message + "</font>");
            if (e1.Message == "")
            {
               // abc = "Insert Susccessfully !!!!!!!";
            }

            trans.Rollback();


            //  conn.Close();
            Response.End();
        }
        finally
        {
            conn.Close();

        }


    }
}
