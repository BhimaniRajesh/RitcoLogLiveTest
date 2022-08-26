<%@ Page Language="C#" MasterPageFile="~/GUI/MasterPage.master" AutoEventWireup="true"
    CodeFile="Payment_2.aspx.cs" Inherits="GUI_Finance_GeneralizedPO_Ver1_POBillEntryPayment_Payment_2" %>

<%@ Register TagPrefix="UC5" TagName="UCMyPaymentControl" Src="~/UserControls/MyPaymentControl_FA.ascx" %>
<%@ Register Src="../../../Common_UserControls/ProgressBar.ascx" TagName="ProgressBar"
    TagPrefix="uc1" %>
<%@ Register Src="../../../Common_UserControls/UserMessage.ascx" TagName="UserMessage"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyCPH1" runat="Server">

    <script type="text/javascript" language="JavaScript" src="./../../../Images/CalendarPopup.js"></script>

    <script type="text/javascript" language="javascript" src="../FA.js"></script>

    <script language="JavaScript" type="text/javascript">
        var cal = new CalendarPopup("testdiv1"); 
	    cal.setCssPrefix("TEST");
	    cal.showNavigationDropdowns();
	     User_CTR_Frm_Name="ctl00$MyCPH1$UCMyPaymentControl1$"
	        var months = new Array("","January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December");
	    FRM_NM="ctl00_MyCPH1_"
	     var FinYear="<%=Session["FinYear"]%>"
		var FinYear_to=parseInt(FinYear)+1
		
		if(FinYear_to<10)
		{
		FinYear_to="0"+FinYear_to
		}
		var FinYear_fromdate="01/04/"+FinYear
		var FinYear_todate="31/03/"+FinYear_to
		var Sle_finyear=FinYear+"-"+FinYear_to
    </script>

    <script language="JavaScript" type="text/javascript">
        function Check_Calc() {
            var grid = document.getElementById("ctl00_MyCPH1_GV_Booking");
            var rows = grid.rows.length;
            var Check_One = "N"
            var Total_amount = "0.00", NetAmt = "0.00", TotalNetAmt = "0.00"
            for (var b = 2; b <= rows - 1; b++) {
                //alert(b)
                if (b < 10) {
                    GV_FRM_NM = "ctl00_MyCPH1_GV_Booking_ctl0" + b + "_";
                }
                else {
                    GV_FRM_NM = "ctl00_MyCPH1_GV_Booking_ctl" + b + "_";
                }
                if (document.getElementById(GV_FRM_NM + "chksrno").checked) {
                    Check_One = "Y"
                    if (document.getElementById(GV_FRM_NM + "TXTCurramt").disabled == true) {
                        document.getElementById(GV_FRM_NM + "TXTCurramt").disabled = false
                        document.getElementById(GV_FRM_NM + "TXTCurramt").value = document.getElementById(GV_FRM_NM + "LblPendamt").innerText
                    }
                    NetAmt = parseFloat(document.getElementById(GV_FRM_NM + "TXTCurramt").value)
                    //  TotalNetAmt=parseFloat(NetAmt)+parseFloat(TotalNetAmt)
                    if (parseFloat(document.getElementById(GV_FRM_NM + "LblPendamt").innerText) < parseFloat(document.getElementById(GV_FRM_NM + "TXTCurramt").value)) {
                        alert("Total Amount Should BE Less then Pending Amount")
                        document.getElementById(GV_FRM_NM + "TXTCurramt").focus()
                        return false;
                    }
                    Total_amount = parseFloat(Total_amount) + parseFloat(document.getElementById(GV_FRM_NM + "TXTCurramt").value)
                    document.getElementById(GV_FRM_NM + "TXTCurramt").value = rounditn(document.getElementById(GV_FRM_NM + "TXTCurramt").value, 2)

                }
                else {
                    document.getElementById(GV_FRM_NM + "TXTCurramt").disabled = true
                    document.getElementById(GV_FRM_NM + "TXTCurramt").value = "0.00"
                }
            }

            if (b < 10) {
                GV_FRM_NM1 = "ctl00_MyCPH1_GV_Booking_ctl0" + b + "_";
            }
            else {
                GV_FRM_NM1 = "ctl00_MyCPH1_GV_Booking_ctl" + b + "_";
            }
            document.getElementById(GV_FRM_NM1 + "Txt_Total_netamt").value = rounditn(Total_amount, 2)
            document.getElementById(FRM_NM + "TxtTds_Rate").value = rounditn(document.getElementById(FRM_NM + "TxtTds_Rate").value, 3)
            document.getElementById(FRM_NM + "TxtTds_Amt").value = rounditn((parseFloat(document.getElementById(FRM_NM + "TxtTds_Rate").value) * parseFloat(Total_amount)) / 100, 2)

            document.getElementById(User_CTR_Frm_Name + "txtNetPay").value = rounditn(parseFloat(document.getElementById(GV_FRM_NM1 + "Txt_Total_netamt").value) - parseFloat(document.getElementById(FRM_NM + "TxtTds_Amt").value), 2)
            document.getElementById(User_CTR_Frm_Name + "txtAmtApplA").value = rounditn(parseFloat(document.getElementById(GV_FRM_NM1 + "Txt_Total_netamt").value) - parseFloat(document.getElementById(FRM_NM + "TxtTds_Amt").value), 2)

            cahs_N_bank()

        }
        function cahs_N_bank() {


            if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Cash") {
                document.getElementById(User_CTR_Frm_Name + "txtCashAmt").value = rounditn(parseFloat(document.getElementById(GV_FRM_NM1 + "Txt_Total_netamt").value) - parseFloat(document.getElementById(FRM_NM + "TxtTds_Amt").value), 2)
                document.getElementById(User_CTR_Frm_Name + "txtChqAmt").value = "0.00"
            }
            else if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Bank") {
                document.getElementById(User_CTR_Frm_Name + "txtChqAmt").value = rounditn(parseFloat(document.getElementById(GV_FRM_NM1 + "Txt_Total_netamt").value) - parseFloat(document.getElementById(FRM_NM + "TxtTds_Amt").value), 2)
                document.getElementById(User_CTR_Frm_Name + "txtCashAmt").value = "0.00"
            }
            else if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Both") {
                document.getElementById(User_CTR_Frm_Name + "txtChqAmt").value = "0.00"
                document.getElementById(User_CTR_Frm_Name + "txtCashAmt").value = "0.00"
            }
        }

        function rounditn(Num, decplaces) {
            Places = decplaces
            if (Places > 0) {

                if ((Num.toString().length - Num.toString().lastIndexOf('.')) > (Places + 1)) {

                    if (Num.toString().lastIndexOf('.') < 0) {
                        return Num.toString() + '.00';
                    }
                    var Rounder = Math.pow(10, Places);
                    return Math.round(Num * Rounder) / Rounder;
                }
                else {
                    //alert("Hi")
                    if (Num.toString().lastIndexOf('.') < 0) {
                        return Num.toString() + '.00';
                    }
                    else {
                        if (Num.toString().lastIndexOf('.') + 1 == Num.toString().length - 1)
                            return Num.toString() + '0';
                        else
                            return Num.toString();

                    }
                }
            }
            else return Math.round(Num);

        }
        function validFloat(event, txtid) {
            var tb = document.getElementById(txtid);
            var txt = tb.value;
            if (event.keyCode == 13)
                return true;

            if (event.keyCode == 46)
                if (txt.indexOf('.') != -1)
                event.keyCode = 0;

            if ((event.keyCode < 46 || event.keyCode > 57) || event.keyCode == 47)
            { event.keyCode = 0; return false; }
        }
        function ValidateDate(obj) {

            if (obj.value != "") {

                if (isDate(obj.value) == false) {

                    obj.focus()
                    return false
                }
                return true
            }
        }
        var dtCh = "/";
        var minYear = "1900";
        var maxYear = "2100";
        function DaysArray(n) {
            for (var i = 1; i <= n; i++) {
                this[i] = 31
                if (i == 4 || i == 6 || i == 9 || i == "11") { this[i] = 30 }
                if (i == 2) { this[i] = 29 }
            }
            return this
        }
        function isDate(dtStr) {
            var daysInMonth = DaysArray(12)
            var pos1 = dtStr.indexOf(dtCh)
            var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
            var strDay = dtStr.substring(0, pos1)
            var strMonth = dtStr.substring(pos1 + 1, pos2)
            var strYear = dtStr.substring(pos2 + 1)
            strYr = strYear
            if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
            if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
            for (var i = 1; i <= 3; i++) {
                if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
            }
            month = parseInt(strMonth)
            day = parseInt(strDay)
            year = parseInt(strYr)
            if (pos1 == -1 || pos2 == -1) {
                alert("The date format should be : dd/mm/yyyy")
                return false
            }
            if (strMonth.length < 1 || month < 1 || month > 12) {
                alert("Please enter a valid month")
                return false
            }
            if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
                alert("Please enter a valid day")
                return false
            }
            if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
                alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
                return false
            }
            if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
                alert("Please enter a valid date")
                return false
            }
            return true
        }
    </script>

    <script language="JavaScript" type="text/javascript">
        function OnSubmit() {

            var server_dt="<%=System.DateTime.Now.ToString("dd/MM/yyyy")%>"

            var dt = document.getElementById(FRM_NM + "TxtVoucherDT").value
            var hdndockdt;
            var Maxdt = "01/01/2007";

            var grid = document.getElementById("ctl00_MyCPH1_GV_Booking");
            var rows = grid.rows.length;
            var Check_One = "N"
            var Total_amount = "0.00", NetAmt = "0.00", TotalNetAmt = "0.00"
            var i = 0
            for (var b = 2; b <= rows - 1; b++) {
                //alert(b)
                if (b < 10) {
                    GV_FRM_NM = "ctl00_MyCPH1_GV_Booking_ctl0" + b + "_";
                    hdndockdt = "ctl00_MyCPH1_GV_Booking_ctl0" + b + "_billdate"
                }
                else {
                    GV_FRM_NM = "ctl00_MyCPH1_GV_Booking_ctl" + b + "_";
                    hdndockdt = "ctl00_MyCPH1_GV_Booking_ctl" + b + "_billdate";
                }
                hdndockdt = document.getElementById(hdndockdt).value;
                if (document.getElementById(GV_FRM_NM + "chksrno").checked) {
                    Check_One = "Y"
                    i = i + 1
                    var Maxdt1 = Maxdt

                    var Maxdt_dd = Maxdt1.substring(0, 2)
                    var Maxdt_mm = Maxdt1.substring(3, 5)
                    var Maxdt_yy = Maxdt1.substring(6, 10)

                    var hdndockdt1 = hdndockdt
                    var hdndockdt_dd = hdndockdt1.substring(0, 2)
                    var hdndockdt_mm = hdndockdt1.substring(3, 5)
                    var hdndockdt_yy = hdndockdt1.substring(6, 10)

                    Maxdt1 = new Date(months[parseFloat(Maxdt_mm)] + " " + parseFloat(Maxdt_dd) + ", " + parseFloat(Maxdt_yy))
                    hdndockdt1 = new Date(months[parseFloat(hdndockdt_mm)] + " " + parseFloat(hdndockdt_dd) + ", " + parseFloat(hdndockdt_yy))
                    // alert(Maxdt1)
                    // alert(hdndockdt1)
                    // alert(Maxdt1)
                    if (hdndockdt1 > Maxdt1) {
                        Maxdt1 = hdndockdt1
                        Maxdt = hdndockdt
                    }
                }
            }
            if (Check_One != "Y") {
                alert("Please Select Atleast One Bill!!!");
                return false;
            }

            if (dt == "") {
                alert("Payment Date Should Not Be Blank !!!")
                document.getElementById(FRM_NM + "TxtVoucherDT").focus();
                return false;
            }
            var dt_dd = dt.substring(0, 2)
            var dt_mm = dt.substring(3, 5)
            var dt_yy = dt.substring(6, 10)

            var sdt = server_dt
            var sdt_dd = sdt.substring(0, 2)
            var sdt_mm = sdt.substring(3, 5)
            var sdt_yy = sdt.substring(6, 10)


            dt = new Date(months[parseFloat(dt_mm)] + " " + parseFloat(dt_dd) + ", " + parseFloat(dt_yy))
            server_dt = new Date(months[parseFloat(sdt_mm)] + " " + parseFloat(sdt_dd) + ", " + parseFloat(sdt_yy))

            var FinYear_fromdate_dd = FinYear_fromdate.substring(0, 2)
            var FinYear_fromdate_mm = FinYear_fromdate.substring(3, 5)
            var FinYear_fromdate_yy = FinYear_fromdate.substring(6, 10)
            FinYear_fromdate1 = new Date(months[parseFloat(FinYear_fromdate_mm)] + " " + parseFloat(FinYear_fromdate_dd) + ", " + parseFloat(FinYear_fromdate_yy))

            var FinYear_todate_dd = FinYear_todate.substring(0, 2)
            var FinYear_todate_mm = FinYear_todate.substring(3, 5)
            var FinYear_todate_yy = FinYear_todate.substring(6, 10)
            FinYear_todate1 = new Date(months[parseFloat(FinYear_todate_mm)] + " " + parseFloat(FinYear_todate_dd) + ", " + parseFloat(FinYear_todate_yy))

            if (dt > FinYear_todate1 || dt < FinYear_fromdate1) {
                alert("You have Selected Finacial Year as " + Sle_finyear + " , SO Payment Date Should Be Between  " + FinYear_fromdate + " to " + FinYear_todate + " !!!")
                document.getElementById(FRM_NM + "TxtVoucherDT").focus();
                return false;
            }

            Fromdate1 = Maxdt1
            Fromdate = Maxdt
            if (dt < Fromdate1) {
                alert("Payment Date Should Be Greater than  " + Fromdate + "  !!!")
                document.getElementById(FRM_NM + "TxtVoucherDT").focus();
                return false;
            }
            if (dt > server_dt) {
                alert("Payment Date should not be greater than today's date !!!")
                document.getElementById(FRM_NM + "TxtVoucherDT").focus();
                return false;
            }


            if (parseFloat(document.getElementById(FRM_NM + "TxtTds_Amt").value) > 0) {
                if (document.getElementById(FRM_NM + "DDL_Tdstype").value == "") {
                    alert("Plz Select Tds Type !!!")
                    document.getElementById(FRM_NM + "DDL_Tdstype").focus();
                    return false;
                }
                if (document.getElementById(FRM_NM + "DDL_TdstFor").value == "") {
                    alert("Plz Select Tds For !!!")
                    document.getElementById(FRM_NM + "DDL_TdstFor").focus();
                    return false;
                }

            }
            //   alert("hi...1")   
            //   alert(document.getElementById(User_CTR_Frm_Name+"ddlPayMode").value) 
            if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "-1") {
                alert("Plz Select Mode of Transacction !!!")
                document.getElementById(User_CTR_Frm_Name + "ddlPayMode").focus();
                return false;
            }
            if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Both" && i > 1) {
                alert("You have Selected Multiple Records For Both Payment,You have to select Single Records For Both Paymtn!!!")
                document.getElementById(User_CTR_Frm_Name + "ddlPayMode").focus();
                return false;
            }
            if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Cash") {
                if (parseFloat(document.getElementById(User_CTR_Frm_Name + "txtCashAmt").value) <= 0) {
                    alert("Cash Amount Must Be Greater Than Zero !!!")
                    // document.getElementById(FRM_Name+"txtreqdt").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "ddrCashcode").value == "") {
                    alert("PLz Select Cash Account !!!")
                    document.getElementById(User_CTR_Frm_Name + "ddrCashcode").focus();
                    return false;
                }

            }
            else if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Bank") {
                if (parseFloat(document.getElementById(User_CTR_Frm_Name + "txtChqAmt").value) <= 0) {
                    alert("Cheque Amount Must Be Greater Than Zero !!!")
                    // document.getElementById(FRM_Name+"txtreqdt").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "ddrBankaccode").value == "") {
                    alert("PLz Select BANK Account !!!")
                    document.getElementById(User_CTR_Frm_Name + "ddrBankaccode").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "txtChqNo").value == "") {
                    alert("PLz Enter Cheque No!!!")
                    document.getElementById(User_CTR_Frm_Name + "txtChqNo").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "txtChqDate").value == "") {
                    alert("PLz Enter Cheque Date !!!")
                    document.getElementById(User_CTR_Frm_Name + "txtChqDate").focus();
                    return false;
                }

            }
            else if (document.getElementById(User_CTR_Frm_Name + "ddlPayMode").value == "Both") {
                if (parseFloat(document.getElementById(User_CTR_Frm_Name + "txtCashAmt").value) <= 0) {
                    alert("Cash Amount Must Be Greater Than Zero !!!")
                    document.getElementById(User_CTR_Frm_Name + "txtCashAmt").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "ddrCashcode").value == "") {
                    alert("PLz Select Cash Account !!!")
                    document.getElementById(User_CTR_Frm_Name + "ddrCashcode").focus();
                    return false;
                }
                if (parseFloat(document.getElementById(User_CTR_Frm_Name + "txtChqAmt").value) <= 0) {
                    alert("Cheque Amount Must Be Greater Than Zero !!!")
                    document.getElementById(User_CTR_Frm_Name + "txtChqAmt").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "ddrBankaccode").value == "") {
                    alert("PLz Select BANK Account !!!")
                    document.getElementById(User_CTR_Frm_Name + "ddrBankaccode").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "txtChqNo").value == "") {
                    alert("PLz Enter Cheque No!!!")
                    document.getElementById(User_CTR_Frm_Name + "txtChqNo").focus();
                    return false;
                }
                if (document.getElementById(User_CTR_Frm_Name + "txtChqDate").value == "") {
                    alert("PLz Enter Cheque Date !!!")
                    document.getElementById(User_CTR_Frm_Name + "txtChqDate").focus();
                    return false;
                }

            }
            if (Check_One == "N") {
                alert("PLz Select  Atleast One Record !!!")
                // document.getElementById(User_CTR_Frm_Name+"txtChqDate").focus();
                return false;
            }
            //        for(var b=2;b<=rows-1;b++)
            //                  {
            //                        //alert(b)
            //                            if(b<10)
            //                            {
            //                                GV_FRM_NM="ctl00_MyCPH1_GV_Booking_ctl0" + b + "_";
            //                                hdndockdt="ctl00_MyCPH1_GV_Booking_ctl0" + b + "_POdate" 
            //                            }
            //                            else
            //                            {
            //                                 GV_FRM_NM="ctl00_MyCPH1_GV_Booking_ctl" + b + "_";
            //                                 hdndockdt="ctl00_MyCPH1_GV_Booking_ctl" + b + "_POdate";
            //                            }
            //                        
            //                            document.getElementById(GV_FRM_NM+"TXTCurramt").disabled=false	
            //                             
            //                  }
            //		document.getElementById(User_CTR_Frm_Name+"txtChqDate").disabled=false	
            //		document.getElementById(User_CTR_Frm_Name+"txtChqNo").disabled=false	
            //		document.getElementById(User_CTR_Frm_Name+"txtChqAmt").disabled=false	
            //		document.getElementById(User_CTR_Frm_Name+"txtNetPay").disabled=false
            //		document.getElementById(User_CTR_Frm_Name+"txtCashAmt").disabled=false	
            //		//document.getElementById(FRM_Name+"txtbalamt").disabled=false	
            //		document.getElementById(FRM_NM+"TxtTds_Amt").disabled=false	
            document.getElementById("ctl00_MyCPH1_ON_Submit").style.display = "none"



        }
    </script>

    <div align="left" style="width: 9.5in;">
        <asp:UpdatePanel ID="updPnl" runat="server">
            <ContentTemplate>
                <table style="width: 1100px">
                    <tr>
                        <td align="left">
                            <a><font class="blklnkund"><b>Module</b></font></a> <b>&gt; </b><a><font class="blklnkund">
                                <b>Reports</b></font></a> <b>&gt; </b><a href=""><font class="blklnkund"><b>Finance</b></font></a>
                            <b>&gt; </b><font class="bluefnt"><b>PO Bill Payments Details</b></font>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table border="0" cellpadding="3" width="565" cellspacing="1" align="left" class="boxbg">
                                <tr style="background-color: #ffffff">
                                    <td style="height: 10px">
                                        <label class="blackfnt">
                                            Date :</label>
                                    </td>
                                    <td style="height: 10px">
                                        <asp:Label ID="lblseldet" runat="server" CssClass="blackfnt"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="background-color: #FFFFFF">
                                    <td style="width: 205">
                                        <label class="blackfnt">
                                            Vendor :</label>
                                    </td>
                                    <td style="width: 343">
                                        &nbsp;<asp:Label ID="lblloc" Text="All" runat="server" CssClass="blackfnt"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="background-color: #FFFFFF">
                                    <td style="width: 205; height: 6px;">
                                        <label class="blackfnt">
                                            Purchase Order Code :</label>
                                    </td>
                                    <td style="width: 343; height: 6px;">
                                        &nbsp;<asp:Label ID="lblro" runat="server" CssClass="blackfnt"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <table border="0" cellpadding="3" width="800" cellspacing="1" align="left" class="boxbg">
                                <tr class="bgbluegrey">
                                    <td align="center">
                                        <font class="blackfnt"><b>PO Bill Payments Details</b></font>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <table border="0" cellpadding="3" width="800" cellspacing="1" align="left" class="boxbg">
                                <tr bgcolor="white">
                                    <td align="Left">
                                        <font class="blackfnt">Payment Voucherno</font>
                                    </td>
                                    <td align="Left">
                                        <font class="redfnt">System Generated...</font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">Voucher Date </font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">
                                            <asp:TextBox ID="TxtVoucherDT" Width="70" runat="server" CssClass="blackfnt" onblur="javascript:return ValidateDate(this);"></asp:TextBox>
                                            <a href="#" onclick="cal.select(ctl00$MyCPH1$TxtVoucherDT,'anchor2','dd/MM/yyyy'); return false;"
                                                name="anchor2" id="a2">
                                                <img src="./../../../images/calendar.jpg" border="0"></img>
                                            </a><font class="blackfnt">dd/mm/yyyy</font></font>
                                    </td>
                                </tr>
                                <tr bgcolor="white">
                                    <td align="Left">
                                        <font class="blackfnt">Vendor Name</font>
                                    </td>
                                    <td align="Left" colspan="3">
                                        <font class="blackfnt">
                                            <asp:Label ID="lblVendor" runat="server" Text=""></asp:Label></font>
                                    </td>
                                </tr>
                                <tr bgcolor="white">
                                    <td align="Left">
                                        <font class="blackfnt">Vendor Service TaxNO.</font>
                                    </td>
                                    <td align="Left">
                                        <font class="redfnt">
                                            <asp:Label ID="V_Staxno" runat="server" Text=""></asp:Label></font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">Vendor PAN NO.</font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">
                                            <asp:Label ID="V_PANNO" runat="server" Text=""></asp:Label></font>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:GridView ID="GV_Booking" runat="server" align="left" BorderWidth="1" CellPadding="2"
                                HeaderStyle-CssClass="bgbluegrey" PagerStyle-HorizontalAlign="left" CssClass="boxbg"
                                BackColor="white" FooterStyle-CssClass="bgbluegrey" AllowPaging="False" PageSize="100"
                                Width="800" AllowSorting="False" AutoGenerateColumns="false" ShowFooter="true"
                                PagerSettings-FirstPageText="[First]" PagerSettings-LastPageText="[Last]" EmptyDataText="No Records Found...">
                                <PagerStyle VerticalAlign="Bottom" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SR.NO.">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrnocomplainlist" Text="<%# Container.DataItemIndex+1 %>" runat="server">
                                            </asp:Label>
                                            <asp:CheckBox ID="chksrno" runat="server" onclick="javascript:Check_Calc()" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill No" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="LblBillno" Text='<%# DataBinder.Eval(Container.DataItem, "Billno") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="LBLBilldt" Text='<%# DataBinder.Eval(Container.DataItem, "Billdt") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Amount" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="LblPaidamt" Text='<%# DataBinder.Eval(Container.DataItem, "Paidamt") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending  Amount" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="LblPendamt" Text='<%# DataBinder.Eval(Container.DataItem, "Pendamt") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total  Amount" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="LblTotal" Text='<%# DataBinder.Eval(Container.DataItem, "Totalamt") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="Lbltotal" Font-Bold="true" runat="server" Text="Total Amount" CssClass="blackfnt"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current  Amount" ItemStyle-Wrap="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="blackfnt" Font-Bold="true" />
                                        <ItemStyle CssClass="blackfnt" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXTCurramt" runat="server" Width="70" MaxLength="8" Style="text-align: Right"
                                                onkeypress="javascript:return validFloat(event,this.getAttribute('id'))" Enabled="false"
                                                Text="0.00" onblur="javascript:return Check_Calc()" BorderStyle="Groove" CssClass="input"></asp:TextBox>
                                            <asp:HiddenField ID="billdate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "billdate") %>' />
                                            <asp:HiddenField ID="Hnd_totalAmt" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Totalamt") %>' />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:TextBox ID="Txt_Total_netamt" Width="70" Text="0.00" Style="text-align: Right"
                                                runat="server" BorderStyle="Groove" Enabled="false" CssClass="input"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 96px">
                            <table border="0" cellpadding="3" width="800" cellspacing="1" align="left" class="boxbg">
                                <tr class="bgbluegrey">
                                    <td colspan="4">
                                        <font class="blackfnt">Tds Details </font>
                                    </td>
                                </tr>
                                <tr bgcolor="white">
                                    <td align="Left">
                                        <font class="blackfnt">Tds Rate (%)</font>
                                    </td>
                                    <td align="Left">
                                        <font class="redfnt">
                                            <asp:TextBox ID="TxtTds_Rate" Style="text-align: Right" runat="server" Width="70"
                                                MaxLength="8" onkeypress="javascript:return validFloat(event,this.getAttribute('id'))"
                                                Text="0.00" onblur="javascript:return Check_Calc()" BorderStyle="Groove" CssClass="input"></asp:TextBox>
                                        </font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">Tds Amt</font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">
                                            <asp:TextBox ID="TxtTds_Amt" Style="text-align: Right" runat="server" Enabled="false"
                                                Width="70" MaxLength="8" onkeypress="javascript:return validInt(event)" Text="0.00"
                                                onblur="javascript:return Check_Calc()" BorderStyle="Groove" CssClass="input"></asp:TextBox>
                                        </font>
                                    </td>
                                </tr>
                                <tr bgcolor="white">
                                    <td align="Left">
                                        <font class="blackfnt">Tds Type</font>
                                    </td>
                                    <td align="Left">
                                        <font class="redfnt">
                                            <asp:DropDownList ID="DDL_Tdstype" runat="server" CssClass="blackfnt">
                                            </asp:DropDownList>
                                        </font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">Tds Deducted For</font>
                                    </td>
                                    <td align="Left">
                                        <font class="blackfnt">
                                            <asp:DropDownList ID="DDL_TdstFor" runat="server" CssClass="blackfnt">
                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Corporate" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="NonCorporate" Value="NC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellspacing="1" cellpadding="4" width="800" align="left" class="boxbg">
                                <caption>
                                    <font class="blackfnt">Note : Please enter the payment details if Net Payable amount
                                        &gt;0 . </font>
                                    <br />
                                    <tr class="bgbluegrey">
                                        <td align="center" colspan="4">
                                            <font class="blackfnt"><b>Payment Details</b></font>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #FFFFFF">
                                        <td align="center" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanePayment" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <UC5:UCMyPaymentControl ID="UCMyPaymentControl1" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblnull" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="Medium"
                                ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table align="left" cellspacing="1" style="width: 88%" class="boxbg" border="0" id="ON_Submit"
                    runat="server">
                    <tr class="bgbluegrey">
                        <td colspan="4" align="center" style="height: 21px">
                            <asp:Button ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" OnClientClick="javascript:return OnSubmit()"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <uc1:ProgressBar ID="ProgressBar1" runat="server" />
                <uc2:UserMessage ID="usrMsg" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="testdiv1" style="position: absolute; visibility: hidden; background-color: white;
        z-index: 99;">
    </div>
</asp:Content>
