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
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;

public partial class GUI_UNI_Net_MIS_Trip_Profitablility_Report_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack )
        {
            string strConnStr = Convert.ToString(Session["SqlProvider"]);

            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("VehicleNo", Request.QueryString["VehicleNo"].ToString());
           
            parameters[1] = new ReportParameter("ConnString", Session["SqlProvider"].ToString());

            rvDocuments.ServerReport.ReportPath = ReportServiceUtilities.ReportFleetPathPrefix + "SMTask";

            rvDocuments.ServerReport.ReportServerCredentials = ReportServiceUtilities.GetReportCredentials();
            rvDocuments.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rvDocuments.ServerReport.ReportServerUrl = ReportServiceUtilities.ReportServerURL;
            rvDocuments.Style.Add("margin-bottom", "26px");

            rvDocuments.ServerReport.SetParameters(parameters);
        }
    }
}
