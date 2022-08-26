<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true"
    CodeFile="Query.aspx.cs" Inherits="GUI_UNI_Net_MIS_OUT_AR_Query" %>

<%@ Register TagName="LH" TagPrefix="Location" Src="~/GUI/Common_UserControls/LocationHirarchy.ascx" %>
<%@ Register TagName="DT" TagPrefix="Date" Src="~/GUI/Common_UserControls/RPT_Date.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">

    <script language="JavaScript" type="text/javascript" src="./../../Images/CalendarPopup.js"></script>

    <script language="javascript" type="text/javascript" src="./../../images/commonJs.js"></script>

    <script language="JavaScript" type="text/javascript">
    var cal = new CalendarPopup("testdiv1"); 
	cal.setCssPrefix("TEST");
	cal.showNavigationDropdowns();
	
	 function OnSub_DATACHECK()
     {
        if(!DateCheck())
        return false;
     }
     
   function cust(ID)
        {
         var strWinFeature = "menubar=no,toolbar=no,location=no,resizable=yes,scrollbars=yes,width=600 ,height=400,status=no,left=60,top=50"
            var strPopupURL = "../PopUp_Cust.aspx?ID="+ID
            
            winNew = window.open(strPopupURL, "_blank", strWinFeature)
        }
//	function Chk(obj)
//	{
//        var txtcustcd=document.getElementById("ctl00_MyCPH1_txtcustcd");
//	    if (document.getElementById("ctl00_MyCPH1_rpttyp_2").checked==true )
//	    {
//		    txtcustcd.disabled=false 
//	    }
//	    else 
//	    {
//		    txtcustcd.disabled=true
//	    }
//	}
    </script>

    <div align="left">
        <table style="width: 10.5in" border="0" align="left" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <font class="blklnkund"><strong>Module</strong></font> <strong>&gt; </strong><font
                        class="blklnkund"><strong>Reports </strong></font><font class="bluefnt"><strong>&gt;</strong>
                            <strong>Finance </strong></font><font class="bluefnt"><strong>&gt;</strong> <strong>
                                Outstanding A/R</strong> </font>
                </td>
            </tr>
            <tr>
                <td class="horzblue">
                    <img src="./../../images/clear.gif" width="2" height="1" /></td>
            </tr>
            <tr>
                <td align="right">
                    <a href="javascript:window.history.go(-1)" title="back">
                        <img alt="Back" src="./../../images/back.gif" border="0">
                    </a>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table cellspacing="1" style="width: 8.0in" class="boxbg" align="left">
                        <tr class="bgbluegrey">
                            <td colspan="7" align="center" style="height: 21px">
                                <asp:Label ID="lblQry" CssClass="blackfnt" Font-Bold="true" runat="server">Select Sales Criteria </asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: white">
                            <td align="left" colspan="4">
                                <Location:LH ID="Fromlc" runat="server" />
                            </td>
                        </tr>
                        <tr style="background-color: white">
                            <td align="left">
                                <font class="blackfnt">Enter Customer</font>
                            </td>
                            <td align="left" colspan="3"><%--txtCustomer txtcustcd--%>
                                <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                                 <input type="button" onclick="cust('C');" value="..." style="width: 18px; height: 21px"
                                    size="" />
                            </td>
                        </tr>
                        <tr style="background-color: white">
                            <td align="left">
                                <font class="blackfnt">As On Date</font>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtasondt" Width="60" MaxLength="10" runat="server"></asp:TextBox>
                                <a href="#" onclick="cal.select(ctl00$MyCPH1$txtasondt,'anchor3','dd/MM/yyyy'); return false;"
                                    name="anchor3" id="a3">
                                    <img src="./../../images/calendar.jpg" border="0"></img>
                                </a>
                                <asp:Label ID="Label1" CssClass="blackfnt" runat="server" Text=" To"></asp:Label>
                                <asp:TextBox ID="txtasondt_to" Width="60" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
                                <a href="#" onclick="cal.select(ctl00$MyCPH1$txtasondt_to,'anchor4','dd/MM/yyyy'); return false;"
                                    name="anchor4" id="a4">
                                    <img src="./../../images/calendar.jpg" border="0"></img>
                                </a>
                            </td>
                        </tr>
                        <tr style="background-color: white">
                            <td align="left">
                                <font class="blackfnt">Select Booking Date</font>
                            </td>
                            <td align="left" colspan="3">
                                <Date:DT ID="DT" runat="server" />
                            </td>
                        </tr>
                        <tr align="center" bgcolor="white">
                            <td colspan="7">
                                <asp:RadioButtonList ID="RadioRPT_SUBTYP" RepeatDirection="horizontal" CssClass="blackfnt"
                                    runat="server">
                                    <asp:ListItem Selected="True" Text="Bill " Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MR" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="white">
                            <td colspan="7">
                                <asp:RadioButtonList ID="rpttyp" RepeatDirection="horizontal"
                                    CssClass="blackfnt" runat="server">
                                    <asp:ListItem Selected="True" Text="Location Wise " Value="L"></asp:ListItem>
                                    <asp:ListItem Text="Customer Wise" Value="C1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="white">
                            <td colspan="4">
                                <asp:RadioButtonList ID="rptmd" RepeatDirection="horizontal" CssClass="blackfnt"
                                    runat="server">
                                    <asp:ListItem Text="HTML Format" Selected="true" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="XLS Download" Value="Y"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="white">
                            <td colspan="7">
                                <asp:Button ID="btnShow" OnClick="btnShow_Click" OnClientClick="Javascript:return OnSub_DATACHECK();"
                                    runat="server" Text="Show" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;
        layer-background-color: white; z-index: 99;">
    </div>
    <div id="Div1" style="position: absolute; visibility: hidden; background-color: white;
        layer-background-color: white; z-index: 99;">
    </div>
</asp:Content>
