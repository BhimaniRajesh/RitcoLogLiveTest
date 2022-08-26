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

public partial class GUI_UNI_MIS_Login_View_Login_2 : System.Web.UI.Page
{

    public static string strDateRange, strloc, stremp, strlogininstence, strreptype;
    public static DataSet ds1 = new DataSet();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Date Range display preparation
            strDateRange = Request.QueryString["strDateRange"].ToString();
            strloc = Request.QueryString["strloc"].ToString();
            stremp = Request.QueryString["stremp"].ToString();
            strlogininstence = Request.QueryString["strlogininstence"].ToString();
            strreptype = Request.QueryString["strreptype"].ToString();
            

        }


        // lables......




        lblseldet.Text = strDateRange;
        lblloc.Text = strloc;
        lblemp.Text = stremp;
        lblli.Text = strlogininstence;
       

       

        DipsplayReport();


    }

    public void DipsplayReport()
    {
        ds1.Clear();
        string[] strArrDtFromTo = strDateRange.Split('-');
        string strfrmdt= strArrDtFromTo[0];
        string strtodt= strArrDtFromTo[1];
        string locval = "";
        string empval ="";
        string lival = "";
        if (strloc != "")
        {
            lblloc.Text = strloc;
            locval = "and CURRBRCD='" + strloc +"'";
        }
        else
        {
         lblloc.Text = "All";
        }

        if(stremp != "")
        {
            lblemp.Text = stremp;
            empval= "and empcd='" + stremp+"'";
        }
        else
        {
            lblemp.Text = "All";
        }

        if (strlogininstence != "")
        {
            lblli.Text = strlogininstence;
            lival = "and duration >=" + strlogininstence + "";
        }
        else
        {
            lblli.Text = "0";
        }
            

       // SqlConnection conn = new SqlConnection("data source=202.87.45.71;UID=sa;PWD=eCfY$@p@$$;Database=RCPL_Ver2_Live;");
     SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
    //    SqlConnection conn = new SqlConnection("data source=202.87.45.71;UID=sa;PWD=$rcpl@@p@$$;Database=RCPL_Ver2_Live;");

        string sql="";
        //int li;
        //li= Convert(Int32,strlogininstence.ToString());

          sql = "select EMPCD,EMPNM,LocName,COUNT(*) AS NOOFLOGIN from webx_login where  convert(varchar,LASTLOGINDT,106)  between convert(datetime,'" + strfrmdt + "',106) and convert(datetime,'" + strtodt + "',106)  " + locval + empval + lival+"" + " group by EMPCD,EMPNM,LocName order by LocName";

            conn.Open();

            SqlCommand sqlCommand = new SqlCommand(sql, conn);



            try
            {

                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);

                
                sqlDA.Fill(ds1);

                GridViewHelper helper = new GridViewHelper(this.GV_S);
                string[] cols = new string[1];
                cols[0] = "LocName";
                //cols[1] = "ShipName";
                helper.RegisterGroup(cols, true, true);
                
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                //helper.RegisterSummary("tot1", SummaryOperation.Sum);

                
        

                GV_S.DataSource = ds1;


            }

            catch (Exception eee)
            {
               

                lblnull.Visible = true;
                lblnull.Text = eee.Message;

            }



            GV_S.DataBind();
            if (GV_S.Rows.Count == 0)
            {
                //lblnull.Visible = true;
                //lblnull.Text = "No Records Found For Given Criteria";
            }

      
    }
    public void pgChange(object sender, GridViewPageEventArgs e)
    {
        GV_S.PageIndex = e.NewPageIndex;

        DipsplayReport();


    }

    public void lst(object sender, EventArgs e)
    {
        int i=GV_S.PageCount;
        GV_S.PageIndex = i;

         DipsplayReport();


    }
    public void fst(object sender, EventArgs e)
    {
        GV_S.PageIndex = 0;

         DipsplayReport();


    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "LocName")
        {
            //row.BackColor = System.Drawing.Color.FromName("#D4E0E7");
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.CssClass = "blackfnt";
            row.BackColor = System.Drawing.Color.White;
            row.Font.Bold = true;
           
            

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
            GV_S.AllowPaging = false;

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;Filename=Login_View.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DipsplayReport();
            GV_S.RenderControl(hw);


            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex1)
        {
            throw ex1;
            // lblloc.Text = ex1.Message;
        }

    }


}
