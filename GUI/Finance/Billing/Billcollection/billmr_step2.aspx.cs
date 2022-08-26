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

public partial class Finance_Billing_Billcollection_billmr_step2 : System.Web.UI.Page
{
    MyFunctions fn = new MyFunctions();
    string Party_code, fincmplbr, fromdt, Todt, PAYBAS;
    string billno_nos;
    public static int totdkt = 0;
    public string chqno, chqdt, indclamt, indchqamt;
    public static string Financial_Year = "";
    public static double totalamtbill = 0;
    public static double totalbillamt = 0,totalPendamt=0;
    public string[] chqno_Arr, chqdt_arr, indclamt_arr, indchqamt_arr, BANKNM_ARR, BANKBRANCH_ARR;
    string finYear = "07";
   // SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["dbConnection"]);
   // SqlConnection conn1 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["dbConnection"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //totalamtbill = 0;
        //totalbillamt = 0;
        //totalPendamt = 0;
        finYear = Financial_Year;
        Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        billno_nos = Request.QueryString.Get("billno_nos");
        Party_code = Request.QueryString.Get("Party_code");
        fincmplbr = Session["brcd"].ToString();
        //Response.Write("fincmplbr - " + fincmplbr);
        fromdt = Request.QueryString.Get("fromdt");
        Todt = Request.QueryString.Get("Todt");
        PAYBAS = Request.QueryString.Get("paybas");
        if (PAYBAS != "All")
        {
            if (PAYBAS == "2")
            {
                PAYBAS = "TBB";
            }
            else
            {
                PAYBAS = "Octroi";
            }
        }
        else
        {
            PAYBAS = "All";
        }
       // Response.Write("PAYBAS " + billno_nos);

        lbldaterange.Text = fromdt.ToString() + "-" + Todt.ToString();
        lblparty.Text = Party_code.ToString() + ":" + fn.Getcustomer(Party_code);
        lblpaybas.Text = PAYBAS.ToString();// fn.Getpaybas(PAYBAS);

        lblmrsbr.Text = fincmplbr + ":" + fn.GetLocation(fincmplbr);
        //txtVoucherDate.Text = fn.Mydate1(DateTime.Now.ToString("dd/MM/yyyy"));
        

        lblpartydetail.Text = Party_code.ToString() + ":" + fn.Getcustomer(Party_code);
        
        if (!IsPostBack)
        {
            totalamtbill = 0;
            totalbillamt = 0;
            totalPendamt = 0;
            BindGrid();

            txtVoucherDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
         foreach (GridViewRow gridrow in dockdata.Rows)
         {

             TextBox tds = (TextBox)gridrow.FindControl("tds");
             TextBox frtrebate = (TextBox)gridrow.FindControl("frtrebate");
             TextBox Claim = (TextBox)gridrow.FindControl("Claim");
             TextBox unexpamt = (TextBox)gridrow.FindControl("unexpamt");
             Label pendamt = (Label)gridrow.FindControl("pendamt");
             Label billamt = (Label)gridrow.FindControl("billamt");
             TextBox totalamt = (TextBox)gridrow.FindControl("totalamt");
             HiddenField txtpendamt = (HiddenField)gridrow.FindControl("txtpendamt");

             if (Session["Client"].ToString() == "RLL" || Session["Client"].ToString() == "RCPL")
             {
                 Claim.Enabled = false;
             }
             // tds.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + lblPendAmt.ClientID + "," + lblbillamt.ClientID + "," + totdkt + ")");
             tds.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + txtpendamt.ClientID + "," + billamt.ClientID + "," + totdkt + "," + totalamt.ClientID + "," + netamt.ClientID + ",this)");
             frtrebate.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + txtpendamt.ClientID + "," + billamt.ClientID + "," + totdkt + "," + totalamt.ClientID + "," + netamt.ClientID + ",this)");
             Claim.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + txtpendamt.ClientID + "," + billamt.ClientID + "," + totdkt + "," + totalamt.ClientID + "," + netamt.ClientID + ",this)");
             pendamt.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + txtpendamt.ClientID + "," + billamt.ClientID + "," + totdkt + "," + totalamt.ClientID + "," + netamt.ClientID + ",this)");
             unexpamt.Attributes.Add("onblur", "javascript:return fcalbillamt(" + tds.ClientID + "," + frtrebate.ClientID + "," + Claim.ClientID + "," + unexpamt.ClientID + "," + txtpendamt.ClientID + "," + billamt.ClientID + "," + totdkt + "," + totalamt.ClientID + "," + netamt.ClientID + ",this)");

             totalbillamt = Convert.ToDouble(pendamt.Text) + totalbillamt;
             netamt.Text = Convert.ToString(totalbillamt);
                     
            // txtchqno.Text = Party_code.ToString();
                    
            
         }
         totalamtbill = totalbillamt;
         TextBox txtchqno = (TextBox)this.chqdetail11.FindControl("txtchqno");
         TextBox txtchqdt = (TextBox)this.chqdetail11.FindControl("txtchqdt");
         TextBox txtindchqamt = (TextBox)this.chqdetail11.FindControl("txtindchqamt");
         HiddenField txtonacctamt = (HiddenField)this.chqdetail11.FindControl("txtonacctamt");
         TextBox txtonacct = (TextBox)this.chqdetail11.FindControl("txtonacct");
         TextBox txtindclamt = (TextBox)this.chqdetail11.FindControl("txtindclamt");
         DropDownList cbopaytype = (DropDownList)this.chqdetail11.FindControl("cbopaytype");
         TextBox colamt = (TextBox)this.chqdetail11.FindControl("colamt");
         colamt.Attributes.Add("onblur", "javascript:return check_pendamt(this," + netamt.ClientID + ")");
    
         TextBox txtbanknm = (TextBox)this.chqdetail11.FindControl("txtbanknm");
         TextBox txtbankbrn = (TextBox)this.chqdetail11.FindControl("txtbankbrn");
         DropDownList tdsacccode = (DropDownList)this.chqdetail11.FindControl("tdsacccode");
         submit.Attributes.Add("onclick", "javascript:return ClearDockets(" + txtchqno.ClientID + "," + txtchqdt.ClientID + "," + txtindchqamt.ClientID + "," + txtonacctamt.ClientID + "," + txtonacct.ClientID + "," + txtindclamt.ClientID + "," + cbopaytype.ClientID + "," + colamt.ClientID + "," + txtbanknm.ClientID + "," + txtbankbrn.ClientID + "," + tottds.ClientID + "," + tdsacccode.ClientID + "," + netamt.ClientID + ")");
     }
         //BindGrid();
    }




    public void BindGrid()
    {
        //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["dbConnection"]);
        //SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim()); SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();
        string sql = "WEBX_BILLMST_Detail";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter myAdapter = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;

        myAdapter.SelectCommand.Parameters.Add("@BILLNOS", SqlDbType.VarChar).Value = billno_nos.Trim();
        

        DataSet ds = new DataSet();

        myAdapter.Fill(ds, "billdata");
        totdkt = ds.Tables[0].Rows.Count;
        dockdata.DataSource = ds;
        dockdata.DataBind();
        conn.Close();    
    }
    public void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
    // BindGrid();
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Build custom header.


            GridView oGridView = (GridView)sender;
            // Label billamt = (Label)oGridView.FindControl("billamt");

            GridViewRow oGridViewRow = new GridViewRow(2, 2, DataControlRowType.Footer, DataControlRowState.Insert);
            TableCell oTableCell = new TableCell();

            //Add Department
            oTableCell.Text = " ";
            oTableCell.ColumnSpan = 2;
            oGridViewRow.Cells.Add(oTableCell);
            oTableCell = new TableCell();
            oTableCell.Text = "Total";
            oTableCell.ColumnSpan = 2;
            oTableCell.CssClass = "blackfnt";
            oGridViewRow.Cells.Add(oTableCell);

            //Add Employee
            oTableCell = new TableCell();
            oTableCell.Text = totdkt.ToString();

            oTableCell.CssClass = "blackfnt";

            oTableCell.ColumnSpan = 7;

            oGridViewRow.Cells.Add(oTableCell);
            oGridView.Controls[0].Controls.AddAt(totdkt + 1, oGridViewRow);

        }
        //BindGrid();
    }
    protected void pgChange(object sender, GridViewPageEventArgs e)
    {
        //dockdata.CurrentPageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void dockdata_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void submit_Click(object sender, EventArgs e)
    {
          

    }
    protected void submit_Click1(object sender, EventArgs e)
    {
        
       
        //SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        //**************for user control data***************************************
        TextBox txtchqno = (TextBox)this.chqdetail11.FindControl("txtchqno");
        TextBox txtchqdt = (TextBox)this.chqdetail11.FindControl("txtchqdt");
        TextBox txtindchqamt = (TextBox)this.chqdetail11.FindControl("txtindchqamt");
        HiddenField txtonacctamt = (HiddenField)this.chqdetail11.FindControl("txtonacctamt");
        TextBox txtonacct = (TextBox)this.chqdetail11.FindControl("txtonacct");
        TextBox txtindclamt = (TextBox)this.chqdetail11.FindControl("txtindclamt");
        DropDownList cbopaytype = (DropDownList)this.chqdetail11.FindControl("cbopaytype");
        TextBox colamt = (TextBox)this.chqdetail11.FindControl("colamt");
        TextBox txtbanknm = (TextBox)this.chqdetail11.FindControl("txtbanknm");
        TextBox txtbankbrn = (TextBox)this.chqdetail11.FindControl("txtbankbrn");
        DropDownList tdsacccode = (DropDownList)this.chqdetail11.FindControl("tdsacccode");
        //***************************************************************************************
       
        string PTCD = Party_code;
        string PTNAME = fn.Getcustomer(Party_code);
        string PAYMODE = cbopaytype.SelectedValue.ToString();
        //string chqno, chqdt, indclamt, indchqamt;
        //string[] chqno_Arr, chqdt_arr, indclamt_arr, indchqamt_arr,BANKNM_ARR,BANKBRANCH_ARR;
        string banknm, bankbr;
        string ErrMsg="";
        if (PAYMODE != "cash")
        {
            chqno = txtchqno.Text.ToString();
            chqdt = txtchqdt.Text.ToString();
            indclamt = txtindclamt.Text.ToString();
            indchqamt = txtindchqamt.Text.ToString();
            if (chqdt != "-")
            {
                chqdt = "'" + chqdt + "'";
            }
            else
            {
                chqdt = "null";
            }
           //  ErrMsg=overbalcheque(chqno,chqdt,indclamt);

        }
       
        string MRSNo = fn.MRNogen(Session["brcd"].ToString());
        string MRSBR = Session["brcd"].ToString();
        string MRBRNNM = fn.GetLocation(MRSBR);
        string paytype = cbopaytype.SelectedValue.ToString();
        chqno_Arr = txtchqno.Text.ToString().Split(',');
        chqdt_arr = txtchqdt.Text.ToString().Split(',');
        indclamt_arr = txtindclamt.Text.ToString().Split(',');
        indchqamt_arr = txtindchqamt.Text.ToString().Split(',');
        BANKNM_ARR = txtbanknm.Text.ToString().Split(',');
        BANKBRANCH_ARR = txtbankbrn.Text.ToString().Split(',');
        if (PAYMODE != "cash")
        {
            for (int i = 0; i < 3; i++)
            {
                chqno = chqno_Arr[i];
                if (chqno != "")
                {
                    chqdt = fn.Mydate1(chqdt_arr[i]);
                    indclamt = indclamt_arr[i];
                    indchqamt = indchqamt_arr[i];
                    banknm = BANKNM_ARR[i];
                    bankbr = BANKBRANCH_ARR[i];
                    if (chqdt != "-")
                    {
                        chqdt = "'" + chqdt + "'";
                    }
                    else
                    {
                        chqdt = "null";
                    }
                    //  ErrMsg=overbalcheque(chqno,chqdt,indclamt);
                }
             
               
            }
        }
        
        ///errmsr*********************
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
      //  conn.Close();
       // conn.Close();
        conn.Open();
      //  Response.Write("ErrMsg -" + ErrMsg);
       // Response.End();
        if (ErrMsg == "")
        {
            
            SqlTransaction trn;
            trn = conn.BeginTransaction();

            //SqlTransaction trn1;


            try  //  try FOR ALL
            {
                
                SqlConnection conn1 = new SqlConnection(Session["SqlProvider"].ToString().Trim());
                string sql = "delete from webx_billchqdetail where docno='" + MRSNo + "'";
                SqlCommand cmd = new SqlCommand(sql, conn, trn);
               // conn.Open();
                cmd.ExecuteNonQuery();
               // conn.Close();
                if (PAYMODE != "cash")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        chqno = chqno_Arr[i];
                        if (chqno != "")
                        {
                            chqdt = fn.Mydate1(chqdt_arr[i]);
                        }
                        indclamt = indclamt_arr[i];
                        indchqamt = indchqamt_arr[i];
                        banknm = BANKNM_ARR[i];
                        bankbr = BANKBRANCH_ARR[i];
                        if (chqno != "")
                        {
                            chqdt = fn.Mydate1(chqdt_arr[i]);
                            if (chqdt != "-")
                            {
                                chqdt = "'" + chqdt + "'";
                            }
                            else
                            {
                                chqdt = "null";
                            }

                            //ErrMsg=overbalcheque(chqno,chqdt,indclamt);
                            string docsf = ".";
                            string doctype = "2";
                            string CloseYN = "1";
                            sql = "insert into webx_billchqdetail(chqno,chqdt,chqamt,clamt,banknm,bankbrn,docno,doctype,docsf)values('" + chqno + "'," + chqdt + "," + indchqamt + "," + indclamt + ",'" + banknm + "','" + bankbr + "','" + MRSNo + "','" + doctype + "','" + docsf + "')";
                            cmd = new SqlCommand(sql, conn, trn);
                            //conn.Open();
                            cmd.ExecuteNonQuery();
                           // conn.Close();

                            if (CloseYN != "")
                            {
                                sql = "select chqamt,adjustamt,banknm from webx_chq_det where chqno='" + chqno + "' and convert(datetime,chqdt,106)=convert(varchar," + chqdt + ",106)";
                                cmd = new SqlCommand(sql, conn, trn);
                                SqlDataReader dr = null;
                             //   conn.Open();
                                dr = cmd.ExecuteReader();
                                string chq_y_n = "";
                                if (dr.Read())
                                {
                                    //NewCode = Convert.ToString(dr["NewCode"]);
                                    chq_y_n = "Y";
                                }
                                else
                                {
                                    chq_y_n = "N";
                                }
                                dr.Close();
                                if (chq_y_n == "Y")
                                {
                                    sql = "update webx_chq_det set adjustamt=adjustamt+" + indclamt + " where chqno='" + chqno + "' and convert(datetime,chqdt,106)=convert(varchar," + chqdt + ",106)";
                                    SqlCommand cmd1 = new SqlCommand(sql, conn, trn);
                                    //conn.Open();
                                    cmd1.ExecuteNonQuery();
                                   // conn.Close();

                                    //   Call BillTransaction(3,session("brcd"),MRSNo,".")
                                }
                                else
                                {
                                    string brcd = Session["brcd"].ToString();
                                    string brnm = fn.GetLocation(brcd);
                                    string ptmsptcd = PTCD;
                                    string ptmsptnm = PTNAME;
                                    sql = "insert into webx_chq_det(Chqno, chqdt, chqamt,banknm, ptmsptcd,ptmsptnm,comments,bacd,banm,empcd,empnm,brcd,brnm,staffcd,staffnm,adjustamt,bankbrn,chq_trf) values('" + chqno + "'," + chqdt + "," + indchqamt + ",'" + banknm + "','" + ptmsptcd + "','" + ptmsptnm + "','','','','','','" + brcd + "','" + brnm + "','',''," + indclamt + ",'" + bankbr + "','" + brcd + "')";
                                    SqlCommand cmd1 = new SqlCommand(sql, conn, trn);


                                 //   conn.Open();
                                    cmd1.ExecuteNonQuery();
                                 //   conn.Close();
                                }
                                
                               // conn.Close();
                            }


                        }

                    }
                }

                //**************end for loop*************************
                string PTMSTEL = "";
                string PTMSEMAIL = "";
                string PTMSADDR = "";
                string PTPIN = "";
                string PTMSCD = Party_code.Trim();
                string PTMSNM = fn.Getcustomer(Party_code.Trim());
                foreach (GridViewRow gridrow in dockdata.Rows)
                {

                    TextBox tds = (TextBox)gridrow.FindControl("tds");
                    TextBox frtrebate = (TextBox)gridrow.FindControl("frtrebate");
                    TextBox Claim = (TextBox)gridrow.FindControl("Claim");
                    TextBox unexpamt = (TextBox)gridrow.FindControl("unexpamt");
                    Label pendamt = (Label)gridrow.FindControl("pendamt");
                    Label billamt = (Label)gridrow.FindControl("billamt");
                    TextBox totalamt = (TextBox)gridrow.FindControl("totalamt");
                    HiddenField txtpendamt = (HiddenField)gridrow.FindControl("txtpendamt");
                    Label txtBillno = (Label)gridrow.FindControl("txtBillno");
                    HiddenField Hidden_svctax_rate = (HiddenField)gridrow.FindControl("Hidden_svctax_rate");
                    HiddenField Hidden_cess_rate = (HiddenField)gridrow.FindControl("Hidden_cess_rate");
                    HiddenField Hidden_H_cess_rate = (HiddenField)gridrow.FindControl("Hidden_H_cess_rate");
                    HiddenField Hidden_svrcamt = (HiddenField)gridrow.FindControl("Hidden_svrcamt");
                    HiddenField ptmscd = (HiddenField)gridrow.FindControl("ptmscd");
                    Party_code = ptmscd.Value;
                    double TOTBILL = Convert.ToDouble(billamt.Text.ToString());
                    double TDSDED = Convert.ToDouble(tds.Text.ToString());
                    double NETAMT = Convert.ToDouble(totalamt.Text.ToString());
                    double FRTDED = Convert.ToDouble(frtrebate.Text.ToString());
                    double CLMDED = Convert.ToDouble(Claim.Text.ToString());
                    double UNEXPDED = Convert.ToDouble(unexpamt.Text.ToString());
                    string DOCNO = txtBillno.Text.ToString();
                    string DOCSF = ".";

                    double cess_rate = Convert.ToDouble(Hidden_cess_rate.Value), Othded_H_cess = 0, Othded_frt = 0, Othded_svctax = 0;

                    double svctax_rate = Convert.ToDouble(Hidden_svctax_rate.Value);
                    double H_cess_rate = Convert.ToDouble(Hidden_H_cess_rate.Value), Othded_cess = 0, OthChrg_frt = 0, OthChrg_svctax = 0, OthChrg_cess = 0;
                    // Response.Write("hi...");
                    // Response.End(); 
                    Othded_frt = FRTDED;
                    if (Convert.ToDouble(Hidden_svrcamt.Value) > 0)
                    {

                        //Othded_frt = (DED_OTH / (1 + (svctax_rate / 100) + (svctax_rate / 100) * (cess_rate / 100)));
                        //Othded_svctax = FRTDED * svctax_rate / 100;
                        //Response.Write("<br>Othded_svctax-" + Othded_svctax);

                        //Othded_cess = Othded_svctax * cess_rate / 100;
                        //Response.Write("<br>Othded_cess-" + Othded_cess);
                        // Othded_H_cess = Othded_svctax * H_cess_rate / 100;
                        // Response.Write("<br>Othded_H_cess-" + Othded_H_cess);
                        // Othded_frt = FRTDED - Othded_svctax - Othded_cess - Othded_H_cess;

                        //***********************new treatment *********************************

                        Othded_frt = (FRTDED / 112.36) * 100;
                        Othded_svctax = (Othded_frt * 12) / 100;
                        Othded_cess = (Othded_svctax * 2) / 100;
                        Othded_H_cess = (Othded_svctax * 1) / 100;

                        //***********************************************************************

                    }
                    else
                    {
                        Othded_frt = FRTDED;
                    }

                    sql = "INSERT INTO webx_MRDET_BILL (MRSNo, MRSSF, TOTBILL, TDSDED, NETAMT, FRTDED, CLMDED, UNEXPDED,DOCNO,DOCSF,FRTDED_frt,FRTDED_svctax,FRTDED_cess,FRTDED_Hedu_cess) VALUES('" + MRSNo + "','.'," + TOTBILL + "," + TDSDED + "," + NETAMT + "," + FRTDED + "," + CLMDED + "," + UNEXPDED + ",'" + DOCNO + "','" + DOCSF + "'," + Othded_frt + "," + Othded_svctax + "," + Othded_cess + "," + Othded_H_cess + ")";
                    cmd = new SqlCommand(sql, conn, trn);

                   // conn.Open();
                    cmd.ExecuteNonQuery();
                   // conn.Close();

                    if ((UNEXPDED > 0))
                    {
                        sql = "update webx_billmst set bcldt='" + fn.Mydate1(txtVoucherDate.Text) + "',clamt=clamt+" + NETAMT + ",BCOLEMPCD='" + Session["empcd"].ToString() + "',pendamt=" + UNEXPDED + " where BillNo='" + DOCNO + "'";

                        cmd = new SqlCommand(sql, conn, trn);
                       // conn.Open();
                        cmd.ExecuteNonQuery();
                       // conn.Close();
                    }
                    else
                    {
                        sql = "update webx_billmst set finclosedt='" + fn.Mydate1(txtVoucherDate.Text) + "',CloseBill='Y',bcldt='" + fn.Mydate1(txtVoucherDate.Text) + "',commitbill='Y',clamt=" + NETAMT + ",BCBSEMPCD='" + Session["empcd"].ToString() + "',BCOLEMPCD='" + Session["empcd"].ToString() + "',billstatus='Bill Collected',pendamt=0 where BillNo='" + DOCNO + "'";
                        cmd = new SqlCommand(sql, conn, trn);
                       // conn.Open();
                        cmd.ExecuteNonQuery();
                       // conn.Close();
                    }

                    sql = "WEBX_Customer_Other";
                    cmd = new SqlCommand(sql, conn, trn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@brcd", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@custcd", SqlDbType.VarChar).Value = Party_code.Trim();

                    SqlDataReader dr = null;
                  //  conn.Open();
                    dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        PTMSCD = Party_code.Trim();
                        PTMSNM = fn.Getcustomer(Party_code.Trim());
                        PTMSTEL = Convert.ToString(dr["CUSTTEL"]);
                        PTMSEMAIL = Convert.ToString(dr["CUSTEMAIL"]);
                        PTMSADDR = Convert.ToString(dr["CUSTADDRESS"]);

                    }
                    dr.Close();
                   // conn.Close();
                }
                double MRSAMT = Convert.ToDouble(totalbillamt.ToString());
                double totNETAMT = Convert.ToDouble(netamt.Text.ToString());
                double DEDUCTION = Convert.ToDouble(MRSAMT - totNETAMT);
                PAYMODE = PAYMODE;
                double MRSCASH = 0;
                double MRSCHQ = 0;
                if (PAYMODE != "Cash")
                {
                    MRSCHQ = Convert.ToDouble(colamt.Text.ToString());
                    MRSCASH = 0;
                    chqno = txtchqno.Text.ToString();
                    chqdt = txtchqdt.Text.ToString();
                    string[] chqno_arr = txtchqno.Text.ToString().Split(',');
                    string[] chq_dtarr = txtchqdt.Text.ToString().Split(',');
                    chqno = chqno_arr[0];
                    chqdt = chq_dtarr[0];
                }
                else
                {

                    MRSCASH = Convert.ToDouble(colamt.Text.ToString());
                    MRSCHQ = 0;

                }
                string MRSCHQNO = "";
                string MRSCHQDT1 = "";

                if (PAYMODE != "Cash")
                {
                    MRSCHQNO = chqno;
                    MRSCHQDT1 = fn.Mydate1(chqdt);

                }
                else
                {
                    MRSCHQNO = "Null";
                    MRSCHQDT1 = "Null";
                }

                string MRSCHQDT = "";//fn.Mydate1(txtchqdt.Text.ToString()).Replace(",","");
                if (MRSCHQDT != "")
                {
                    MRSCHQDT = "'" + MRSCHQDT + "'";
                }
                else
                {
                    MRSCHQDT = "null";
                }
                string MRSBANK = txtbanknm.Text.ToString();
                string MRSPRINT_YN = "N";
                string MRS_CLOSED = "Y";
                string MR_CANCEL = "N";
                string BILLMR = "Y";
                string tdsacccode1 = "";//  tdsgrpcode_arr[0];
                string tdsaccdesc1 = "";//  tdsgrpcode_arr[1];
                if (tdsacccode.Text.ToString() != "")
                {
                    string[] tdsgrpcode_arr;
                    tdsgrpcode_arr = tdsacccode.Text.ToString().Split('~');
                    tdsacccode1 = tdsgrpcode_arr[0];
                    tdsaccdesc1 = tdsgrpcode_arr[1];
                }
                else
                {
                    tdsacccode1 = "";
                    tdsaccdesc1 = "";
                }
                string tdsfor = "";
                string BankAcccode = "", BankAccdesc = "";
                if (PAYMODE != "Cash")
                {
                    sql = "INSERT INTO webx_MR_HDR(ENTRYBY, ENTRYDT, MRSNo, MRSSF, MRSDT, MRSTYPE, MRSBR, MRBRNNM, PTCD, PTNAME, PTPIN, PTTEL, PTADD, PAYBAS, MRSAMT,NETAMT, DEDUCTION, MRSCASH, MRSCHQ, MRSCHQNO, MRSCHQDT, MRSBANK, MRSPRINT_YN, MRS_CLOSED, DED_TDS, MR_CANCEL, BILLMR,finclosedt, DED_FRT, paymode,BankAcccode,BankAccdesc,tdsacccode,tdsaccdesc,tdsfor) VALUES ('" + Session["empcd"].ToString() + "',getdate(),'" + MRSNo + "','.','" + fn.Mydate1(txtVoucherDate.Text) + "','2','" + MRSBR + "','" + MRBRNNM + "','" + PTMSCD + "','" + PTMSNM + "','" + PTPIN + "','" + PTMSTEL + "','" + PTMSADDR + "',''," + MRSAMT + "," + totNETAMT + "," + DEDUCTION + "," + MRSCASH + "," + MRSCHQ + ",'" + MRSCHQNO + "','" + MRSCHQDT1 + "','" + MRSBANK + "','" + MRSPRINT_YN + "','" + MRS_CLOSED + "',0,'" + MR_CANCEL + "','" + BILLMR + "',getdate(),0,'" + PAYMODE + "','" + BankAcccode + "','" + BankAccdesc + "','" + tdsacccode1 + "','" + tdsaccdesc1 + "','" + tdsfor + "')";
                }
                else
                {
                    sql = "INSERT INTO webx_MR_HDR(ENTRYBY, ENTRYDT, MRSNo, MRSSF, MRSDT, MRSTYPE, MRSBR, MRBRNNM, PTCD, PTNAME, PTPIN, PTTEL, PTADD, PAYBAS, MRSAMT,NETAMT, DEDUCTION, MRSCASH, MRSCHQ, MRSCHQNO, MRSCHQDT, MRSBANK, MRSPRINT_YN, MRS_CLOSED, DED_TDS, MR_CANCEL, BILLMR,finclosedt, DED_FRT, paymode,BankAcccode,BankAccdesc,tdsacccode,tdsaccdesc,tdsfor) VALUES ('" + Session["empcd"].ToString() + "',getdate(),'" + MRSNo + "','.','" + fn.Mydate1(txtVoucherDate.Text) + "','2','" + MRSBR + "','" + MRBRNNM + "','" + PTMSCD + "','" + PTMSNM + "','" + PTPIN + "','" + PTMSTEL + "','" + PTMSADDR + "',''," + MRSAMT + "," + totNETAMT + "," + DEDUCTION + "," + MRSCASH + "," + MRSCHQ + ",NUll,NUll,NULL,'" + MRSPRINT_YN + "','" + MRS_CLOSED + "',0,'" + MR_CANCEL + "','" + BILLMR + "',getdate(),0,'" + PAYMODE + "','" + BankAcccode + "','" + BankAccdesc + "','" + tdsacccode1 + "','" + tdsaccdesc1 + "','" + tdsfor + "')";
                }
                // Response.Write("<br>sql-" + sql);
                // Response.End();                  
                cmd = new SqlCommand(sql, conn, trn);
                //conn.Open();
                cmd.ExecuteNonQuery();
                //conn.Close();
               // conn.Open();
                cmd = new SqlCommand("usp_BillTransaction", conn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                int transno = 3;
                //string finYear = "07";

                cmd.Parameters.AddWithValue("@transNo", transno);
                cmd.Parameters.AddWithValue("@docNo", MRSNo);
                cmd.Parameters.AddWithValue("@finYear", Financial_Year);
                cmd.ExecuteNonQuery();


               // conn.Close();
               // conn.Open();
                cmd = new SqlCommand("usp_BillTransaction", conn, trn);
                cmd.CommandType = CommandType.StoredProcedure;
                //   int transno = 2;
                //   string finYear = "07";

                cmd.Parameters.AddWithValue("@transNo", '2');
                cmd.Parameters.AddWithValue("@docNo", MRSNo);
                cmd.Parameters.AddWithValue("@finYear", Financial_Year);
                cmd.ExecuteNonQuery();

                //trn.Rollback();
                trn.Commit();
                 conn.Close();
                  Response.Redirect("BillMrDone.aspx?mrsno="+MRSNo);
            }
        
       
        catch (Exception e1)
        {
            Response.Write("<br><br><br><font class='blackboldfnt' >Error : " + e1.Message + "</font>");
            string msg = e1.Message.ToString();
            msg = msg.Replace('\n', ' ');
            
            trn.Rollback();
            //conn.Close();
         //   trn1.Rollback();
            //Response.Redirect("Message.aspx?" + e1.Message);
            Response.End();

        }
       }
       
    }
}
