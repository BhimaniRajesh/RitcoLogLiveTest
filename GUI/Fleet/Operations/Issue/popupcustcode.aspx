<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupcustcode.aspx.cs" Inherits="GUI_Fleet_Operations_Issue_popupcustcode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

   <title>FLEET - Customer Code List</title>
<head runat="server">
<link id="Link1" href="~/GUI/images/style.css" rel="Stylesheet" type="text/css" runat="server" />
   
    
    <script language="javascript" type="text/javascript">
        
        function SelectLocation(objLocation)
        {
        
        
        
            var mNo = "<%=strQSParam %>"
            
           // alert(mNo");
            
            if(mNo == 2)
            {
                window.opener.document.forms[0].ctl00$MyCPH1$txtCustCode.value = objLocation.value
                window.close()
            }
          
            
        }
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    <table border="0" width="100%" cellspacing="0" class="boxbg"  >
    
    <tr class="bgbluegrey">
    <td>
    
    
       <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AllowSorting="true" 
                            AutoGenerateColumns="false" BorderWidth="1" CellPadding="7" CellSpacing="1" EmptyDataText="No Records Found..." 
                            HeaderStyle-CssClass="dgHeaderStyle" PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="left"
                            Width="350px" class="boxbg">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" ItemStyle-BackColor="white" ItemStyle-Width="40px" >
                                    <ItemTemplate>
                                        <input name="MyRadioButton" type="radio" value='<%# Eval("custcd") %>' onclick="SelectLocation(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="custcd" HeaderText="Customer Code">
                                    <ItemStyle CssClass="blackfnt" HorizontalAlign="Left" BackColor="white" Width="80px" />
                                    <HeaderStyle CssClass="bgpink" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="custnm" HeaderText="Customer Name">
                                    <ItemStyle CssClass="blackfnt" HorizontalAlign="Left" BackColor="white"/>
                                    <HeaderStyle CssClass="bgpink" HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>
                              </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <HeaderStyle CssClass="dgHeaderStyle" />
                        </asp:GridView>
                   
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
