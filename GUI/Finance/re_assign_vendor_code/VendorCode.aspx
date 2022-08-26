<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorCode.aspx.cs" Inherits="GUI_Finance_re_assign_vendor_code_VendorCode" %>

<link href="../../../GUI/admin/CityMaster/include/style.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BranchList</title>

    <script language="javascript" type="text/javascript">

        function SelectLocation(objLocation) {
            var st = objLocation.value
            st = st.split(",");
            window.opener.document.forms[0].document.getElementById("hdnCD").value = st[0]
            window.close();


        }    
       
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnCD" runat="server" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AllowSorting="true"
        AutoGenerateColumns="false" BorderWidth="1" CellPadding="7" CellSpacing="1" EmptyDataText="No Records Found..."
        HeaderStyle-CssClass="dgHeaderStyle" PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="left"
        Width="350px" class="boxbg">
        <Columns>
            <asp:TemplateField HeaderText="Select" ItemStyle-BackColor="white" ItemStyle-Width="40px">
                <ItemTemplate>
                    <input name="MyRadioButton" type="radio" value='<%# Convert.ToString(Eval("VendorCode"))%>'
                        onclick="SelectLocation(this)" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code">
                <ItemStyle CssClass="blackfnt" HorizontalAlign="Left" BackColor="white" Width="80px" />
                <HeaderStyle CssClass="bgpink" HorizontalAlign="Left" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorName" HeaderText="Vendor Name">
                <ItemStyle CssClass="blackfnt" HorizontalAlign="Left" BackColor="white" />
                <HeaderStyle CssClass="bgpink" HorizontalAlign="Left" Wrap="False" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Left" />
        <HeaderStyle CssClass="dgHeaderStyle" />
    </asp:GridView>
    <div>
    </div>
    </form>
</body>
</html>
