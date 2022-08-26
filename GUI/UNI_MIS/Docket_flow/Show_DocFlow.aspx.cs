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

public partial class DOC_FLOW_Show_DocFlow : System.Web.UI.Page
{


    public static string strDateRange;
    public static string strro;
    public static string strloc;
    public static string strcust;
    public static string strflow;
    public static string strdt;
    public static string docketis;


    public static int intCurrentPageNo;
    public static int intPageSize = 10;



    //SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
   // static string strConnection = "data source=202.87.45.72;UID=sa;PWD=ecfy@pra$$ana;Database=ASL_Live;";
 //  static string strConnection = "data source=202.87.45.71;UID=sa;PWD=ecfy$@p@$$;Database=RCPL_ver2_live";

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //hypWelcome.NavigateUrl = "../../welcome.asp"; //Use an appropriate path

        //hypReport.Text = "Reports";
        //hypReport.NavigateUrl = "../reportmenu.asp"; //Use an appropriate path

        //hypFinance.Text = "Finance";
        //hypFinance.NavigateUrl = "../rpt_finance.asp"; //Use an appropriate path

        //if (HIDCurrentPageNo.Value == "")
        //{
        //    HIDCurrentPageNo.Value = "1";
        //}

       // intCurrentPageNo = Convert.ToInt32(HIDCurrentPageNo.Value);


        if (!IsPostBack)
        {
            docketis = Session["DocketCalledAs"].ToString();
            //Date Range display preparation
            strDateRange = Request.QueryString["DOCDate"].ToString();
            strro = Request.QueryString["RO"].ToString();
            if (strro == "-All-")
            {
                strro = "ALL";
            }
            strloc = Request.QueryString["Location"].ToString();
            if (strloc == "-All-")
            {
                strloc = "ALL";
            }
            strcust = Request.QueryString["Cust"].ToString();
            if (strcust == "")
            {
                strcust = "";
            }
            

            


        }


        strflow = Request.QueryString["Flow"].ToString();
        strdt = Request.QueryString["DateType"].ToString();
        lblseldet.Text = strDateRange;
        lblro.Text = strro;
        lblloc.Text = strloc;
        lblcust.Text = strcust;
        string s1, s2;
        if (strdt == "dockdt")
        {
            s1 = lbldt.Text;
            lbldt.Text = "Booking"+s1;
        }
        else
        {
            s1 = lbldt.Text;
            lbldt.Text = "Delivry"+s1;
        }



        DisplayDocFlow();



    }



    protected void NavigationLink_Click(Object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "First":
                intCurrentPageNo = 1;
                break;
            case "Last":
                intCurrentPageNo = Int32.Parse(TotalPages.Text);
                break;
            case "Next":
                intCurrentPageNo = Int32.Parse(CurrentPage.Text) + 1;
                break;
            case "Prev":
                intCurrentPageNo = Int32.Parse(CurrentPage.Text) - 1;
                break;
        }

        //HIDCurrentPageNo.Value = intCurrentPageNo.ToString();

      //  DisplayDocFlow();

    }

    private void DisplayDocFlow()
    {
       // SqlConnection sqlConn = new SqlConnection(strConnection);

        SqlConnection sqlConn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        sqlConn.Open();

        SqlCommand sqlCommand = new SqlCommand();

        sqlCommand.Connection = sqlConn;
        sqlCommand.CommandType = CommandType.StoredProcedure;

        if (strflow == "0")
        {
            sqlCommand.CommandText = "webx_UNI_DKTFLOW_REP";
            lblflow.Text = "Booking Flow";
        }
        else
        {
            sqlCommand.CommandText = "webx_UNI_DKT_DLY_Flow";
            lblflow.Text = "Delivry Flow";

        }


        
        //Managing Date Range for input parameters to SP
        string[] strArrDtFromTo = strDateRange.Split('-');
        sqlCommand.Parameters.AddWithValue("@FROMDT", strArrDtFromTo[0]);
        sqlCommand.Parameters.AddWithValue("@TODT", strArrDtFromTo[1]);
        sqlCommand.Parameters.AddWithValue("@RO", strro);
        sqlCommand.Parameters.AddWithValue("@ORGNCD", strloc);
        sqlCommand.Parameters.AddWithValue("@CSGNCD", strcust);
        sqlCommand.Parameters.AddWithValue("@seldate", strdt);



        try
        {

            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);
       
        DataSet ds = new DataSet();
        DataSet dsPages = new DataSet();

        if (CurrentPage.Text == "")
        {
            CurrentPage.Text = "1";
            intCurrentPageNo = 1;
          //  HIDCurrentPageNo.Value = "1";
            FirstPage.Enabled = false;
            PreviousPage.Enabled = false;
        }
        else
        {
            CurrentPage.Text = intCurrentPageNo.ToString();
        }

        //try
        //{

        //    sqlDA.Fill(ds);

        //    GridView1.Visible = true;

        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();

        //}
        //catch (Exception e)
        //{
        //    lblcust.Text = e.Message;
        //}


    //         yet to Comp



        sqlDA.Fill(ds, ((intCurrentPageNo - 1) * intPageSize), intPageSize, "dtDOCFLOW");
        sqlDA.Fill(dsPages);
   


        if (ds.Tables[0].Rows.Count == 0)
        {
            tblNoRecords.Visible = true;

         
            tblPageNavigation.Visible = false;
            //tblButtons.Visible = false;
            //lblComment.Visible = false;
        }
        else
        {
            double dblTotalRecords = dsPages.Tables[0].Rows.Count;
            double dblTotalPages = Convert.ToDouble(System.Math.Ceiling(Convert.ToDecimal(dblTotalRecords / intPageSize)));

          //  Logic for Managing Page Navigation Links
            if (intCurrentPageNo == 1)
            {
                FirstPage.Enabled = false;
                PreviousPage.Enabled = false;
                NextPage.Enabled = true;
                LastPage.Enabled = true;
            }
            else if (intCurrentPageNo == dblTotalPages)
            {
                FirstPage.Enabled = true;
                PreviousPage.Enabled = true;
                NextPage.Enabled = false;
                LastPage.Enabled = false;
            }
            else
            {
                FirstPage.Enabled = true;
                PreviousPage.Enabled = true;
                NextPage.Enabled = true;
                LastPage.Enabled = true;
            }


            if (dblTotalPages == 1)
            {
                FirstPage.Enabled = false;
                PreviousPage.Enabled = false;
                NextPage.Enabled = false;
                LastPage.Enabled = false;
            }

            //Logic for populating Page No.'s in Drop Down List for Page
           // ddlPages.Items.Clear();

            ListItem lItem;

            for (int i = 1; i <= dblTotalPages; i++)
            {
                lItem = new ListItem();

                lItem.Text = i.ToString();
                lItem.Value = lItem.Text;

              //  ddlPages.Items.Add(lItem);

                //if (i == Convert.ToInt32(HIDCurrentPageNo.Value))
                //{
                //    ddlPages.SelectedIndex = ddlPages.Items.Count - 1;
                //}

                lItem = null;
            }

            TotalPages.Text = dblTotalPages.ToString();




            DataTable dt = new DataTable();

            dt.Columns.Add("RowCounter", typeof(string));
            dt.Columns.Add("loccode", typeof(string));
            dt.Columns.Add("locname", typeof(string));
            dt.Columns.Add("TOTALDOCKETS", typeof(string));
            dt.Columns.Add("LEFT_TOTDKTS", typeof(string));
            dt.Columns.Add("NOTLEFT_TOTDKTS", typeof(string));
            dt.Columns.Add("ARRV_TOTDKTS", typeof(string));
            dt.Columns.Add("NOTARRV_TOTDKTS", typeof(string));
            dt.Columns.Add("DLYATTEMPT_TOTDKTS", typeof(string));
            dt.Columns.Add("NOTDLYATTEMPT_TOTDKTS", typeof(string));
            dt.Columns.Add("DLYINTIME_TOTDKTS", typeof(string));
            dt.Columns.Add("DLYLATE_TOTDKTS", typeof(string));
            dt.Columns.Add("PARTDLY_TOTDKTS", typeof(string));
            dt.Columns.Add("UNDLY_TOTDKTS", typeof(string));


            DataRow dr;

            
            TimeSpan ts;

          

            int loopCounter = ((intCurrentPageNo - 1) * intPageSize) + 1;

          

            //   string strHrefLink = "popupmenu2('../../finance/Billing/viewprintupdbills/billcover_Print.asp','" + drRows["BILLNO"].ToString() + "',0)";


            foreach (DataRow drRows in ds.Tables[0].Rows)
            {
                dr = dt.NewRow();

                dr["RowCounter"] = loopCounter++;


              //  dr["aHrefLink"] = strHrefLink;

                dr["locname"] = drRows["loccode"].ToString() + " : " + drRows["locname"].ToString();

                dr["loccode"] = drRows["loccode"].ToString();
                
                //*********dr[" "] = drRows["PTMSCD"].ToString() + " : " + drRows["PTMSNM"].ToString();


                dr["TOTALDOCKETS"] = drRows["TOTALDOCKETS"].ToString();
                dr["LEFT_TOTDKTS"] = drRows["LEFT_TOTDKTS"].ToString();
                dr["NOTLEFT_TOTDKTS"] = drRows["NOTLEFT_TOTDKTS"].ToString();
                dr["ARRV_TOTDKTS"] = drRows["ARRV_TOTDKTS"].ToString();
                dr["NOTARRV_TOTDKTS"] = drRows["NOTARRV_TOTDKTS"].ToString();
                dr["DLYATTEMPT_TOTDKTS"] = drRows["DLYATTEMPT_TOTDKTS"].ToString();

                dr["NOTDLYATTEMPT_TOTDKTS"] = drRows["NOTDLYATTEMPT_TOTDKTS"].ToString();
                dr["DLYINTIME_TOTDKTS"] = drRows["DLYINTIME_TOTDKTS"].ToString();
                dr["DLYLATE_TOTDKTS"] = drRows["DLYLATE_TOTDKTS"].ToString();
                dr["PARTDLY_TOTDKTS"] = drRows["PARTDLY_TOTDKTS"].ToString();
                dr["UNDLY_TOTDKTS"] = drRows["UNDLY_TOTDKTS"].ToString();

                dt.Rows.Add(dr);


                rptBillDetails.DataSource = dt;
                rptBillDetails.DataBind();
 

               

            }

        }
    }
    catch (Exception eee)
    {
        lblcust.Text = eee.Message;
    }




    }

    private void dispxls()
    {
        // SqlConnection sqlConn = new SqlConnection(strConnection);

        SqlConnection sqlConn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        sqlConn.Open();

        SqlCommand sqlCommand = new SqlCommand();

        sqlCommand.Connection = sqlConn;
        sqlCommand.CommandType = CommandType.StoredProcedure;

        if (strflow == "0")
        {
            sqlCommand.CommandText = "webx_UNI_DKTFLOW_REP";
            lblflow.Text = "Booking Flow";
        }
        else
        {
            sqlCommand.CommandText = "webx_UNI_DKT_DLY_Flow";
            lblflow.Text = "Delivry Flow";

        }



        //Managing Date Range for input parameters to SP
        string[] strArrDtFromTo = strDateRange.Split('-');
        sqlCommand.Parameters.AddWithValue("@FROMDT", strArrDtFromTo[0]);
        sqlCommand.Parameters.AddWithValue("@TODT", strArrDtFromTo[1]);
        sqlCommand.Parameters.AddWithValue("@RO", strro);
        sqlCommand.Parameters.AddWithValue("@ORGNCD", strloc);
        sqlCommand.Parameters.AddWithValue("@CSGNCD", strcust);
        sqlCommand.Parameters.AddWithValue("@seldate", strdt);



        try
        {

            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);

            DataSet ds = new DataSet();


            sqlDA.Fill(ds);



            if (ds.Tables[0].Rows.Count == 0)
            {
                tblNoRecords.Visible = true;


                tblPageNavigation.Visible = false;
                //tblButtons.Visible = false;
                //lblComment.Visible = false;
            }
            else
            { 

                DataTable dt = new DataTable();

                dt.Columns.Add("RowCounter", typeof(string));
                dt.Columns.Add("loccode", typeof(string));
                dt.Columns.Add("locname", typeof(string));
                dt.Columns.Add("TOTALDOCKETS", typeof(string));
                dt.Columns.Add("LEFT_TOTDKTS", typeof(string));
                dt.Columns.Add("NOTLEFT_TOTDKTS", typeof(string));
                dt.Columns.Add("ARRV_TOTDKTS", typeof(string));
                dt.Columns.Add("NOTARRV_TOTDKTS", typeof(string));
                dt.Columns.Add("DLYATTEMPT_TOTDKTS", typeof(string));
                dt.Columns.Add("NOTDLYATTEMPT_TOTDKTS", typeof(string));
                dt.Columns.Add("DLYINTIME_TOTDKTS", typeof(string));
                dt.Columns.Add("DLYLATE_TOTDKTS", typeof(string));
                dt.Columns.Add("PARTDLY_TOTDKTS", typeof(string));
                dt.Columns.Add("UNDLY_TOTDKTS", typeof(string));


                DataRow dr;


                TimeSpan ts;



                int loopCounter = 1;
                    //((intCurrentPageNo - 1) * intPageSize) + 1;



                //   string strHrefLink = "popupmenu2('../../finance/Billing/viewprintupdbills/billcover_Print.asp','" + drRows["BILLNO"].ToString() + "',0)";


                foreach (DataRow drRows in ds.Tables[0].Rows)
                {
                    dr = dt.NewRow();

                    dr["RowCounter"] = loopCounter++;


                    //  dr["aHrefLink"] = strHrefLink;

                    dr["locname"] = drRows["loccode"].ToString() + " : " + drRows["locname"].ToString();

                    dr["loccode"] = drRows["loccode"].ToString();
            
                    //*********dr[" "] = drRows["PTMSCD"].ToString() + " : " + drRows["PTMSNM"].ToString();
                     
                    dr["TOTALDOCKETS"] = drRows["TOTALDOCKETS"].ToString();
                    dr["LEFT_TOTDKTS"] = drRows["LEFT_TOTDKTS"].ToString();
                    dr["NOTLEFT_TOTDKTS"] = drRows["NOTLEFT_TOTDKTS"].ToString();
                    dr["ARRV_TOTDKTS"] = drRows["ARRV_TOTDKTS"].ToString();
                    dr["NOTARRV_TOTDKTS"] = drRows["NOTARRV_TOTDKTS"].ToString();
                    dr["DLYATTEMPT_TOTDKTS"] = drRows["DLYATTEMPT_TOTDKTS"].ToString();

                    dr["NOTDLYATTEMPT_TOTDKTS"] = drRows["NOTDLYATTEMPT_TOTDKTS"].ToString();
                    dr["DLYINTIME_TOTDKTS"] = drRows["DLYINTIME_TOTDKTS"].ToString();
                    dr["DLYLATE_TOTDKTS"] = drRows["DLYLATE_TOTDKTS"].ToString();
                    dr["PARTDLY_TOTDKTS"] = drRows["PARTDLY_TOTDKTS"].ToString();
                    dr["UNDLY_TOTDKTS"] = drRows["UNDLY_TOTDKTS"].ToString();

                    dt.Rows.Add(dr);
                    
                }
                
                rptBillDetails.DataSource = dt;
                rptBillDetails.DataBind();
 

            }
        }
        catch (Exception eee)
        {
            lblcust.Text = eee.Message;
        }
         
    }
     

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btn_csv_Click(object sender, EventArgs e)
    {
        int ind = 0;
        int a = 0;
        try
        {
           // GV_Booking.AllowPaging = false;

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;Filename=Docket_Flow_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            dispxls();
            newtab.RenderControl(hw);


            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex1)
        {
            throw ex1;
            // lblloc.Text = ex1.Message;
        }

    }

    public void rpt(object sender, RepeaterItemEventArgs sa)
    {

        if (sa.Item.ItemType == ListItemType.Item)
        {



            HyperLink h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11;
            h1 = (HyperLink)sa.Item.FindControl("Hyperlink1");
            h2 = (HyperLink)sa.Item.FindControl("Hyperlink2");
            h3 = (HyperLink)sa.Item.FindControl("Hyperlink3");
            h4 = (HyperLink)sa.Item.FindControl("Hyperlink4");
            h5 = (HyperLink)sa.Item.FindControl("Hyperlink5");
            h6 = (HyperLink)sa.Item.FindControl("Hyperlink6");
            h7 = (HyperLink)sa.Item.FindControl("Hyperlink7");
            h8 = (HyperLink)sa.Item.FindControl("Hyperlink8");
            h9 = (HyperLink)sa.Item.FindControl("Hyperlink9");
            h10 = (HyperLink)sa.Item.FindControl("Hyperlink10");
            h11 = (HyperLink )sa.Item.FindControl("Hyperlink11");
            if (h1.Text == "0")
            {
                h1.NavigateUrl = "";
                h1.Font.Underline = false;
            }
            if (h2.Text == "0")
            {
                h2.NavigateUrl = "";
                h2.Font.Underline = false;
            }
            if (h3.Text == "0")
            {
                h3.NavigateUrl = "";
                h3.Font.Underline = false;
            }
            if (h4.Text == "0")
            {
                h4.NavigateUrl = "";
                h4.Font.Underline = false;
            }
            if (h5.Text == "0")
            {
                h5.NavigateUrl = "";
                h5.Font.Underline = false;
            }
            if (h6.Text == "0")
            {
                h6.NavigateUrl = "";
                h6.Font.Underline = false;
            }
            if (h7.Text == "0")
            {
                h7.NavigateUrl = "";
                h7.Font.Underline = false;
            }
            if (h8.Text == "0")
            {
                h8.NavigateUrl = "";
                h8.Font.Underline = false;
            }
            if (h9.Text == "0")
            {
                h9.NavigateUrl = "";
                h9.Font.Underline = false;
            }
            if (h10.Text == "0")
            {
                h10.NavigateUrl = "";
                h10.Font.Underline = false;
            }
            
            
            if (h11.Text == "0")
            {
                h11.NavigateUrl = "";
                h11.Font.Underline = false;
            }
        }







    }

        

}
