<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODHandover_ViewPrint.aspx.cs" Inherits="GUI_Operations_FM_NewPODModules_PODHandover_ViewPrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View/Print POD Handover</title>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Width="100%" ProcessingMode="Remote" ShowParameterPrompts="false"
                ShowPromptAreaButton="true" ToolBarItemHoverBackColor="Aqua" ToolBarItemPressedHoverBackColor="BlueViolet"
                ExportContentDisposition="AlwaysAttachment" CssClass="blackfnt" Height="600px">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
