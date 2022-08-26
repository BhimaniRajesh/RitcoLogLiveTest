<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="GUI_UNI_Net_MIS_Sales_EXP_Register_Result" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    	<%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />--%>
    <meta http-equiv="X-UA-Compatible" content="IE=egde" />
    <title>Sales Register</title>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
		<rsweb:ReportViewer ExportContentDisposition="AlwaysAttachment" ID="ReportViewer1"
			runat="server" Font-Names="Verdana" CssClass="blackfnt" Height="600px" ShowParameterPrompts="false"
			ShowPromptAreaButton="true" ToolBarItemHoverBackColor="Aqua" ToolBarItemPressedHoverBackColor="BlueViolet"
			ProcessingMode="Remote" Width="100%">
		</rsweb:ReportViewer> 
    </form>
</body>
</html>
