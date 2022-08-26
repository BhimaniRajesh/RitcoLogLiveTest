using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
public partial class GUI_Fleet_Reports_Vehicle_Expenes_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strConnStr = Session["SqlProvider"].ToString();

            ReportViewer1.Visible = true;
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (userAgent.Contains("MSIE 7.0"))
            {
                // ReportViewer1.Style.Add("margin-bottom", "26px");
                // ReportViewer1.Attributes.Add("style", "overflow:auto;");
            }
            ReportViewer1.ServerReport.ReportServerCredentials = ReportServiceUtilities.GetReportCredentials();
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = ReportServiceUtilities.ReportServerURL;
            ReportViewer1.ServerReport.ReportPath = ReportServiceUtilities.ReportFleetPathPrefix + "rpt_Vehicle_Expense";

            ReportViewer1.Style.Add("margin-bottom", "26px");


            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Start_dt", Cls_VehicleRegister.StrStart_dt));
            parameters.Add(new ReportParameter("End_dt", Cls_VehicleRegister.StrEnd_dt));
            parameters.Add(new ReportParameter("loccode", Cls_VehicleRegister.Strloccode));
            parameters.Add(new ReportParameter("VehicleNo", Cls_VehicleRegister.StrVehicleNo));
            parameters.Add(new ReportParameter("Company_Code", Cls_VehicleRegister.strCompany));
            parameters.Add(new ReportParameter("ConnString", strConnStr));
            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();
        }
    }
}
