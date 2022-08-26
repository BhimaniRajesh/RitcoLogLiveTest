<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="GRN_DataList.aspx.cs" Inherits="GUI_Finance_GeneralizedPO_GRNGeneration_GRN_DataList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">

<script language="javascript" src="../../../Images/commonJs.js" type="text/javascript"></script>

    <script language="javascript" src="../../Billing_Ver1/Billing.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function SelectAllcheckBox() {
            var dgGeneral = document.getElementById("ctl00_MyCPH1_GRNData");
            var rows, Prefix, chkRights, chkAll;
            chkAll = document.getElementById("ctl00_MyCPH1_GRNData_ctl01_chkSelectAll");
            if (dgGeneral != null) {
                rows = dgGeneral.rows.length;
                for (i = 1; i < rows - 1; i++) {
                    j = i + 1;
                    if (j < 10) {
                        Prefix = "ctl00_MyCPH1_GRNData_ctl0" + j;
                    }
                    else {
                        Prefix = "ctl00_MyCPH1_GRNData_ctl" + j;
                    }
                    chkSelect = document.getElementById(Prefix + "_chkb");
                    if (chkAll.checked)
                        chkSelect.checked = true;
                    else
                        chkSelect.checked = false;
                }
            }
            return true;
        }
    </script>

    <div>
        <table width="800" border="0" cellpadding="0" cellspacing="0">
            <tr bgcolor="white">
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="tblMain" runat="server" HorizontalAlign="center" CellSpacing="1" BorderWidth="0"
                        Width="100%" CssClass="ftbl">
                        <asp:TableRow CssClass="hrow" VerticalAlign="Middle" ID="trNonDeliveryReason" runat="server">
                            <asp:TableCell HorizontalAlign="center" ID="tdDocketHeader" runat="server" ColumnSpan="4"
                                Style="height: 20px;" Font-Bold="true"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite" VerticalAlign="Middle" ID="trDocketNo" runat="server">
                            <asp:TableCell HorizontalAlign="left" Width="25%" CssClass="blackfnt" ID="tdDocketNo"
                                runat="server"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="left" Width="25%" Wrap="true" ID="tdlblRecordID"
                                runat="server">
                                <asp:Label ID="lblDocketNo" runat="server" CssClass="blackfnt"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite" VerticalAlign="Middle" ID="TableRow1" runat="server">
                            <asp:TableCell HorizontalAlign="left" Width="25%" CssClass="blackfnt" ID="tdManualDocketNo"
                                runat="server"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="left" Width="25%" Wrap="true" ID="TableCell2"
                                runat="server">
                                <asp:Label ID="lblManualDocketNo" runat="server" CssClass="blackfnt"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite" VerticalAlign="Middle" ID="trFromDateToDate" runat="server">
                            <asp:TableCell HorizontalAlign="left" Width="25%" CssClass="blackfnt" ID="tdFromDateToDate"
                                runat="server">
                                                              Date Range</asp:TableCell>
                            <asp:TableCell HorizontalAlign="left" Width="25%" Wrap="true" ID="tdlblDateFromTo"
                                runat="server">
                                <asp:Label ID="lblFromDateToDate" runat="server" CssClass="blackfnt"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite" VerticalAlign="Middle" ID="trNoOfDays" runat="server">
                            <asp:TableCell HorizontalAlign="left" Width="25%" CssClass="blackfnt" ID="tdNoOfDays"
                                runat="server">
                                                                    Party Code</asp:TableCell>
                            <asp:TableCell HorizontalAlign="left" Width="25%" Wrap="true" ID="tdlblNoOfDays"
                                runat="server">
                                <asp:Label ID="lblPartyCode" runat="server" CssClass="blackfnt"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr bgcolor="white">
                <td style="height: 15px">
                </td>
            </tr>
            <tr bgcolor="white" id="Tr_Stn" runat="server">
                <td>
                    <asp:GridView align="left" ID="GRNData" runat="server" BorderWidth="0" CellPadding="3"
                        CellSpacing="1" HeaderStyle-CssClass="bgbluegrey" FooterStyle-CssClass="bgbluegrey"
                        AllowSorting="true" AllowPaging="false" CssClass="boxbg" OnRowDataBound="GvDocketDetails_RowDataBound"
                        AutoGenerateColumns="false" ShowFooter="true" EmptyDataText="No Records Found..."
                        Width="800">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" BackColor="white" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkb"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="white">
                                <HeaderTemplate>
                                    PO Code</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="blackfnt" ID="GRNNO" Text='<%# DataBinder.Eval(Container.DataItem,"pocode") %>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" BackColor="White"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Podate" HeaderStyle-Font-Bold="true" HeaderText="PO Date"
                                ItemStyle-BackColor="white" ItemStyle-Wrap="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Vendorname" HeaderStyle-Font-Bold="true" HeaderText="Vendor Name"
                                ItemStyle-Wrap="false" ItemStyle-BackColor="white">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalQty" HeaderStyle-Font-Bold="true" HeaderText="Ordered Qty"
                                ItemStyle-BackColor="white" ItemStyle-Wrap="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RECEIVEDQTY" HeaderStyle-Font-Bold="true" HeaderText="Received Qty"
                                ItemStyle-BackColor="white" ItemStyle-Wrap="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BALANCEQTY" HeaderStyle-Font-Bold="true" HeaderText="Balance Qty"
                                ItemStyle-BackColor="white" ItemStyle-Wrap="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" CssClass="blackfnt" />
                                <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                            </asp:BoundField>                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td height="25px">
                </td>
            </tr>
            <tr>
                <td width="100%" style="border: solid 1px" align="center" class="bgbluegrey">
                    <table width="800" border="0" cellpadding="0" cellspacing="0" class="boxbg">
                        <tr>
                            <td class="bgbluegrey">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="75px" CssClass="fbtn"
                                    OnClick="btnSubmit_Click" OnClientClick="javascript:return CheckForCheckBox();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript" type="text/javascript">
        function CheckForCheckBox() {
            var TargetBaseControl = document.getElementById('<%= this.GRNData.ClientID %>');
            var TargetChildControl = "ChkItem";
            var count = 0;
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].checked == true)
                    count += 1;
            }
            if (count == 0) {
                alert("Select Atleast One Record");
                return false;
            }
            //Reset Counter
            return true;
        }

    </script>
</asp:Content>

