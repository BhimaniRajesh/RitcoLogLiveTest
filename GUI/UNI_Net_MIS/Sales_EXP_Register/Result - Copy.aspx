<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="GUI_UNI_Net_MIS_Sales_EXP_Register_Result" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Height="400px"  CssClass="boxbg"   ZoomMode="FullPage" ProcessingMode="Remote" Width="892px">
      
        <ServerReport DisplayName="opn doc reg" ReportPath="/erp/Report2"></ServerReport>
    </rsweb:ReportViewer>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server">
        <table style="width: 10.5in" border="0" align="left" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <font class="blklnkund"><strong>Module</strong></font> <strong>&gt; </strong><font
                        class="blklnkund"><strong>Reports </strong></font><font class="bluefnt"><strong>&gt;
                        </strong><strong>Sales & Expense Register</strong> </font>
                </td>
            </tr>
            <tr>
                <td class="horzblue">
                   <%-- <img src="./../../images/clear.gif" width="2" height="1">--%>
                </td>
            </tr>
            <%--<tr>
                <td align="right">
                    <a href="javascript:window.history.go(-1)" title="back">
                        <img alt="Back" src="./../../images/back.gif" border="0">
                    </a>
                    <div align="Left">
                        <asp:UpdateProgress ID="ProgressIndicator" runat="server">
                            <ProgressTemplate>
                                <div id="progressArea">
                                    <asp:Label ID="lblLS" runat="server" Text="Please wait..." CssClass="redfnt" Font-Bold="true"></asp:Label>
                                    <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/GUI/images/indicator.gif" />
                                </div>
                                <br />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <table border="0" horizontalalign="left" id="Cri" runat="server" cellpadding="3"
                        cellspacing="1" style="width: 5.5in;" class="boxbg">
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Date
                            </td>
                            <td>
                                <asp:Label runat="server" ID="LBLDATE"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Location
                            </td>
                            <td>
                                <asp:Label runat="server" ID="LBLBRANCH"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Paybasis
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblPaybasis"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Mode Type
                            </td>
                            <td>
                                <asp:Label ID="lblmode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Booking Type
                            </td>
                            <td>
                                <asp:Label ID="lblBKGTYP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Load Type
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="white" class="blackfnt">
                            <td>
                                Status
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
       <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
<rsweb:ReportViewer ExportContentDisposition="AlwaysAttachment" ID="ReportViewer1"
            runat="server" Font-Names="Verdana" CssClass="blackfnt" Height="600px" ShowParameterPrompts="false"
            ShowPromptAreaButton="true" ToolBarItemHoverBackColor="Aqua" ToolBarItemPressedHoverBackColor="BlueViolet"
            ProcessingMode="Remote" Width="100%">
        </rsweb:ReportViewer>
    </asp:Panel>
    </form>
</body>
</html>
