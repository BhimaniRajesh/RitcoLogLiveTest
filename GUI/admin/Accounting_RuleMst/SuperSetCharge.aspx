<%@ Page Language="C#" AutoEventWireup="true" Trace="false" MasterPageFile="~/GUI/MasterPage.master"
    CodeFile="SuperSetCharge.aspx.cs" Inherits="GUI_Octroi_ViewnPrint_DText" %>

<asp:Content ContentPlaceHolderID="MyCPH1" ID="cont1" runat="server">

    <script language="javascript" type="text/javascript">
        function alterDisplay(txtid,lblid)
        {
            var txtchargename=document.getElementById(txtid);
            var lblchargename=document.getElementById(lblid);

            if(txtchargename.style.display=='none')
               {
                    txtchargename.style.display="block";
                    lblchargename.style.display="none";
                    txtchargename.focus();
                    txtchargename.select();
               }
            else
            {
                txtchargename.style.display="none";
                lblchargename.style.display="block";
                lblchargename.innerText=txtchargename.value;
            }
           
        }
    </script>

    <br />
    <br />
    <asp:UpdatePanel ID="updone" runat="server">
        <ContentTemplate>
            <asp:Panel Width="10in" runat="server">
                <asp:Table ID="Table1" runat="server" CellSpacing="1" CssClass="boxbg" Width="80%">
                    <asp:TableRow CssClass="bgbluegrey">
                        <asp:TableCell HorizontalAlign="Center" Font-Bold="true" ColumnSpan="2">Charges Master</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="bgwhite" ID="trlist" runat="server">
                        <asp:TableCell Width="350px">
                            <asp:Label ID="lbllistname" runat="server" CssClass="blackfnt"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="ddllist" runat="server" DataValueField="codeid" DataTextField="codedesc"
                                AutoPostBack="true" CssClass="blackfnt">
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="bgwhite" ID="trgrid" runat="server">
                        <asp:TableCell>
                            <asp:GridView ID="grvcharges" runat="server" BorderWidth="0" CellSpacing="1" CellPadding="2"
                                AllowSorting="true" Width="100%" CssClass="boxbg" BackColor="#8ba0e5" HeaderStyle-CssClass="bgbluegrey"
                                AutoGenerateColumns="false" HeaderStyle-Wrap="false" OnRowDataBound="grvcharges_RowDataBound"
                                EmptyDataText="No Records Found...">
                                <Columns>
                                    <asp:TemplateField HeaderText="Charge Name">
                                        <ItemStyle BackColor="White" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblchargename" Text='<%# DataBinder.Eval(Container.DataItem,"chargename") %>'
                                                runat="server" CssClass="blackfnt"></asp:Label>
                                            <asp:Label ID="lblfixed" runat="server" CssClass="redfnt"></asp:Label>
                                            <asp:HiddenField ID="hdnchargecode" Value='<%# DataBinder.Eval(Container.DataItem,"chargecode") %>'
                                                runat="server" />
                                            <asp:TextBox ID="txtchargename" Style="display: none; background-color: Yellow;"
                                                Text='<%# DataBinder.Eval(Container.DataItem,"chargename") %>' Width="520px"
                                                MaxLength="50" runat="server" BorderStyle="Groove" CssClass="blackfnt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Operator">
                                        <ItemStyle BackColor="White" HorizontalAlign="Center" Width="200px" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddloperator" CssClass="blackfnt" runat="server">
                                                <asp:ListItem Value="+">Add</asp:ListItem>
                                                <asp:ListItem Value="-">Subtract</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active Flag">
                                        <ItemStyle BackColor="White" HorizontalAlign="Center" Width="100px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkactiveflag" runat="server" />
                                            <asp:HiddenField ID="hdnactiveflag" Value='<%# DataBinder.Eval(Container.DataItem,"activeflag") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Code">
                                        <ItemStyle BackColor="White" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <div id="Dic_Acccode" runat="server" class="boxbg" />
                                            <%--   <atlas:AutoCompleteExtender    runat="server"  ID="Ext1"  DropDownPanelID="Dic_Acccode" >
                                              <atlas:AutoCompleteProperties TargetControlID="txtacccode" ServicePath="../AtlasAutoComplete.asmx" ServiceMethod="GetAcccode" MinimumPrefixLength="1" Enabled="true" />
                                              </atlas:AutoCompleteExtender>--%>
                                            <ajaxToolkit:AutoCompleteExtender ID="Ext1" runat="server" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetAcccode" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ServicePath="../../services/WebService.asmx"
                                                TargetControlID="txtacccode">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:TextBox ID="txtacccode" runat="server" Width="300" CssClass="blackfnt" BorderStyle="Groove"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="bgbluegrey" ID="trsubmit" runat="server">
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="blackfnt" OnClick="btnsubmit_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:HiddenField ID="hdnbasedon" runat="server" />
            <asp:HiddenField ID="Hnd_DocType" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppMain" runat="server">
        <ProgressTemplate>
            <table style="border: solid #8ba0e5 2px; z-index: 98; position: fixed; width: 200px;
                -left: -60px; top: 50%; left: 40%;" width="190px" cellpadding="0" bgcolor="white">
                <tr>
                    <td align="right">
                        <img src="../../images/loading.gif" alt="" />
                    </td>
                    <td>
                        <font face="verdana" color="#0000ff" size="2">&nbsp;&nbsp;&nbsp;<b>Please Wait ...</b></font>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" Style="position: fixed; z-index: 97; top: 0px; left: 0px;
                background-color: #FFFFFF; width: 100%; height: 100%; filter: Alpha(Opacity=50);
                opacity: .50; -moz-opacity: .50;" runat="server">
                <asp:Panel Style="z-index: 98; position: fixed; width: 120px; -left: -60px; top: 50%;
                    left: 50%;" ID="Panel2" runat="server">
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
