<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="GRNCriteria.aspx.cs" Inherits="GUI_Finance_GeneralizedPO_GRNGeneration_GRNCriteria" %>

<%@ Register TagName="DT" TagPrefix="Date" Src="~/GUI/Common_UserControls/RPT_Date.ascx" %>
<%@ Register TagName="CMPFT" TagPrefix="Company" Src="~/GUI/Common_UserControls/CompayFilter.ascx" %>
<%@ Register TagName="LH" TagPrefix="Location" Src="~/GUI/Common_UserControls/LocationHirarchy.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">

    <script src="../../../Images/CalendarPopup.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../images/CalendarPopup.js"></script>
    
    <script language="javascript" type="text/javascript" src="../../../images/commonJs.js"></script>

    <script language="javascript" type="text/javascript">
        var cal = new CalendarPopup("testdiv1");
        cal.setCssPrefix("TEST");
        cal.showNavigationDropdowns();
        function Validate() 
        {
            var PONo = document.getElementById('ctl00_MyCPH1_PONo');
            var ManualPONo = document.getElementById('ctl00_MyCPH1_POManualNo');

            if (PONo.value == '' && ManualPONo.value == '' && document.getElementById('ctl00_MyCPH1_Txt_Custcd').value == '') {
                alert('Please Enter the GRN Generation Criteria');
                PONo.focus();
                return false;
            }
            else 
            {
                return true;
            }
         }
         function ShowPOFilter() {
             document.getElementById('ctl00_MyCPH1_TrCompany').style.display = 'block';
             document.getElementById('ctl00_MyCPH1_TrLocation').style.display = 'block';
             document.getElementById('ctl00_MyCPH1_TrPOType').style.display = 'block';
             document.getElementById('ctl00_MyCPH1_TrPODate').style.display = 'block';
             document.getElementById('ctl00_MyCPH1_TrParty').style.display = 'block';
             
         }
         function HidePOFilter() {
             document.getElementById('ctl00_MyCPH1_TrCompany').style.display = 'none';
             document.getElementById('ctl00_MyCPH1_TrLocation').style.display = 'none';
             document.getElementById('ctl00_MyCPH1_TrPOType').style.display = 'none';
             document.getElementById('ctl00_MyCPH1_TrPODate').style.display = 'none';
             document.getElementById('ctl00_MyCPH1_TrParty').style.display = 'none';
         }
         function Show_assetNM(Obj, Type) {
             if (Obj.value == "") {
                 return false;
             }
             if (Type != "ManualPONo" && Type != "PONo") {
                 var Search_Char = "~"
                 if (Obj.value.indexOf(Search_Char) == -1) {
                     alert("Vendor Code is not Valid... Please Enter Valid Vendor Code");
                     Obj.focus();
                     return false
                 }
             }
             var TxtAssetcode = Obj.id;
             var txtastnm = TxtAssetcode.replace("txtassetcd", "txtastnm");
             var Assetcode_Arr = document.getElementById(TxtAssetcode).value.split("~");
             if (Type != "PONo" && Type != "ManualPONo") 
             {
                 Assetcode = Assetcode_Arr[1]
             }
             else 
             {
                 Assetcode = document.getElementById(TxtAssetcode).value
             }
             if (Assetcode != "") {
                 var findobj = false;
                 findobj = GetXMLHttpObject();
                 if (findobj) {
                     var strpg = "CheckExist.aspx?mode=" + Type + "&Code=" + Assetcode + "&sid=" + Math.random();
                     findobj.open("GET", strpg, true);
                     findobj.onreadystatechange = function() {
                         if (findobj.readyState == 4 && findobj.status == 200) {
                             var res = findobj.responseText.split("|");

                             if (Type == "Vendor") {
                                 if (res[0] == "false") {
                                     alert("Vendor Code is not Valid... Please Enter Valid Vendor Code");
                                     document.getElementById(TxtAssetcode).value = "";
                                     document.getElementById(TxtAssetcode).focus();
                                     return false;
                                 }
                             }
                             else if (Type == "ManualPONo" || Type == "PONo") {
                                 if (res[0] == "false") {
                                     alert("Please Enter Valid PO Number.");
                                     document.getElementById(TxtAssetcode).value = "";
                                     document.getElementById(TxtAssetcode).focus();
                                     return false;
                                 }
                             }
                             else {
                                 if (res[0] == "false") {
                                     alert("Item Code is not Valid... Please Enter Valid Item Code");
                                     document.getElementById(TxtAssetcode).value = "";
                                     document.getElementById(TxtAssetcode).focus();
                                     return false;
                                 }
                             }
                         }
                     }
                     findobj.send(null);
                 }
                 return false;
             }
         }
    </script>

    <div>
        <table style="width: 10in" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table border="0" cellpadding="3" cellspacing="1" class="boxbg" style="width: 75%;"
                        align="left">
                        <tr class="bgbluegrey" style="height: 25px">
                            <td align="center" colspan="3">
                                <strong><span style="font-size: 8pt; font-family: Verdana">GRN Generation Criteria</span></strong>
                            </td>
                        </tr>
                        <tr bgcolor="white">
                            <%--<td style="width: 10px;">
                                <asp:RadioButton ID="rdDocketCri" GroupName="grpcri" runat="server" />
                            </td>--%>
                            <td align="center" style="width: 100px;">
                                <asp:Label ID="lbl_DocNo" runat="server" CssClass="blackfnt" Text="Enter PO Number"></asp:Label>
                            </td>
                            <td style="width: 339px;" colspan="2">
                                <asp:TextBox ID="PONo" runat="server" MaxLength="100" BorderStyle="Groove" 
                                CssClass="EntryText" Width="150px" onblur="javascript:Show_assetNM(this,'PONo')"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr bgcolor="white" align="center">
                            <td align="center" colspan="3">
                                <span style="font-size: 8pt; font-family: Verdana; text-align: center"><b>OR</b>
                                </span>
                            </td>
                        </tr>
                        <tr bgcolor="white">
                            <%--<td style="width: 10px;">
                                <asp:RadioButton ID="RadioButton1" GroupName="grpcri" runat="server" />
                            </td>--%>
                            <td align="left">
                                <asp:Label ID="lbl_DocManualNo" runat="server" CssClass="blackfnt" Text="Enter Manual PO No"></asp:Label>
                            </td>
                            <td style="width: 339px;" colspan="2">
                                <asp:TextBox ID="POManualNo" runat="server" MaxLength="100" Width="150px" 
                                BorderStyle="Groove" CssClass="EntryText" onblur="javascript:Show_assetNM(this,'ManualPONo')"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr bgcolor="white" align="center">
                            <td align="center" colspan="3">
                                <span style="font-size: 8pt; font-family: Verdana; text-align: center"><b>OR</b>
                                
                                </span>
                            </td>
                        </tr>
                        <tr style="background-color: white">
                            <td align="left" colspan="3">
                                <font class="bluefnt"><i><b>Filters : -</b></i></font> <a href="Javascript:ShowPOFilter();">
                                    <font class="bluefnt"><u><b>Show</b></u></font> </a><font class="blackfnt">/</font>
                                <a href="Javascript:HidePOFilter();"><font class="bluefnt"><u><b>Hide</b></u></font>
                                </a>
                            </td>
                        </tr>
                        <tr id="TrCompany" runat="server" style="background-color: white; display: none;">
                            <td align="left">
                                <font class="blackfnt">Select Company</font>
                            </td>
                            <td align="left" colspan="2">
                                <Company:CMPFT ID="CMPFT1" runat="server" />
                            </td>
                        </tr>
                        <tr id="TrLocation" runat="server" style="background-color: white; display: none;">
                            <td align="left" colspan="3">
                                <Location:LH ID="Fromlc" runat="server" />
                            </td>
                        </tr>
                        <tr id="TrPOType" style="background-color: white; display: none;" runat="server">
                            <td>
                                <label class="blackfnt">
                                    Select P.O Type :
                                </label>
                            </td>
                            <td colspan="2" align="left" style="height: 21px">
                                <asp:DropDownList ID="ddlMatCat" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="TrPODate" runat="server" bgcolor="white" style="font-size: 12pt; font-family: Times New Roman;background-color: white; display: none;">
                           <td>
                                <font class="blackfnt">Select PO Date</font>
                            </td>
                            <td align="left" colspan="2">
                                <Date:DT ID="DT" runat="server" />
                            </td>
                        </tr>
                        <tr id="TrParty" class="nrow" runat="server" style="background-color: white; display: none;">
                            <td style="width: 120px">
                                <asp:Label ID="lblPartyCode" runat="server" CssClass="nfnt">Select Vendor Code</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Txt_Custcd" runat="server" onblur="javascript:Show_assetNM(this,'Vendor')"
                                    CssClass="ltxtm" Width="270px" MaxLength="100"></asp:TextBox><br />
                                <div id="Abc" runat="server" class="boxbg" />
                                <ajaxToolkit:AutoCompleteExtender ID="ExCutomer" runat="server" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionListItemCssClass="autocomplete_listItem" Enabled="True" MinimumPrefixLength="1"
                                    ServiceMethod="GetVendor" ServicePath="~/GUI/Finance/GeneralizedPO/AtlasAutoComplete.asmx"
                                    TargetControlID="Txt_Custcd">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr class="bgbluegrey" style="font-size: 12pt; font-family: Times New Roman;">
                            <td align="center" colspan="3" style="height: 25px">
                                <asp:Button ID="ButSubmit" runat="server" Text="Submit" OnClick="ButSubmit_Click"
                                    CssClass="BtnClass" OnClientClick="javascript:return Validate();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;
        z-index: 99;">
    </div>
</asp:Content>
