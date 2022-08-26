<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true"
    CodeFile="Query.aspx.cs" Inherits="GUI_Fleet_Reports_Vehicle_Expen_Query" %>

<%@ Register TagName="DT" TagPrefix="Date" Src="~/GUI/Common_UserControls/RPT_Date.ascx" %>
<%@ Register TagName="CMPFT" TagPrefix="Company" Src="~/GUI/Common_UserControls/CompayFilter.ascx" %>
<%@ Register Assembly="AtlasControlToolkit" Namespace="AtlasControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">

    <script src="../../../images/commonJs.js" language="javascript" type="text/javascript"></script>

    <script src="../../../images/CalendarPopup.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function nwOpen() {

            window.open("Popup-Vehicle.aspx?", "pbr", "menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=387,height=220,status=no,left=220,top=70");


        }
        var cal = new CalendarPopup("testdiv1");
        cal.setCssPrefix("TEST");
        cal.showNavigationDropdowns();
        function cust(ID) {
            ///var FrmChild = window.open('../PopUp_Cust.aspx?ID="+ID' ,'myWindow','height=285,width=450,resizable=yes,scrollbars=yes,left=310,top=15'); 
            var strWinFeature = "menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=600 ,height=400,status=no,left=60,top=50"
            var strPopupURL = "../PopUp_Cust.aspx?ID=" + ID

            winNew = window.open(strPopupURL, "_blank", strWinFeature)
        }
        function OnSub_DATACHECK() {

            var txtDateFrom = document.getElementById("ctl00$MyCPH1$DT$txtDateFrom");
            var txtDateTo = document.getElementById("ctl00$MyCPH1$DT$txtDateTo");
            var ddlBranch = document.getElementById("ctl00_MyCPH1_ddlBranch");
            //alert(ddlBranch.value);
            // return false;
            var txtVehicleNo = document.getElementById("ctl00_MyCPH1_txtVehicleNo");
            var ddlCompanyList = document.getElementById("ctl00$MyCPH1$CMPFT1$ddlCompanyList");
            var hdnRptId = document.getElementById("ctl00_MyCPH1_hdnRptId"); // added by Manisha 21/5/2014
            window.open("Result.aspx?Company_Code=" + ddlCompanyList.value + "&Start_dt=" + txtDateFrom.value + "&End_dt=" + txtDateTo.value + "&loccode=" + ddlBranch.value + "&VehicleNo=" + txtVehicleNo.value + "&hdnRptId=" + hdnRptId.value + "");
            return false;
        }
        
    </script>

    <div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;
        layer-background-color: white; z-index: 99;">
    </div>
   
   <input type="hidden" id="hdnRptId" runat="server" /> <%-- added by Manisha 21/5/2014 --%>
 <table cellspacing="1" style="width: 100%">
        <tr style="background-color: white">
            <td align="left">
                <asp:Label ID="lblPageHead" runat="server" CssClass="blackfnt" Font-Bold="True">Vehicle Expenes Register</asp:Label>
                <hr align="center" color="#8ba0e5" size="1" />
            </td>
        </tr>
    </table>
    <table cellspacing="1" style="width: 800px">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" RenderMode="Inline" runat="server">
                    <ContentTemplate>
                        <table cellspacing="1" style="width: 100%" class="boxbg">
                            <tr class="bgbluegrey">
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblQry" CssClass="blackfnt" Font-Bold="true" runat="server">Search Criteria</asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr1" visible="true" runat="server" style="background-color: white">
                                <td align="left">
                                    <asp:Label ID="Label12" CssClass="blackfnt" Text="Select Company Type" runat="server"></asp:Label>
                                </td>
                                <td align="left" valign="top">
                                    <Company:CMPFT ID="CMPFT1" runat="server" />
                                </td>
                            </tr>
                            <tr style="background-color: white">
                                <td align="right" valign="center" style="text-align: left; width: 69px;">
                                    <asp:Label ID="Label6" CssClass="blackfnt" runat="server" Font-Bold="True" Width="171px">Date Range:</asp:Label>
                                </td>
                                <td align="left" colspan="1">
                                    <Date:DT ID="DT" EnableTillDate="false" runat="server" />
                                </td>
                            </tr>
                            <tr style="background-color: white">
                                <td align="right" style="vertical-align: top; width: 25%; text-align: left">
                                    <asp:Label ID="Label4" runat="server" CssClass="blackfnt" Font-Bold="True" Width="150px">Select Branch :</asp:Label>
                                </td>
                                <td align="left" style="width: 123%;">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: white">
                                <td align="right" style="vertical-align: top; width: 25%; text-align: left">
                                    <asp:Label ID="Label2" runat="server" CssClass="blackfnt" Font-Bold="True" Width="150px">Vehicle No :</asp:Label>
                                </td>
                                <td align="left" style="width: 123%;">
                                    <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="Input" Width="129px"></asp:TextBox><input
                                        id="Vehno" runat="server" type="button" value="..." onclick="javascript:nwOpen('Popup-Vehicle.aspx',null,'height=250,width=300,left=350,top=200,status=no,resizable=no,scrollbars=yes,toolbar=no,location=no,menubar=no');" />
                                </td>
                            </tr>
                            <tr style="background-color: white">
                                <td colspan="2" align="right" style="width: 100%">
                                    <asp:UpdatePanel ID="UPShowCustomer" UpdateMode="Conditional" RenderMode="Inline"
                                        runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShow" runat="server" Text="Submit" ValidationGroup="VGDtFromTo"
                                                OnClientClick="Javascript: return OnSub_DATACHECK();" />
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
</asp:Content>
