<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCOF.aspx.cs" Inherits="GUI_CIR_PrintCOF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" src="~/GUI/images/CommonJS.js" type="text/javascript" ></script>
       <link id="Link1" href="~/GUI/images/style.css" rel="Stylesheet" type="text/css" runat="server" />
 
</head>
<body onload ="javascript:window.print()">

   <form id="form1" runat="server">
   
   <br/>
   <br/>
<p align="center">
 <table cellspacing="1" cellpadding="3" width="800px"  border="0" >
       <tr style="background-color:White" align="center">
       <td align="center"> 
  
      <table cellspacing="1" cellpadding="3" width="800px">
      <tr style="background-color:White" align="center">
      <td align="center">
                   <asp:Label ID="Label1"   class="blackfnt"  runat="server" Font-Bold="true"  Font-Size="12"> WITHOUT PREJUDICE</asp:Label>
      
      
      </td>
      </tr>
       <tr align="center">
      <td align="center" style="height: 25px">
                       <asp:Label  ID="lblHead2" class="blackfnt"  runat="server" Font-Bold="true" Font-Size="14">  CERTIFICATE  OF  FACTS </asp:Label>
      
      
      </td>
      </tr>
      </table>
  <br/>
     <hr/> 
      
      <br/>
    
      
    
       <table cellspacing="1" cellpadding="3" width="800px"  border="0" >
      <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="Label2"   class="blackfnt"  runat="server" Font-Bold="true" >Issueing Office</asp:Label>
      
      
      </td>
       <td align="left">
                   <asp:Label ID="Label3"   class="blackfnt"  runat="server" Font-Bold="true"  > Station Code/Name</asp:Label>
      
      
      </td>
      
     <%--  <td align="left">
                   <asp:Label ID="Label4"   class="blackfnt"  runat="server" Font-Bold="true"  > Station Name</asp:Label>
      
      
      </td>--%>
      
      
      
       <td align="left">
                   <asp:Label ID="Label5"   class="blackfnt"  runat="server" Font-Bold="true"  >Date</asp:Label> <asp:Label ID="Label25"   class="blackfnt"  runat="server"  ></asp:Label>
      
       <%-- <asp:RequiredFieldValidator ID="RFVlblDate" runat="server" ControlToValidate="lblDate"
                                ErrorMessage="*" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="blackfnt"
                                ErrorMessage="!" ControlToValidate="lblDate" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
      --%>
      </td>
      
      
      </tr>
      
        <tr style="background-color:White" align="center" >
      <td align="left" >
                   <asp:Label ID="lblIssueOff"   class="blackfnt"  runat="server"   ></asp:Label>
      
      
      </td>
       <td align="left">
                   <asp:Label ID="lblStnCode"   class="blackfnt"  runat="server"   ></asp:Label>
      
      
      </td>
      
       <%--<td align="left">
                   <asp:Label ID="lblStnName"   class="blackfnt"  runat="server"    ></asp:Label>
      
      </td>--%>
      
      
      
       <td align="left">
                   <asp:Label ID="lblDate"   class="blackfnt"  runat="server"  ></asp:Label>
      
      
      </td>
      
      
      </tr>
      </table>
      <hr/>
      <br/>
       <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="lblHead"   class="blackfnt"  runat="server" Font-Bold="true" >For Claiming Party</asp:Label>
      
      
      </td>
       <td align="left">
                   <asp:Label ID="lblClaimParty"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      
      </td>
   
      </tr>
      <tr style="background-color:White" align="center">
        <td>
        </td>
      <td align="left" >
          <asp:Label ID="lblPName"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      </td>
      
      </tr>
      
        <tr style="background-color:White" align="center">
        <td>
        </td>
      <td align="left" >
          <asp:Label ID="lblCLPAdd"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      </td>
      
      </tr>
      
        <tr style="background-color:White" align="center">
      <td align="left" >
          <asp:Label ID="lblHClaimBasis"   cssclass="blackfnt"  runat="server" Font-Bold="true">Claims Basis</asp:Label>
      </td>
      <%--</tr>
       <tr style="background-color:White" align="center">--%>
      <td align="left" >
          <asp:Label ID="lblClaimBasis"   cssclass="blackfnt"  runat="server"  ></asp:Label>
      </td>
    <%--  </tr>--%>
      
      
        <tr style="background-color:White" align="center">
      <td align="left" >
          <asp:Label ID="DocketNo"   cssclass="blackfnt" Text="Docket No:" runat="server" Font-Bold="true"></asp:Label>
      </td>
       <td align="left" >
          <asp:Label ID="lblDocNote"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>
         <td align="left" >
          <asp:Label ID="lblHDocDate"   cssclass="blackfnt" Text="Docket Date:" runat="server" Font-Bold="true"></asp:Label>
      </td>
         <td align="left" >
          <asp:Label ID="lblDocDate"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>
      </tr>
      </table>
      <br/>
      <hr/>
          <table cellspacing="1" cellpadding="3" width="800px"  border="0" >
      <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="Label6"   class="blackfnt"  runat="server" Font-Bold="true"  >Booking Office</asp:Label>
      
      
     <%-- </td>
       <td align="left">
                   <asp:Label ID="Label7"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Origin Station</asp:Label>
      
      
      </td>--%>
   
     
  
       
        
      <td align="left" >
          <asp:Label ID="Label8"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Delivery Office</asp:Label>
      
      </td>
      
   
   <%--   <td align="left" >
          <asp:Label ID="Label9"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Destination Office</asp:Label>
      </td>--%>
      </tr>
        <tr style="background-color:White" align="center">
      <td align="left" >
          <asp:Label ID="lblBKGStation1"   cssclass="blackfnt"  runat="server"></asp:Label>
      
    
      
      
      <%--<td align="left" >
          <asp:Label ID="lblBKGStation2"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>--%>
       <td align="left" >
          <asp:Label ID="lblDelOff"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>
       <%--  <td align="left" >
          <asp:Label ID="lblDestStn"   cssclass="blackfnt"   runat="server"></asp:Label>
      </td>--%>
     
      </tr>
      </table>
      
      <br/>
      <hr/>
         <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="lblS"   class="blackfnt"  runat="server" Font-Bold="true"  >Said to Contain:</asp:Label> <%--asp:Label ID="lblSaidtocontain"   cssclass="blackfnt"  runat="server" ></asp:Label>--%>
      
      
      </td>
     <td align="left">
             <asp:Label ID="lblSaidtocontain"   cssclass="blackfnt"  runat="server" ></asp:Label>
      
      
      </td> 
      
 <%--     <td align="left" >
          <asp:Label ID="Label11"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Booked At:</asp:Label>  <%-- <asp:Label ID="lblBookedAt"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      </td>
      
   
     <td align="left" >
          <asp:Label ID="lblBookedAt"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td> --%>
      </tr>
     
      </table>
      <br/>
      <hr/>
         <table cellspacing="1" cellpadding="3" width="800px"  border="0">
      <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="Label10"   class="blackfnt"  runat="server" Font-Bold="true">Pkg Booked:</asp:Label>
      
      
      </td>
       <td align="left">
                   <asp:Label ID="Label12"   cssclass="blackfnt"  runat="server"  Font-Bold="true" >Pkg Delivered</asp:Label>
      
      
      </td>
      
      <td align="left" >
          <asp:Label ID="Label13"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Actual Booked Wt</asp:Label>
      
      </td>
      
   
      <td align="left" >
          <asp:Label ID="Label14"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Delivered Wt</asp:Label>
      </td>
      <td align="left" >
          <asp:Label ID="Label15"   cssclass="blackfnt"  runat="server" Font-Bold="true" >Delivered On</asp:Label>
      </td>
      </tr>
      
         <tr style="background-color:White" align="center">
      <td align="left" >
                   <asp:Label ID="lblPkgB"   class="blackfnt"  runat="server"   ></asp:Label>
      
      
      </td>
       <td align="left">
                   <asp:Label ID="lblPkgD"   cssclass="blackfnt"  runat="server" ></asp:Label>
      
      
      </td>
      
      <td align="left" >
          <asp:Label ID="lblABW"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      </td>
      
   
      <td align="left" >
          <asp:Label ID="lblDWt"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>
      <td align="left" >
          <asp:Label ID="lblDOn"   cssclass="blackfnt"  runat="server"></asp:Label>
      </td>
      </tr>
     
      </table>
      
      <br/>
      <hr/>
       <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="center">
      <td align="left">
          <asp:Label ID="Label16"   cssclass="blackfnt"  runat="server">     We do hereby certify that at the time of delivery of the  above Docket, the following was observed.The certificate is issued entirely without prejudice and does not constitute any  admission of liability or claim whatsover on our part. </asp:Label>
      
      </td>
      </tr>
      </table>
      <br/>
        <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label17"   cssclass="blackfnt"  runat="server" Font-Bold="true">Outward Conditions of Packages:</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="lblconditions"   cssclass="blackfnt"  runat="server"></asp:Label>
      
      </td>
      </tr>
    
      <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label18"   cssclass="blackfnt"  runat="server" Font-Bold="true">Details of Facts:</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="txtDFacts"   cssclass="blackfnt"  runat="server" ></asp:Label>
      
      </td>
      </tr>
       <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label19"   cssclass="blackfnt"  runat="server" Font-Bold="true">Declared Value:</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="lblDValue"   cssclass="blackfnt"  runat="server" ></asp:Label>
      
      </td>
      </tr>
         <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label4"   cssclass="blackfnt"  runat="server" Font-Bold="true">Customer Invoice Number:</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="lblInvoice"   cssclass="blackfnt"  runat="server" ></asp:Label>
      
      </td>
      </tr>
        <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label20"   cssclass="blackfnt"  runat="server" Font-Bold="true">Amount Of Loss(In Rs.):</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="txtAmtLoss"   cssclass="blackfnt"  runat="server" ></asp:Label>
                   <%--    <asp:RequiredFieldValidator ID="reqRequestBy1" runat="server" Display="Dynamic" Text="!" ControlToValidate="txtAmtLoss"></asp:RequiredFieldValidator>--%>
    <%--    <asp:RangeValidator ID="RangeValidator5" runat="server"  MinimumValue="0"  MaximumValue="99999999" Display="Dynamic" Text="*" ControlToValidate="txtAmtLoss"></asp:RangeValidator>--%>
      </td>
      </tr>
      
         <tr style="background-color:White" align="left">
      <td align="left"  >
          <asp:Label ID="Label21"   cssclass="blackfnt"  runat="server"  Font-Bold="true">Issued By the party with</asp:Label>
      
      </td>
      <td align="left"  >
          <asp:Label ID="lblIssuedParty"   cssclass="blackfnt"  runat="server"  ></asp:Label>
      
      </td>
      </tr>
      </table>
      <br/>
         <br/>
        <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="left">
      <td align="left">
        <asp:Label ID="Label22"   cssclass="blackfnt"  runat="server"  Font-Bold="true">Signature of the Party with seal:</asp:Label><%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Label ID="Label23"   cssclass="blackfnt"  runat="server">For:</asp:Label>--%>
     </td>
   <td align="center">
        <asp:Label ID="Label23"   cssclass="blackfnt"  runat="server">For</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCompany" Font-Bold="TRUE"   cssclass="blackfnt"  runat="server"></asp:Label>
     </td>
     </tr>
     </table>
          <br/>
             <br/>
              <br/>
             <br/>
      <hr/>
    
       <table cellspacing="1" cellpadding="3" width="800px" >
      <tr style="background-color:White" align="center">
      <td align="left">
        <asp:Label ID="Label24"   cssclass="blackfnt"  runat="server">    The Court in Mumbai City alone shall have jurisdiction in respect of all claim and dispute arising under this Certificate of Facts. </asp:Label>
     </td>
    
     </tr>
     </table>
     
   </p>

    

  </form>
</body>
</html>
