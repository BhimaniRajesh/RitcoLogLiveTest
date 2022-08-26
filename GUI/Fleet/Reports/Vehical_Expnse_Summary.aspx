<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/GUI/MasterPage.master" CodeFile="Vehical_Expnse_Summary.aspx.cs" Inherits="GUI_Fleet_Reports_Vehical_Expnse_Summary" %>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="DateCalendar" TagName="Header" Src="~/GUI/DateSelector.ascx" %>

<%@ Register Assembly="AtlasControlToolkit" Namespace="AtlasControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="DT" TagPrefix="Date" Src="~/GUI/Common_UserControls/RPT_Date.ascx" %>
<%@ Register TagName="popUp" TagPrefix="popUp" Src="~/GUI/Common_UserControls/PopUpControl.ascx" %>
<%@ Register TagName="CMPFT" TagPrefix="Company" Src="~/GUI/Common_UserControls/CompayFilter.ascx" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server" >
  <script src="../../images/commonJs.js"  language="javascript" type="text/javascript"></script>

<script src="../../images/CalendarPopup.js"  language="javascript" type="text/javascript"></script>

  <script language="javascript" type="text/javascript">
        var cal = new CalendarPopup("testdiv1"); 
        cal.setCssPrefix("TEST");
        cal.showNavigationDropdowns();
        function cust(ID)
        {
         ///var FrmChild = window.open('../PopUp_Cust.aspx?ID="+ID' ,'myWindow','height=285,width=450,resizable=yes,scrollbars=yes,left=310,top=15'); 
         var strWinFeature = "menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=600 ,height=400,status=no,left=60,top=50"
            var strPopupURL = "../PopUp_Cust.aspx?ID="+ID
            
            winNew = window.open(strPopupURL, "_blank", strWinFeature)
        }
    </script>
    
  <script language="javascript">
  
         function nwOpen()
        {
            window.open("Popup-Vehicle.aspx?","pbr","menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=387,height=220,status=no,left=220,top=70")
            

        }
         function OnSub_DATACHECK()
    {
      
       var txtDateFrom=document.getElementById("ctl00$MyCPH1$DT$txtDateFrom");
       var txtDateTo=document.getElementById("ctl00$MyCPH1$DT$txtDateTo");
       var ddlBranch=document.getElementById("ctl00_MyCPH1_ddlBranch");
        var txtVehicleNo=document.getElementById("ctl00_MyCPH1_txtVehicleNo");
       var ddlvehmodel=document.getElementById("ctl00_MyCPH1_ddlvehmodel");
      var ddlCompanyList=document.getElementById("ctl00$MyCPH1$CMPFT1$ddlCompanyList");
      var hdnRptId = document.getElementById("ctl00_MyCPH1_hdnRptId"); // added by Manisha 21/5/2014
      window.open("Vehical_Expnse_Summary_Result.aspx?Start_dt=" + txtDateFrom.value + "&End_dt=" + txtDateTo.value + "&loccode=" + ddlBranch.value + "&Type_Code=" + ddlvehmodel.value + "&VehicleNo=" + txtVehicleNo.value + "&Company_Code=" + ddlCompanyList.value + "&hdnRptId=" + hdnRptId.value + "");
        //return false;
    }
        
   </script>
         
<div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;layer-background-color: white; z-index: 99;">
</div>
 <asp:UpdateProgress ID="uppMain" runat="server">
<ProgressTemplate>
    <iframe frameborder="0" src="about:blank"
        style="border:0px;position:absolute;z-index:9;left:0px;top:0px;width:expression(this.offsetParent.scrollWidth);height:expression(this.offsetParent.scrollHeight);filter:progid:DXImageTransform.Microsoft.Alpha(Opacity=75, FinishOpacity=0, Style=0, StartX=0, FinishX=100, StartY=0, FinishY=100);"></iframe>
        <div style="position:absolute;z-index:10;left:expression((this.offsetParent.clientWidth/2)-(this.clientWidth/2)+this.offsetParent.scrollLeft);top:expression((this.offsetParent.clientHeight/2)-(this.clientHeight/2)+this.offsetParent.scrollTop);"><img src="../../images/loading.gif" /><font face=verdana color=blue size=4>&nbsp;<b>Please Wait...</b></font></div>
</ProgressTemplate>
</asp:UpdateProgress>
   
   <input type="hidden" id="hdnRptId" runat="server" /> <%-- added by Manisha 21/5/2014 --%>

 <table cellspacing="1" style="width: 100%">
        <tr style="background-color: white">
            <td align="left">
                <asp:Label ID="lblPageHead" runat="server" CssClass="blackfnt" Font-Bold="True">Vehicle Expenes Register</asp:Label>
                <hr align="center" color="#8ba0e5" size="1" />
            </td>
        </tr>
    </table>
    
 <table cellspacing="1" style="width: 65%">
        <tr>
            <td><asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" RenderMode="Inline" runat="server">
                <ContentTemplate>
                <table cellspacing="1" style="width: 75%" class="boxbg">
                    
                    <tr class="bgbluegrey">
                        <td colspan="3" align="center">
                            <asp:Label ID="lblQry" CssClass="blackfnt" Font-Bold="true" runat="server">Search Criteria</asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr1" visible="true" runat="server" style="background-color: white">
                        <td   align="left" colspan="1" style="width: 250px"  >
                            <asp:Label ID="Label18"  Font-Bold="True" CssClass="blackfnt" Text="Select Company Type" runat="server"></asp:Label>
                        </td>
                        <td align="left" valign="top"  colspan="2" >
                           <Company:CMPFT ID="CMPFT1" runat="server" />
                       </td>
                    </tr>  
                   <tr style="background-color: white">
                        <td align="right" valign="center" style="width: 154px; text-align: left;">
                        <asp:Label ID="Label6" CssClass="blackfnt" runat="server" Font-Bold="True">Transaction Date Range</asp:Label></td>
                        <td align="left" colspan="2">
                        <Date:DT ID="DT" EnableTillDate="false" runat="server" />
                        </td>
                    </tr>         
                   
                    <tr style="background-color: white">
                        <td align="right" style="vertical-align: top; width: 25%; text-align: left">
                            <asp:Label ID="Label4" runat="server" CssClass="blackfnt" Font-Bold="True" Width="150px">Select Branch :</asp:Label></td>
                        <td align="left" colspan="2" style="width: 123%;">
                           <asp:DropDownList ID ="ddlBranch"  runat="server" Width="150px" ></asp:DropDownList> 
                         </td>
                    </tr>
                    
                    <tr style="background-color: white">
                        <td align="right" style="vertical-align: top; width: 25%; text-align: left">
                            <asp:Label ID="Label1" runat="server" CssClass="blackfnt" Font-Bold="True" Width="150px">Select Vehicle Model :</asp:Label></td>
                        <td align="left" colspan="2" style="width: 123%;">
                           <asp:DropDownList ID ="ddlvehmodel"  runat="server" Width="150px" ></asp:DropDownList> 
                         </td>
                    </tr>
                    
                    <tr style="background-color: white">
                        <td align="right" style="vertical-align: top; width: 25%; text-align: left">
                            <asp:Label ID="Label2" runat="server" CssClass="blackfnt" Font-Bold="True" Width="150px">Vehicle No :</asp:Label></td>
                        <td align="left" colspan="2" style="width: 123%;">
                            <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="Input" Width="129px"></asp:TextBox>
                            <input id="VEHNO" runat="server"   type="button" value="..." onclick="javascript:nwOpen('Popup-Vehicle.aspx',null,'height=250,width=300,left=350,top=200,status=no,resizable=no,scrollbars=yes,toolbar=no,location=no,menubar=no');"  />
                            
                           </td>
                    </tr>
                    
                   

   <tr style="background-color: white">
                        <td colspan="4" align="right" style="width: 100%">
                           <asp:UpdatePanel ID="UPShowCustomer" UpdateMode="Conditional" RenderMode="Inline" runat="server">
                                <ContentTemplate>
                                 <asp:Button ID="btnShow" runat="server" Text="Submit"   OnClientClick="Javascript: return OnSub_DATACHECK();" /> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        
            </td>
        </tr>
       
    </table>
    <br>
     <br />
 </asp:Content>
