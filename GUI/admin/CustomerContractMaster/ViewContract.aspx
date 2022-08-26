<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewContract.aspx.cs" Inherits="ViewContract"
    EnableEventValidation="false" %>

<html>
<head runat="server">
    <title>View Customer Contract </title>

    <script type="text/javascript" language="javascript">
        function printContract() {
            var hdncontractid = document.getElementById("hdncontractid");
            window.open("PrintCustomerContract.aspx?contractid=" + hdncontractid.value, null, "");
            return false;
        }
    </script>

    <link href="../../../GUI/images/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" id="frmmain">
    <asp:Panel ID="pnlMain" runat="server">
        <asp:Table ID="tblMain" runat="server" Width="10in" HorizontalAlign="left">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Table ID="tblHeadXLS" runat="server" Width="10in" HorizontalAlign="Right">
                        <asp:TableRow HorizontalAlign="Right">
                            <asp:TableCell Width="50%">
                                <asp:LinkButton ID="lnkprint1" Font-Bold="true" OnClientClick="javascript:return printContract()"
                                    runat="server">Print Contract</asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:LinkButton ID="lnkBtnXLS" Font-Bold="true" OnClick="lnkBtnXLS_Click" runat="server">Download XLS</asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="tblMain2" runat="server" Width="10in" HorizontalAlign="left">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Panel ID="Panel1" runat="server" Style="width: 10in;" HorizontalAlign="left">
                                    <asp:Table runat="server" CellSpacing="1" ID="tblheader" CssClass="boxbg" Style="width: 100%;">
                                        <asp:TableRow ID="step1" CssClass="bgbluegrey" BorderWidth="1px">
                                            <asp:TableCell HorizontalAlign="left" ColumnSpan="4">
                                <b>Step 1 : Contract Details</b>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="trrcplcustname" runat="server" BackColor="#ffffff" CssClass="blackfnt"
                                            BorderWidth="1px">
                                            <asp:TableCell>
                                Customer Code
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustcode" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Customer Name
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustname" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="trrcplcontractid" BorderWidth="1px" runat="server" BackColor="#ffffff"
                                            CssClass="blackfnt">
                                            <asp:TableCell>
                                Contract ID
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblrcplcontractid" runat="server" CssClass="redfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="trcustcodename" BorderWidth="1px" runat="server" BackColor="#ffffff"
                                            CssClass="blackfnt">
                                            <asp:TableCell>
                                Customer Code & Name
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustcodename" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Contract ID
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcontractid" runat="server" CssClass="redfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="trDate" BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Contract Date
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblcontractdate" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Valid Until
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lblvaliduntil" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Effective From Date</asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left">
                                                <asp:Label ID="lbleffectivedate" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Contract Signing Location
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcontractsignloc" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Customer Representative
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustrepresent" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Company Employee Name
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcompempname" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Designation
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustempdesig" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Designation
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcompempdesig" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Customer Witness</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcustwitness" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Company Witness
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcompwitness" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Billing Address</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblbilladdress" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                City-Pincode
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcitypincode" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Party Category</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblpartycat" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Contract Category
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcontractcat" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                            <asp:TableCell>
                                Service Tax Applicable</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblstaxapplicable" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                Accounts Representative
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblaccrepresent" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" ID="trcommitedbusiness" runat="server">
                                            <asp:TableCell>
                                Commited Business
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcommitedbusiness" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="2">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" ID="treditinfo" runat="server">
                                            <asp:TableCell CssClass="blackfnt">
                                Last Edit By
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbllasteditby" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>Last Edit Date
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbllasteditdate" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow BorderWidth="1px" BackColor="#ffffff" ID="trcontractmode" runat="server">
                                            <asp:TableCell>
                                        Contract Mode
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcontractmode" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            Remarks
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblremarks" runat="server" CssClass="blackfnt"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                    &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <br />
                                <asp:Table ID="tbldetail" runat="server" CellSpacing="1" CssClass="boxbg" Width="10in">
                                    <asp:TableRow ID="step2" BorderWidth="1px" CssClass="bgbluegrey">
                                        <asp:TableCell HorizontalAlign="left" ColumnSpan="5">
                            <b>Step 2 : Contract Details</b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell Style="width: 20%;">
                            Mode of Transport
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbltransair" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbltransroad" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbltransexpress" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbltranstrain" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Type of Load
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblloadsundry" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblloadftl" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="4">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Pick-Up Delivery
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblgd" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblgg" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbldd" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbldg" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            FOV Type
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblfovtype" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblfovflatrate" runat="server" CssClass="blackfnt"></asp:Label>
                                            <asp:Label ID="lblfovcarrierrate" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:Label ID="lblfovownerrate" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                        </asp:TableCell>
                                        <asp:TableCell>
                            Insurance Percentage
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblinsuranceper" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Type of Service
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblcoddod" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbloda" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblvolumetric" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbldacc" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Demurrage After Days
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbldemafterdays" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="3">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Demurrage Charges
                                        </asp:TableCell>
                                        <asp:TableCell>
                            Minimum Charges</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblmindemchrg" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                            Per Kg.</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbldemperkg" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Bill Generation Location
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblbillgenloc" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                            Billing Instance</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblbillinginstance" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Bill Submission at Location
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblbillsubloc" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                            Total Credit Limit(in Rs)</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lbltotlimitrs" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            Bill Collection at Location</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblbillcolloc" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                            Credit days from Generation
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblcreditdaysfromgen" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell>
                            SKU Y/N
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblsku" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                            Service Tax Paid By</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblstaxpaidby" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                    &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell BorderWidth="1px" HorizontalAlign="Left">
                                <br />
                                <asp:Table runat="server" ID="ChargesMst" CellPadding="1" CellSpacing="1" Style="width: 7in;"
                                    CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                    &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <br />
                                <asp:Table ID="tblminfreight" runat="server" CellSpacing="1" CssClass="boxbg" Width="5in">
                                    <asp:TableRow BorderWidth="1px" CssClass="bgbluegrey">
                                        <asp:TableCell ColumnSpan="3" Font-Bold="true">
                            Minimum Freight Details
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt" HorizontalAlign="center">
                                        <asp:TableCell HorizontalAlign="center">
                            Mode of Transport
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                            Rate
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                            Basis
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell HorizontalAlign="center">
                            Air
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="rate_air" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="basis_air" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell HorizontalAlign="center">
                            Road
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="rate_road" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="basis_road" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell HorizontalAlign="center">
                            Train
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="rate_train" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="basis_train" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell HorizontalAlign="center">
                            Express
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="rate_express" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="basis_express" runat="server" CssClass="blackfnt"></asp:Label></b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                    &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <br />
                                <asp:Table ID="Table1" runat="server" CellSpacing="1" CssClass="boxbg" Width="6in">
                                    <asp:TableRow BorderWidth="1px" CssClass="bgbluegrey">
                                        <asp:TableCell HorizontalAlign="left" ColumnSpan="5">
                            <b>Step 3 : Contract Details</b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt" HorizontalAlign="center">
                                        <asp:TableCell HorizontalAlign="center" ColumnSpan="5">
                            <b>Freight Matrix Sundry</b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" BackColor="#ffffff" CssClass="blackfnt">
                                        <asp:TableCell Style="width: 20%;" ColumnSpan="3">
                            Type of contract
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:Label ID="typeofContrct" runat="server" CssClass="blackfnt"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <br />
                                <asp:Table runat="server" ID="Slabhdr" HorizontalAlign="left" Visible="false" CellPadding="3"
                                    CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                    <asp:TableRow BorderWidth="1px" CssClass="bgbluegrey">
                                        <asp:TableCell ColumnSpan="12" HorizontalAlign="center"> 
          <b>Slab Range </b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow BorderWidth="1px" CssClass="bgbluegrey">
                                        <asp:TableCell BorderWidth="1px" ColumnSpan="2" HorizontalAlign="center"> 
          <b>Slab 1</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="center"> <b>Slab 2</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="center"> <b>Slab 3</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="center"> <b>Slab 4</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="center"> <b>Slab 5</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="center"> <b>Slab 6</b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                    &nbsp;
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Table runat="server" ID="Branc_road" HorizontalAlign="left" CellPadding="3"
                                    CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Table runat="server" ID="Table2" HorizontalAlign="left" CellPadding="3" CellSpacing="1"
                                    Style="width: 8.5in;" CssClass="boxbg">
                                    <asp:TableRow BorderWidth="1px" BackColor="white" ID="ratetype_id" Visible="false">
                                        <asp:TableCell ColumnSpan="5" HorizontalAlign="center"> 
                        <b>Rate Type</b>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="5" HorizontalAlign="center">
                                            <b>
                                                <asp:Label ID="ratetyp" runat="server" CssClass="blackfnt"></asp:Label>
                                            </b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Table runat="server" ID="Single_slab" Visible="false" HorizontalAlign="left"
                                    CellPadding="3" CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="bgwhite">
                            <asp:TableCell>
                                <asp:Repeater ID="rptftl" runat="server" OnItemDataBound="rptftl_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Table runat="server" Width="100%" CellSpacing="1" CssClass="boxbg">
                                            <asp:TableRow CssClass="bgbluegrey">
                                                <asp:TableCell Font-Bold="true">
                                                        FTL Freight Matrix
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow CssClass="bgwhite">
                                                <asp:TableCell>
                                                    <asp:Label ID="lblmodename" CssClass="blackfnt" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow CssClass="bgwhite">
                                                <asp:TableCell>
                                                    <asp:GridView HorizontalAlign="left" ID="grvftl" runat="server" BorderWidth="0" CellSpacing="1"
                                                        AllowSorting="true" PagerStyle-HorizontalAlign="left" CssClass="boxbg" HeaderStyle-CssClass="bgbluegrey"
                                                        FooterStyle-CssClass="<%=boxbg%>" AutoGenerateColumns="true" HeaderStyle-Font-Bold="true"
                                                        Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="bgwhite" EmptyDataText="No Records Found..."
                                                        OnRowDataBound="grvftl_RowDataBound">
                                                        <Columns>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table runat="server" ID="OdaHDr" Visible="false" HorizontalAlign="left" CellPadding="3"
                                    CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table runat="server" ID="ODA_Km" Visible="false" HorizontalAlign="left" CellPadding="3"
                                    CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table runat="server" ID="Oda_charges" Visible="false" HorizontalAlign="left"
                                    CellPadding="3" CellSpacing="1" Style="width: 8.5in;" CssClass="boxbg">
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <br />
                    <asp:Table ID="tblBtnXls" runat="server" Width="10in" HorizontalAlign="Right">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:LinkButton ID="btnXLS" Font-Bold="true" OnClick="btnXLS_Click" runat="server">Download XLS</asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:HiddenField ID="hdncustcode" runat="server" />
        <asp:HiddenField ID="hdncontractid" runat="server" />
        <asp:HiddenField ID="hdnftls" runat="server" />
    </asp:Panel>
    </form>
</body>
</html>
<%--<asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <HeaderTemplate>
                                                                        From
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfrom" Text='<%# DataBinder.Eval( Container.DataItem,"fromloc") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <HeaderTemplate>
                                                                        To
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblto" Text='<%# DataBinder.Eval( Container.DataItem,"toloc") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype1" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate1" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate1") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays1" Text='<%# DataBinder.Eval( Container.DataItem,"trdays1") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype2" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate2" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate2") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays2" Text='<%# DataBinder.Eval( Container.DataItem,"trdays2") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype3" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate3" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate3") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays3" Text='<%# DataBinder.Eval( Container.DataItem,"trdays3") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype4" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate4" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate4") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays4" Text='<%# DataBinder.Eval( Container.DataItem,"trdays4") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype5" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate5" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate5") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays5" Text='<%# DataBinder.Eval( Container.DataItem,"trdays5") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype6" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate6" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate6") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays6" Text='<%# DataBinder.Eval( Container.DataItem,"trdays6") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype7" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate7" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate7") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays7" Text='<%# DataBinder.Eval( Container.DataItem,"trdays7") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype8" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate8" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate8") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays8" Text='<%# DataBinder.Eval( Container.DataItem,"trdays8") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype9" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate9" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate9") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays9" Text='<%# DataBinder.Eval( Container.DataItem,"trdays9") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required" HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblftltype10" CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle BackColor="white" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrate10" Text='<%# DataBinder.Eval( Container.DataItem,"ftlrate10") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="blackfnt" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemStyle BackColor="white" CssClass="blackfnt" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltrdays10" Text='<%# DataBinder.Eval( Container.DataItem,"trdays10") %>'
                                                                            CssClass="blackfnt" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
