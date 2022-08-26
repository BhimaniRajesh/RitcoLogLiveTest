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

public partial class GUI_Finance_MoneyRecipt_Updatemr_step2 : System.Web.UI.Page
{
    public static string strDateRange, mrno, docno, strfrmdt = "", strtodt = "", Mrstype = "", Mrstype_desc="";
    public static int intTotalRecords = 0;
    string Branch;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Button1.Attributes.Add("onclick", "javascript:return CheckDocketSelection(" + txtdocketlist.ClientID + "," + txtdocketlist_count.ClientID + ")");
       
        if (!IsPostBack)
        {
            //Date Range display preparation
            strDateRange = Request.QueryString["strDateRange"].ToString();
            Session["dt"] = strDateRange;
            mrno = Request.QueryString["mrno"].ToString();
            docno = Request.QueryString["docno"].ToString();
            Mrstype = Request.QueryString["mrstype"].ToString();
            lbl_mrstype.Text = Mrstype_desc;
            if (Mrstype == "1")
            {
                Mrstype_desc = "Paid MR";
            }
            else if (Mrstype == "2")
            {
                Mrstype_desc = "Bill MR";
            }
            else if (Mrstype == "All")
            {
                Mrstype_desc = "All";
            }
            else if (Mrstype == "3")
            {
                Mrstype_desc = "ToPay MR";
            }
            else if (Mrstype == "4")
            {
                Mrstype_desc = "Octroi";
            }
            else if (Mrstype == "7")
            {
                Mrstype_desc = "Misc. MR";
            }
            else if (Mrstype == "9")
            {
                Mrstype_desc = "TripSheet Bill MR";
            }
            else if (Mrstype == "-")
            {
                Mrstype_desc = "-";
            }
            
            //Button1.Visible = false;


        }
        if (mrno == "" && docno == "")
        {
            lblseldet.Text = strDateRange;
        }
        else
        {
            lblseldet.Text = "-";
        }
        str_mrsno.Text=mrno;
        Str_dockno.Text = docno;
        lbl_mrstype.Text = Mrstype_desc;
        if (mrno == "")
        {
            str_mrsno.Text = "-";
        }
        if (docno == "")
        {
            Str_dockno.Text = "-";
        }
        //Response.Write("<br>strfrmdt-" + strfrmdt);
        //Response.Write("<br>strtodt-" + strtodt);
        //Response.Write("<br>mrno-" + mrno);
        //Response.Write("<br>docno-" + docno);
        //Response.Write("<br>Mrstype-" + Mrstype);
        //////Response.Write("<br>strfrmdt-" + strfrmdt);

        DipsplayReport();


    }

    public void DipsplayReport()
    {

        string[] strArrDtFromTo = strDateRange.Split('-');
        if (mrno == "" && docno == "")
        {
            strfrmdt = strArrDtFromTo[0];
            strtodt = strArrDtFromTo[1];
           // Mrstype = "";
        }
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        // SqlConnection conn = new SqlConnection("data source=202.87.45.67;UID=sa;PWD=s@lc0nnect$;Database=UFL_VER2_TEST3;");

        string sql = "";


        conn.Open();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = conn;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        Branch = Session["brcd"].ToString();
        if (Branch == "HQTR")
        {
            sqlCommand.CommandText = "WEBX_MR_vplist_ho";
            string brcd = Session["brcd"].ToString();
            sqlCommand.Parameters.AddWithValue("@DATEFROM", strfrmdt);
            sqlCommand.Parameters.AddWithValue("@DATETO", strtodt);
            sqlCommand.Parameters.AddWithValue("@MRNO", mrno);
            sqlCommand.Parameters.AddWithValue("@DOCKNO", docno);
            sqlCommand.Parameters.AddWithValue("@DOCKSF", "");
            sqlCommand.Parameters.AddWithValue("@MRTYPE", Mrstype);
            sqlCommand.Parameters.AddWithValue("@brcd", brcd);

        }
        else
        {
            sqlCommand.CommandText = "WEBX_MR_vplist";
            string brcd = Session["brcd"].ToString();
            sqlCommand.Parameters.AddWithValue("@DATEFROM", strfrmdt);
            sqlCommand.Parameters.AddWithValue("@DATETO", strtodt);
            sqlCommand.Parameters.AddWithValue("@MRNO", mrno);
            sqlCommand.Parameters.AddWithValue("@DOCKNO", docno);
            sqlCommand.Parameters.AddWithValue("@DOCKSF", "");
            sqlCommand.Parameters.AddWithValue("@MRTYPE", Mrstype);
            sqlCommand.Parameters.AddWithValue("@brcd", brcd);
        }
        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
        DataSet ds = new DataSet();
        da.Fill(ds);
        intTotalRecords = ds.Tables[0].Rows.Count;
        GV_Booking.DataSource = ds;
        GV_Booking.DataBind();
    }

    public void pgChange(object sender, GridViewPageEventArgs e)
    {
        GV_Booking.PageIndex = e.NewPageIndex;

        DipsplayReport();


    }

    public void lst(object sender, EventArgs e)
    {
        int i = GV_Booking.PageCount;
        GV_Booking.PageIndex = i;

        DipsplayReport();


    }
    public void fst(object sender, EventArgs e)
    {
        GV_Booking.PageIndex = 0;

        DipsplayReport();


    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        string mrno = "";
        string MRsno = "";

       // string[] strArrDtFromTo = strDateRange.Split('-');
        // strfrmdt = strArrDtFromTo[0];
        // strtodt = strArrDtFromTo[1];

        string docketlist = txtdocketlist.Value.ToString();
        string[] docketlist_arr;
        docketlist_arr = docketlist.Split(',');
        string checkeddockno = txtdocketlist_count.Value.ToString();
        int total_checked = Convert.ToInt32(checkeddockno);

        if (total_checked == 1)
        {
            string mr_no = docketlist;
            mrno = "'" + mr_no + "'";
            MRsno = mr_no;
        }
        else
        {

            for (int i = 0; i <= total_checked; i++)
            {
                string mr_no = docketlist_arr[i].ToString();
                if (mrno == "")
                {
                    mrno = "'" + mr_no + "'";
                    MRsno = mr_no;
                }
                else
                {
                    mrno = mrno + "," + "'" + mr_no + "'";
                    MRsno = MRsno + "," + mr_no;
                }

            }
        }
        //Response.Write("<br>billno_nos - " + billno_nos);
        //Response.End();
        Response.Redirect("UpdateMR_step3.aspx?MRsno=" + MRsno + "&fromdt=" + strfrmdt + "&Todt=" + strtodt + "&Mrstype_desc=" + Mrstype_desc + "&Mrstype=" + Mrstype);
   
    }


    protected void GV_Booking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime d1;
            string s = ((Label)(e.Row.FindControl("lblmrsdt"))).Text;
            if (s != "" && s != null)
            {
                d1 = Convert.ToDateTime(s);
                ((Label)(e.Row.FindControl("lblmrsdt"))).Text=d1.ToString("dd MMM yy");
            }
            s = "";
            s = ((Label)(e.Row.FindControl("lblcbsdt"))).Text;
            if (s != "" && s != null)
            {
                d1 = Convert.ToDateTime(s);
                ((Label)(e.Row.FindControl("lblcbsdt"))).Text = d1.ToString("dd MMM yy");
            }
            s = "";
            s = ((Label)(e.Row.FindControl("lblmrstype"))).Text;
            //((LinkButton)(e.Row.FindControl("lnkpop"))).Attributes.Add("onclick", "return OpenInWindow('" + DataBinder.Eval (e.Row.DataItem,"mrsno") + "')");
            if (s == "1")
            {
                ((Label)(e.Row.FindControl("lblmrstype"))).Text = "Paid MR";
                //((LinkButton)(e.Row.FindControl("lnkpop"))).Visible = false;
                ((Label)(e.Row.FindControl("lblpop"))).Visible = true;
            }
            else if (s == "2")
            {
                ((Label)(e.Row.FindControl("lblmrstype"))).Text = "Bill MR";
                //((LinkButton)(e.Row.FindControl("lnkpop"))).Visible = true;
                ((Label)(e.Row.FindControl("lblpop"))).Visible = true;
            }
            else if (s == "3")
            {
                ((Label)(e.Row.FindControl("lblmrstype"))).Text = "ToPay MR";
                //((LinkButton)(e.Row.FindControl("lnkpop"))).Visible = false;
                ((Label)(e.Row.FindControl("lblpop"))).Visible = true;
            }
        }

    }
}
