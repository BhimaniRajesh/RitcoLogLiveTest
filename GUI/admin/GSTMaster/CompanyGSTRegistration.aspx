<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyGSTRegistration.aspx.cs" Inherits="GUI_admin_GSTMaster_CompanyGSTRegistration" %>

<%@ Register Src="~/GUI/Common_UserControls/UserMessage.ascx" TagPrefix="uc1" TagName="UserMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">
    <script type="text/javascript">
        var btnSubmitID = '<%=btnSubmit.ClientID%>';
        var btnNextID = '<%=btnSubmitCriteria.ClientID%>';
        var tdCompanyCriteriaID = '<%=tdCompanyCriteria.ClientID%>';
        var tdCompanyHeaderDetailID = '<%=tdCompanyHeaderDetail.ClientID%>';
        var tdCompanyDetailID = '<%=tdCompanyDetail.ClientID%>';
        var Table1SubmitID = '<%=Table1.ClientID%>';
        var txtCompanyNameID = '<%=txtCompanyName.ClientID%>';
        var hdnCompanyCodeID = '<%=hdnCompanyCode.ClientID%>';
        var hdnValidID = '<%=hdnValid.ClientID%>';

        var txtGSTCityID = '<%=txtGSTCity.ClientID%>';
        var ddlGSTStateID = '<%=ddlGSTState.ClientID%>';
        var hdnGSTCityID = '<%=hdnGSTCity.ClientID%>';
        var hdnGSTStateID = '<%=hdnGSTState.ClientID%>';

        var txtGSTBillingAddresID = '<%=txtGSTBillingAddres.ClientID%>';
        var txtCGSTNumberID = '<%=txtCGSTNumber.ClientID%>';
        var txtIGSTNumberID = '<%=txtIGSTNumber.ClientID%>';

        var tdCompanyDetailExcelID = '<%=tdCompanyDetailExcel.ClientID%>';
        var rbEntrytypeID = '<%=rbEntrytype.ClientID%>';

        var serviceUrl = '<%=ResolveClientUrl("~/services/GSTCommonService.asmx")%>';
    </script>
    <script language="javascript" type="text/javascript" src="JS/GSTCommon.js?v=22"></script>
    <script language="javascript" type="text/javascript" src="JS/CompanyGSTRegistration.js?v=22"></script>
    <style type="text/css">
        .imgBtnEvents {
            float: left;
        }
    </style>
    <br />
    <table style="width: 100%;">
        <tr>
            <td></td>
        </tr>
        <tr runat="server" id="trMain1">
            <td>
                <asp:Panel ID="pnlFilter" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table id="tblMain" runat="server" class="boxbg" cellpadding="3" cellspacing="1" width="1210px">
                                <tr class="hrow">
                                    <td colspan="3" align="center">
                                        <asp:Label runat="server" ID="Label1" Text="Company GST Registration" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tdCompanyCriteria" runat="server" class="bgwhite">
                                    <td width="150px">
                                        <span style="margin-right: 15px;">Company Name</span>
                                    </td>
                                    <td width="430px">
                                        <asp:HiddenField runat="server" ID="hdnCompanyCode"></asp:HiddenField>
                                        <asp:TextBox CssClass="ltxt" runat="server" ID="txtCompanyName"
                                            onblur="return onValidateCompanyExist(this);"
                                            Width="350px"></asp:TextBox>
                                        <%--onblur="javascript : return CheckCompanyId(this)" --%>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnSubmitCriteria" Text="Next" OnClick="btnSubmitCriteria_Click" OnClientClick="return onNextClick(this);" />
                                        <asp:HiddenField runat="server" ID="hdnValid" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                            </table>
                            <table class="boxbg" runat="server" id="tdCompanyHeaderDetail" width="1210px">
                                <tr style="display: none">
                                    <td colspan="4" class="hrow align-center">
                                        <center><b>GST Detail</b></center>
                                    </td>
                                </tr>
                                <tr class="bgwhite" style="display: none">
                                    <td>GST Billing State</td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="hdnGSTState"></asp:HiddenField>
                                        <%--<asp:TextBox runat="server" ID="txtGSTState" CssClass="ltxt" onblur="return onValidateStateExist(this);" Width="200"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlGSTState" runat="server" onchange="onDdlBillingStateSelection_Change(this)" DataTextField="StateName" DataValueField="StateCode" Width="200"></asp:DropDownList>
                                    </td>
                                    <td>GST Billing City</td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="hdnGSTCity"></asp:HiddenField>
                                        <asp:TextBox runat="server" ID="txtGSTCity" CssClass="ltxt" Width="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="bgwhite" style="display: none">
                                    <td colspan="1">GST Billing Address</td>
                                    <td colspan="3">
                                        <asp:TextBox CssClass="ltxt" runat="server" ID="txtGSTBillingAddres" TextMode="MultiLine" Rows="2" Columns="3" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td colspan="4" class="hrow align-center">
                                        <center><b>GSTIN Details </b></center>
                                    </td>
                                </tr>
                                <tr class="bgwhite" style="display: none">
                                    <td>CGST number</td>
                                    <td>
                                        <asp:TextBox CssClass="ltxt" runat="server" ID="txtCGSTNumber" Width="200" onblur="return onValidateCGSTNumberExist(this);" onkeypress="return ValidAlphaNumeric(event,alphanumeric);" MaxLength="16"></asp:TextBox>
                                    </td>
                                    <td>IGST number </td>
                                    <td>
                                        <asp:TextBox CssClass="ltxt" runat="server" ID="txtIGSTNumber" Width="200" onblur="return onValidateIGSTNumberExist(this);" onkeypress="return ValidAlphaNumeric(event,alphanumeric);" MaxLength="16"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="hrow align-center">
                                        <center><b>Company Detail</b></center>
                                    </td>
                                </tr>
                                <tr class="bgwhite">
                                    <td style="width: 150px">Company Name
                                    </td>
                                    <td colspan="3">
                                        <asp:Label runat="server" ID="lblCutomerID" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="bgwhite">
                                    <td>Entry Type 
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList runat="server" ID="rbEntrytype" RepeatDirection="Horizontal" onclick="onBillingTypeChange(this)">
                                            <asp:ListItem Text="Manual" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Excel" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmitCriteria" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:Table ID="tblDetails" runat="server" CssClass="boxbg" CellPadding="3" CellSpacing="1"
                        HorizontalAlign="Left" Width="1210px">
                        <asp:TableRow CssClass="bgwhite" runat="server" ID="tdCompanyDetail">
                            <asp:TableCell ColumnSpan="2">
                                <asp:UpdatePanel runat="server" ID="Updetail">
                                    <ContentTemplate>
                                        <table class="boxbg" width="100%">
                                            <tr>
                                                <td colspan="4" class="hrow align-center">
                                                    <center><b>GSTIN Details </b></center>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="boxbg" id="tblOrderDetail" width="100%">
                                            <asp:Repeater ID="rptOrderEntry" runat="server" OnItemDataBound="rptOrderEntry_ItemDataBound"
                                                OnItemCommand="rptOrderEntry_ItemCommand">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th class="hrow align-center">Sr.No</th>
                                                        <th class="hrow align-center" style="width: 120px">Billing State / UT</th>
                                                        <th class="hrow align-center">State/ UT name</th>
                                                        <th class="hrow align-center">Address</th>
                                                        <th class="hrow align-center">City</th>
                                                        <th class="hrow align-center">Location</th>
                                                        <%--//commented on 04-jul-2017, to provide one column for both Provisional / GSTIN Number.
                                                            <th class="hrow align-center">Provisional ID Number</th>--%>
                                                        <th class="hrow align-center">GST Type</th>
                                                        <th class="hrow align-center">Provisional / GSTIN Number</th>
                                                        <th class="hrow align-center">&nbsp;&nbsp;&nbsp; Option</th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="bgwhite">
                                                        <td class="align-center"><%# Container.ItemIndex + 1%></td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlStatetype" Width="140px" onchange="onChangesStateType(this)">
                                                                <asp:ListItem Text="State" Value="ST"></asp:ListItem>
                                                                <asp:ListItem Text="Union territory" Value="UT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnStateTypeCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"StateTypeCode") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtStateUTName" Text='<%# DataBinder.Eval(Container.DataItem,"StateName") %>' onblur="return onValidateStateExist(this,true);" />
                                                            <asp:HiddenField ID="hdnStateUTCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"StateCode") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtAddress" Width="180px" TextMode="MultiLine" Rows="2" Columns="5" Text='<%# DataBinder.Eval(Container.DataItem,"Address") %>' />
                                                            <asp:HiddenField ID="hdnAddress" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Address") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtCity" Text='<%# DataBinder.Eval(Container.DataItem,"City") %>' />
                                                            <asp:HiddenField ID="hdnCityCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"CityCode") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtLocation" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>' onblur="return onValidateLocationExist(this);" />
                                                            <asp:HiddenField ID="hdnLocationCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LocationCode") %>' />
                                                        </td>
                                                        <%--//commented on 04-jul-2017, to provide one column for both Provisional / GSTIN Number.
                                                            <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtProvisionalNumber" Text='<%# DataBinder.Eval(Container.DataItem,"ProvisionalNumber") %>'  onkeypress="return ValidAlphaNumeric(event,alphanumeric);" MaxLength="16" />
                                                            <asp:HiddenField ID="hdnProvisionalNumber" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ProvisionalNumber") %>' />
                                                        </td>--%>
                                                        <td>
                                                            <asp:HiddenField ID="hdnGSTTypeCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"GSTTypeCode") %>' />
                                                            <asp:Label runat="server" ID="lblGSTType" Text='<%# DataBinder.Eval(Container.DataItem,"GSTType") %>' Width="140px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="ltxt" runat="server" ID="txtGSTTiNumber" Text='<%# DataBinder.Eval(Container.DataItem,"GSTINumber") %>' onkeypress="return ValidAlphaNumeric(event,alphanumeric);" MaxLength="16" />
                                                            <asp:HiddenField ID="hdnGSTTiNumber" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"GSTINumber") %>' />
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:ImageButton ID="CmdAddRows" CausesValidation="false" CssClass="DDFloatLeft imgBtnEvents" OnClientClick="javascript:return  OnAddRow(this.getAttribute('id'));"
                                                                runat="server" ImageUrl="~/images/png/24/button-add.png" CommandName="Add" CommandArgument='<%# Container.ItemIndex + 1%>' AlternateText="Add Rows" OnClick="CmdAddRows_Click" />
                                                            <asp:ImageButton ID="CmdDeleteRows" CausesValidation="false" CssClass="dis-none imgBtnEvents"
                                                                runat="server" ImageUrl="~/images/png/24/button-cross.png" CommandName="Delete" CommandArgument='<%# Container.ItemIndex + 1%>' AlternateText="Delete Rows" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite" runat="server" ID="tdCompanyDetailExcel">
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <table class="boxbg" width="500px">
                                    <tr>
                                        <td colspan="4" class="hrow align-center">
                                            <center><b>GSTIN Details </b></center>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblFileUpload" runat="server" class="boxbg" width="500px">
                                    <tr class="bgwhite">
                                        <td>
                                            <b>Select File </b>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fUploader" runat="server" CssClass="btn btn-primary" />
                                        </td>
                                    </tr>
                                    <tr class="bgwhite">
                                        <td colspan="2">&nbsp;<asp:LinkButton ID="lnkFormat" runat="server" Text="Click here to download XLS Format"
                                            ToolTip="Click here to download XLS Format" OnClick="lnkFormat_Click" />

                                            <%--  <a runat="server"  id="hrfFormat" >Click here to download XLS Format</a>--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </td>
        </tr>
        <tr runat="server" id="trMain2">
            <td>
                <asp:Table ID="Table1" runat="server" CssClass="boxbg" CellPadding="3" CellSpacing="1"
                    HorizontalAlign="Left" Width="1210px">
                    <asp:TableHeaderRow CssClass="bgbluegrey">
                        <asp:TableHeaderCell ColumnSpan="2">
                            <uc1:UserMessage runat="server" ID="msgBox" />
                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit" OnClientClick="javascript : return onSubmitClick();" />
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </td>
        </tr>
        <tr runat="server" id="trMain3" visible="false">
            <td>
                <asp:Table ID="tblDetailSaved" runat="server" CssClass="boxbg" CellPadding="3" CellSpacing="1"
                    HorizontalAlign="Left" Width="1210px">
                    <asp:TableHeaderRow CssClass="bgbluegrey">
                        <asp:TableHeaderCell ColumnSpan="2">
                            Company GST Details are successfully stored.
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow CssClass="bgwhite">
                        <asp:TableCell ColumnSpan="2">
                            <a href="CompanyGSTRegistration.aspx">Click Here to Add Other Company GST Detail.</a>
                            <asp:Label runat="server" ID="lblDone"></asp:Label><br />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr runat="server" id="trXLSResult" visible="false">
            <td>
                <table class="boxbg">
                    <tr class="bgwhite">
                        <td rowspan="4">
                            <asp:Image ImageUrl="~/GUI/images/excel02.gif" ID="imgXLS" runat="server" Height="40px" Width="40px" />
                        </td>
                    </tr>
                    <tr class="bgwhite">
                        <td>
                            <b>&nbsp;GST Upload Result :</b>

                            &nbsp;<asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr id="tr_Success" runat="server" class="bgwhite">
                        <td>&nbsp;<asp:LinkButton ID="lnkResultSuccess" runat="server" Text="Click here to download result (Succeeded)"
                            ToolTip="Click to Download result XLS" OnClick="lnkResultSuccess_Click" />
                        </td>
                    </tr>
                    <tr id="tr_Fail" runat="server" class="bgwhite">
                        <td>&nbsp;<asp:LinkButton ID="lnkResultFail" runat="server" Text="Click here to download result (Failed)"
                            ToolTip="Click to Download result XLS" OnClick="lnkResultFail_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
