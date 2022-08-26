<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/GUI/MasterPage.master" CodeFile="JournalVoucher.aspx.cs" Inherits="GUI_Finance_DebitCredit_JournalVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">
<br />

<br />

<div align="center" style="width:7in;">
    <table bgcolor="#808080" border="0"class="boxbg" style="width:7in;">
          <tr class="blackfnt">
            <td align="center" valign="middle" class="blackfnt" bgcolor="#ffffff" style="width:50%;">
                <font class="bluefnt"><b>JOURNAL VOUCHER</b></font><br />
                <asp:Label ID="lbljnvoucher" CssClass="bluefnt" runat="server"></asp:Label>
            </td>
            <td align="left" class="bgbluegrey"  style="width:50%;">
                    <table bgcolor="#808080" border="0" cellpadding="3" cellspacing="1" class="boxbg" style="width:100%;">
                          <tr class="blackfnt">
                            <td align="left" class="bgbluegrey">Voucher No.</td>
                            <td align="left" bgcolor="#ffffff">
                                <asp:Label ID="lblvoucherno" CssClass="blackfnt" runat="server"></asp:Label>
                            </td>
                          </tr>
                          
                          <tr class="blackfnt">
                            <td align="left" class="bgbluegrey">Voucher Date</td>
                            <td align="left" bgcolor="#ffffff">
                                <asp:Label ID="lblvoucherdate" CssClass="blackfnt" runat="server"></asp:Label>
                            </td>
                          </tr>
                          
                          <tr class="blackfnt">
                            <td align="left" class="bgbluegrey">Branch </td>
                            <td align="left" bgcolor="#ffffff">
                                <asp:Label ID="lblbranchcode" CssClass="blackfnt" runat="server"></asp:Label> : <asp:Label ID="lblbranchname" CssClass="blackfnt" runat="server"></asp:Label>
                            </td>
                          </tr>
                    </table>
            </td>
        </tr>
    </table>
<br />


    <asp:Table ID="tblacclist" runat="server" CellPadding="3" CssClass="boxbg" CellSpacing="1" style="width: 7in;">
        <asp:TableRow CssClass="bgbluegrey">
            <asp:TableCell CssClass="blackfnt" Font-Bold="true">A/C CODE</asp:TableCell>
            <asp:TableCell CssClass="blackfnt" Font-Bold="true">A/C NAME</asp:TableCell>
            <asp:TableCell ColumnSpan="2" CssClass="blackfnt" Font-Bold="true">AMOUNT</asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    
<%--
    <table bgcolor="#808080" border="0" cellpadding="3" cellspacing="1" class="boxbg" style="width:9in;">
          <tr class="blackfnt">
            <td align="left" style="width:20%;" class="bgbluegrey">Branch </td>
            <td align="left" style="width:45%;" bgcolor="#ffffff">
                <asp:Label ID="lblbranch" CssClass="blackfnt" runat="server"></asp:Label>
            </td>
            <td align="left" style="width:20%;" class="bgbluegrey">Voucher No.</td>
            <td align="left" style="width:15%;" bgcolor="#ffffff">
            </td>
          </tr>
    </table>--%>
</div>
    
       
</asp:Content>
