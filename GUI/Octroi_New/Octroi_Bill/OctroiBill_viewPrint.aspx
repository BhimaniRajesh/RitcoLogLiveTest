<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OctroiBill_viewPrint.aspx.cs" Inherits="GUI_Octroi_Octroi_Bill_OctCustBill_viewPrint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Octroi</title>
<% if (Ptrinyn == "0")
   { %>    
        <link id="Link1" href="~/GUI/images/style.css" rel="Stylesheet" type="text/css" runat="server" />
<% } %>
<% else if (Ptrinyn == "1")
   { %>    
        <link id="Link2" href="~/GUI/images/style_print.css" rel="Stylesheet" type="text/css" runat="server" />
<% } %>    
</head>
<% if (Ptrinyn == "0")
   { %>
        <body style="font-size: 12pt; font-family: Times New Roman">
<% } %>
<% else if (Ptrinyn == "1")
   { %>
        <body style="font-size: 12pt; font-family: Times New Roman" onload="javascript:window.print();">    
<% } %>
    <form id="form1" runat="server">
    <div style="width: 9in">
        <%--SUMMARY HEADER LINE--%>
        <br />
        <table bgcolor="#808080" border="0" cellpadding="3" cellspacing="1" class="<%=boxbg %>"
            width="85%">
            <tr class="<%=bgbluegrey %>">
                <td align="center" class="blackfnt">
                    Octroi Bill Summary</td>
            </tr>
        </table>
        <br />
        <br />
        <%--SUMMARY TABLE--%>
        <table bgcolor="#808080" border="0" cellpadding="3" cellspacing="1" class="<%=boxbg %>"
            width="85%">
            <tr bgcolor="#ffffff">
                <td class="blackfnt" align="left" style="width: 25%">
                    Bill Number :
                </td>
                <td class="blackfnt" style="width: 25%;" align="left">
                    <asp:Label ID="lblbillno" runat="server"></asp:Label>
                </td>
                <td class="blackfnt" align="left" style="width: 25%">
                    Bill Date :
                </td>
                <td class="blackfnt" style="width: 25%" align="left">
                    <asp:Label ID="lblbilldate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td class="blackfnt" align="left" style="width: 25%">
                    Party Name :
                </td>
                <td class="blackfnt" align="left" colspan="3" style="width: 75%">
                    <asp:Label ID="lblpartyname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td class="blackfnt" align="left">
                    Generated at Location :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblgenloc" runat="server"></asp:Label>
                </td>
                <td class="blackfnt" align="left">
                    Bill Submission at :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblsubloc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td class="blackfnt" align="left">
                    Generated for Location :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblgenforloc" runat="server"></asp:Label>
                </td>
                <td class="blackfnt" align="left">
                    Bill Collection at :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblcollectat" runat="server"></asp:Label>
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td class="blackfnt" align="left">
                    Bill Amount :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblbillamt" runat="server"></asp:Label>
                </td>
                <td class="blackfnt" align="left">
                    Due Date :
                </td>
                <td class="blackfnt" align="left">
                    <asp:Label ID="lblduedate" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <p class="blackfnt" align="center">
            <strong>Details</strong></p>
        <%--GRID VIEW OF DATA--%>
        <asp:GridView align="left" ID="docdata" runat="server" BorderWidth="0" CellPadding="2"
            CellSpacing="1" HeaderStyle-CssClass="bgbluegrey" AllowSorting="true" AllowPaging="true"
            PageSize="10" PagerStyle-HorizontalAlign="left" CssClass="boxbg" FooterStyle-CssClass="boxbg"
            PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" ShowFooter="true"
            PagerSettings-FirstPageText="[First]" PagerSettings-LastPageText="[Last]" EmptyDataText="No Records Found..."
            OnPageIndexChanging="pgChange" OnRowDataBound="GridView_RowDataBound" OnSelectedIndexChanged="docdata_SelectedIndexChanged"
            Width="85%">
            <Columns>
                <asp:TemplateField HeaderText="Sr.No.">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="White" CssClass="blackfnt" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblsrnocomplainlist" Text="<%# Container.DataItemIndex+1 %>" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Dockno" HeaderText="Doc No.">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ORGNCD" HeaderText="Origin">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DESTCD" HeaderText="Destination">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OCTAMT" HeaderText="Octroi Amt ">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="clearance_chrg" HeaderText="Clearance Charge">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="processing_per" HeaderText="Processing(%)">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="process_chrg" HeaderText="Processing Chrg.">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OTCHG" HeaderText="Other Chrg.">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TOTAMT" HeaderText="Total">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RECPTNO" HeaderText="Reciept No.">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="RECPTDT" HeaderText="Reciept Date">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" CssClass="blackfnt" />
                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" CssClass="bgbluegrey" />
            <HeaderStyle CssClass="bgbluegrey" />
            <PagerSettings FirstPageText="[First]" LastPageText="[Last]" Mode="NumericFirstLast" />
            <FooterStyle CssClass="boxbg" />
        </asp:GridView>
        <asp:HiddenField ID="grrwocnt" runat="server" />
        <asp:Label Width="85%" Visible="false" ID="lblremark" runat="server" CssClass="blackfnt"></asp:Label>
    </div>
    </form>
</body>
</html>

