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

public partial class GUI_admin_setting_cust_opn_bal_PopupVendor : System.Web.UI.Page
{
    String strSQL = "";
    public int intTotalRecords = 0;
    public static byte byteQSParam;
    public static string branch = "",vendortype="";
    protected void Page_Load(object sender, EventArgs e)
    {

        branch = Convert.ToString(Request.QueryString["branch"]);
        vendortype = Convert.ToString(Request.QueryString["Vendortype"]);
        string[] vendortype_arr = Request.QueryString["Vendortype"].Split('~');
        vendortype = vendortype_arr[0].ToString();
        string scr1 = @"<script>
        function SelectCustomer(obj)
            {
            var lococde=obj.value;
            var code = lococde
            Cust_code=code
           

            window.opener.update2(Cust_code);
            window.close();
            }
            </script>;";
        Page.RegisterClientScriptBlock("SelectCustomer", scr1);

        if (!IsPostBack)
        {
        }
        txtVendorCode.Focus();
    }//select vendorcode,vendorname from webx_VENDOR_HDR order by vendorname
    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(Session["SqlProvider"].ToString().Trim());
        string loccls = "", destcls = "";
        if (txtVendorCode.Value != "")
        {
            loccls = " AND a.vendorcode LIKE '" + txtVendorCode.Value.Replace(" ", "").Trim() + "%'";
        }
        else if (txtVendorName.Value != "")
        {
            destcls = " AND a.vendorname LIKE '" + txtVendorName.Value.Replace("'", "''").Trim() + "%'";
        }
        strSQL = "SELECT  distinct a.vendorcode,vendorname  FROM webx_VENDOR_HDR a WITH(NOLOCK) inner join webx_VENDOR_det b WITH(NOLOCK) on a.vendorcode=b.vendorcode  where a.vendorcode is not null " + loccls + destcls + "  and  PATINDEx ('%" + branch + "%',vendorbrcd)>0 and vendor_type='" + vendortype + "'  order by vendorname";

        conn.Open();

        SqlCommand sqlCommand = new SqlCommand(strSQL, conn);
        SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);

        DataSet ds = new DataSet();

        sqlDA.Fill(ds);

        DataTable dt = new DataTable();

        dt.Columns.Add("vendorcode", typeof(string));
        dt.Columns.Add("vendorname", typeof(string));

        DataRow dr;

        intTotalRecords = ds.Tables[0].Rows.Count;

        foreach (DataRow drRows in ds.Tables[0].Rows)
        {
            dr = dt.NewRow();

            dr["vendorcode"] = drRows["vendorcode"].ToString();
            dr["vendorname"] = drRows["vendorname"].ToString();

            dt.Rows.Add(dr);
        }

        rptCustomer.DataSource = dt;
        rptCustomer.DataBind();
    }
}
