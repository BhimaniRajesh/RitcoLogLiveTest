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
using System.IO;

public partial class GUI_Finance_Vendor_BA_payment_BA_Payment_Update_rate_commision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        conn.Open();
        string str_dktlist = DKTlist.Text.ToString().Trim();

        string str_sql = "select top 100 dockno+','+isnull(trn_mod,'1')+','+isnull(paybas,'p01')+','+rtrim(DRS_bacode)+','+convert(varchar,isnull(subtotal,1),106) +','+convert(varchar,isnull(actwt,1),106)  from webx_BA_calculation with(NOLOCK) where  dockno in  ('" + str_dktlist.Replace(" ", "").Replace(",", "','") + "')";
        //str_sql = "select  dockno+','+isnull(trn_mod,'1')+','+isnull(paybas,'p01')+','+rtrim(DRS_bacode)+','+convert(varchar,isnull(subtotal,1),106) +','+convert(varchar,isnull(actwt,1),106)  from webx_BA_calculation with(NOLOCK) where  drs_bacode='V00895' and slabrate_dely=0";
       //Response.Write("<br> str_VendList : " + str_sql);
     //  Response.End();

       
            SqlTransaction trans;

            trans = conn.BeginTransaction();
            try
            {
                string sql_update = "exec WebxNet_updateDLYRate '" + str_dktlist + "'";
                //string sql_update = "update webx_BA_calculation with(ROWLOCK) set commission_bkg='" + BKG_comminssion.ToString() + "',slabrate_bkg='" + BKG_SLBRate.ToString() + "',lasteditby='10001' where dockno='" + docknno + "'";
                //Response.Write(" <BR> Update : " + sql_update);
                SqlCommand sqlcmd25 = new SqlCommand(sql_update, conn);
                sqlcmd25.Transaction = trans;
                sqlcmd25.ExecuteNonQuery();
                trans.Commit();

                
            }
            catch (Exception e1)
            {
                Response.Write("<br><br><br><font class='blackboldfnt' >Error : " + e1.Message + "</font>");
               
                trans.Rollback();


                conn.Close();
                Response.End();
            }



       

        dgDocket.Visible = true;
        lblsubmit.Visible = true;
        string str_sql1 = "select distinct dockno,slabrate_dely,commission_dely,drs_BACODE,actwt,subtotal from webx_BA_calculation with(NOLOCK) where  dockno in  ('" + str_dktlist.Replace(" ", "").Replace(",", "','") + "') and slabrate_dely>0 and commission_dely>0";
      //  Response.Write(" <BR> Update : " + str_sql1);
        SqlCommand sqlcmd12 = new SqlCommand(str_sql1, conn);
        SqlDataAdapter da = new SqlDataAdapter(sqlcmd12);

        DataSet ds = new DataSet();
        //dgDocket.AllowPaging = false;
        da.Fill(ds);
        dgDocket.DataSource = ds;
        dgDocket.DataBind();


        conn.Close();
    }
}
