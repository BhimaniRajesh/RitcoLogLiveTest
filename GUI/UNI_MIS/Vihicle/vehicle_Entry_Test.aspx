<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true" CodeFile="vehicle_Entry_Test.aspx.cs" Inherits="vehicle_Entry" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">

<script language="javascript" type="text/javascript">
            
            
            
            var mMonths = new Array("", "January", "February", "March", 
                                  "April", "May", "June", "July", 
                                  "August", "September", "October", 
                                  "November", "December");
            
            function BranchPopup(strPopupURL)
            {
                var strWinFeature = "height=280,width=350,resizable=yes,scrollbars=yes,left=420,top=5"
                
                winNew = window.open(strPopupURL, "_blank", strWinFeature)
            }
            
            function CheckDecimalValue(obj)
            {
	            var temp = obj.value
	            var msg = ""
            						
	            if (temp=="")
	            {
		            msg = "Value can not be blank"
	            }
	            else if(isNaN(temp))
	            {
		            msg = "Value should be Numeric"
	            } 					  
	            else if(parseFloat(temp)<0)
	            {
		            msg = "Value should be greater than zero"
	            }
	            else if(temp.indexOf(".")>0 )
	            {
		            msg = "Value should not contain decimal point"
	            }
	            
	            if(msg != "")
	            {
	                alert(msg);
	                obj.focus();
		            return false;
		        }
		        else
		        {
	                obj.value=Math.round(temp)
	                return true;					 				   
	            }
            }
            
            function MyDateDiff(start, end, interval, rounding)
            {
                var iOut = 0;
                
                /*
                // Create 2 error messages, 1 for each argument. 
                var startMsg = "Check the Start Date and End Date\n"
                    startMsg += "must be a valid date format.\n\n"
                    startMsg += "Please try again." ;
            		
                var intervalMsg = "Sorry the dateAdd function only accepts\n"
                    intervalMsg += "d, h, m OR s intervals.\n\n"
                    intervalMsg += "Please try again." ;
                */
                
                var bufferA = Date.parse(start);
                var bufferB = Date.parse(end);
    	
                // check that the start parameter is a valid Date. 
                if (isNaN(bufferA) || isNaN(bufferB))
                {
                    alert( startMsg ) ;
                    return null ;
                }
	
                // check that an interval parameter was not numeric. 
                if ( interval.charAt == 'undefined' ) {
                    // the user specified an incorrect interval, handle the error. 
                    alert( intervalMsg ) ;
                    return null ;
                }
    
                var number = bufferB-bufferA ;
                
                // what kind of add to do? 
                switch (interval.charAt(0))
                {
                    case 'd': case 'D': 
                        iOut = parseInt(number / 86400000) ;
                        if(rounding) iOut += parseInt((number % 86400000)/43200001) ;
                        break ;
                    case 'h': case 'H':
                        iOut = parseInt(number / 3600000 ) ;
                        if(rounding) iOut += parseInt((number % 3600000)/1800001) ;
                        break ;
                    case 'm': case 'M':
                        iOut = parseInt(number / 60000 ) ;
                        if(rounding) iOut += parseInt((number % 60000)/30001) ;
                        break ;
                    case 's': case 'S':
                        iOut = parseInt(number / 1000 ) ;
                        if(rounding) iOut += parseInt((number % 1000)/501) ;
                        break ;
                    default:
                    // If we get to here then the interval parameter
                    // didn't meet the d,h,m,s criteria.  Handle
                    // the error. 		
                    alert(intervalMsg) ;
                    return null ;
                }
                return iOut ;
            }
            
            
            function isDate(dateStr)
            {
                var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                var matchArray = dateStr.match(datePat); // is the format ok?

                if (matchArray == null)
                {
                    alert("Please enter date as either dd/mm/yyyy or mm-dd-yyyy.");
                    return false;
                }

                day = matchArray[1];
                month = matchArray[3]; // parse date into variables
                year = matchArray[5];

                if (month < 1 || month > 12)
                { // check month range
                    alert("Month must be between 1 and 12.");
                    return false;
                }

                if (day < 1 || day > 31)
                {
                    alert("Day must be between 1 and 31.");
                    return false;
                }

                if ((month==4 || month==6 || month==9 || month==11) && day==31)
                {
                    alert("Month " + month + " doesn`t have 31 days!")
                    return false;
                }

                if (month == 2)
                { // check for february 29th
                    var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                    if (day > 29 || (day==29 && !isleap))
                    {
                        alert("February " + year + " doesn`t have " + day + " days!");
                        return false;
                    }
                }
                return true; // date is valid
            }
            
           
            
        </script>


<table cellspacing="1" style="width: 100%; height: 263px;" border="0">
            
            
                <tr align="center">
                    <td style="height: 324px" align="center" colspan="3">
                        <table cellspacing="1" style="width: 90%" class="boxbg" border="0"> 
                            <tr class="bgbluegrey">
                                <td colspan="3" align="center" style="height: 21px">
                                    <asp:Label ID="lblQry" CssClass="blackfnt" 
                                        Font-Bold="true" runat="server">
                                        Select Criteria
                                    </asp:Label>
                                </td>
                            </tr>
                            
                            
                            <tr style="background-color: white">
                         <td align="left">
                            
                                    <asp:Label ID="lbl"  CssClass="blackfnt" Text="Select RO" runat="server" ></asp:Label>    
                                </td>
                                
                                <td align="left" colspan="2" >
                                    &nbsp;
                                    <asp:UpdatePanel ID="up3" runat="server">
                                                            <ContentTemplate>
                             
                                     <%--<asp:Button ID="btnBranchPopup" runat="server" Text="..." OnClick="BranchPopup(this)" PopupURL="../../mis/popup-cust.asp?13" />--%>
                                                                &nbsp;<asp:DropDownList ID="ddlro" runat="server" OnSelectedIndexChanged="ddlro_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                      </td>
                                     
                            </tr>
                    
                            <tr style="background-color: white">
                                <td align="left" style="height: 24px; width: 100px;">
                                    <asp:Label ID="Label1"  CssClass="blackfnt" Text="Select Location" runat="server" Width="113px"></asp:Label>
                                </td>
                                
                                <td align="left" colspan="2" valign="top" style="height: 24px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                &nbsp;<asp:DropDownList ID="ddlloc" runat="server" AutoPostBack="True">
                                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                                </asp:DropDownList>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                </td>
                                
                                
                            </tr>
                            
                                  <tr bgcolor="#ffffff" style="font-size: 12pt; font-family: Times New Roman">
                                                        <td valign="top" align ="left">
                                                       <asp:Label ID="Label3"  CssClass="blackfnt" Text="Select" runat="server" Width="102px"></asp:Label>    
                                                            </td>
                                                        <td  align="left" style="font-size: 12pt; font-family: Times New Roman" valign="top">
                                                            <asp:UpdatePanel ID="up1" runat="server">
                                                            <ContentTemplate>
                                    <asp:RadioButtonList ID="radDate" runat="server" ValidationGroup="VGDtFromTo" CssClass="blackfnt"
                                        AutoPostBack="true" OnSelectedIndexChanged="radDate_SelectedIndexChanged" RepeatColumns="1" Width="244px">
                                        <asp:ListItem Selected="True" Value="0" Text=" Date Range [dd/mm/yyyy]"></asp:ListItem>
                                        <asp:ListItem Value="1" Text=" Last Week (Including Today)"></asp:ListItem>
                                        <asp:ListItem Value="2" Text=" Today:"></asp:ListItem>
                                        <asp:ListItem Value="3" Text=" Till Date:"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    </ContentTemplate>
                               </asp:UpdatePanel>
                                                        </td>
                                                        
                                                        <td valign="top" align="left" style="width: 359px">
                             <asp:UpdatePanel ID="up2" runat="server">
                                                            <ContentTemplate>
                                    <asp:Label ID="lblFrom" CssClass="blackfnt" runat="server" Text=" From: "></asp:Label>
                                    <asp:TextBox ID="txtDateFrom" runat="Server" ValidationGroup="VGDtFromTo" Width="80"></asp:TextBox>
                                    <asp:Label ID="lblTo" CssClass="blackfnt" runat="server" Text=" To: "></asp:Label>
                                    <asp:TextBox ID="txtDateTo" runat="Server" ValidationGroup="VGDtFromTo" Width="80"></asp:TextBox>&nbsp;<br />
                                    <br />
                                    <br />
                                    <br />
                                </ContentTemplate>
                                <Triggers>
                                  
                                </Triggers>
                           </asp:UpdatePanel>
                        </td>
                        </tr>
                        
                      <%--   <tr bgcolor="#ffffff" style="font-size: 12pt; font-family: Times New Roman">
                                <td align="center" colspan="3" valign="top">
                                    </td>
                            </tr>--%>
                            
                            <tr style="background-color: white">
                                <td align="left" style="width: 100px; height: 22px">
                                    <asp:Label ID="Label2" runat="server" CssClass="blackfnt" Text="Document Type" Width="113px"></asp:Label></td>
                                   
                                 
                                <td align="left" colspan="2" style="height: 22px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                    <asp:DropDownList ID="ddlDoctype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDoctype_SelectedIndexChanged">
                                        <asp:ListItem>-- All --</asp:ListItem>
                                        <asp:ListItem Value="T">THC</asp:ListItem>
                                        <asp:ListItem Value="P">PRS</asp:ListItem>
                                         <asp:ListItem Value="D">DRS</asp:ListItem>
                                          <asp:ListItem Value="PD">PRS+DRS</asp:ListItem>
                                    </asp:DropDownList>
                                    </ContentTemplate>
                                    </asp:UpdatePanel></td>
                            </tr>
                            
                            <tr style="background-color: white">
                                                        <td valign="top" align ="left">
                                                         <asp:Label ID="Label5" runat="server" CssClass="blackfnt" Text="Enter Document No :" Width="143px"></asp:Label>
                                                                              </td>
                                                        <td style="font-size: 12pt; font-family: Times New Roman" valign="top" align="left" colspan="2">
                                                            &nbsp;
                                                            
                                       
                                                            <asp:TextBox ID="txtdoc" runat="server"></asp:TextBox>
                                                             <asp:Label ID="Label6" runat="server" CssClass="blackfnt" Text="[seperated by comma if multiple] " Width="341px"></asp:Label>
                                                                              
                                                            
                                                            </td>
                                                            </tr>
                                                            
                                                            
                                                    
                                       
                            
                            
                            
                             
                                                            
                                                            
                            
                            
                                    
                            <tr style="background-color: white">
                                
                             <td valign="top" align ="left"></td>  
                                                            
                                <td align="left" colspan="2" style="font-size: 12pt; font-family: Times New Roman"
                                    valign="top">
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                    <asp:Panel  runat="server" id ="pnl1" Visible="false"> 
                                    
                                    
                                        <table border="0" cellspacing="1" style="width: 100%; height: 263px">
                <tr class="bgbluegrey">
                    <td align="center"  colspan="4"  style="height: 21px">
                        <asp:Label ID="Label11" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Select Criteria For THC
                                    </asp:Label>
                    </td>
                </tr>
                
                
                
                   <tr class="bgbluegrey">
                    <td colspan="2" >
                   <asp:Label ID="Label12" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Document Details
                                    </asp:Label>
                    </td>
                    </tr>
                     <tr>
                     
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox41" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox41_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                
                
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server"                                          UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="THC Document No" Value="thcno"></asp:ListItem>
                                    <asp:ListItem Text="Date" Value="thcdt"></asp:ListItem>
                                    <asp:ListItem Text="Location" Value="thcbr"></asp:ListItem>
                                    <asp:ListItem Text="Vehichle No." Value="vehno"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Vendor Type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor Name" Value="vendor_name"></asp:ListItem>
                                    <asp:ListItem Text="Route" Value="routename"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox41" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
              <tr class="bgbluegrey">
                    <td colspan="2" >
                          <asp:Label ID="Label7" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Financial Details- Advance </asp:Label>
                    </td>
                     </tr>
                     <tr>
                     
                    <td colspan="2" align="right">
                
                   
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox42" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox42_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server"                                        UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Amount" Value="advamt"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Other Amount" Value=""></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid " Value="advamt"></asp:ListItem>
                                    <asp:ListItem Text="Toal Amount" Value="TOTALAMTPAID "></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid by" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Advance Paid At" Value="advpaidat"></asp:ListItem>
                                    <asp:ListItem Text="Advance Voucher No. " Value="advvoucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Advance Payment Date." Value="advpaiddt"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox42" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                  
                    
                    
                    <td colspan="2">
                        <asp:Label ID="Label37" runat="server" CssClass="blackfnt" Font-Bold="true">
                                   Financial Details- Balance
                                      
                                    </asp:Label>
                    </td>
                     </tr>
                    <tr>
                     <td colspan="2" align="right" >
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox1_CheckedChanged" Text="Select All"  Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                   
                   
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server"                                      UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList3" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Claims" Value="dclaim"></asp:ListItem>
                                    <asp:ListItem Text="Deductions" Value="oth_ded"></asp:ListItem>
                                    <asp:ListItem Text="Incentive" Value="incentive"></asp:ListItem>
                                    <asp:ListItem Text="Penalty" Value="penalty"></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid" Value="bal_colamt"></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid By" Value="balpaidby"></asp:ListItem>
                                    <asp:ListItem Text="Balanace Paid At" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Voucher Number" Value="balvoucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Date." Value="balvoucherdt"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
               
              
               <tr class="bgbluegrey">
                    <td colspan="2" >
                    
                       <asp:Label ID="Label14" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     Financial Details- Bill Entry 
                                      
                                    </asp:Label>
                    </td></tr>
                    <tr>
                     <td colspan="2" align="right" >
                   
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox2_CheckedChanged" Text="Select All"  Font-Bold="true"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server"                                    UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList4" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Enabled="false" Text="Bill Entry Date" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Bill Entry Number" Value="vendorBENo"></asp:ListItem>
                                    <asp:ListItem Text="Vendor Bill Number" Value="vendorbillNo"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Vendor Bill Amount" Value="bal_colamt"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Payment Voucher Number" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Payment Date" Value="PaymentDt"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
               <tr class="bgbluegrey">
                    <td colspan="2" >
                        <asp:Label ID="Label15" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Docket Details
                                    </asp:Label>
                    </td>
                    </tr>
                    <tr>
                     <td colspan="2" align="right" >
                       <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox3_CheckedChanged" Text="Select All" Width="95px" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server"                                  UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList5" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Number of Cnotes Moved" Value="Total_docket"></asp:ListItem>
                                    <asp:ListItem Text="Total Actual Weight" Value="ld_actuwt"></asp:ListItem>
                                    <asp:ListItem Text="Total Freight" Value="Total_freight"></asp:ListItem>
                                    <asp:ListItem Text="Dep. Date" Value="dept_dt"></asp:ListItem>
                                    <asp:ListItem Text="Dep. Time" Value="dept_tm"></asp:ListItem>
                                    <asp:ListItem Text="Arrv. date" Value="arrv_dt"></asp:ListItem>
                                    <asp:ListItem Text="Arrv. Time" Value="arrv_tm"></asp:ListItem>
                                    <asp:ListItem Text="Start KM" Value="OPENKM"></asp:ListItem>
                                    <asp:ListItem Text="Close KM" Value="CLOSEKM"></asp:ListItem>
                                    <asp:ListItem Text="Travel Distance" Value="Dist"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox3" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="center" colspan="3" style="height: 22px">
                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click1" Text="Show" Width="75px" />
                    </td>
                </tr>
            </table>
                             
                                    </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlDoctype" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                                </td>
                            </tr>
                        
                         <tr style="background-color: white">
                                <td align="left" valign="top">
                                </td>
                                <td align="left" colspan="2" style="font-size: 12pt; font-family: Times New Roman"
                                    valign="top">
                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                    <%--<asp:Panel  runat="server" id ="m1" Visible="false">--%>
                                    <asp:Panel id="m1" runat="server" Visible="false">
                                  
                                  <table border="1" cellspacing="1" style="width: 100%; height: 263px">
                <tr class="bgbluegrey">
                    <td align="center" colspan="3" style="height: 14px">
                        <asp:Label ID="Label16" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Select Criteria For PRS
                                    </asp:Label>
                    </td>
                </tr>
                                      <tr>
                                          <td align="center" colspan="3">
                                              <table  border="1" style="height: 80px">
                                                  <tr style="background-color: white">
                                                      <td style="width: 5025px" align="left" colspan="3" >
                        <asp:Label ID="Label17" runat="server" CssClass="blackfnt" Text="Deliverd By"></asp:Label></td>
                                                      <td style="width: 1365px" align="left">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddldelby" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldelby_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="BA">BA</asp:ListItem>
                                    <asp:ListItem Value="Own">Own</asp:ListItem>
                                </asp:DropDownList><%--<asp:Button ID="btnBranchPopup" runat="server" Text="..." OnClick="BranchPopup(this)" PopupURL="../../mis/popup-cust.asp?13" />--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                                                      </td>
                                                      <td style="width: 651px" align="left">
                        <asp:Label ID="Label18" runat="server" CssClass="blackfnt" Text="Vendor Type"  ></asp:Label></td>
                                                      <td style="width: 2299px" align="left">
                         <asp:UpdatePanel ID="UpdatePanel20" runat="server"                               UpdateMode="Conditional" RenderMode="inline">
                                                            <ContentTemplate>
                                    
                <asp:Panel id="pnlventype" runat="server" Visible="false">
                    &nbsp;<asp:DropDownList ID="ddlventype2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlventype2_SelectedIndexChanged"  >
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Attached</asp:ListItem>
                            <asp:ListItem Value="2">Own</asp:ListItem>
                        </asp:DropDownList>
                        </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddldelby" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                                                      </td>
                                                  </tr>
                                                  <tr style="background-color: white">
                                                      <td style="width: 325px" align="left" colspan="3" >
                        <asp:Label ID="Label19" runat="server" CssClass="blackfnt" Text="BA / Vendor"></asp:Label></td>
                                                      <td style="width: 1365px" align="left">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlvenPRS" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvenPRS_SelectedIndexChanged">
                                    <asp:ListItem Text="-All-" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                                                      </td>
                                                      <td style="width: 651px" align="left">
                        <asp:Label ID="Label20" runat="server" CssClass="blackfnt" Text="Staff" ></asp:Label></td>
                                                      <td style="width: 299px" align="left">
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                            <ContentTemplate>
                         <asp:Panel id="pnlstaff" runat="server" Visible="false">
                             &nbsp;<asp:DropDownList ID="ddlstaff" runat="server" >
                        </asp:DropDownList>
                         </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddldelby" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                                                      </td>
                                                  </tr>
                                              </table>
                                          </td>
                                      </tr>
               <%-- <tr style="background-color: white">
                    <td align="left" style="width: 123px">
                        &nbsp;&nbsp;
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;
                        
                        
                        
                        </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" style="width: 123px">
                        &nbsp;&nbsp;
                    </td>
                    <td align="left">
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        
                        
                        </td>
                </tr>--%>
                <tr class="bgbluegrey">
                    <td colspan="3" >
                        <asp:Label ID="Label21" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Document Details
                                    </asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="3" align="right">
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox4_CheckedChanged" Text="Select All" Font-Bold="True" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
               <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server"                           UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList6" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">                  
                                   
                                    <asp:ListItem Text="PRS Document No" Value="pdcno"></asp:ListItem>
                                    <asp:ListItem Text="Date" Value="pdcdt"></asp:ListItem>
                                    <asp:ListItem Text="Location" Value="pdcbr"></asp:ListItem>
                                    <asp:ListItem Text="Vehichle No." Value="vehno"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Vendor Type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor Name" Value="vendorname"></asp:ListItem>
                                    
                                    
                                   
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox4" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="3" >
                        <asp:Label ID="Label22" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Financial Details- Advance </asp:Label>
                    </td>
                         </tr>
                    <tr>
                    <td colspan="3" align="right">
               
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox5_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server"                         UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList7" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    
                                    <asp:ListItem Text="Amount" Value="advamt"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Other Amount" Value=""></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid " Value="advamt"></asp:ListItem>
                                    <asp:ListItem Text="Toal Amount" Value="TOTALAMTPAID "></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid by" Value=""></asp:ListItem>
                                    <asp:ListItem  Enabled="false" Text="Advance Paid At" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Advance Voucher No. " Value="adv_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Advance Payment Date." Value="adv_VoucherDt"></asp:ListItem>
                                    <asp:ListItem Text="Hire Charges." Value="PDC_AMT"></asp:ListItem>
                                
                                    
                                    
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox5" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="3" >
                        <asp:Label ID="Label23" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Financial Details- Balance
                                    </asp:Label>
                    </td>
                         </tr>
                    <tr>
                    <td colspan="3" align="right">
                   <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox6_CheckedChanged" Text="Select All" Font-Bold="True" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server"                       UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList8" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                   
                               
                               <asp:ListItem Text="Claims" Value="dclaim"></asp:ListItem>
                                    <asp:ListItem Text="Deductions"  Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Incentive" Value="" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Penalty" Value="penalty"></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid By" Value="bal_paidby"></asp:ListItem>
                                    <asp:ListItem Text="Balanace Paid At" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Voucher Number" Value="bal_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Date." Value="bal_VoucherDt"></asp:ListItem>
                               
                               
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox6" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="3" >
                        <asp:Label ID="Label24" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     Financial Details- Bill Entry 
                                      
                                    </asp:Label>
                    </td>
                         </tr>
                    <tr>
                    <td colspan="3" align="right">
                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox7" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox7_CheckedChanged" Text="Select All"  Font-Bold="true"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server"                     UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList9" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                  
                               <asp:ListItem  Text="Bill Entry Date" Value="vendorBEdt"></asp:ListItem>
                                    <asp:ListItem Text="Bill Entry Number" Value="vendorBENo"></asp:ListItem>
                                    <asp:ListItem Text="Vendor Bill Number" Value="vendorbillNo"></asp:ListItem>
                                    <asp:ListItem  Text="Vendor Bill Amount" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Payment Voucher Number" Value="bal_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Payment Date" Value="PaymentDt"></asp:ListItem>
                             
                              
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox7" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="3">
                        <asp:Label ID="Label25" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Docket Details
                                    </asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox8" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox8_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel32" runat="server"                   UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList10" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Number of Cnotes Moved" Value="dktot"></asp:ListItem>
                                    <asp:ListItem Text="Total Actual Weight" Value="chrgwt"></asp:ListItem>
                                    <asp:ListItem Text="Total Freight" Value="frt"></asp:ListItem>
                                    <asp:ListItem Text="Total Cnotes Sent for Delivery" Value="DKT_D"></asp:ListItem>
                                    <asp:ListItem Text="Total Actual Weight" Value="chrgwt_D"></asp:ListItem>
                                    <asp:ListItem Text="Total Freight" Value="FRT_D"></asp:ListItem>
                                    <asp:ListItem Text="Start KM" Value="OPENKM"></asp:ListItem>
                                    <asp:ListItem Text="Close KM" Value="CLOSEKM"></asp:ListItem>
                                    <asp:ListItem Text="Travel Distance" Value="Dist"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox8" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="center" colspan="3" style="height: 22px">
                        <asp:Button ID="BtnPRS" runat="server" OnClick="BtnPRS_Click1" Text="Show" Width="75px" />
                    </td>
                </tr>
            </table>

                                  
                                     </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlDoctype" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                                </td>
                            </tr>
      
                           
                            
                            
                            
                      <tr style="background-color: white">
                                <td align="left" valign="top">
                                </td>
                                <td align="left" colspan="2" style="font-size: 12pt; font-family: Times New Roman"
                                    valign="top">
                                     <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                            <ContentTemplate>
                                    <%--<asp:Panel  runat="server" id ="m1" Visible="false">--%>
                                    <asp:Panel id="pnlDRS" runat="server" Visible="false">
                                  
                                  <table border="0" cellspacing="1" style="width: 100%; height: 263px">
                <tr class="bgbluegrey">
                    <td align="center" colspan="3" style="height: 21px">
                        <asp:Label ID="Label26" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Select Criteria For DRS
                                    </asp:Label>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" style="width: 123px">
                        <asp:Label ID="Label27" runat="server" CssClass="blackfnt" Text="Deliverd By"></asp:Label>&nbsp;
                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddldelbyDRS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldelbyDRS_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="BA">BA</asp:ListItem>
                                    <asp:ListItem Value="Own">Own</asp:ListItem>
                                </asp:DropDownList><%--<asp:Button ID="btnBranchPopup" runat="server" Text="..." OnClick="BranchPopup(this)" PopupURL="../../mis/popup-cust.asp?13" />--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;
                         <asp:UpdatePanel ID="UpdatePanel35" runat="server"                UpdateMode="Conditional" RenderMode="inline">
                                                            <ContentTemplate>
                                    
                <asp:Panel id="pnlventypeDRS" runat="server" Visible="false">
                        <asp:Label ID="Label28" runat="server" CssClass="blackfnt" Text="Vendor Type"  ></asp:Label>
                        <asp:DropDownList ID="ddlventypeDRS" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlventypeDRS_SelectedIndexChanged"  >
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Attached</asp:ListItem>
                            <asp:ListItem Value="2">Own</asp:ListItem>
                        </asp:DropDownList>
                        </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddldelbyDRS" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                        
                        
                        
                        </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" style="width: 123px">
                        <asp:Label ID="Label29" runat="server" CssClass="blackfnt" Text="BA / Vendor"></asp:Label>&nbsp;
                        <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlvenDRS" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvenDRS_SelectedIndexChanged">
                                    <asp:ListItem Text="-All-" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        &nbsp; &nbsp; &nbsp;
                        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                            <ContentTemplate>
                         <asp:Panel id="pnlstaffdrs" runat="server" Visible="false">
                        <asp:Label ID="Label30" runat="server" CssClass="blackfnt" Text="Staff" ></asp:Label>
                        <asp:DropDownList ID="ddlstaffDRS" runat="server" OnSelectedIndexChanged="ddlstaffDRS_SelectedIndexChanged" >
                        </asp:DropDownList>
                         </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddldelbyDRS" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                        
                        
                        </td>
                </tr>
                   <tr class="bgbluegrey">
                    <td colspan="2" >
                  <asp:Label ID="Label31" runat="server" CssClass="blackfnt" Font-Bold="true">
                                        Document Details
                                    </asp:Label>
                    </td>
                    
                    </tr>
                     <tr>
                    <td  colspan="2" align="right"  >
                 <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox9" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox9_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel39" runat="server"            UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList11" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                   
                                    <asp:ListItem Text="PRS Document No" Value="pdcno"></asp:ListItem>
                                    <asp:ListItem Text="Date" Value="pdcdt"></asp:ListItem>
                                    <asp:ListItem Text="Location" Value="pdcbr"></asp:ListItem>
                                    <asp:ListItem Text="Vehichle No." Value="vehno"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Vendor Type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor Name" Value="vendorname"></asp:ListItem>
                                    </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox9" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="2">
                        <asp:Label ID="Label32" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Financial Details- Advance </asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox10" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox10_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel41" runat="server"          UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList12" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Amount" Value="advamt"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Other Amount" Value=""></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid " Value="advamt"></asp:ListItem>
                                    <asp:ListItem Text="Toal Amount" Value="" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Advance Paid by" Value=""></asp:ListItem>
                                    <asp:ListItem  Enabled="false" Text="Advance Paid At" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Advance Voucher No. " Value="adv_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Advance Payment Date." Value="adv_VoucherDt"></asp:ListItem>
                                    <asp:ListItem Text="Hire Charges." Value="PDC_AMT"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox10" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="2">
                        <asp:Label ID="Label33" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Financial Details- Balance
                                    </asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox11" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox11_CheckedChanged" Text="Select All" Font-Bold="True" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel43" runat="server"        UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList13" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Claims" Value="dclaim"></asp:ListItem>
                                    <asp:ListItem Text="Deductions" Value="" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Incentive" Value="" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Penalty" Value="" Enabled="false"></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid" Value="balamtbrcd" ></asp:ListItem>
                                    <asp:ListItem Text="Balance Paid By" Value="bal_paidby"></asp:ListItem>
                                    <asp:ListItem Text="Balanace Paid At" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Voucher Number" Value="bal_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Balance Payment Date." Value="bal_VoucherDt"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox11" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="2">
                        <asp:Label ID="Label34" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     Financial Details- Bill Entry 
                                      
                                    </asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox12" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox12_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel45" runat="server"      UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList14" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem  Text="Bill Entry Date" Value="vendorBEdt"></asp:ListItem>
                                    <asp:ListItem Text="Bill Entry Number" Value="vendorBENo"></asp:ListItem>
                                    <asp:ListItem Text="Vendor Bill Number" Value="vendorbillNo"></asp:ListItem>
                                    <asp:ListItem  Text="Vendor Bill Amount" Value="balamtbrcd"></asp:ListItem>
                                    <asp:ListItem Enabled="false" Text="Payment Voucher Number" Value="bal_voucherNo"></asp:ListItem>
                                    <asp:ListItem Text="Payment Date" Value="PaymentDt"></asp:ListItem>
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox12" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="bgbluegrey">
                    <td colspan="2">
                        <asp:Label ID="Label35" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Docket Details
                                    </asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox13" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox13_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel47" runat="server"    UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList15" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                                    <asp:ListItem Text="Number of Cnotes Moved" Value="dktot"></asp:ListItem>
                                    <asp:ListItem Text="Total Actual Weight" Value="chrgwt"></asp:ListItem>
                                    <asp:ListItem Text="Total Freight" Value="frt"></asp:ListItem>
                                    </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox13" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                               <tr class="bgbluegrey">
                    <td colspan="2">
                        <asp:Label ID="Label36" runat="server" CssClass="blackfnt" Font-Bold="true">
                                     
                                      Delivery Details
                                    </asp:Label>
                    </td>
                    </tr>
                    <tr>
                    
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox14" runat="server" AutoPostBack="true" CssClass="blackfnt"
                                    OnCheckedChanged="CheckBox14_CheckedChanged" Text="Select All" Font-Bold="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>    
                                   
                               <tr style="background-color: white">
                    <td align="left" colspan="3" style="height: 21px">
                        <asp:UpdatePanel ID="UpdatePanel48" runat="server"  UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxList16" runat="server" CellPadding="2" CellSpacing="2"
                                    CssClass="blackfnt" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Right"
                                    Width="700px">
                      
                                    <asp:ListItem Text="Total Cnotes Sent for Delivery" Value="DKT_D"></asp:ListItem>
                                    <asp:ListItem Text="Total Actual Weight" Value="chrgwt_D"></asp:ListItem>
                                    <asp:ListItem Text="Total Freight" Value="FRT_D"></asp:ListItem>
                                      <asp:ListItem Text="Cnotes Deliverd" Value="DKT_Y"></asp:ListItem>
                                        <asp:ListItem Text="Freight" Value="FRT_Y"></asp:ListItem>
                                          <asp:ListItem Text="weight" Value="chrgwt_Y"></asp:ListItem>
                                      <asp:ListItem Text="Cnotes Not Deliverd" Value="DKT_N"></asp:ListItem>
                                        <asp:ListItem Text="Freight" Value="FRT_N"></asp:ListItem>
                                          <asp:ListItem Text="weight" Value="chrgwt_N"></asp:ListItem>
                                      
                                      <asp:ListItem Text="Cnotes Part Deliverd" Enabled="false" Value=" "></asp:ListItem>
                                        <asp:ListItem Text="Freight" Value=" " Enabled="false"></asp:ListItem>
                                          <asp:ListItem Text="weight" Value=" "  Enabled="false"></asp:ListItem>
                                      <asp:ListItem Text="Start KM" Value="OPENKM"></asp:ListItem>
                                    <asp:ListItem Text="Close KM" Value="CLOSEKM"></asp:ListItem>
                                    <asp:ListItem Text="Travel Distance" Value="Dist"></asp:ListItem>
                                     </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CheckBox14" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                               
                <tr style="background-color: white">
                    <td align="center" colspan="3" style="height: 22px">
                        <asp:Button ID="btnDRS" runat="server" OnClick="btnDRS_Click1" Text="Show" Width="75px" />
                    </td>
                </tr>
            </table>

                                  
                                     </asp:Panel>
                                      </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlDoctype" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
                                </td>
                            </tr>
          
                            
                            
                      
                            
                            
                    
                                                                </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    
                                    
                                    
                                    </tr>
                                    
                                    </table>

</asp:Content>
