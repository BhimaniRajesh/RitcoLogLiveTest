<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocationHirarchy_to.ascx.cs" Inherits="GUI_UNI_MIS_LocationHirarchy_to" %>
<link href="../../GUI/images/style.css" rel="stylesheet" type="text/css" />
<div>
    <div id="tabs" style="text-align: left; padding: 0px 0px 0px 0px; color: Black; border-bottom-color: DarkGray">
        <table cellspacing="1" cellpadding ="3" border="0" class="boxbg" style="width: 100%">
            <tr id="trRO" style="background-color: white" runat="server">
                <td class="blackfnt">
                  Select  RO
                </td>
                <td class="blackfnt">
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboRO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboRO_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="trAO" style="background-color: white" runat="server">
                <td class="blackfnt">
                   select AO
                </td>
                <td class="blackfnt">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboAO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboAO_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="trLO" style="background-color: white" runat="server">
                <td class="blackfnt">
                 Select   Location
                </td>
                <td class="blackfnt">
                    <asp:UpdatePanel ID="updatepanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboLO" runat="server" AutoPostBack="true">
                            </asp:DropDownList></ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboRO" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboAO" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</div>
