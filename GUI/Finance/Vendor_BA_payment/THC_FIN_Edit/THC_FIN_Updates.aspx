<%@ Page Language="C#"  MasterPageFile="~/GUI/MasterPage.master"   EnableEventValidation="false" AutoEventWireup="true" CodeFile="THC_FIN_Updates.aspx.cs" Inherits="GUI_Finance_Vendor_BA_payment_THC_FIN_Edit_THC_FIN_Updates" %>


   <asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" Runat="Server">
   <script language="javascript" type="text/javascript" >
var Frmnae="ctl00$MyCPH1$"
var months = new Array("","January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December");
    
        var FinYear="<%=Session["FinYear"]%>"
		var FinYear_to=parseInt(FinYear)+1
		
		if(FinYear_to<10)
		{
		FinYear_to="0"+FinYear_to
		}
		var FinYear_fromdate="01/04/"+FinYear
		var FinYear_todate="31/03/"+FinYear_to
		var Sle_finyear=FinYear+"-"+FinYear_to
function fsubmit()
{
   
         var server_dt="<%=System.DateTime.Now.ToString("dd/MM/yyyy")%>"
            var dt = document.getElementById("ctl00_MyCPH1_hdThcdt").value
            
          //  return false;
             var dt_dd=dt.substring(0,2)
				            var dt_mm=dt.substring(3,5)
				            var dt_yy=dt.substring(6,10)

		                    var sdt =server_dt
				            var sdt_dd=sdt.substring(0,2)
				            var sdt_mm=sdt.substring(3,5)
				            var sdt_yy=sdt.substring(6,10)
		
		    dt=new Date(months[parseFloat(dt_mm)] + " " + parseFloat(dt_dd) + ", " + parseFloat(dt_yy))
		                    server_dt=new Date(months[parseFloat(sdt_mm)] + " " + parseFloat(sdt_dd) + ", " + parseFloat(sdt_yy))
                
                var FinYear_fromdate_dd=FinYear_fromdate.substring(0,2)
				var FinYear_fromdate_mm=FinYear_fromdate.substring(3,5)
				var FinYear_fromdate_yy=FinYear_fromdate.substring(6,10)
				FinYear_fromdate1=new Date(months[parseFloat(FinYear_fromdate_mm)] + " " + parseFloat(FinYear_fromdate_dd) + ", " + parseFloat(FinYear_fromdate_yy))
				
				var FinYear_todate_dd=FinYear_todate.substring(0,2)
				var FinYear_todate_mm=FinYear_todate.substring(3,5)
				var FinYear_todate_yy=FinYear_todate.substring(6,10)
				FinYear_todate1=new Date(months[parseFloat(FinYear_todate_mm)] + " " + parseFloat(FinYear_todate_dd) + ", " + parseFloat(FinYear_todate_yy))
//			 alert(dt)
//			 alert(FinYear_todate1)
//			 alert(FinYear_fromdate1)
				if (dt>FinYear_todate1 || dt<FinYear_fromdate1)
				{
						alert( "You have Selected Finacial Year as "+Sle_finyear +" , SO PDC Date Should Be Between  "+ FinYear_fromdate +" to "+ FinYear_todate +" !!!")
						//document.getElementById(Frmnae+"txtVoucherDate").focus();
						return false;
				}
				
    
    
    document.getElementById(Frmnae+"svrcamt").disabled=false;
     document.getElementById(Frmnae+"cessamt").disabled=false;
    document.getElementById(Frmnae+"hcessamt").disabled=false;
         
    document.getElementById(Frmnae+"tel_chrg").disabled=false;
     document.getElementById(Frmnae+"balamt").disabled=false;
     document.getElementById(Frmnae+"advanceamt").disabled=false;
     document.getElementById(Frmnae+"tds_chrg").disabled=false;
     
     
}
function fbalamt()
{
    
    var pacamt=parseFloat(document.getElementById(Frmnae+"contractamt").value)
    if(pacamt=="")
    {
    document.getElementById(Frmnae+"contractamt").value="0.00"
    }
     var tds_rate=parseFloat(document.getElementById(Frmnae+"tds_rate").value) 
     if(tds_rate=="")
    {
    document.getElementById(Frmnae+"tds_rate").value="0.00"
    }
     var svtax_rate=parseFloat(document.getElementById(Frmnae+"svtax_rate").value) 
     if(svtax_rate=="")
    {
    document.getElementById(Frmnae+"svtax_rate").value="0.00"
    }
    totalbalanceamt=0
    
    if(tds_rate>0)
    {
        document.getElementById(Frmnae+"tds_chrg").value=parseFloat(tds_rate)*parseFloat(pacamt)/100
    }
    else
    {
    document.getElementById(Frmnae+"tds_chrg").value="0.00"
    }
    
    if(svtax_rate>0)
    {
        document.getElementById(Frmnae+"svrcamt").value=parseFloat(svtax_rate)*parseFloat(pacamt)/100
        document.getElementById(Frmnae+"cessamt").value=parseFloat(2)*parseFloat(document.getElementById(Frmnae+"svrcamt").value)/100
        document.getElementById(Frmnae+"hcessamt").value=parseFloat(1)*parseFloat(document.getElementById(Frmnae+"svrcamt").value)/100
    }
    else
    {
        document.getElementById(Frmnae+"svrcamt").value="0.00"
        document.getElementById(Frmnae+"cessamt").value="0.00"
        document.getElementById(Frmnae+"hcessamt").value="0.00"

    }
    var svctax=parseFloat(document.getElementById(Frmnae+"svrcamt").value) 
    if(svctax=="")
    {
    document.getElementById(Frmnae+"svrcamt").value="0.00"
    }
     var cess=parseFloat(document.getElementById(Frmnae+"cessamt").value) 
     if(cess=="")
    {
    document.getElementById(Frmnae+"cessamt").value="0.00"
    }
     var hedu_cess=parseFloat(document.getElementById(Frmnae+"hcessamt").value) 
     if(hedu_cess=="")
    {
    document.getElementById(Frmnae+"hcessamt").value="0.00"
    }
         
    var advanceamt=parseFloat(document.getElementById(Frmnae+"advanceamt").value)  
     if(advanceamt=="")
    {
    document.getElementById(Frmnae+"advanceamt").value="0.00"
    }
    var balamt=parseFloat(document.getElementById(Frmnae+"balamt").value)  
     if(balamt=="")
    {
    document.getElementById(Frmnae+"balamt").value="0.00"
    }
    var telamt=parseFloat(document.getElementById(Frmnae+"tel_chrg").value)
     if(telamt=="")
    {
    document.getElementById(Frmnae+"tel_chrg").value="0.00"
    }
    var mamulchrg=parseFloat(document.getElementById(Frmnae+"mamul_chrg").value) 
     if(mamulchrg=="")
    {
    document.getElementById(Frmnae+"mamul_chrg").value="0.00"
    }
    var Loadchrg=parseFloat(document.getElementById(Frmnae+"load_chrg").value) 
     if(Loadchrg=="")
    {
    document.getElementById(Frmnae+"load_chrg").value="0.00"
    }
    var Load_oper=document.getElementById(Frmnae+"loadOpertype").value 
       
    
     var tdsamt=parseFloat(document.getElementById(Frmnae+"tds_chrg").value) 
      if(tdsamt=="")
    {
    document.getElementById(Frmnae+"tds_chrg").value="0.00"
    }
    if(Load_oper=="P")
    {
        totalbalanceamt=pacamt+telamt-mamulchrg+Loadchrg-tdsamt+svctax+cess+hedu_cess-advanceamt
    }
    else
    {
         totalbalanceamt=pacamt+telamt-mamulchrg-Loadchrg-tdsamt+svctax+cess+hedu_cess-advanceamt
    }
    if(parseFloat(totalbalanceamt)<0)
    {
        totalbalanceamt=0
    }
    
    document.getElementById(Frmnae+"balamt").value=rounditn(totalbalanceamt)
    
}


function rounditn(Num,decplaces)
	{
		Places=decplaces
		if (Places > 0) 
			{
			
			if ((Num.toString().length - Num.toString().lastIndexOf('.')) > (Places+1)) 
			{
			
				if (Num.toString().lastIndexOf('.') < 0) 
				{
					return Num.toString() +'.00';
				}
				var Rounder = Math.pow(10, Places);
				return Math.round(Num * Rounder) / Rounder;
			}
			else 
			{
				//alert("Hi")
				if (Num.toString().lastIndexOf('.') < 0) 
				{
					return Num.toString() +'.00';
				}
				else
				{  
					if (Num.toString().lastIndexOf('.')+1==Num.toString().length-1)
						return Num.toString() +'0';	
					else
						return Num.toString();				   

				}
			}
			}
		else return Math.round(Num);
		
	}
function validFloat(event,txtid)
{
    var tb=document.getElementById(txtid);
    var txt=tb.value;
    if(event.keyCode==13)
        return true;
        
         if(event.keyCode==46)
          if(txt.indexOf('.')!=-1)
            event.keyCode=0;
        
    if((event.keyCode<46 || event.keyCode>57) || event.keyCode==47)
        {event.keyCode=0;return false;}
}
function validpanno(obj)
{

frm_name="ctl00_MyCPH1_"
	

		 if (document.getElementById(frm_name+"pan_no").value!="")
        	{
           	 	if (document.getElementById(frm_name+"pan_no").value.length!=10)
           	 	{
           			 alert("PAN number should be of 10 character")
            			document.getElementById(frm_name+"pan_no").focus();
            			return false;
            		}
        	}


	
}
// onkeypress="validFloat(event,this.getAttribute('id'))"
</script>
<div align="left">
<br />
<br />
<br />
 <table border="0" style="width:9.5in" bgcolor="#808080" cellpadding="2" cellspacing="1" class="boxbg">
 <tr bgcolor="white"><td align="center" class="blackfnt"> <b><%= Session["THCCalledAs"].ToString() %>  Financail Edit</b></td></tr>
 <br />
  <tr bgcolor="white"><td align="center" class="blackfnt"> <b><%= Session["THCCalledAs"].ToString() %>  Details</b></td></tr>
  <tr  bgcolor="#ffffff"><td>
   <table style="width:9.5in" >
    <tr  bgcolor="#ffffff">
             <td >
                         <table cellspacing="1" class="boxbg" style="width:9.5in">
                                 <tr class="blackfnt" bgcolor="#ffffff">
                                   <td align="left">
                                                    <%= Session["THCCalledAs"].ToString() %> NO</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="THCno" runat="server"></asp:Label>
                                    </td>
                                     <td align="left">
                                                    <%= Session["THCCalledAs"].ToString() %> Branch</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="THCBr" runat="server"></asp:Label>
                                    </td>
                                    </tr>
                                     <tr class="blackfnt" bgcolor="#ffffff">
                                        <td align="left">Manual
                                            <%= Session["THCCalledAs"].ToString() %>
                                            NO</td>
                                        <td align="left" nowrap colspan="3">
                                        <asp:TextBox ID="txtManualTHCNo" runat="server" Width="70px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr class="blackfnt" bgcolor="#ffffff">
                                   <td align="left">
                                                    <%= Session["THCCalledAs"].ToString() %> Date</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="Thcdt" runat="server"></asp:Label>
                                                      <asp:HiddenField  ID="hdThcdt" Value="" runat="server" />
                                    </td>
                                     <td align="left">
                                                    Route Name</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="routename" runat="server"></asp:Label>
                                    </td>
                                    </tr>
                                    <tr class="blackfnt" bgcolor="#ffffff">
                                   <td align="left">
                                                    Vendore Name</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="Venodrename" runat="server"></asp:Label>
                                    </td>
                                     <td align="left">
                                                    Vehicleno</td>
                                  <td align="left" nowrap>
                                                   <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                      ID="vehno" runat="server"></asp:Label>
                                    </td>
                                    </tr>
                                                                                                            
                                  </table>
                                  </td></tr>
   </table>
  </td></tr>
  
  <br />
   <tr bgcolor="white"><td align="center" class="blackfnt"> <b><%= Session["THCCalledAs"].ToString() %>  Financail Details</b></td></tr>
   <br />
 <tr style="background-color: White" runat="server" id="trcharges" >
                                                                                        <td colspan="10" align="left" bgcolor="#ffffff">
                                                                                            <table width="800px">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellspacing="1" class="boxbg" style="width: 100%;">
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Standard Contract Amout</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:Label Width="70px" Style="text-align: right;" Text="0.00" CssClass="bluefnt"
                                                                                                                        ID="stdpcamt" runat="server"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Contract Amount (+)</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Style="text-align: right;" Text="0.00" onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="contractamt" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Telephone Chargtes
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px"      Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="tel_chrg" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Hamali
                                                                                                                    <asp:DropDownList ID="loadOpertype" runat="server" onblur="javascript:fbalamt();">
                                                                                                                        <asp:ListItem Value="P">(+)</asp:ListItem>
                                                                                                                        <asp:ListItem Value="M">(-)</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px"   Style="text-align: right;" ID="load_chrg"  onkeypress="validFloat(event,this.getAttribute('id'))" Text="0.00" onblur="javascript:fbalamt();"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Mamul (-)
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px"   Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="mamul_chrg" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    TDS Rate (%)</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="tds_rate" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    TDS Amount (-)</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Enabled="false" Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="tds_chrg" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Service Tax Rate (%)</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px"  Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="svtax_rate" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Service Tax (+)
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Enabled="false" Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="svrcamt" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Cess (+)
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Enabled="false" Style="text-align: right;" Text="0.00" onkeypress="validFloat(event,this.getAttribute('id'))"  ID="cessamt" onblur="javascript:fbalamt();"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Higher Cess (+)
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:HiddenField Value="0" ID="txtcwt" runat="server" />
                                                                                                                    <asp:TextBox Width="70px"  Enabled="false" Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" ID="hcessamt" onblur="javascript:fbalamt();"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <%-- <tr class="blackfnt" bgcolor="#ffffff">
                                                                <td align="left">CWT (-)</td>
                                                                <td align="left">
                                                                    <asp:TextBox Width="70px" style="text-align:right;" Text="0.00" ID="txtcwt" onblur="javascript:fbalamt();"  runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>--%>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <table cellspacing="1" class="boxbg" style="width: 100%;">
                                                                                                            <%--<tr class="blackfnt" bgcolor="#ffffff">
                                                                <td align="left">Height (+)</td>
                                                                <td align="left">
                                                                    <asp:TextBox Width="70px" style="text-align:right;" Text="0.00"  ID="txtheight" onblur="javascript:fbalamt();"  runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>--%>
                                                                                                            <%--<tr class="blackfnt" bgcolor="#ffffff">
                                                                <td align="left">Detendtion (+)</td>
                                                                <td align="left">
                                                                
                                                                    <asp:TextBox Width="70px" style="text-align:right;" Text="0.00" onblur="javascript:fbalamt();" ID="txtdetentionchg" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>--%>
                                                                                                            <%-- <tr class="blackfnt" bgcolor="#ffffff">
                                                        <td align="left">Multi Point (+)</td>
                                                        <td align="left">
                                                            <asp:TextBox Width="70px" style="text-align:right;" Text="0.00"  onblur="javascript:fbalamt();"  ID="txtmultipt" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    TDS Type
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:HiddenField Value="0" ID="txtheight" runat="server" />
                                                                                                                    <asp:HiddenField Value="0" ID="txtdetentionchg" runat="server" />
                                                                                                                    <asp:HiddenField Value="0" ID="txtmultipt" runat="server" />
                                                                                                                    <asp:DropDownList ID="tdsacccode" runat="server">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <%--                                                    <tr class="blackfnt" bgcolor="#ffffff">
                                                        <td align="left">TDS Deducated For </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="tdsfor" runat="server" onblur="javascript:fbalamt();" >
                                                                <asp:ListItem Value="S">Select</asp:ListItem>
                                                                <asp:ListItem Value="C">Corporate</asp:ListItem>
                                                                <asp:ListItem Value="N">Non Corporate</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
--%>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Party PAN Number
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Style="text-align: left;" onblur="javascript:return validpanno(this);"
                                                                                                                         MaxLength="10" ID="pan_no" runat="server"></asp:TextBox>
                                                                                                                    <asp:HiddenField Value="0" ID="advanceamt_paid" runat="server" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <%--  <tr class="blackfnt" bgcolor="#ffffff">
                                                        <td align="left">Advance Paid </td>
                                                        <td align="left">
                                                            <asp:TextBox Width="70px" style="text-align:right;" Text="0.00" onblur="javascript:fbalamt();" ID="advanceamt_paid" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Advance Amount</td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Enabled="false" Style="text-align: right;" Text="0.00"  onkeypress="validFloat(event,this.getAttribute('id'))" onblur="javascript:fbalamt();"
                                                                                                                        ID="advanceamt" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    Advance Amount Paid By
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Style="text-align: left;" onblur="javascript:fbalamt();this.value=this.value.toUpperCase();"
                                                                                                                        ID="advpaidby" runat="server"></asp:TextBox>
                                                                                                                    <asp:HiddenField runat="server" ID="netpayamt" Value="0" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    <b>Balance Amount</b>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px"  Enabled="false" Style="text-align: right;" ID="balamt"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr class="blackfnt" bgcolor="#ffffff">
                                                                                                                <td align="left">
                                                                                                                    <b>Balance Payable At</b></td>
                                                                                                                <td align="left">
                                                                                                                    <asp:TextBox Width="70px" Style="text-align: left;" ID="balamtbr" onblur="javascript:return balPayAtBlur(this.value)"
                                                                                                                        runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr bgcolor="white"><td align="center"> <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="blackfnt" OnClick="btnSubmit_Click"/></td></tr>
</table>


</div>
  <script language="javascript" type="text/javascript" >
								   window.onload=fbalamt
								   </script>
								  
								 
								     <div id="testdiv1" style="position:absolute;visibility:hidden;background-color:white;layer-background-color:white;z-index:99;"></div>
								     </asp:Content>
