<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewExpenseEntryPP.aspx.cs" Inherits="GUI_Finance_Vendor_BA_payment_View_N_Print_ViewExpenseEntryFV" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fuel Expense View</title>
    <link href="../../../images/style.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdfldPrint_YN" runat="server" />
        <div>
            <table border="0" cellspacing="1" width="95%" cellpadding="4" align="center" class="boxbg">
                <tr bgcolor="#FFFFFF">
                    <td align="center">
                        <font class="blackfnt"><b>Fuel Expense Entry Summary</b></font></td>
                </tr>
            </table>
            <br />
            <table border="0" cellspacing="1" cellpadding="4" width="95%" align="center" class="boxbg">
                <tr bgcolor="#FFFFFF">
                    <td align="left">
                        <div align="left">
                            <font class="bluefnt">Bill Entry No</font>
                        </div>
                    </td>
                    <td>
                        <font class="bluefnt"><asp:Label ID="lblBillNo" runat="server"></asp:Label></font>
                    </td>
                    <td align="left">
                        <div align="left">
                            <font class="bluefnt">Bill Entry Date</font>
                        </div>
                    </td>
                    <td>
                        <font class="bluefnt"><asp:Label ID="lblBillEntryDt" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td align="left">
                        <div align="left">
                            <font class="bluefnt">Vendor Name </font>
                        </div>
                    </td>
                    <td width="30%">
                        <font class="bluefnt"><asp:Label ID="lblVendorName" runat="server"></asp:Label></font>
                    </td>
                    <td align="left">
                        <div align="left">
                            <font class="bluefnt">Vendor Accounted to</font>
                        </div>
                    </td>
                    <td nowrap>
                        <font class="bluefnt"><asp:Label ID="lblVendorAccTo" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td height="21" bgcolor="#FFFFFF" align="left" width="166">
                        <div align="left">
                            <font class="bluefnt">Vendor Bill Number</font></div>
                    </td>
                    <td height="21" bgcolor="#FFFFFF" align="left" width="222">
                        <font class="bluefnt"><asp:Label ID="lblVendorBillNo" runat="server"></asp:Label></font>
                    </td>
                    <td valign="top">
                        <div align="left">
                            <font class="bluefnt">Vendor Bill Amount</font>
                        </div>
                    </td>
                    <td align="left">
                        <font class="bluefnt"><asp:Label ID="lblVendorBillAmt" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td height="21" bgcolor="#FFFFFF" align="left" width="166">
                        <div align="left">
                            <font class="bluefnt">Vendor Bill Date</font>
                        </div>
                    </td>
                    <td height="21" bgcolor="#FFFFFF" align="left" width="222">
                        <font class="bluefnt"><asp:Label ID="lblVendorBillDt" runat="server"></asp:Label></font>
                    </td>
                    <td height="21" bgcolor="#FFFFFF" align="left" width="166">
                        <div align="left">
                            <font class="bluefnt">Due Date</font>
                        </div>
                    </td>
                    <td height="21" bgcolor="#FFFFFF" align="left" width="222">
                        <font class="bluefnt"><asp:Label ID="lblDueDt" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td>
                        <div align="left">
                            <font class="bluefnt">Ref. NO.</font>
                        </div>
                    </td>
                    <td>
                        <font class="bluefnt"><asp:Label ID="lblRefNo" runat="server"></asp:Label></font>
                    </td>
                    <td valign="top">
                        <div align="left">
                            <font class="bluefnt">Remark</font>
                        </div>
                    </td>
                    <td>
                        <font class="bluefnt"><asp:Label ID="lblRemarks" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <%--<tr bgcolor="#FFFFFF">
                    <td valign="top">
                        <div align="left">
                            <font class="bluefnt">Other Deduction</font>
                        </div>
                    </td>
                    <td valign="top">
                        <font class="bluefnt"><asp:Label ID="lblOtherDeduction" runat="server"></asp:Label></font>
                    </td>
                    <td valign="top">
                        <div align="left">
                            <font class="bluefnt">Discount Received</font>
                        </div>
                    </td>
                    <td valign="top">
                        <font class="bluefnt"><asp:Label ID="lblDiscountRecvd" runat="server"></asp:Label></font>
                    </td>
                </tr>--%>
                <tr bgcolor="#FFFFFF">
                    <td valign="top">
                        <div align="left">
                            <font class="blackfnt"></font>
                        </div>
                    </td>
                    <td valign="top">
                        <font class="blackfnt"> </font>
                    </td>
                    <td valign="top">
                        <div align="left">
                            <font class="blackfnt"><strong>Net Amount</strong></font>
                        </div>
                    </td>
                    <td valign="top">
                        <font class="blackfnt"><asp:Label ID="lblNetAmt" runat="server"></asp:Label></font>
                    </td>
                </tr>
            </table>
            
            
            <!----  EXPENSE HEADS STARTS HERE ---->
            <br>
            
            
            
            
            
          <table border="0" cellspacing="1" width="95%" cellpadding="4" align="center" >  
                    
                            <tr class="bgbluegrey">
                <td colspan=4 align="center"><font class=blackfnt><b>HSD/OIL Expenses</b></font></td>
            </tr>
                
                
          

 
                             
                            
                
                        
             <tr style="background-color: white">
                <td   colspan="4" align="left">
                                    
                  <asp:DataGrid ID="dgExpense" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="2" CssClass="blackfnt" BackColor="White" BorderColor="#006699" BorderStyle="None"
                                                    BorderWidth="3px" Width="100%" >
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Sr.No." >
                                                            <ItemTemplate>
                                                                <center>
                                                                    <%# Container.ItemIndex+1 %>.
                                                                    <asp:Label ID="count" runat="server" Visible="false" Text=' <%# Container.ItemIndex+1 %>'></asp:Label>
                                                                </center>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                        </asp:TemplateColumn>
                                                           <asp:BoundColumn     DataField="Tripsheetno"  HeaderText="TripSheet No."  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Place"  HeaderText="Place"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="FuelType"  HeaderText="Fuel Type"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Last_Km_Read"  HeaderText="Last Km. Reading"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="KM_Reading"  HeaderText="Current Km. Reading"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="BillNo"  HeaderText="Slip No."  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="BillDt"  HeaderText="Slip Date"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Diesel_Ltr"  HeaderText="Diesel (Qtn. in Litres)"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Diesel_Rate"  HeaderText="Diesel Rate/litres"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Amount"  HeaderText="Amount"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="EXE_AMT"  HeaderText="Amount Approved By Fleet Executive"  ></asp:BoundColumn>
                                                           <asp:BoundColumn     DataField="Remark"  HeaderText="Remarks"  ></asp:BoundColumn>
                                                           
                                                           
                                                           
                                                           
                                                               
                      
                                                        
                                                        </Columns>
                                                    <PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Left" BackColor="White"
                                                        ForeColor="#000066" Wrap="False" Mode="NumericPages"></PagerStyle>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <ItemStyle ForeColor="#000066" />
                                                    <HeaderStyle Font-Bold="False" ForeColor="Black" CssClass="bgbluegrey"
                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:DataGrid>                  
                      

                </td>
            </tr>
          
                           
            
            </table>
            
            
      
      <!--<table Width="95%" align=center  cellspacing="1" cellpadding="4" class="boxbg" border="0">
        <tr style="background-color: white">
        <td  align=right width="83%">
            <font class=blackfnt><b>Total</b></font>
        </td>
          
        <td >
            <asp:Label ID="lblTotalAmt" runat="server" Font-Bold="true" CssClass="blackfnt"></asp:Label>
        </td>
                                           
         <td align="right" >
             <asp:Label ID="lblTotalExeAmt"  CssClass="blackfnt" Font-Bold="true"   runat="server"  ></asp:Label>
        </td>
       
        
        
      </tr>
      </table>-->
            
            
           
            
            
            <br>
           <!----  EXPENSE HEADS ENDS HERE ---->
              
            <table border="0" cellspacing="1" cellpadding="4" width="95%" align="center" class="boxbg">
                <tr class="bgbluegrey">
                    <td align="left">
                        <font class="blackfnt"><b>Tax</font></td>
                    <td align="left">
                        <font class="blackfnt"><b>Account Head</font></td>
                    <td align="right">
                        <font class="blackfnt"><b>Approved Rate (%)</font></td>
                    <td align="right">
                        <font class="blackfnt"><b>Value (in Rs.)</font></td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt">Service Tax</font></td>
                    <td>
                        <font class="blackfnt">SERVICE TAX ( CENVET ) </font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblServiceTaxRate" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblServiceTaxValue" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt">Education Cess</font></td>
                    <td>
                        <font class="blackfnt"><font class="blackfnt">Cenvat (Education Cess) Receivable A/c </font></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblEduCessRate" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblEduCessValue" runat="server"></asp:Label></font>
                    </td>
                </tr>
                
                <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt">Higher Education Cess</font></td>
                    <td>
                        <font class="blackfnt"><font class="blackfnt">Cenvat (Higher Edu Cess) Receivable A/c </font></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblHigherEduRate" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblHigherEduValue" runat="server"></asp:Label></font>
                    </td>
                </tr>
                
                <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt">TDS</td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblTDS" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblTDSRate" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblTDSValue" runat="server"></asp:Label></font>
                    </td>
                </tr>
                <%--//changed by Anupam kkc--%>
                <tr bgcolor="#FFFFFF">
                <td>
                    <font class="blackfnt">Swachh Bharat Cess</font>
                </td>
                <td>
                    <font class="blackfnt"><font class="blackfnt">Cenvat (Swachh Bharat Cess) Receivable
                        A/c </font></font>
                </td>
                <td align="right">
                    <font class="blackfnt">
                        <asp:Label ID="lblSBRate" runat="server"></asp:Label></font>
                </td>
                <td align="right">
                    <font class="blackfnt">
                        <asp:Label ID="lblSBValue" runat="server"></asp:Label></font>
                </td>
            </tr>
              <tr bgcolor="#FFFFFF">
                <td>
                    <font class="blackfnt">Krishi Kalyan Cess (+)</font>
                </td>
                <td>
                    <font class="blackfnt"><font class="blackfnt">Krishi Kalyan Cess
                    </font></font>
                </td>
                <td align="right">
                    <font class="blackfnt">
                        <asp:Label ID="LblKKCessRate" runat="server"></asp:Label></font>
                </td>
                <td align="right">
                    <font class="blackfnt">
                        <asp:Label ID="LblKKCessAmt" runat="server"></asp:Label></font>
                </td>
            </tr>
                <%--//changed by Anupam kkc--%>
               <tr class="bgbluegrey">
                    <td align="left" colspan="4" >
                        <font class="blackfnt"><b>Other Details</font></td>
                    <%--<td align="left">
                        <font class="blackfnt"><b>Account Head</font></td>
                    <td align="right">
                        <font class="blackfnt"><b>Approved Rate (%)</font></td>
                    <td align="right">
                        <font class="blackfnt"><b>Value (in Rs.)</font></td>--%>
                </tr>
               
                  <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt"></td>
                    <td align="left">
                        <font class="blackfnt"><asp:Label ID="Label1" Font-Bold="true" Text="Discount Type"  runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="Label2" Font-Bold="true"  runat="server" Text="Discount (in %)"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="Label3" Font-Bold="true" Text="Discount Amt" runat="server"></asp:Label></font>
                    </td>
                </tr>
                 
                <tr bgcolor="#FFFFFF">
                    <td>
                        <font class="blackfnt">Discount Received</td>
                    <td align="left">
                        <font class="blackfnt"><asp:Label ID="lblDiscountType" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblPerc" runat="server"></asp:Label></font>
                    </td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblDiscount" runat="server"></asp:Label></font>
                    </td>
                </tr>
                
                <tr bgcolor="#FFFFFF">
                    <td colspan="3" >
                        <font class="blackfnt"><b>Net Payable Amount</b> </font></td>
                    <td align="right">
                        <font class="blackfnt"><asp:Label ID="lblNetPayableAmt" runat="server"></asp:Label></font>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>