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

public partial class GUI_Finance_Vendor_BA_payment_PrepareVoucher_Vendor_ViewTHCAdvance : System.Web.UI.Page
{
    public static string voucherNo;
    public double totnetpayment = 0.00;
    MyFunctions fn = new MyFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            voucherNo = Convert.ToString(Request.QueryString["advvoucher"]);
            string[] Split = voucherNo.Split(new Char[] { ',' });
            voucherNo = Convert.ToString(Split[0]);
            lblVoucherNo.Text = voucherNo;
            string print_yn = Convert.ToString(Split[1]);

            if (Session["Client"].ToString() == "PRRL")
            {
                imgLogo.ImageUrl = "~/GUI/images/clip_image002.gif";
            }
            if (Session["Client"].ToString() == "TLL")
            {
                imgLogo.ImageUrl = "~/GUI/images/TLL_Logo.gif";
            }
            else if (Session["Client"].ToString() == "EXL")
            {
                imgLogo.ImageUrl = "~/GUI/images/exl_logo.jpg";
            }
            else if (Session["Client"].ToString() == "Varuna")
            {
                imgLogo.ImageUrl = "~/GUI/images/Varuna_logoNew.jpg";
            }
            else if (Session["Client"].ToString() == "RITCO")
            {
                imgLogo.ImageUrl = "~/GUI/images/webxpress_logo.gif";
            }
            else if (Session["Client"].ToString() == "RCPL")
            {
                imgLogo.ImageUrl = "~/GUI/images/rcpl_logo.jpg";
            }
            else if (Session["Client"].ToString() == "Agility")
            {
                imgLogo.ImageUrl = "~/GUI/images/Agility lobo.jpg";
            }
            else if (Session["Client"].ToString() == "ASL")
            {
                imgLogo.ImageUrl = "~/GUI/images/ASL_New.bmp";
            }
            else
            {
                imgLogo.ImageUrl = "~/GUI/images/webxpress_logo.gif";
            }

            SqlConnection conn1 = new SqlConnection(Session["SqlProvider"].ToString().Trim());
            conn1.Open();

            string sql1 = "select convert(varchar,advvoucherdt,06) as advvoucherdt, convert(varchar,adv_Chqdate,06) as adv_Chqdate,* from webx_THC_SUMMARY where advvoucherno='" + voucherNo + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn1);
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                lblVoucherDt.Text = dr["advvoucherdt"].ToString();
                //lblVoucherDt.Text = (fn.Mydate1(lblVoucherDt.Text)).ToString();
                lblMode.Text = dr["adv_transtype"].ToString();
                if (lblMode.Text == "Cash")
                {
                    lblPaymentAmt.Text = dr["advamt"].ToString();
                    lblChequeNo.Text = "--";
                    lblChequeDt.Text = "--";
                }
                else
                {
                    lblPaymentAmt.Text = dr["advamt"].ToString();
                    lblChequeNo.Text = dr["adv_Chqno"].ToString();
                    lblChequeDt.Text = dr["adv_Chqdate"].ToString();
                }
                 lblPaymentAcc.Text = dr["adv_Acccode"].ToString();
                 NumberToText m = new NumberToText();
                 lblWords.Text = m.numberToText(lblPaymentAmt.Text);
                //lblChequeNo.Text = dr["adv_Chqno"].ToString();
                //lblChequeDt.Text = dr["adv_Chqdate"].ToString();
                SqlConnection conn_code = new SqlConnection(Session["SqlProvider"].ToString().Trim());
                conn_code.Open();

                string sql_code = "select Accdesc from webx_acctinfo where acccode ='" + lblPaymentAcc.Text + "'";
                SqlCommand sqlcmd1 = new SqlCommand(sql_code, conn_code);
                SqlDataReader dr1;
                dr1 = sqlcmd1.ExecuteReader();
                if (dr1.Read())
                {
                    lblPaymentAcc.Text = dr1["Accdesc"].ToString();
                }
                dr1.Close();
                conn_code.Close();
            }
            dr.Close();


            sql1 = "select top 1 convert(varchar,transdate,06) as advvoucherdt, convert(varchar,Chqdate,06) as adv_Chqdate,* from webx_vouchertrans_arch where voucherno='" + voucherNo + "'";
            cmd1 = new SqlCommand(sql1, conn1);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                lblVoucherDt.Text = dr["advvoucherdt"].ToString();
                lblMode.Text = dr["transtype"].ToString();
                if (lblMode.Text == "Cash")
                {
                    lblPaymentAmt.Text = dr["credit"].ToString();
                    lblChequeNo.Text = "--";
                    lblChequeDt.Text = "--";
                }
                else
                {
                    lblPaymentAmt.Text = dr["credit"].ToString();
                    lblChequeNo.Text = dr["Chqno"].ToString();
                    lblChequeDt.Text = dr["adv_Chqdate"].ToString();
                }
                lblPaymentAcc.Text = dr["Acccode"].ToString();
                NumberToText m = new NumberToText();
                lblWords.Text = m.numberToText(lblPaymentAmt.Text);
                
            }
            dr.Close();
            conn1.Close();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        foreach (GridViewRow gridrow in dockdata.Rows)
        {
            Label TotNet = (Label)gridrow.FindControl("indadvamt");

            if (TotNet.Text != "")
            {
                totnetpayment = totnetpayment + Convert.ToDouble(TotNet.Text);
                TotNet.Text = totnetpayment.ToString();
            }
            else
            {
                totnetpayment = 0.00;
                TotNet.Text = "0.00";
            }
        }
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();

        string sql = "WEBX_Voucher_Detail_m";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;

        da.SelectCommand.Parameters.Add("@voucherno", SqlDbType.VarChar).Value = voucherNo.ToString().Trim();
        DataSet ds = new DataSet();
        da.Fill(ds, "tab1");
        //totdkt = ds.Tables[0].Rows.Count;
        dockdata.DataSource = ds;
        dockdata.DataBind();
        conn.Close();
    }
}
