<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvViewPrint.aspx.cs" Inherits="GUI_Finance_ViewPrint_Advice_Register_AdvViewPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WebXpress: Advice View</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman" runat="server" id="formbody">
<form id="frm123" runat="server">
<div align="left">
        <br /><br />
        <table bgcolor="#808080" border="<%=border%>" cellpadding="3" cellspacing="1" class="boxbg" style="width:7.5in;">
            <tr class="blackfnt">
                <td align="left" valign="top" bgcolor="#ffffff" rowspan="4" style="width: 294px">
                    <%--<img id="imglogo" runat="server" src="../../images/webxpress_logo.GIF" height="50" width="110" />--%>
                    <asp:Image Height="50" Width="110" ID="imgLogo" runat="server" />
                </td>
                <td align="left" class="bgbluegrey" style="width: 150px">Advice No</td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lbladviceno" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" class="bgbluegrey" style="width: 150px">Advice Date</td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lbladvicedt" CssClass="blackfnt" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" class="bgbluegrey" style="width: 150px">Advice Type</td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lbladvicetyp" CssClass="blackfnt" runat="server"></asp:Label></td>
            </tr> 
            <tr class="blackfnt">
                <td align="left" class="bgbluegrey" style="width: 150px">Advice Status</td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lbladvicestatus" CssClass="blackfnt" runat="server"></asp:Label></td>
            </tr> 
            <tr class="blackfnt">
                <td align="center" bgcolor="#ffffff" style="font-size:medium; width: 294px">
                    <asp:Label ID="lbladvtype" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="bgbluegrey" style="width: 150px">Amount</td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lbladviceamt" CssClass="blackfnt" runat="server"></asp:Label></td>
            </tr>   
        </table>
        <br />
        <table bgcolor="#808080" border="<%=border%>" cellpadding="3" cellspacing="1" class="boxbg" style="width:7.5in;">
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px"></td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><strong>Advice Generation</strong></td>
                <td align="left" bgcolor="#ffffff"><strong>Advice Acknowledgement</strong></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Voucher No</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgenvoucherno" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccvoucherno" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Location</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgenloc" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccloc" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Date</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgendt" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccdt" runat="server"></asp:Label></td>
            </tr> 
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Generated By</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgenemp" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccemp" runat="server"></asp:Label></td>
            </tr>  
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Cash / Bank</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgenpayment" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccpayment" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Bank Name</td>
                <td align="left" bgcolor="#ffffff" style="width: 290px"><asp:Label ID="lblgenbanknm" runat="server"></asp:Label></td>
                <td align="left" bgcolor="#ffffff"><asp:Label ID="lblaccbanknm" runat="server"></asp:Label></td>
            </tr>
            <tr class="blackfnt">
                <td align="left" bgcolor="#ffffff" style="width: 154px">Narration</td>
                <td align="left" bgcolor="#ffffff" style="width: 282px" colspan="2"><asp:Label ID="lblgennarration" runat="server"></asp:Label></td>
            </tr> 
        </table>
        <%--<table bgcolor="#808080" border="<%=border%>" cellpadding="3" cellspacing="1" class="boxbg" style="width:7.5in;">
            <tr class="bgbluegrey">
                <td align="center" colspan="4">
                    <strong>TAM Details</strong>
                </td>
            </tr>
            <tr style="background-color: White">
                <td class="blackfnt" colspan="4">
                    <asp:GridView ID="dgGeneral" Width="100%" runat="server" AllowPaging="False" AllowSorting="False"
                    AutoGenerateColumns="False" HeaderStyle-CssClass="bgbluegrey" EmptyDataText="No Records Found..."
                    EmptyDataRowStyle-CssClass="bgwhite" EmptyDataRowStyle-HorizontalAlign="Left"
                    BorderWidth="0" RowStyle-CssClass="bgwhite" CssClass="boxbg" CellSpacing="1">
                    <Columns>
                        <asp:BoundField DataField="TAMNO" HeaderText="TAM No" />
                        <asp:BoundField DataField="Advice_No" HeaderText="Advice No" />
                        <asp:BoundField DataField="Approved_Amount" HeaderText="Approved Amount" />
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>--%>
        <br /><br />
        <table bgcolor="#808080" border="<%=border%>" cellpadding="3" cellspacing="1" class="boxbg" style="width:7.5in;">
            <tr bgcolor="#ffffff">
                <td align="left" class="blackfnt" style="width: 152px"><br /></td>
                <td align="center" class="blackfnt" style="width: 289px"></td>
                <td align="right" class="blackfnt"></td>
            </tr>
            <tr bgcolor="#ffffff">
                <td align="left" class="blackfnt" style="width: 152px">Passing Authority</td>
                <td align="center" class="blackfnt" style="width: 289px">Cashier/Acc</td>
                <td align="right" class="blackfnt">Manager/Director</td>
            </tr>
        </table> 
        <br /><br />
        <div style="width:7.49in;" align="left"> <font class="blackfnt">ENCLOSE ALL SUPPORTING DOCUMENT</font></div><br />
        <br /><br />
        <div style="width:7.48in;" align="left"> <font class="blackfnt">Signature</font></div><br />
    </div>
</form>
</body>
</html>