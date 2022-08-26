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

public partial class GUI_Admin_Vendor_contract_vendorContract_mileagebasis : System.Web.UI.Page
{

    int  srno, vehtype = 0;
    decimal slabfrom, Slabto, slabrate = 0, slabvalue = 0;
    public static string vendorCode, ContID, Flag, docName, Chargetype, matrixcode, Matrixdesc, calmethod, calmethod1, kmrs, slabbasis = "Per Km.", chargebasis, Fixed_YN, Rate_YN, chrgbasis = "Per trip";
    public int sno = 0;
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {

        Flag = Request.QueryString.Get("Flag");
        vendorCode = Request.QueryString.Get("vendorCode");
        ContID = Request.QueryString.Get("ContID");
        cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        if (!IsPostBack)
        {
            //Inetialized();
            //BindGrid();
            //// popAgnt();
            ////Validation();
            //intetilisedrows();
            //roww.Text = "5";

            txtmaxchrg.Text = "0.00";
            txtminchrg.Text = "0.00";
            if (Flag == "Y")
            {
                kmrs = "KM";
                RetriveData();
            }
            
        }
        
        

        txtmaxchrg.Attributes.Add("onblur", "javascript:return Check_num(this)");
        txtminchrg.Attributes.Add("onblur", "javascript:return Check_num(this)");
       
    }
    private void RetriveData()
    {
        // SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();

        String sql_edit = "select slabfrom,slabto,docname from webx_vendor_contract_charges with(NOLOCK) where vendorcode='" + vendorCode + "' and chargetype='Fixed' and matrixcode='9'";
        SqlCommand cmd3 = new SqlCommand(sql_edit, cn);
        SqlDataReader dr = cmd3.ExecuteReader();
        //String mcode = "";
        while (dr.Read())
        {
            //txtaddwtchrg.Text = dr["otherchrg"].ToString();
            // mcode = dr["matrixcode"].ToString();
            cboapptrans.SelectedValue = dr["docname"].ToString();
            txtminchrg.Text = dr["slabfrom"].ToString();
            txtmaxchrg.Text = dr["slabto"].ToString();
        }
        dr.Close();

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        int a, b;
        string SQL_MAXSRNO = "select srno from webx_vendor_contract_matrix where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' and matrixcode='9'";

        SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();
        SqlCommand MAXSRNO = new SqlCommand(SQL_MAXSRNO, cn);

        a = Convert.ToInt32(MAXSRNO.ExecuteScalar());

        string SQL_MAXSRNO1 = "select max(srno) from webx_vendor_contract_matrix";
        SqlCommand MAXSRNO1 = new SqlCommand(SQL_MAXSRNO1, cn);

        b = Convert.ToInt32(MAXSRNO1.ExecuteScalar());


        if (Flag == "Y")
        {
            srno = b;
        }
        else
        {
            if (b == null)
            {
                srno = 1;
            }
            else
            {
                srno = b + 1;
            }
        }


        docName = Convert.ToString(cboapptrans.SelectedValue);
        Chargetype = "NA";
        matrixcode = "9";
        Matrixdesc = "Mileage matrix";
        decimal OtherChrg = 0;
        calmethod = "Higher";
        //OtherChrg = Convert.ToDecimal(txtaddwtchrg.Text);

        String SQL_Insert_update, SQL_Delete, SQL_Delete1, SQL_Delete2, SQL_update_vendor, SQL_update_vendor_contract;
        SqlTransaction trans;
        trans = cn.BeginTransaction();
        try
        {
            if (Flag == "N")
            {
                SQL_Insert_update = "exec webx_vendor_contract_matrix_InsertData '" + vendorCode + "','" + matrixcode + "','" + Matrixdesc + "','" + Chargetype + "','" + docName + "','" + ContID + "'," + OtherChrg + "," + srno + ",'" + calmethod + "'";

            }
            else
            {
                SQL_Insert_update = "exec webx_vendor_contract_matrix_updateData " + srno + ",'" + matrixcode + "','" + Matrixdesc + "','" + Chargetype + "','" + docName + "'," + OtherChrg + ",'" + calmethod + "'";

                SQL_Delete = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','" + matrixcode + "','" + ContID + "'";
                //SQL_Delete1 = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','2','" + ContID + "'";
                //SQL_Delete2 = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','1','" + ContID + "'";

                SqlCommand sqlcmd1 = new SqlCommand(SQL_Delete, cn);
                sqlcmd1.Transaction = trans;
                sqlcmd1.ExecuteNonQuery();
                //SqlCommand sqlcmd2 = new SqlCommand(SQL_Delete1, cn);
                //sqlcmd2.Transaction = trans;
                //sqlcmd2.ExecuteNonQuery();
                //SqlCommand sqlcmd3 = new SqlCommand(SQL_Delete2, cn);
                //sqlcmd3.Transaction = trans;
                //sqlcmd3.ExecuteNonQuery();
            }
            SqlCommand sqlcmd = new SqlCommand(SQL_Insert_update, cn);
            sqlcmd.Transaction = trans;
            sqlcmd.ExecuteNonQuery();


            //insert_data();

            if (calmethod == "Fixed" || calmethod == "Both" || calmethod == "Higher")
            {
                slabfrom = Convert.ToDecimal(txtminchrg.Text);
                Slabto = Convert.ToDecimal(txtmaxchrg.Text);
                slabrate = 0;

                chargebasis = Convert.ToString(txtchrgbasis.Value);
                Fixed_YN = "N";
                Rate_YN = "N";
                //calmethod1 = Convert.ToString(cbokmrs.SelectedValue);
                slabvalue = 0;
                vehtype = 0;
                calmethod1 = "NA";
                String sql_insertDetails = "exec webx_vendor_contract_charges_insertData '" + vendorCode + "','" + matrixcode + "'," + slabfrom + "," + Slabto + "," + slabvalue + "," + slabrate + ",'" + chargebasis + "','" + docName + "','" + Fixed_YN + "','" + Rate_YN + "','" + ContID + "','" + vehtype + "'," + srno + ",0,0,'NA','" + calmethod1 + "','Fixed'";

                Response.Write(" <br> sql_insertDetails : " + sql_insertDetails);
                SqlCommand insertDetails = new SqlCommand(sql_insertDetails, cn);
                insertDetails.Transaction = trans;
                insertDetails.ExecuteNonQuery();
            }


            SQL_update_vendor = "update webx_vendor_hdr set contract_YN='Y' where VendorCode='" + vendorCode + "'";
            SqlCommand update_vendor = new SqlCommand(SQL_update_vendor, cn);
            update_vendor.Transaction = trans;
            update_vendor.ExecuteNonQuery();

            SQL_update_vendor_contract = "update webx_vendor_contract_summary set complete_YN='Y' where VendorCode='" + vendorCode + "' and CONTRACTCD='" + ContID + "'";
            SqlCommand update_vendor_contract = new SqlCommand(SQL_update_vendor_contract, cn);
            update_vendor_contract.Transaction = trans;
            update_vendor_contract.ExecuteNonQuery();

            trans.Commit();

            cn.Close();

        }
        catch (Exception e1)
        {
            Response.Write("<font class='redfnt' >Error : " + e1.Message + "</font>");

            trans.Rollback();


            cn.Close();
            Response.End();
        }

        string final;
        final = "?ContID=" + ContID;
        final += "&Flag=" + Flag;
        final += "&vendorCode=" + vendorCode;

        Response.Redirect("vendorContract_Done.aspx" + final);
    }


    //protected void insert_data()
    //{
    //    SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
    //    cn.Open();

    //    if (calmethod == "Fixed" || calmethod == "Both" || calmethod == "Higher")
    //    {
    //        slabfrom = Convert.ToDecimal(txtminchrg.Text);
    //        Slabto = Convert.ToDecimal(txtmaxchrg.Text);
    //        slabrate = 0;

    //        chargebasis = Convert.ToString(txtchrgbasis.Value);
    //        Fixed_YN = "N";
    //        Rate_YN = "N";
    //        //calmethod1 = Convert.ToString(cbokmrs.SelectedValue);
    //        slabvalue = 0;
    //        vehtype = 0;
    //        calmethod1 = "NA";
    //        String sql_insertDetails = "exec webx_vendor_contract_charges_insertData '" + vendorCode + "','" + matrixcode + "'," + slabfrom + "," + Slabto + "," + slabvalue + "," + slabrate + ",'" + chargebasis + "','" + docName + "','" + Fixed_YN + "','" + Rate_YN + "','" + ContID + "','" + vehtype + "'," + srno + ",0,0,'NA','" + calmethod1 + "','Fixed'";
                                    
    //        Response.Write(" <br> sql_insertDetails : " + sql_insertDetails);
    //        SqlCommand insertDetails = new SqlCommand(sql_insertDetails, cn);
    //        insertDetails.ExecuteNonQuery();
    //    }
    //    cn.Close();
    //}
}
