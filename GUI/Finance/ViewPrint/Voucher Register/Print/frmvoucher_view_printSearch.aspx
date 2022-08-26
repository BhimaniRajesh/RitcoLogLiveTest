<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/GUI/MasterPage.master" CodeFile="frmvoucher_view_printSearch.aspx.cs" Inherits="GUI_Finance_voucher_Print_frmvoucher_view_printSearch" %>
<%@ Reference Control="~/GUI/DateSelector.ascx" %>
<%@ Reference Control="~/GUI/DateSelectorTo.ascx" %>
<%@Register TagPrefix="SControls" TagName="DateSelector" src="~/GUI/DateSelector.ascx" %>
<%@Register TagPrefix="SControlsTo" TagName="DateSelectorTo" src="~/GUI/DateSelectorTo.ascx" %>
<asp:Content ContentPlaceHolderID="MyCPH1" ID="voucher_vp_search" runat="server">
<script type="text/javascript" language="javascript" src="../../../cal/popcalendar.js"></script>
<script type="text/javascript">
var redToday;
var redFromTo;
var redSevenDay;
      function Error()
      {
        if (document.getElementById('ctl00$MyCPH1$dlstSalesAmount').value == "")
        {
            alert("Please Select Account");
            return false;
        }
        else if (document.getElementById('ctl00$MyCPH1$chk').value == "1")
        {
        if (document.getElementById('ctl00$MyCPH1$txtDateFrom$txt_Date').value == "" || document.getElementById('ctl00$MyCPH1$txtDateTo$txt_Date').value == "")
            {
                alert("Please Select Date Range");
                return false;
            }
            else
            {
                return true;
            }
        }
                
      }
      function CHK1(ClientId)
      {
        var idid = ClientId.value;
        document.getElementById('ctl00$MyCPH1$chk').value = ""
        if (idid == "redFromTo")
        {
            document.getElementById('ctl00$MyCPH1$chk').value = "1";
        }
        else
        {
            document.getElementById('ctl00$MyCPH1$chk').value = "0";
        }
        
      }
  
    
  </script>
<br /><br /><br />
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                
              <tr>
                <td style="width: 10%"></td> 
                <td width="90%">
                    <table border="0" cellpadding="1" cellspacing="1" class="boxbg" width="600px">
		 <tr class="bgbluegrey"> 
      <td  align="center" colspan="4"> 
        <p align="center"><font class="blackfnt">Select Branch / Profit Center </font></p>
      </td>
    </tr>
		<tr bgcolor="#FFFFFF"> 
        <td  align="center" colspan="2"> 
          <div align="right"><font class="blackfnt">Select Branch / Profit Center</font> 
            : </div>
        </td>
        <td bgcolor="#FFFFFF" align="center" colspan="2"> 
          <div align="left"><font class="blackfnt">&nbsp;<asp:DropDownList ID="dlstBranch" runat="server" Width="250px">
              </asp:DropDownList>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dlstBranch"
                  ErrorMessage="*"></asp:RequiredFieldValidator></font></div>
          
        </td>
      </tr> 
      
				<tr class="bgbluegrey"> 
      <td  align="center" colspan="4"> 
        <p align="center"><font class=blackfnt>Select Voucher Date Range 
</font> 
      </td>
    </tr>
    <tr bgcolor="#FFFFFF">
				<TD align=center style="width: 11%">
                    <asp:RadioButton ID="redFromTo" runat="server" GroupName="red"/></TD>
			     <TD><SControls:DateSelector ID="txtDateFrom" runat="server" />   
			        </TD>
			    <TD style="width: 302px">
			    <SControlsTo:DateSelectorTo ID="txtDateTo" runat="server" />
			    
				</TD>
			</TR>
    <center>
    <tr> 
        <td bgcolor="#FFFFFF" align="center" colspan="1" style="width: 11%"> 
            <asp:RadioButton ID="redToday" runat="server" GroupName="red" />&nbsp;</td>
        <td bgcolor="#FFFFFF" align="center" colspan="3"> 
          <div align="left"><font class=blackfnt>&nbsp;&nbsp;Today</font> 
          </div>
        </td>
      </tr>
    <center>
      <tr> 
        <td bgcolor="#FFFFFF" align="center" colspan="1" style="width: 11%"> 
            <asp:RadioButton ID="redSevenDay" runat="server" GroupName="red" Checked="True"/>&nbsp;</td>
        <td bgcolor="#FFFFFF" align="center" colspan="3"> 
          <div align="left"><font class=blackfnt>&nbsp;&nbsp;Last 7 Days</font> 
          </div>
        </td>
      </tr>
     <tr> 
        <td bgcolor="#FFFFFF" align="center" colspan="1" style="width: 11%"> &nbsp;<font class="blackfnt">Voucher Type</font></td>
        <td bgcolor="#FFFFFF" align="center" colspan="3"> 
          <div align="left"><font class=blackfnt>&nbsp;<asp:DropDownList ID="dlstVoucherType" runat="server">
                  <asp:ListItem Value="C">Credit Voucher</asp:ListItem>
                  <asp:ListItem Value="D">Debit Voucher</asp:ListItem>
              </asp:DropDownList></font></div>
        </td>
      </tr>
    </center>
                  <tr> 
					<td align="center" colspan="4" bgcolor="White"><b><font class="blackfnt" >
					<asp:Button ID="Button1" runat="server" Text="Submit" BorderStyle="None" style="text-decoration: underline" CssClass="blackfnt" Font-Size="10pt" Height="23px" OnClick="Button1_Click" Width="83px" OnClientClick="return Error(this);" />
					</font></td>
			      </tr>
				  <%--<tr bgcolor="#FFFFFF"> 
					
					<td align="center"  height="22" colspan="4">
                        <Acct_Gropus:Acct ID="acct_gropus" runat="server" />
                        </td>
				  </tr>--%>
				<center>
				</center>
			</table>
                </td>
              </tr>
            </table>
            <asp:TextBox ID="chk" runat="server" BackColor="White" BorderColor="White" BorderStyle="None" ForeColor="White" Width="1px">0</asp:TextBox>
</asp:Content>