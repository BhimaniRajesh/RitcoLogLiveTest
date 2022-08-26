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

public partial class GUI_Operations_LoadingSheet_DisplayMFResult : System.Web.UI.Page
{
    public static string strMFNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strMFNo = Request.QueryString["MFNo"];
            HidMFNo.Value = Request.QueryString["MFNo"];
            HidManMFNo.Value = Request.QueryString["ManMFNo"];

            getDocumentDetails();
        }

        lblMFNo.InnerHtml = HidMFNo.Value;

        //lblHeader.InnerHtml = "<b>" + HidMF.Value + " Generation Result";
        lblHeader.InnerHtml = "<b>Transaction Completed</b>";
        lblMF.InnerHtml = HidMF.Value + "&nbsp;";
        lblManMFNo.InnerHtml = HidManMFNo.Value.Trim();
        //lblVPMF.InnerHtml = "<a href=\"Javascript:OpenInWindow('../TCS/PrintTCS/FrmMenifestView.aspx?MFNo=" + strMFNo + ",0',400,600,10,10)\"><u>View</u></a>|<a href=\"Javascript:OpenInWindow('../TCS/PrintTCS/FrmMenifestView.aspx?MFNo=" + strMFNo + ",1',400,600,10,10)\"><u>Print</u></a>|<a href=\"Javascript:OpenInWindow('../../Print/MenifestPrint.aspx?tcno=" + strMFNo + "',400,600,10,10)\"><u>Dos Print</u></a>";
        lblVPMF.InnerHtml = "<a href=\"Javascript:OpenInWindow('../TCS/PrintTCS/FrmMenifestView.aspx?MFNo=" + strMFNo + ",0',400,600,10,10)\"><u>View</u></a>";
        //lblVPMF.InnerHtml = "<a href=\"Javascript:OpenInWindow('../TCS/PrintTCS/FrmMenifestView.aspx?MFNo=" + HidMFNo.Value + "',400,600,10,10)\"><u>View</u></a>";
        //lblMoreMF.InnerHtml = "<a href=\"./MFMain.aspx\"><u>Click here to prepare more " + HidMF.Value + "s</u></a>";
        //lblTHC.InnerHtml = "<a href=\"../THC/THCMain.aspx\"><u>Click here to prepare " + HidTHC.Value + "</u></a>";
        //lblLSMFOptions.InnerHtml = "<a href=\"./../LSMFOptions.aspx\"><u>" + HidLS.Value + " &amp; " + HidMF.Value + " Options</u></a>";

        Page.Title = "Display " + HidMF.Value + " Result";
    }

    private void getDocumentDetails()
    {
        HidLS.Value = "";
        HidTHC.Value = "";
        HidMF.Value = "";

        SqlConnection sqlConn = new SqlConnection(Session["SqlProvider"].ToString().Trim());

        sqlConn.Open();

        string strSQLForDocument = "select CodeID, CodeDesc from webx_master_general where codetype = 'DOCS'";

        SqlCommand sqlCommand = new SqlCommand(strSQLForDocument, sqlConn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);

        DataSet dsDocument = new DataSet();

        sqlDA.Fill(dsDocument);

        if (dsDocument.Tables[0].Rows.Count > 0)
        {
            for (int intLoopCounter = 0; intLoopCounter < dsDocument.Tables[0].Rows.Count; intLoopCounter++)
            {
                switch (dsDocument.Tables[0].Rows[intLoopCounter]["CodeID"].ToString())
                {
                    case "LS":
                        HidLS.Value = dsDocument.Tables[0].Rows[intLoopCounter]["CodeDesc"].ToString();
                        break;
                    case "MF":
                        HidMF.Value = dsDocument.Tables[0].Rows[intLoopCounter]["CodeDesc"].ToString();
                        break;
                    case "THC":
                        HidTHC.Value = dsDocument.Tables[0].Rows[intLoopCounter]["CodeDesc"].ToString();
                        break;
                }
            }
        }
    }
}
