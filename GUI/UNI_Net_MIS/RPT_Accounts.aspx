<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master"   AutoEventWireup="true" CodeFile="RPT_Accounts.aspx.cs" Inherits="GUI_UNI_Net_MIS_RPT_Accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">
<br />
<table style"width:8.5 in; " border="0" align="left" cellpadding="0" cellspacing="0">
    	<tr> 
          <td height="30"><font class="blklnkund"><strong> Module</strong></font>
          
		<strong> &gt; </strong><font class="blklnkund"><strong>Administrator </strong></font>

<strong> &gt; </strong> <font class="blklnkund"><strong>Reports </strong></font>



<font class=bluefnt><strong> &gt; </strong>
		  <strong>Accounts</strong> </font></td>
        </tr>
        <tr> 
          <td class="horzblue"><img src="../images/clear.gif" width="2" height="1" /></td>
        </tr>
        <tr> 
          <td><img src="../images/clear.gif" width="15" height="10"/></td>
        </tr>

   
        
        <tr> 
          <td align=right><img src="../images/back.gif" /></td>
        </tr>
                    <tr> 
                      <td height="300" valign="top">
                      
                      
                      
                      
                      <asp:Table id="tblRptOpn" HorizontalAlign="left" runat="server" CellPadding="3" CellSpacing="1" style="width:8.5in;" CssClass="boxbg">
        <asp:TableRow ID="tblrow" Visible="false" runat="server">
        <asp:TableCell>
        <asp:Label ID="Label1"  Text="No Reports Found" CssClass="blackfnt" runat="server"></asp:Label>
        
        </asp:TableCell></asp:TableRow>
        </asp:Table>
       
          <table id="tblnorec"   visible="false" cellpadding="3"  cellspacing="1" runat="server" style="width:8.5in;">
        <tr >
        <td align="center">
        <asp:Label ID="Label2"  ForeColor="Red" Text="No Accounts Reports Available For This User" CssClass="blackfnt" runat="server"></asp:Label>
        </td>
        </tr>
        </table>
                      
                      
                      </td>
                    </tr>
                    <tr> 
                      <td>&nbsp;</td>
                    </tr>
                    <tr>
                      <td></td>
                    </tr>
                  </table>
</asp:Content>
