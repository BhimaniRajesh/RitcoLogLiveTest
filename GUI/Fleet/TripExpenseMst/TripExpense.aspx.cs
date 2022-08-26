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

public partial class GUI_Admin_TripExpense : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfId.Value = "";  
            BindData(gv_Trip_Expense);
            RowNo.Style["display"] = "none";
            tb_AccCode.Attributes.Add("onblur", "AccBlur(" + tb_AccCode.ClientID.ToString() + "," + tb_Description.ClientID.ToString() + ")");
            btnAccCode.Attributes.Add("onclick", "window.open('AccCodePopUp.aspx?AccCode=" + tb_AccCode.ClientID.ToString() + "&AccDesc=" + tb_Description.ClientID.ToString() + "',null,'left=300, top=150, height=300, width= 500, status=n o, resizable= no, scrollbars= yes, toolbar= no,location= no, menubar= no');return false;");
        }
    }
    public void BindData(GridView gv)
    {
        TripExpense objTripExpense = new TripExpense(Session["SqlProvider"].ToString().Trim());
        objTripExpense.BindGridTripExpense(gv);
        gv.SelectedIndex = -1;
        
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        TripExpense objTripExpense = new TripExpense(Session["SqlProvider"].ToString().Trim());
        bool IsCommitted = false, IsDuplicate = false;
        try
        {
            objTripExpense.Polarity = tb_Polarity.Text.ToString();  
            objTripExpense.Trip_Exp_Header = tb_Trip_Exp.Text.ToString().Trim();
            objTripExpense.Entry_By = SessionUtilities.CurrentEmployeeID.ToString().Trim();
            objTripExpense.AccCode = tb_AccCode.Text.Trim();  
            if (chk_Active_Flag.Checked == true) { objTripExpense.Active_Flag = "True"; } else { objTripExpense.Active_Flag = "False"; }
            

            objTripExpense.BeginTransaction(); 
            try
            {
                if (hfId.Value == "")
                {
                    objTripExpense.ID = "";
                    objTripExpense.InsertTripExpense();
                }
                else
                {
                    objTripExpense.ID = hfId.Value;
                    objTripExpense.UpdateTripExpense();  
                }
                IsCommitted = objTripExpense.CommitTransaction();
            }
            catch (Exception ex)
            {
                objTripExpense.RollBackTransaction();
                lbl_Page_Error.Text = ex.Message.ToString();
            }
        }
        catch (Exception ex)
        {
            
            lbl_Page_Error.Text = ex.Message.ToString();
        }

        if (IsCommitted)
        {
            lbl_Page_Error.ForeColor = System.Drawing.Color.Green;
            lbl_Page_Error.Text = "Trip Expenses Successfully Updated.";
            BindData(gv_Trip_Expense);
            tb_Trip_Exp.Text = "";
            hfId.Value = "";
            tb_Polarity.Text = "";
            tb_AccCode.Text = "";
            tb_Description.Text = "";
            chk_Active_Flag.Checked = false;
            RowNo.Style["display"] = "none";
        }
    }
    protected void gv_Trip_Expense_SelectedIndexChanged(object sender, EventArgs e)
    {
        RowNo.Style["display"] = "block";
        Label lbl_TripExpHdr = (Label)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("lbl_TripExpHdr");
        Label lbl_Polarity = (Label)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("lbl_Polarity");
        Label lbl_Active_Flag = (Label)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("lbl_Active_Flag");
        HiddenField hfId1 = (HiddenField)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("hfId");

        HiddenField hf_AccCode = (HiddenField)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("hf_AccCode");
        Label lbl_Account_Code = (Label)gv_Trip_Expense.Rows[gv_Trip_Expense.SelectedIndex].FindControl("lbl_Account_Code");

        tb_Trip_Exp.Text = lbl_TripExpHdr.Text;
        tb_Polarity.Text = lbl_Polarity.Text;
        if (lbl_Active_Flag.Text == "True")
        {
            chk_Active_Flag.Checked = true;
        }
        else
        {
            chk_Active_Flag.Checked = false ;
        }
        hfId.Value = hfId1.Value;
        tb_AccCode.Text = hf_AccCode.Value;

        string[] str = lbl_Account_Code.Text.Split(':');
        if (str.Length > 1)
        {
            tb_Description.Text = str[1].ToString();
        }
        tb_Trip_Exp.Focus();
    }
    protected void lnk_btn_Add_Click(object sender, EventArgs e)
    {
        RowNo.Style["display"] = "block";
    }
    protected void btn_Cencel_Click(object sender, EventArgs e)
    {
        RowNo.Style["display"] = "none";
        BindData(gv_Trip_Expense);
        tb_Trip_Exp.Text = "";
        tb_Polarity.Text = ""; 
        hfId.Value = "";
        lbl_Page_Error.Text = "";
        tb_AccCode.Text = "";
        tb_Description.Text = "";
    }
    protected void gv_Trip_Expense_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gv_Trip_Expense_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Trip_Expense.PageIndex  = e.NewPageIndex;
        BindData(gv_Trip_Expense);
    }
    protected void gv_Trip_Expense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            
        }
    }
}
