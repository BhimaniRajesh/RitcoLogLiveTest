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

public partial class GUI_Finance_Vendor_BA_payment_PaymentAgainstBillEntry_Vendor_VendorBill_Done : System.Web.UI.Page
{
    string GenerateBillNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblBillNo.Text = Request.QueryString.Get("GenerateBillNo");
    }
}
