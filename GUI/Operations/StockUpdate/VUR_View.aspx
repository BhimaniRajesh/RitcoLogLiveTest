<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VUR_View.aspx.cs" Inherits="GUI_Operations_StockUpdate_VUR_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ExportContentDisposition="AlwaysAttachment"     ID="ReportViewer1" runat="server" Font-Names="Verdana"   CssClass="blackfnt"
        Height="600px"   ShowParameterPrompts="false" ShowPromptAreaButton="true"  ToolBarItemHoverBackColor="Aqua" ToolBarItemPressedHoverBackColor="BlueViolet"      ProcessingMode="Remote" Width="100%">
      
    </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
