<%@ Page Language="C#" AutoEventWireup="true" 
    CodeFile="Result.aspx.cs" Inherits="GUI_Operations_Enroute_Status_Update_Vehicle_Movement_Report_Result" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <rsweb:ReportViewer ShowBackButton="true" ExportContentDisposition="AlwaysAttachment"
                                ID="ReportViewer1" runat="server" Font-Names="Verdana" CssClass="blackfnt" ProcessingMode="Remote"
                                Height="600px" Width="100%" ShowParameterPrompts="False">
                                <ServerReport DisplayName="Docket Movement Report"></ServerReport>
                            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
	
	