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

public partial class GUI_Admin_Vendor_contract_vendorContract_longhaulroad_km : System.Web.UI.Page
{
    int _lastEditedPage, srno,vehtype = 0;
    decimal slabfrom, Slabto,MinKM, slabrate=0, slabvalue=0;
    public static string vendorCode, ContID, Flag, docName, Chargetype, matrixcode, Matrixdesc, calmethod,calmethod1, kmrs, slabbasis = "Per Km.",chargebasis,Fixed_YN,Rate_YN, chrgbasis = "Per trip";   
    public int sno = 0;
    SqlConnection cn;
    public DataSet1 _dataSet = new DataSet1();
    protected void Page_Load(object sender, EventArgs e)
    {
        Flag = Request.QueryString.Get("Flag");
        vendorCode = Request.QueryString.Get("vendorCode");
        ContID = Request.QueryString.Get("ContID");
        cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        if (!IsPostBack)
        {
            Inetialized();
            BindGrid();
            // popAgnt();
            //Validation();
            intetilisedrows();
            roww.Text = "5";

            if (Flag == "Y")
            {
                kmrs = "KM";
                //RetriveData();
            }

            txtmaxchrg.Text = "0.00";
            txtminchrg.Text = "0.00";
            txtminvehrate.Text = "0.00";
            if (Flag == "Y")
            {

                RetriveData();
            }
        }




        txtminvehrate.Attributes.Add("onblur", "javascript:return Check_num(this)");
        txtmaxchrg.Attributes.Add("onblur", "javascript:return Check_num(this)");
        txtminchrg.Attributes.Add("onblur", "javascript:return Check_num(this)");
         
    }

     private void RetriveData()
    {
        SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();

        String sql_edit = "select slabfrom,slabto,docname,calmethod,chargebasis,slabrate,slabvalue from webx_vendor_contract_charges where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' and matrixcode='2' and chargetype='Fixed'";
        SqlCommand cmd3 = new SqlCommand(sql_edit, cn);
        SqlDataReader dr = cmd3.ExecuteReader();
        //String mcode = "";
        while (dr.Read())
        {
          //  txtaddwtchrg.Text = dr["otherchrg"].ToString();
            // mcode = dr["matrixcode"].ToString();
            txtminKM.Text = dr["slabvalue"].ToString();
            txtminvehrate.Text = dr["slabrate"].ToString();
            cboapptrans.SelectedValue = dr["docname"].ToString();
            txtminchrg.Text = dr["slabfrom"].ToString(); 
            txtmaxchrg.Text = dr["slabto"].ToString();
            cbokmrs.SelectedValue = dr["calmethod"].ToString();
            cbochrgbasis.SelectedValue = dr["chargebasis"].ToString();
        }
        dr.Close();
        String sql_edit1 = "select Distinct vehtype from webx_vendor_contract_charges where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' and matrixcode='2' and chargetype='Variable'   ";
        SqlCommand cmd31 = new SqlCommand(sql_edit1, cn);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd31);
        da.Fill(ds, "Main");

        string vehtype_data = "", routecd_data_frm = "", routecd_data_to = "";
        sno = ds.Tables[0].Rows.Count;

        DataTable dt = new DataTable();

        //for (int j = 0; j < sno; j++)
        //{
        SqlDataReader dr1 = cmd31.ExecuteReader();
        String abcd1 = "", abcd2 = "", abcd3 = "";

        while (dr1.Read())
        {
            String Temp1 = dr1["vehtype"].ToString();
            if (Temp1 != abcd1)
            {

                // String Temp2 = dr2["slabto"].ToString();
                if (vehtype_data == "")
                {

                    vehtype_data = Convert.ToString(dr1["vehtype"]);
                }
                else
                {
                    vehtype_data = vehtype_data + "~" + Convert.ToString(dr1["vehtype"]);
                }

                abcd1 = dr1["vehtype"].ToString();

            }




        }

        dr1.Close();


        String sql_edit12 = "select Distinct slabfrom,slabto from webx_vendor_contract_charges where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' and matrixcode='2' and chargetype='Variable'  ";
        SqlCommand cmd312 = new SqlCommand(sql_edit12, cn);
        SqlDataReader dr12 = cmd312.ExecuteReader();
        while (dr12.Read())
        {

            String Temp2 = dr12["slabfrom"].ToString();
            String Temp3 = dr12["slabto"].ToString();
            if (Temp2 != abcd2 && Temp3 != abcd3)
            {

                // String Temp2 = dr2["slabto"].ToString();
                if (routecd_data_frm == "" && routecd_data_to=="")
                {

                    routecd_data_frm = Convert.ToString(dr12["slabfrom"]);
                    routecd_data_to = Convert.ToString(dr12["slabto"]);
                }
                else
                {

                    routecd_data_frm = routecd_data_frm + "~" + Convert.ToString(dr12["slabfrom"]);
                    routecd_data_to = routecd_data_to + "~" + Convert.ToString(dr12["slabto"]);
                }

                abcd2 = dr12["slabfrom"].ToString();
                abcd3 = dr12["slabto"].ToString();

            }
        }
        dr12.Close();
        //vehtypedata_str = "";
        //while (dr1.Read())
        //{

        //    vehtypedata_str = dr1["slabvalue"].ToString() + "~" + dr1["vehtype"].ToString() + "~" + dr1["routecd"].ToString();
        //    if (vehtype_data == "")
        //    {
        //        vehtype_data = vehtypedata_str;
        //    }
        //    else
        //    {
        //        vehtype_data = vehtype_data + "#" + vehtypedata_str;
        //    }

        //}
        //dr1.Close();
        // }
        string[] vehtype_data_arr = vehtype_data.Split('~');

        string[] routecd_data_arr_frm = routecd_data_frm.Split('~');
        string[] routecd_data_arr_to = routecd_data_to.Split('~');
        int v1 = vehtype_data_arr.Length;
        int Rl = routecd_data_arr_frm.Length;

        int i = 0;
        int j = 0;
        foreach (GridViewRow gridrow in GV_D.Rows)
        {
            //DropDownList Route = (DropDownList)gridrow.FindControl("ddRoute");
            TextBox txtfromval = (TextBox)gridrow.FindControl("txtfromval");
            TextBox txttoval = (TextBox)gridrow.FindControl("txttoval");

            DropDownList Vehtype0 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype");
            DropDownList Vehtype1 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype1");
            DropDownList Vehtype2 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype2");
            DropDownList Vehtype3 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype3");
            DropDownList Vehtype4 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype4");


            TextBox txtchrg0 = (TextBox)gridrow.FindControl("txtchrg");
            TextBox txtchrg1 = (TextBox)gridrow.FindControl("txtchrg1");
            TextBox txtchrg2 = (TextBox)gridrow.FindControl("txtchrg2");
            TextBox txtchrg3 = (TextBox)gridrow.FindControl("txtchrg3");
            TextBox txtchrg4 = (TextBox)gridrow.FindControl("txtchrg4");
            string temp = "", temp1 = "";
            string[] arry;
            //arry[0]="none";

            if (i < Rl)
            {
                //Route.SelectedValue = routecd_data_arr[i];
                txtfromval.Text = routecd_data_arr_frm[i];
                txttoval.Text = routecd_data_arr_to[i];

                String from_cd = routecd_data_arr_frm[i];
                if (from_cd == "")
                {
                    txtfromval.Text = "0.00";
                }

                String to_cd = routecd_data_arr_to[i];
                if (from_cd == "")
                {
                    txttoval.Text = "0.00";
                }


                String Vehtypno;

                for (int vi = 0; vi < v1; vi++)
                {
                    Vehtypno = vehtype_data_arr[vi];
                    if (vi == 0)
                    {
                        Vehtype0.SelectedValue = vehtype_data_arr[vi];

                    }
                    if (vi == 1)
                    {
                        Vehtype1.SelectedValue = vehtype_data_arr[vi];
                    }
                    if (vi == 2)
                    {
                        Vehtype2.SelectedValue = vehtype_data_arr[vi];
                    }
                    if (vi == 3)
                    {
                        Vehtype3.SelectedValue = vehtype_data_arr[vi];
                    }
                    if (vi == 4)
                    {
                        Vehtype4.SelectedValue = vehtype_data_arr[vi];
                    }

                    String sql_edit3 = "select  vehtype,slabrate as slabvalue,routecd from webx_vendor_contract_charges where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' and matrixcode='2'  and slabfrom='" + from_cd + "' and slabto='" + to_cd + "' and convert(varchar,vehtype,106)='" + Vehtypno + "'  order by chargeid";
                    SqlCommand cmd33 = new SqlCommand(sql_edit3, cn);
                    SqlDataReader dr3 = cmd33.ExecuteReader();

                    while (dr3.Read())
                    {
                        if (vi == 0)
                        {
                            txtchrg0.Text = Convert.ToString(dr3["slabvalue"]);
                        }
                        if (vi == 1)
                        {
                            txtchrg1.Text = Convert.ToString(dr3["slabvalue"]);
                        }
                        if (vi == 2)
                        {
                            txtchrg2.Text = Convert.ToString(dr3["slabvalue"]);
                        }
                        if (vi == 3)
                        {
                            txtchrg3.Text = Convert.ToString(dr3["slabvalue"]);
                        }
                        if (vi == 4)
                        {
                            txtchrg4.Text = Convert.ToString(dr3["slabvalue"]);
                        }

                    }

                    dr3.Close();
                }



                txtchrg0.Attributes.Add("onblur", "javascript:return Check_num(this)");
                txtchrg1.Attributes.Add("onblur", "javascript:return Check_num(this)");
                txtchrg2.Attributes.Add("onblur", "javascript:return Check_num(this)");
                txtchrg3.Attributes.Add("onblur", "javascript:return Check_num(this)");
                txtchrg4.Attributes.Add("onblur", "javascript:return Check_num(this)");


                i = i + 1;
            }







        }
    }


    private void Inetialized()
    {
        _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");
        _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");
        _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");
        _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");
        _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");


    }

    protected void roww_TextChanged(object sender, EventArgs e)
    {
        _dataSet.WorkExperianceData.Clear();
        int rowno;
        rowno = Convert.ToInt32(roww.Text);
        for (int i = 0; i < rowno; i++)
        {
            _dataSet.WorkExperianceData.AddWorkExperianceDataRow("", "", "", "", "", "", "", "", "", "", "");
            BindGrid();
            intetilisedrows();
        }
        // Validation();
    }
    private void BindGrid()
    {
        GV_D.DataSource = _dataSet.WorkExperianceData;
        GV_D.DataBind();
        _lastEditedPage = GV_D.PageIndex;
    }

    private void intetilisedrows()
    {

        cn.Open();
        foreach (GridViewRow gridrow in GV_D.Rows)
        {
            //DropDownList Route = (DropDownList)gridrow.FindControl("ddRoute");
            //DropDownList Vehtype = (DropDownList)gridrow.FindControl("ddVehtype");

            TextBox txtfromval = (TextBox)gridrow.FindControl("txtfromval");
            TextBox txttoval = (TextBox)gridrow.FindControl("txttoval");

            TextBox txtchrg = (TextBox)gridrow.FindControl("txtchrg");
            TextBox txtchrg1 = (TextBox)gridrow.FindControl("txtchrg1");
            TextBox txtchrg2 = (TextBox)gridrow.FindControl("txtchrg2");
            TextBox txtchrg3 = (TextBox)gridrow.FindControl("txtchrg3");
            TextBox txtchrg4 = (TextBox)gridrow.FindControl("txtchrg4");

           

            //St.Items.Add("--Select--");

            //string sqlstr = "select '--Select--' as rutnm,'' as rutcd from webx_rutmas  union select (rutcd +' : '+rutnm) as rutnm,rutcd from webx_rutmas where rutcat='L' and activeFlag='Y' order by [rutnm]";
            //SqlCommand cmd = new SqlCommand(sqlstr, cn);
            //SqlDataReader dr = cmd.ExecuteReader();

            //while (dr.Read())
            //{

            //    Route.Items.Add(new ListItem(dr.GetValue(0).ToString(), dr.GetValue(1).ToString()));
            //}

            //dr.Close();

            txtfromval.Text = "0.00";
            txttoval.Text = "0.00";

            txtchrg.Text = "0.00";
            txtchrg1.Text = "0.00";
            txtchrg2.Text = "0.00";
            txtchrg3.Text = "0.00";
            txtchrg4.Text = "0.00";

            txtfromval.Attributes.Add("onblur", "javascript:return Check_num(this)");
            txttoval.Attributes.Add("onblur", "javascript:return Check_num(this)");

            txtchrg.Attributes.Add("onblur", "javascript:return Check_num(this)");
            txtchrg1.Attributes.Add("onblur", "javascript:return Check_num(this)");
            txtchrg2.Attributes.Add("onblur", "javascript:return Check_num(this)");
            txtchrg3.Attributes.Add("onblur", "javascript:return Check_num(this)");
            txtchrg4.Attributes.Add("onblur", "javascript:return Check_num(this)");

            //string sqlstr2 = "select '--Select--' as Type_Name,'' as Type_code from webx_vehicle_type  union select Type_Name=(convert(varchar,Type_code,106)+' : '+[Type_Name]),Type_code=convert(varchar,Type_code,106) from webx_vehicle_type where  activeFlag='Y' order by [Type_code]";
            //SqlCommand cmd1 = new SqlCommand(sqlstr2, cn);
            //SqlDataReader dr1 = cmd1.ExecuteReader();

            //while (dr1.Read())
            //{

            //    Vehtype.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
            //}

            //dr1.Close();



        }


        cn.Close();
    }
    protected void GV_D_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList Vehtype = (DropDownList)e.Row.FindControl("ddVehtype");
            DropDownList Vehtype1 = (DropDownList)e.Row.FindControl("ddVehtype1");
            DropDownList Vehtype2 = (DropDownList)e.Row.FindControl("ddVehtype2");
            DropDownList Vehtype3 = (DropDownList)e.Row.FindControl("ddVehtype3");
            DropDownList Vehtype4 = (DropDownList)e.Row.FindControl("ddVehtype4");
            string sqlstr2 = "select '--Select--' as Type_Name,0 as Type_code from webx_vehicle_type  union select Type_Name=(convert(varchar,Type_code,106)+' : '+[Type_Name]),Type_code=convert(varchar,Type_code,106) from webx_vehicle_type where  activeFlag='Y' order by [Type_code]";
            cn.Open();
            SqlCommand cmd1 = new SqlCommand(sqlstr2, cn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            Vehtype.Items.Clear();
            Vehtype1.Items.Clear();
            Vehtype2.Items.Clear();
            Vehtype3.Items.Clear();
            Vehtype4.Items.Clear();

            while (dr1.Read())
            {

                Vehtype.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
                Vehtype1.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
                Vehtype2.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
                Vehtype3.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
                Vehtype4.Items.Add(new ListItem(Convert.ToString(dr1[0]), Convert.ToString(dr1[1])));
            }
            dr1.Close();
            cn.Close();
        }
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
        int a, b;
        string SQL_MAXSRNO = "select srno from webx_vendor_contract_matrix where vendorcode='" + vendorCode + "' and contractcd='" + ContID + "' ";//and matrixcode='2'";

        SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();
        SqlCommand MAXSRNO = new SqlCommand(SQL_MAXSRNO, cn);

        a = Convert.ToInt32(MAXSRNO.ExecuteScalar());

        string SQL_MAXSRNO1 = "select max(srno) from webx_vendor_contract_matrix";
        SqlCommand MAXSRNO1 = new SqlCommand(SQL_MAXSRNO1, cn);

        b = Convert.ToInt32(MAXSRNO1.ExecuteScalar());


        if (Flag == "Y")
        {
            srno = a;
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
        matrixcode = "2";
        Matrixdesc = "Long haul-road- Per Km basis";

        decimal OtherChrg = 0;


        OtherChrg = Convert.ToDecimal(txtmaxchrg.Text);
        calmethod = "Higher";
        SqlTransaction trans;
        trans = cn.BeginTransaction();
        try
        {

            String SQL_Insert_update, SQL_Delete, SQL_Delete1, SQL_Delete2, SQL_update_vendor, SQL_update_vendor_contract;

            if (Flag == "N")
            {
                SQL_Insert_update = "exec webx_vendor_contract_matrix_InsertData '" + vendorCode + "','" + matrixcode + "','" + Matrixdesc + "','" + Chargetype + "','" + docName + "','" + ContID + "'," + OtherChrg + "," + srno + ",'" + calmethod + "'";

            }
            else
            {
                SQL_Insert_update = "exec webx_vendor_contract_matrix_updateData " + srno + ",'" + matrixcode + "','" + Matrixdesc + "','" + Chargetype + "','" + docName + "'," + OtherChrg + ",'" + calmethod + "'";

                SQL_Delete = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','" + matrixcode + "','" + ContID + "'";
                SQL_Delete1 = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','1','" + ContID + "'";
                //SQL_Delete2 = "exec webx_vendor_contract_charges_deletedata '" + vendorCode + "','9','" + ContID + "'";

                SqlCommand sqlcmd1 = new SqlCommand(SQL_Delete, cn);
                sqlcmd1.Transaction = trans;
                sqlcmd1.ExecuteNonQuery();
                SqlCommand sqlcmd2 = new SqlCommand(SQL_Delete1, cn);
                sqlcmd2.Transaction = trans;
                sqlcmd2.ExecuteNonQuery();
                //SqlCommand sqlcmd3 = new SqlCommand(SQL_Delete2, cn);
                //sqlcmd3.Transaction = trans;
                //sqlcmd3.ExecuteNonQuery();
            }
            SqlCommand sqlcmd = new SqlCommand(SQL_Insert_update, cn);
            sqlcmd.Transaction = trans;
            sqlcmd.ExecuteNonQuery();


            insert_data();


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

    protected void insert_data()
    {

        SqlConnection cn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        cn.Open();

        SqlTransaction trans;
        trans = cn.BeginTransaction();
        try
        {

            if (calmethod == "Fixed" || calmethod == "Both" || calmethod == "Higher")
            {
                slabfrom = Convert.ToDecimal(txtminchrg.Text);
                Slabto = Convert.ToDecimal(txtmaxchrg.Text);
                slabrate = Convert.ToDecimal(txtminvehrate.Text);
                slabvalue = Convert.ToDecimal(txtminKM.Text);
                chargebasis = Convert.ToString(cbochrgbasis.SelectedValue);
                Fixed_YN = "N";
                Rate_YN = "N";
                calmethod1 = Convert.ToString(cbokmrs.SelectedValue);
                //slabvalue = 0;
                vehtype = 0;

                String sql_insertDetails = "exec webx_vendor_contract_charges_insertData '" + vendorCode + "','" + matrixcode + "'," + slabfrom + "," + Slabto + "," + slabvalue + "," + slabrate + ",'" + chargebasis + "','" + docName + "','" + Fixed_YN + "','" + Rate_YN + "','" + ContID + "','" + vehtype + "'," + srno + ",0,0,'NA','" + calmethod1 + "','Fixed'";

                Response.Write(" <br> sql_insertDetails : " + sql_insertDetails);
                SqlCommand insertDetails = new SqlCommand(sql_insertDetails, cn);
                insertDetails.Transaction = trans;
                insertDetails.ExecuteNonQuery();
            }

            foreach (GridViewRow gridrow in GV_D.Rows)
            {
                //DropDownList Route = (DropDownList)gridrow.FindControl("ddRoute");
                DropDownList Vehtype0 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype");
                DropDownList Vehtype1 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype1");
                DropDownList Vehtype2 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype2");
                DropDownList Vehtype3 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype3");
                DropDownList Vehtype4 = (DropDownList)GV_D.HeaderRow.FindControl("ddVehtype4");

                TextBox txtfromval = (TextBox)gridrow.FindControl("txtfromval");
                TextBox txttoval = (TextBox)gridrow.FindControl("txttoval");

                TextBox txtchrg0 = (TextBox)gridrow.FindControl("txtchrg");
                TextBox txtchrg1 = (TextBox)gridrow.FindControl("txtchrg1");
                TextBox txtchrg2 = (TextBox)gridrow.FindControl("txtchrg2");
                TextBox txtchrg3 = (TextBox)gridrow.FindControl("txtchrg3");
                TextBox txtchrg4 = (TextBox)gridrow.FindControl("txtchrg4");



                if (calmethod == "Variable" || calmethod == "Both" || calmethod == "Higher")
                {
                    chargebasis = slabbasis;
                    if (chargebasis == "")
                    {
                        chargebasis = "Per Km.";
                    }

                    Fixed_YN = "N";
                    Rate_YN = "N";

                    slabvalue = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        //Response.Write("<br> Route : " + Route);

                        decimal chrgvalue = 0;
                        //int vehtype = 0;
                        //  String chargebasis, Fixed_YN, Rate_YN, routecd, calmethod1;



                        //if(slabrate=="")
                        //{
                        //    slabrate = 0;
                        //}

                        if (i == 0)
                        {
                            chrgvalue = Convert.ToDecimal(txtchrg0.Text);
                            vehtype = Convert.ToInt32(Vehtype0.SelectedValue);
                        }
                        else if (i == 1)
                        {
                            chrgvalue = Convert.ToDecimal(txtchrg1.Text);
                            vehtype = Convert.ToInt32(Vehtype1.SelectedValue);
                        }
                        else if (i == 2)
                        {
                            chrgvalue = Convert.ToDecimal(txtchrg2.Text);
                            vehtype = Convert.ToInt32(Vehtype2.SelectedValue);
                        }
                        else if (i == 3)
                        {
                            chrgvalue = Convert.ToDecimal(txtchrg3.Text);
                            vehtype = Convert.ToInt32(Vehtype3.SelectedValue);
                        }
                        else if (i == 4)
                        {
                            chrgvalue = Convert.ToDecimal(txtchrg4.Text);
                            vehtype = Convert.ToInt32(Vehtype4.SelectedValue);
                        }

                        // routecd = Route.SelectedValue.ToString();
                        slabfrom = Convert.ToDecimal(txtfromval.Text);
                        Slabto = Convert.ToDecimal(txttoval.Text);
                        // Response.Write("<br> Route : " + chrgvalue);
                        //Response.Write("<br> Route : " + vehtype);
                        if (vehtype != 0 && chrgvalue > 0 && slabfrom > 0 && Slabto > 0)
                        {

                            slabrate = chrgvalue;
                            String sql_insertDetails = "exec webx_vendor_contract_charges_insertData '" + vendorCode + "','" + matrixcode + "'," + slabfrom + "," + Slabto + "," + slabvalue + "," + slabrate + ",'" + chargebasis + "','" + docName + "','" + Fixed_YN + "','" + Rate_YN + "','" + ContID + "','" + vehtype + "'," + srno + ",0,0,'NA','NA','Variable'";

                            Response.Write(" <br> sql_insertDetails : " + sql_insertDetails);
                            SqlCommand insertDetails = new SqlCommand(sql_insertDetails, cn);
                            insertDetails.Transaction = trans;
                            insertDetails.ExecuteNonQuery();

                        }


                    }
                }

                



            }
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

    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
