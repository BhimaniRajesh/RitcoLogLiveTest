   
    
    <%@ Page Language="C#" AutoEventWireup="true"     CodeFile="TripSheetTHCRes.aspx.cs" Inherits="GUI_Fleet_Reports_TripSheet" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AtlasControlToolkit" Namespace="AtlasControlToolkit" TagPrefix="atlas" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<table width="100%">
            <tr>
                <td>
                   <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
<rsweb:reportviewer id="ReportViewer1" runat="server" ShowBackButton="true"
                        processingmode="Remote" Height="1200px" Width="1100px" SizeToReportContent="True">
                        </rsweb:reportviewer>
                        
                   </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>