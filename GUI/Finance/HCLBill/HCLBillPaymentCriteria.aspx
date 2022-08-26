﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="HCLBillPaymentCriteria.aspx.cs" Inherits="GUI_Finance_HCLBill_HCLBillPaymentCriteria" %>

<%@ Register TagPrefix="UserControl" TagName="DateRange" Src="~/GUI/Common_UserControls/RPT_Date.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">

    <script language="javascript" type="text/javascript">
        var cal = new CalendarPopup("divDate");
        cal.setCssPrefix("TEST");
        cal.showNavigationDropdowns();
    </script>
    <div style="width: 800px;">
        <table class="stbl" cellspacing="1" cellpadding="3" style="width: 900px" id="tblCriteria" runat="server">
            <tr class="hrow">
                <td colspan="2" align="center">
                    <b>HCL Bill Payment Criteria</b>
                    <UserControl:UserMessage runat="server" ID="UserMessage" />
                </td>
            </tr>
            <tr class="nrow">
                <td>
                    <b>Branch Name</b>
                </td>
                <td>
                    <asp:Label ID="lblBranch" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="nrow" id="trDate">
                <td><b>Select Date Range</b></td>
                <td align="left">
                    <UserControl:DateRange ID="dtBookingDate" runat="server" />
                </td>
            </tr>
            <tr class="nrow">
                <td colspan="4" align="center">
                    <b>OR</b>
                </td>
            </tr>
            <tr class="nrow">
                <td>
                    <b>Bill No</b>
                </td>
                <td>
                    <asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="hrow" id="trSubmit">
                <td align="center" colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divDate" style="position: absolute; visibility: hidden; background-color: white; layer-background-color: white; z-index: 99; left: -74px; top: 1px;">
    </div>
</asp:Content>

