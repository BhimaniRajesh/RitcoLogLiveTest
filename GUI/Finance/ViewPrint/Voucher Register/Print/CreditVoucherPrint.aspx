<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreditVoucherPrint.aspx.cs" Inherits="GUI_Finance_voucher_Print_CreditVoucherPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WebXpress: Credit Voucher Print</title>
    <link id="Link1" href="../../../../images/style_print.css" rel="Stylesheet" type="text/css" runat="server" />
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
    <div>

<br />
<asp:Label ID="lblvoucherheader" Font-Bold="true" style="left:50;" CssClass="blackfnt" runat="server"></asp:Label>
<br /><br />
    <table bgcolor="#808080" border="1" cellpadding="3" cellspacing="1" class="boxbg" style="width:7in;">
          <tr class="blackfnt">
            <td align="left" class="bgbluegrey">Branch</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lblbranch" CssClass="blackfnt" runat="server"></asp:Label></td>
            <td align="left" class="bgbluegrey">Voucher No.</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lblvoucherno" runat="server"></asp:Label></td>
          </tr>
          <tr class="blackfnt">
            <td align="left" class="bgbluegrey">Received From</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lblrecfrom" runat="server"></asp:Label></td>
            <td align="left" class="bgbluegrey">Date</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lbldate" runat="server"></asp:Label></td>
          </tr>

<%--
          <tr class="blackfnt">
            <td align="left" class="bgbluegrey">Narration</td>
            <td align="left" bgcolor="#ffffff" colspan="3"><asp:Label ID="lblNarration" runat="server"></asp:Label></td>
          </tr>
--%>

          <tr class="blackfnt">
            <td align="left" class="bgbluegrey">Cheque Number</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lbl_Cheque_Number" CssClass="blackfnt" runat="server"></asp:Label></td>
            <td align="left" class="bgbluegrey">Cheque Date</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lbl_Cheque_Date" runat="server"></asp:Label></td>
          </tr>
          <tr class="blackfnt">
            <td align="left" class="bgbluegrey">Bank Name</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lbl_Bank_Name" CssClass="blackfnt" runat="server"></asp:Label></td>
            <td align="left" class="bgbluegrey">Branch Name</td>
            <td align="left" bgcolor="#ffffff"><asp:Label ID="lbl_Banch_Name" runat="server"></asp:Label></td>
          </tr>
    </table>
    <br /><br />
    
       <font class="blackfnt"><b>A/C. : </b></font> <asp:Label CssClass="blackfnt" ID="lblaccno" runat="server"></asp:Label> : 
        <asp:Label ID="lblcashtype" CssClass="blackfnt" runat="server"></asp:Label>
        
       <br /><br />
        
        <table bgcolor="#808080" border="0" cellpadding="3" cellspacing="1" class="boxbg" style="width:7in;">
          <tr bgcolor="#ffffff">
            <td align="left" class="blackfnt">
                <table border="1" cellpadding="3" width="100%" bgcolor="#808080" cellspacing="1" class=boxbg>
                    <tr bgcolor="white"> 
                        <td align="center" class="blackfnt" width="40%">Description</td>
                        <td align="center" class="blackfnt" width="15%">Debit Amount</td>
                        <td align="center" class="blackfnt" width="15%">Credit Amount</td>
                        <td align="center" class="blackfnt" width="30%">Narration</td>
                    </tr>
                </table>
            </td>            
          </tr>
          <tr bgcolor="#ffffff">
            <td align="left" class="blackfnt">
                <asp:Repeater ID="rptrDebitVoucher" runat="server">
                    <ItemTemplate>
                        <table border="1" cellpadding="3" width="100%" bgcolor="#808080" cellspacing="1" class=boxbg>
                            <tr bgcolor="white"> 
                                <td align="left" width="40%"><font class=blackfnt><%#DataBinder.Eval(Container.DataItem, "AccountDescription")%></font></td>
                                <td align="right" width="15%"><font class=blackfnt><%#DataBinder.Eval(Container.DataItem, "DebitAmount")%></font></td>
                                <td align="right" width="15%"><font class=blackfnt><%#DataBinder.Eval(Container.DataItem, "CreditAmount")%></font></td>
                                <td align="left" width="30%"><font class=blackfnt><%#DataBinder.Eval(Container.DataItem, "Narration")%></font></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
          </tr>
          <tr bgcolor="#ffffff">
            <td align="left" class="blackfnt">
                <table border="1" cellpadding="3" width="100%" bgcolor="#808080" cellspacing="1" class=boxbg>
                    <tr bgcolor="white"> 
                        <td align="right" class="blackfnt" width="40%"><strong>Total Rs.</strong></td>
                        <td align="right" class="blackfnt" width="15%"><asp:Label ID="lblTotalDebitAmount" runat="server"></asp:Label></td>
                        <td align="right" class="blackfnt" width="15%"><asp:Label ID="lblTotalCreditAmount" runat="server"></asp:Label></td>
                        <td width="35%"></td>
                    </tr>
                </table>
            </td>
          </tr>
          <tr bgcolor="#ffffff">
            <td align="left" class="blackfnt" colspan="3">
                <asp:Label ID="lblinwords" runat="server"></asp:Label>
            </td>
          </tr>
        </table>  
          
        
        <br />
        <table bgcolor="#808080" border="0" class="boxbg" cellpadding="3" cellspacing="1" style="width:7in;">
          <tr bgcolor="#ffffff">
            <td align="left" class="blackfnt">Passing Authority </td>
            <td align="center" class="blackfnt">Cashier/Acc</td>
            <td align="right" class="blackfnt">Manager/Director</td>
          </tr>
        </table>  

<br />
 <div style="width:7in;" align="left"> <font class="blackfnt">ENCLOSE ALL SUPPORTING DOCUMENT</font></div><br />
 <asp:Label ID="lbltotalmoney" Font-Bold="true" runat="server"></asp:Label>
 <br /><br />
 <div style="width:7in;" align="right"> <font class="blackfnt">Signature</font></div><br />

    </div>
    </form>
</body>
</html>
