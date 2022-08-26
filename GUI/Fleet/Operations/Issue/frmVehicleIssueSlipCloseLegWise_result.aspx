<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="frmVehicleIssueSlipCloseLegWise_result.aspx.cs" Inherits="GUI_Fleet_Operations_Issue_frmVehicleIssueSlipCloseLegWise_result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ViewPrint(tVSlipId, tPrintMode) {
            var strurl = "frmViewPrintVehicleIssue_New.aspx?VSlipNo=" + tVSlipId + "&PrintMode=" + tPrintMode
            popup = window.open(strurl, "", "height=475,width=850,scrollbars=yes,left=100,top=125");
        }
        function ViewPrintPay(tVSlipId, tPrintMode) {
            //alert("advance");
            var strurl = "DebitVoucherView.aspx?voucherno=" + tVSlipId + "&PrintMode=" + tPrintMode
            popup = window.open(strurl, "", "height=475,width=850,scrollbars=yes,left=100,top=125");
        }
        function ViewDocument(url) {
            window.open("<%=ResolveClientUrl("~/DocumentViewer.aspx") %>?DocumentURL=" + url, "es", "width=600,height=400,fullscreen=yes,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
        }
        function DownloadDocument(url) {
            window.open(blobBase + '/' + url);
        }
    </script>
    <%
        string VSlipId = "", Flag = "";
        VSlipId = Request.QueryString["VSlipId"];
        Flag = Request.QueryString["mFlag"];
        lblDocNoVal.Text = VSlipId;
    %>
    <table cellspacing="1" style="width: 100%">
        <tr style="background-color: white">
            <td align="left">
                <asp:Label ID="lblPageHead" CssClass="blackfnt" Font-Bold="true" runat="server">Vehicle Issue Slip</asp:Label>
                <hr align="center" size="1" color="#8ba0e5">
            </td>
        </tr>
    </table>
    <br>
    <table border="0" align="left" class="boxbg" cellpadding="2" cellspacing="1" width="60%">
        <tr class="bgbluegrey">
            <td align="center" colspan="3">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="true" Text="Following document is Updated" CssClass="blackfnt"></asp:Label>
            </td>
        </tr>
        <tr style="background-color: White;display:none">
            <td align="center">
                <asp:Label ID="lblDocType" runat="server" Text="Document Name" Font-Bold="true" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblDocNo" runat="server" Text="Document No." Font-Bold="true" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">&nbsp;</td>
        </tr>
        <tr style="background-color: White;display:none">
            <td align="center">
                <asp:Label ID="lblDocNameVal" runat="server" Text="Vehicle Issue" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblDocNoVal" runat="server" Text="" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">
                <%
                    if (Flag != "U")
                    {
                %>
                <a href="javascript:ViewPrint('<%=VSlipId%>','0')"><font class="blackfnt">View</font></a>
                <asp:Label ID="lblSep1" runat="server" Text="|" CssClass="blackfnt"></asp:Label>
                <a href="javascript:ViewPrint('<%=VSlipId%>','1')"><font class="blackfnt">Print</font></a>
                <asp:Label ID="lblSep2" runat="server" Text="|" CssClass="blackfnt"></asp:Label>
                <asp:LinkButton ID="LinkXLS" runat="server" Text="XLS" CausesValidation="true" CssClass="blackfnt"></asp:LinkButton>
                <% }%>
            </td>
        </tr>
        <%if (mvoucherno != "" && modeval == "Advance")
          {%>
        <tr style="background-color: White;display:none">
            <td align="center">
                <asp:Label ID="Label4" runat="server" Text="Advance Payment Voucher" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="LblVoucherNo" runat="server" Text="" CssClass="blackfnt"></asp:Label>
            </td>
            <td align="center">
                <a href="javascript:ViewPrintPay('<%=mvoucherno%>','0')"><font class="blackfnt">View</font></a>
                <asp:Label ID="Label6" runat="server" Text="|" CssClass="blackfnt"></asp:Label>
                <a href="javascript:ViewPrintPay('<%=mvoucherno%>','1')"><font class="blackfnt">Print</font></a>
            </td>
        </tr>
        <%} %>
        <tr class="bgbluegrey" style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:Label ID="lblNextStep" runat="server" Text="Your Next Step" Font-Bold="true" CssClass="blackfnt"></asp:Label>
            </td>
        </tr>
        <%if (modeval == "Advance")
          {%>
        <tr style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:LinkButton ID="lnkStep1" runat="server" Text="Enter More Trip Advance" CssClass="blackfnt" PostBackUrl="frmCloseVehicleIssueSlipQueryV2.aspx?mode=Advance" />
            </td>
        </tr>
        <%} %>
        <%if (modeval == "Close")
          {%>
        <tr style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Close More Vehicle Issue Slip" CssClass="blackfnt" PostBackUrl="frmCloseVehicleIssueSlipQueryV2.aspx?mode=Close" />
            </td>
        </tr>
        <%} %>
        <%if (modeval == "FE")
          {%>
        <tr style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:LinkButton ID="LinkButton2" runat="server" Text="Financial Edit for More Vehicle Issue Slip" CssClass="blackfnt" PostBackUrl="frmCloseVehicleIssueSlipQueryV2.aspx?mode=FE" />
            </td>
        </tr>
        <%} %>
        <%if (modeval == "TD")
          {%>
        <tr style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:LinkButton ID="LinkButton3" runat="server" Text="Close More Vehicle Issue Slip" CssClass="blackfnt" PostBackUrl="frmCloseVehicleIssueSlipQueryV2.aspx?mode=TripDetails" />
            </td>
        </tr>
        <%} %>
        <%if (modeval == "TLW")
          {%>
        <tr style="background-color: White;display:none">
            <td align="left" colspan="3">
                <asp:LinkButton ID="LinkButton4" runat="server" Text="Close More Vehicle Issue Slip" CssClass="blackfnt" PostBackUrl="frmCloseVehicleIssueSlipQueryV2.aspx?mode=TripCloseLegWise" />
            </td>
        </tr>
        <%} %>
        <tr runat="server" id="trEtNo" visible="false" style="background-color: White">
            <td colspan="3">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gvEtNo" runat="server" AutoGenerateColumns="False"
                                HeaderStyle-CssClass="dgHeaderStyle" CssClass="dgRowStyle"
                                BorderColor="#8BA0E5" Width="100%">
                                <%--<HeaderStyle CssClass="dgHeaderStyle" />--%>
                                <Columns>
                                    <asp:BoundField HeaderText="Trip Sheet No."  DataField="Tripsheetno">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ETHC No." DataField="ExternalThcNo">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ETHC Date" DataField="ThcDate">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="From" DataField="FromCity">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="To" DataField="ToCity">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Freight" DataField="ContractAmount">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Advance In Total" DataField="AdvanceAmount">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Balance" DataField="BalanceAmount">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Download Document" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblUloadLink" runat="server" Font-Underline="true" Text="View"
                                                Target="_blank" CssClass="bluefnt" NavigateUrl='<%# string.Format("ViewETScammedDocument.aspx?DocName={0}", Server.UrlEncode(DataBinder.Eval(Container.DataItem,"lblUloadLink").ToString()))  %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="bgwhite" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Document" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible="false">
                                        <HeaderStyle ForeColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <a href="#" onclick='javascript:ViewDocument("<%# DataBinder.Eval(Container.DataItem,"lblUloadLink") %>"); return false;'
                                                display='<%# DataBinder.Eval(Container.DataItem, "lblUloadLink").ToString() == "" ? "none":"block" %>'>
                                                <u><%# DataBinder.Eval(Container.DataItem, "lblUloadLink").ToString() == "" ? "" : "View Document" %></u>
                                            </a>
                                            <br />
                                            <%-- <a href="#" onclick='javascript:DownloadDocument("<%# DataBinder.Eval(Container.DataItem,"lblUloadLink") %>"); return false;'
                                                display='<%# DataBinder.Eval(Container.DataItem, "lblUloadLink").ToString() == "" ? "none":"block" %>'>
                                                <u><%# DataBinder.Eval(Container.DataItem, "lblUloadLink").ToString() == "" ? "" : "Download Document" %></u>
                                            </a>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


