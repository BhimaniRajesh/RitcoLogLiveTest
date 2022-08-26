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
using System.Globalization;
using System.Data.SqlClient;
using System.IO;

public partial class GUI_UNI_MIS_Account_Reports_TrailBalance_ver3_Result : System.Web.UI.Page
{
    SqlConnection Conn;
    public string SQL_TB,FLG="N";
    public static int intTotalRecords = 0;
    string fincmplbr = "", Financial_Year = "", fin_year2 = "", fin_year1 = "", fin_year = "",TableName2="", TableName1 = "", TableName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string RMD;
        
        Financial_Year = Session["FinYear"].ToString().Substring(2, 2);
        fin_year = Session["FinYear"].ToString();
        double fin_year_next = Convert.ToDouble(Financial_Year) + 1;
        double fin_year_next1 = Convert.ToDouble(Financial_Year) - 1;
        fin_year = Session["FinYear"].ToString().Substring(2, 2) + "-" + fin_year_next.ToString().PadLeft(2, '0');
        fin_year1 = Session["FinYear"].ToString().Substring(2, 2) + "_" + fin_year_next.ToString().PadLeft(2, '0');
        fin_year2 = fin_year_next1.ToString().PadLeft(2, '0') + "_" + Session["FinYear"].ToString().Substring(2, 2);

        TableName = "webx_acctopening_employee_" + fin_year1;
        TableName1 = "webx_acctopening_" + fin_year1;
        TableName2 = "webx_acctopening_employee_" + fin_year2;
        LblNote.Text = " Note : Following Transaction Are Comming From Fin Year-" + fin_year2.Replace("_", "-") + " And It Will Set Opening Balance for Fin Year " + fin_year;
        if (!IsPostBack)
        {
            Button2.Text = "Set Opening Balance For Year " + fin_year;
            RMD = Request.QueryString["RMD"].ToString();

            if (RMD == "N")
            {
                BindGrid();
            }
            else
            {
                BindGrid();
                DWN_XLS();

            }
        }
        Button2.Attributes.Add("onclick", "javascript:return onSubmit()");
    }
    public void BindGrid()
    {
        Conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        Conn.Open();

        string RO = "", LO = "", DateType = "", fromdt = "", todt = "", RPT = "", ST_CAT = "";
        string ST_ACC = "", ACC = "", st_cust_vend = "", st_cust_vend_name = "", rpt_SUB = "";

        RO = Request.QueryString["RO"].ToString();
        LO = Request.QueryString["LO"].ToString();
        DateType = Request.QueryString["DateType"].ToString();
        fromdt = Request.QueryString["fromdt"].ToString();
        todt = Request.QueryString["todt"].ToString();
        RPT = Request.QueryString["RPT"].ToString();
        ST_CAT = Request.QueryString["ST_CAT"].ToString();
        ST_ACC = Request.QueryString["ST_ACC"].ToString();
        ACC = Request.QueryString["ACC"].ToString();
        st_cust_vend = Request.QueryString["st_cust_vend"].ToString();
        st_cust_vend_name = Request.QueryString["st_cust_vend_name"].ToString();
        rpt_SUB = Request.QueryString["rpt_SUB"].ToString();

        LBLDATE.Text = fromdt + " - " + todt;
        LBLBRANCH.Text = RO;
        LBLRPT.Text = LO;
        lblGrp.Text = ST_CAT;
        LBLSUBRPT.Text = ST_ACC;
        lblCust_vend.Text = st_cust_vend_name;

        if (RPT == "3")
        {
            lblCust_vend_typ.Text = "Customer";
        }
        else if (RPT == "4")
        {
            lblCust_vend_typ.Text = "Vendor";
        }
        else if (RPT == "5")
        {
            lblCust_vend_typ.Text = "Employee";
        } 
        else if (RPT == "6")
        {
            lblCust_vend_typ.Text = "Driver";
        }
        else 
        {
            lblCust_vend_typ.Text = "Customer/Vendor/Employee/Driver";
        }
   

        string YearString = Session["FinYear"].ToString().Substring(2, 2);
        string CloseYear = Convert.ToString((Convert.ToInt16(YearString) + 1));
        if (CloseYear.Length == 1)
        {
            CloseYear = "0" + CloseYear;
        }
        DateTime yrstartdt;
        string yearSuffix = Convert.ToString(YearString) + "_" + Convert.ToString(CloseYear);
        SqlTransaction trans;
        trans = Conn.BeginTransaction();

        try
        {

            SQL_TB = "exec USP_BalanceTransfer_Vendor_Customer '" + fromdt + "','" + todt + "','" + LO + "','All','" + ST_CAT + "','All','" + st_cust_vend + "','" + RPT + "','" + rpt_SUB + "','" + fin_year2 + "'";
            //Response.Write("<BR> SQL_TB : " + SQL_TB);
           // Response.End();
             SqlCommand sqlcmd12 = new SqlCommand(SQL_TB, Conn);
            sqlcmd12.Transaction = trans;

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd12);
            DataSet ds = new DataSet();
            da.Fill(ds, "tab");
            GV_Records.DataSource = ds;
            intTotalRecords = ds.Tables[0].Rows.Count;
            GV_Records.DataBind();
             

            trans.Commit();
            
        }
        catch (Exception e1)
        {
            Response.Write("<br><br><br><font class='blackboldfnt' >Error : " + e1.Message + "</font>");
            if (e1.Message == "")
            {
                // abc = "Insert Susccessfully !!!!!!!";

            }
            Response.Write("<BR> SQL_TB : " + SQL_TB);
            trans.Rollback();
            Conn.Close();
            Response.End();
        }

        Conn.Close();

    }

    public void DWN_XLS()
    {
        
        //TBL_CRI.Border = 1;

        //TBL_TB.BorderWidth = 1;
        //TBL_TB.BorderColor = System.Drawing.Color.Gray;
        //TBL_TB.Font.Name = "verdana";
        //TBL_TB.Font.Size = 8;
        
       
        //    //BindGrid();
       
        //StringWriter stringWrite;
        //HtmlTextWriter htmlWrite;
        //HtmlForm frm = new HtmlForm();
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=TB.xls");
        //Response.Charset = "";
       
        //Response.ContentType = "application/vnd.ms-xls";
        //stringWrite = new System.IO.StringWriter();
        //htmlWrite = new HtmlTextWriter(stringWrite);
        //Controls.Add(frm);
        
        //frm.Controls.Add(TBL_CRI);
        //frm.Controls.Add(TBL_TB);
        //frm.RenderControl(htmlWrite);
        //Response.Write(stringWrite.ToString());
        //Response.End();
    }

    protected void btn_csv_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();
        SqlTransaction trans;
        trans = conn.BeginTransaction();
        SqlDataReader dr_ind;
        string sql_ind="";
        try
        {
            string AccountCode = "", Accountdesc = Request.QueryString["st_cust_vend"].ToString(), sql = "", Loccode = "";
            int i = 0;
            double opendebit = 0, opencredit = 0;
            foreach (GridViewRow gr in GV_Records.Rows)
            {

                if (((CheckBox)gr.FindControl("Chkcust")).Checked == true)
                {
                    string opendebitstr = ((TextBox)gr.FindControl("TxtDebit")).Text.ToString();
                    string opencreditstr = ((TextBox)gr.FindControl("TxtCredit")).Text.ToString();
                    Loccode = ((HiddenField)gr.FindControl("hnd_Loccode")).Value.ToString();
                    if (opendebitstr == "" || opendebitstr == null)
                    {
                        opendebitstr = "0.00";
                    }
                    if (opencreditstr == "" || opencreditstr == null)
                    {
                        opencreditstr = "0.00";
                    }
                    opendebit = Convert.ToDouble(opendebitstr);
                    opencredit = Convert.ToDouble(opencreditstr);
                    AccountCode = ((HiddenField)gr.FindControl("hnd_Acccode")).Value;

                    sql = "delete from  " + TableName1 + " where  acccode='" + AccountCode + "' and brcd='" + Loccode + "'   ";
                    Response.Write("<br>sql-" + sql);
                    SqlCommand cmdUpdate = new SqlCommand(sql, conn, trans);
                    cmdUpdate.ExecuteNonQuery();

                    sql = "insert into " + TableName1 + "   VALUES (0,0,'" + Session["Finyear"].ToString() + "','" + AccountCode + "','" + Loccode + "'," + opendebit + "," + opencredit + ",null,getdate())  ";
                    Response.Write("<br>sql-" + sql);
                    cmdUpdate = new SqlCommand(sql, conn, trans);
                    cmdUpdate.ExecuteNonQuery();

                     
                }
                
            }
            trans.Commit();
            conn.Close();
            Response.Redirect("../OpeningBalance/SelectBranch.aspx");
        }
        catch (Exception e1)
        {
            Response.Write("<br><br><br><font class='blackboldfnt' >Error : " + e1.Message + "</font>");
            if (e1.Message == "")
            {
                string abc = "Insert Susccessfully !!!!!!!";
            }
            else
            {
                trans.Rollback();
                conn.Close();
            }

            Response.End();
        }
    }
}
