﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
public partial class GUI_Fleet_Reports_DriverSettlement_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            string strConnStr = Session["SqlProvider"].ToString();

            ReportViewer1.Visible = true;

            ReportViewer1.ServerReport.ReportServerCredentials = ReportServiceUtilities.GetReportCredentials();
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = ReportServiceUtilities.ReportServerURL;
            ReportViewer1.ServerReport.ReportPath = ReportServiceUtilities.ReportFleetPathPrefix + "DriverSettlementReport";
            ReportViewer1.Style.Add("margin-bottom", "26px");
     
            ReportParameter[] parameters = new ReportParameter[8];

            parameters[0] = new ReportParameter("CreatedFrom", Request.QueryString["CreatedFrom"].ToString());
            parameters[1] = new ReportParameter("CreatedTo", Request.QueryString["CreatedTo"].ToString());
            parameters[2] = new ReportParameter("Status", Request.QueryString["Status"].ToString());
            parameters[3] = new ReportParameter("Branch", Request.QueryString["Branch"].ToString());
            parameters[4] = new ReportParameter("Driver_Name", Request.QueryString["Driver_Name"].ToString());
            parameters[5] = new ReportParameter("Company_Code", Request.QueryString["Company_Code"].ToString());
            parameters[6] = new ReportParameter("DateGNorCL", Request.QueryString["DateGNorCL"].ToString());
            parameters[7] = new ReportParameter("ConnString", strConnStr);

            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();

            //UpdatePanel1.Update();
        }

    }
}
