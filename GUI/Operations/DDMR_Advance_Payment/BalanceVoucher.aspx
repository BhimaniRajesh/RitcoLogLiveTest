<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceVoucher.aspx.cs" Inherits="GUI_Finance_VendorPayment_Ver1_Advance_Balance_Payment_AdvanceVoucher" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Voucher View</title>
    <link type="text/css" rel="stylesheet" href="../../../../images/style.css" />
    <script language="javascript" type="text/javascript">
        function ViewPrint(Path) 
        {
            //alert("Hi");
            var winOpts = "scrollbars=yes,resizable=yes,width=" + 700 + ",height=" + 400 + "";
            confirmWin = window.open(Path, "", winOpts);
            //var winOpts = "menubar=no,toolbar=no,location=no,scrollbars=yes,resizable=yes,status=no,width=" + 700 + ",height=" + 400 + ",left=" + 60 + ",top=" + 125 + "";
            //window.open(Path, "", winOpts);
            //window.open("" + VoucherNo + "", "es", "menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=650,height=290,status=no,left=60,top=125");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <table border="0" style="width: 800" bgcolor="#808080" cellpadding="2" cellspacing="1"
                            class="boxbg">
                            <tr class="bgbluegrey">
                                <td colspan="4" align="center">
                                    <font class="bluefnt"><b>
                                        <asp:Label ID="lbl_title" runat="server" CssClass="blackfnt" Font-Bold="true"></asp:Label></b></font></td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">
                                        <asp:Label ID="Lbl_Docno" runat="server" CssClass="blackfnt"></asp:Label></font>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_voucher_No" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td>
                                    <font class="blackfnt">
                                        <asp:Label ID="Lbl_DocDate" runat="server" CssClass="blackfnt"></asp:Label></font>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_voucher_Dt" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">Manual Voucher No:</font>
                                </td>
                                <td>
                                    <asp:Label ID="Lbl_manualVRNO" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td>
                                    <font class="blackfnt">Vendor Name</font>
                                </td>
                                <td>
                                    <asp:Label ID="Lbl_Vendorname" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">Document Type</font>
                                </td>
                                <td>
                                    <asp:Label ID="Lbl_Voucher_type" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="TR_balance" runat="server">
                    <td>
                        <table border="0" style="width: 800" bgcolor="#808080" cellpadding="2" cellspacing="1"
                            class="boxbg">
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                            <tr class="bgbluegrey">
                                <td align="center" colspan="4">
                                    <font class="bluefnt"><b>Vendor Contract Details </b></font>
                                </td>
                            </tr>
                            <tr bgcolor="white" id="TR_KM_Based_BAL_1" runat="server" visible="false">
                                <td align="Left" nowrap>
                                    <font class="blackfnt">Vendor Contract Type</font>
                                </td>
                                <td align="Left" nowrap>
                                    <asp:Label ID="Lbl_ContractType_1" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Rate/Additional Km.</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="Lbl_Rate_1" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white" id="TR_KM_Based_BAL_2" runat="server" visible="false">
                                <td align="Left">
                                    <font class="blackfnt">Total Travel Km.</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="Lbl_Tot_KM_1" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Contract Amt.(AS Per Contract Based On Total Travel Km.)</font>
                                </td>
                                <td align="Right">
                                    <asp:Label ID="Lbl_ContractAmt_1" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="BillDetail" runat="server">
                    <td>
                        <table border="0" style="width: 800" bgcolor="#808080" cellpadding="2" cellspacing="1"
                            class="boxbg">
                            <asp:HiddenField ID="click_count" runat="server" Value="0" />
                            <asp:HiddenField ID="Hnd_ServerDt" runat="server" Value="0" />
                            <tr class="bgbluegrey">
                                <td align="center" colspan="4">
                                    <font class="bluefnt"><b>Vendor Bill Details </b></font>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF" id="Tr_Bill1" runat="server">
                                <td align="Left">
                                    <font class="blackfnt">Vendor Name</font>
                                </td>
                                <td align="Left" colspan="3">
                                    <asp:Label ID="VendorName" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF" id="Tr_Bill3" runat="server">
                                <td align="Left">
                                    <font class="blackfnt">Vendor Bill Amount</font>
                                </td>
                                <td align="Right">
                                    <asp:Label ID="Billamt" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Vendor Bill Date</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="BillDate" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF" id="Tr_Bill4" runat="server">
                                <td align="Left">
                                    <font class="blackfnt">Due Days</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="DueDays" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Due Date</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="DueDate" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white" id="TR_KM_Based_1" runat="server" visible="false">
                                <td align="Left" nowrap>
                                    <font class="blackfnt">Vendor Contract Type</font>
                                </td>
                                <td align="Left" nowrap>
                                    <asp:Label ID="Lbl_ContractType" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Rate/Additional Km.</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="Lbl_Rate" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white" id="TR_KM_Based_2" runat="server" visible="false">
                                <td align="Left">
                                    <font class="blackfnt">Total Travel Km.</font>
                                </td>
                                <td align="Left">
                                    <asp:Label ID="Lbl_Tot_KM" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td align="Left">
                                    <font class="blackfnt">Contract Amt.(AS Per Contract Based On Total Travel Km.)</font>
                                </td>
                                <td align="Right">
                                    <asp:Label ID="Lbl_ContractAmt" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td align="Left">
                                    <font class="blackfnt">Remarks</font>
                                </td>
                                <td align="Left" colspan="3">
                                    <asp:Label ID="Lbl_Remarks" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:GridView ID="VoucherView" ShowFooter="true" EmptyDataText="No Records Found....."
                            AllowPaging="false" PagerStyle-Mode="NumericPages" CssClass="boxbg" AutoGenerateColumns="true"
                            HeaderStyle-CssClass="bgbluegrey" runat="server" SelectedIndex="1" BorderWidth="0"
                            CellPadding="3" CellSpacing="1" Width="800" RowStyle-Wrap="false" HeaderStyle-Wrap="false"
                            RowStyle-HorizontalAlign="Right" FooterStyle-BackColor="white" RowStyle-BackColor="white">
                            <PagerStyle HorizontalAlign="Center" CssClass="boxbg" />
                            <HeaderStyle CssClass="bgbluegrey" />
                            <FooterStyle CssClass="boxbg" />
                        </asp:GridView>
                        <asp:GridView ID="GridView1" ShowFooter="true" EmptyDataText="No Records Found....."
                            AllowPaging="false" PagerStyle-Mode="NumericPages" CssClass="boxbg" AutoGenerateColumns="False"
                            HeaderStyle-CssClass="bgbluegrey" runat="server" SelectedIndex="1" BorderWidth="0"
                            CellPadding="3" CellSpacing="1" Width="800" RowStyle-Wrap="false" HeaderStyle-Wrap="false"
                            RowStyle-HorizontalAlign="Right" FooterStyle-BackColor="white" RowStyle-BackColor="white">
                            <Columns>
                                <asp:BoundField Visible="true" DataField="BillNo" HeaderText="Bill No">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Bill Date" HeaderText="Bill Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Vendor Billno" HeaderText="Vendor Bill No">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Vendor Code & Name" HeaderText="Vendor Code & Name">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Current Payment" HeaderText="Current Payment">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pending Amount" HeaderText="Pending Amount">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total Amount" HeaderText="Total Amount">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                    <ItemStyle BackColor="white" Wrap="False" CssClass="blackfnt" HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderText="Print" HeaderStyle-Font-Bold="true" ItemStyle-Wrap="false">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                            <ItemStyle BackColor="white" CssClass="blackfnt" />
                                            <ItemTemplate>
                                                <label class="blackfnt"><a href="<%#DataBinder.Eval(Container.DataItem, "Path")%>">Print</a></label>
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="boxbg" />
                            <HeaderStyle CssClass="bgbluegrey" />
                            <FooterStyle CssClass="boxbg" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="TaxDetail" runat="server">
                    <td>
                        <br />
                        <table border="0" cellpadding="1" cellspacing="1" class="boxbg" width="800">
                            <tr class="bgbluegrey">
                                <td style="text-align: center" class="blackfnt" colspan="4">
                                    <b>S.tax & TDS Details</b>
                                </td>
                            </tr>
                            <tr class="bgbluegrey">
                                <td style="text-align: center" class="blackfnt">
                                    <strong><font color="red">Enabled Service Tax Y/N-</font></strong>
                                </td>
                                <td>
                                    <asp:Label ID="Stax_YN" runat="server" CssClass="blackfnt"></asp:Label></td>
                                <td style="text-align: center" class="blackfnt">
                                    <strong><font color="red">Enabled TDS Y/N-</font></strong>
                                </td>
                                <td>
                                    <asp:Label ID="Tds_YN" runat="server" CssClass="blackfnt"></asp:Label></td>
                            </tr>
                            <tr class="bgbluegrey">
                                <td style="text-align: center" class="blackfnt" colspan="2">
                                    <strong>ADD Service Tax(+) </strong>
                                </td>
                                <td style="text-align: center" class="blackfnt" colspan="2">
                                    <strong>LESS TDS (-) </strong>
                                </td>
                            </tr>
                            <tr style="background-color: White">
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;Service tax (+)</td>
                                <td style="text-align: Right" class="blackfnt">
                                    &nbsp;<asp:Label ID="STax" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;TDS Section</td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;<asp:Label ID="TDSLedger" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color: White">
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;Education Cess (+)</td>
                                <td style="text-align: Right" class="blackfnt">
                                    &nbsp;<asp:Label ID="Cess" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;TDS Rate</td>
                                <td style="text-align: Right" class="blackfnt">
                                    &nbsp;<asp:Label ID="TdsRate" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color: White">
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;Higher Education Cess (+)</td>
                                <td style="text-align: Right" class="blackfnt">
                                    &nbsp;<asp:Label ID="HCess" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;TDS Amount (-)</td>
                                <td style="text-align: Right" class="blackfnt">
                                    &nbsp;<asp:Label ID="TDSAmt" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color: White">
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;Service Tax Reg No.</td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;<asp:Label ID="StaxRegNo" runat="server" CssClass="blackfnt"></asp:Label></td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;PAN Number</td>
                                <td style="text-align: left" class="blackfnt">
                                    &nbsp;<asp:Label ID="PANNO" runat="server" CssClass="blackfnt"></asp:Label>
                                    <asp:HiddenField ID="Hnd_totalAmount" runat="server" Value="0.00" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="PaymentDetail" runat="server">
                    <td>
                        <br />
                        <table border="0" style="width: 800" bgcolor="#808080" cellpadding="2" cellspacing="1"
                            class="boxbg">
                            <tr class="bgbluegrey">
                                <td colspan="4" align="center">
                                    <font class="bluefnt"><b>Voucher Payment Details</b></font></td>
                            </tr>
                            <tr bgcolor="white">
                                <td width="200">
                                    <font class="blackfnt">Payment Mode </font>
                                </td>
                                <td width="200">
                                    <asp:Label ID="Lbl_Paymode" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td width="200">
                                </td>
                                <td width="200">
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">Cash Amount </font>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Lbl_CashAMt" Font-Bold="true" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td>
                                    <font class="blackfnt">Cash Account </font>
                                </td>
                                <td nowrap>
                                    <asp:Label ID="Lbl_cash_accode" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">Cheque Amount </font>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Lbl_Chqamt" Font-Bold="true" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td nowrap>
                                    <font class="blackfnt">Bank Account </font>
                                </td>
                                <td nowrap>
                                    <asp:Label ID="Lbl_bankCode" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt">Cheque No. </font>
                                </td>
                                <td>
                                    <asp:Label ID="Lbl_chqno" Font-Bold="true" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                                <td>
                                    <font class="blackfnt">Cheque date </font>
                                </td>
                                <td>
                                    <asp:Label ID="Lbl_chqDate" Font-Bold="true" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                            <tr bgcolor="white">
                                <td>
                                    <font class="blackfnt"></font>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <font class="blackfnt"><b>NET PAYABLE </b></font>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Lbl_netamt" Font-Bold="true" runat="server" CssClass="blackfnt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
