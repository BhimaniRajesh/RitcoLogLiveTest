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


public partial class VehicleKMUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
  
    public void BindGrid()
    {
        Vehicle_KmUpdate vehUpdate = new Vehicle_KmUpdate(Convert.ToString(Session["SqlProvider"]));
        vehUpdate.VEHNO = tb_Vehicle_No.Text;
        vehUpdate.BindVehicle(gv_Vehicle);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Vehicle_KmUpdate vehUpdate = new Vehicle_KmUpdate(Convert.ToString(Session["SqlProvider"]));
        bool IsCommitted = false;
        foreach (GridViewRow row in gv_Vehicle.Rows)
        {
            HiddenField hf_VEH_INTERNAL_NO = (HiddenField)row.FindControl("hf_VEH_INTERNAL_NO");
            TextBox tb_Tmp_Current_KM_Read = (TextBox)row.FindControl("tb_Tmp_Current_KM_Read");

            vehUpdate.BeginTransaction();
            try
            {
                vehUpdate.Veh_Internal_No = Convert.ToInt32(hf_VEH_INTERNAL_NO.Value);
                vehUpdate.TMP_CURRENT_KM_READ = tb_Tmp_Current_KM_Read.Text.Trim();
                vehUpdate.UpdateKMRead();
                IsCommitted = vehUpdate.CommitTransaction();
            }
            catch (Exception ex)
            {
                vehUpdate.RollBackTransaction();
                lbl_Page_Error.ForeColor = System.Drawing.Color.Red;
                lbl_Page_Error.Text = ex.Message.ToString();
            }
        }
        if (IsCommitted)
        {
            lbl_Page_Error.ForeColor = System.Drawing.Color.Green;
            lbl_Page_Error.Text = "Current KM Reading Updated Successfully.";
            BindGrid();
        }
        //UpdatePanel1.Update();
        //UpdatePanel3.Update();
    }
    protected void gv_Vehicle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            if (gv_Vehicle.Rows.Count > 0)
            {
                HiddenField hf_Tmp_Current_KM_Read = (HiddenField)e.Row.FindControl("hf_Tmp_Current_KM_Read");
                TextBox tb_Tmp_Current_KM_Read = (TextBox)e.Row.FindControl("tb_Tmp_Current_KM_Read");
                Label lbl_Tmp_Current_KM_Read = (Label)e.Row.FindControl("lbl_Tmp_Current_KM_Read");
                lbl_Tmp_Current_KM_Read.Attributes.Add("onclick", "javascript:return alterDisplay('" + tb_Tmp_Current_KM_Read.ClientID.ToString() + "','" + lbl_Tmp_Current_KM_Read.ClientID.ToString() + "','" + hf_Tmp_Current_KM_Read.ClientID.ToString() + "')");
                tb_Tmp_Current_KM_Read.Attributes.Add("onblur", "javascript:return alterDisplay('" + tb_Tmp_Current_KM_Read.ClientID.ToString() + "','" + lbl_Tmp_Current_KM_Read.ClientID.ToString() + "','" + hf_Tmp_Current_KM_Read.ClientID.ToString() + "')");
            }
        }
    }
    protected void btnVehSubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void tb_Vehicle_No_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
