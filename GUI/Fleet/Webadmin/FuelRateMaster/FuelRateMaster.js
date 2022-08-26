User_Ctrl_FrmNm = "ctl00_MyCPH1_UCMyPaymentControl1_"


/*************************************************************************************************************
            FUNCTION VALIDATES Vendor/Vehicle COde
*************************************************************************************************************/
var SelVendor = "";
function VenDor_Vechicle_Blur(Obj, Mode) {
    
    var mydiv = document.getElementById("myDiv");

    //alert("Invalid");
    if (Obj.value == "") {
        mydiv.innerHTML = "";
        return false;
    }
    var Search_Char = "~"
    if (Obj.value.indexOf(Search_Char) == -1) {
        alert("Invalid  " + Mode + "  Name~Code Format");
        Obj.focus();
        mydiv.innerHTML = "";
        return false
    }

    Obj.value = Obj.value.toUpperCase()
    var Code_ID = Obj;

    var Code_Value = Code_ID.value;
    Code_Value_Arr = Code_Value.split("~");

    var Code_Value_0 = Code_Value_Arr[0]
    var Code_Value_1 = Code_Value_Arr[1]
	
	 $("#lnkPopUp").show();
	SelVendor = Code_Value_1;
	
    if (Code_Value_1 == "")
        return false;

    var findobj = false;
    findobj = GetXMLHttpObject();

    if (findobj) {
        // alert("Hi..1")
        //alert(Code_Value_1)
        //alert(Mode)
        var strpg = "../CheckExist.aspx?Mode=" + Mode + "&Code=" + Code_Value_1 + "&sid=" + Math.random();
        findobj.open("GET", strpg, true);
        findobj.onreadystatechange = function () {

            // alert("Hi..2")
            // alert(findobj.readyState)
            // alert(findobj.status)
            if (findobj.readyState == 4 && findobj.status == 200) {

                //alert(findobj.responseText)
                var res = findobj.responseText.split("|");
                if (res[0] == "false") {
                    alert(Mode + "  Code is not Valid... Please Enter Valid " + Mode + " Code");
                    Code_ID.value = "";
                    Code_ID.focus();
					 $("#lnkPopUp").hide();
                    return false;
                }

            }
        }
        findobj.send(null);
    }

    return false;
}
$(document).ready(function () {
    $("#lnkPopUp").hide();
});

function openPopUp() {
    window.open('./FuelRateUpdation_SubmitForValidationPopUp.aspx?VendorCode=' + SelVendor, '_blank', 'top=0, left=0, width=900, height=700, scrollbars=yes, menubar=no,toolbar=no,status=1,resizable=yes');
}


function CheckFromValidCityN(txtFromCity, hfFromCity) {
    var currentTime = new Date()
    var str_Index = "";
    // alert("HI");
    if (txtFromCity.value == "") {
        return true;
    }
    createXMLHttpRequest();
    if (xmlHttpRequest) {
        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4) {
                var returnValue = xmlHttpRequest.responseText.split("~");
                if (returnValue[0] == "N") {
                    alert("Invalid City. !!! Please check.");
                    txtFromCity.focus();
                    return false;
                }
                else {

                    hfFromCity.value = returnValue[1];

                }
            }
        }
        xmlHttpRequest.open("GET", "AJAX_Validator/AjaxResponse.aspx?Function=CheckValidCityCode&datetime=" + currentTime + "&CityName=" + txtFromCity.value, false);
        xmlHttpRequest.send(null);
    }
}


function createXMLHttpRequest() {
    // Mozilla, Safari,...
    if (window.XMLHttpRequest) { xmlHttpRequest = new XMLHttpRequest(); if (xmlHttpRequest.overrideMimeType) xmlHttpRequest.overrideMimeType("text/xml"); }
        // IE
    else if (window.ActiveXObject) { try { xmlHttpRequest = new ActiveXObject("Msxml2.XMLHTTP"); } catch (e) { try { xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP"); } catch (e) { } } }
}


/*****************************************************************************************************************
THIS FUCNTION IS USED TO RETRIEVE HTTP OBJECT
return type XMLHttpObject
*****************************************************************************************************************/
function GetXMLHttpObject() {
    var xmlHttp;
    try {
        xmlHttp = new XMLHttpRequest();    // Firefox, Opera 8.0+, Safari    
    }
    catch (e) {
        // Internet Explorer    
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try
            { xmlHttp = new ActiveXObject("Microsoft.XMLHTTP"); }
            catch (e) {
                alert("Your browser does not support AJAX!");
                return false;
            }
        }
    }
    return xmlHttp;
}

//*****************************************************************************

function CheckEmpty_VendorName(Vendor) {
    //debugger
    var txtVendor = document.getElementById("ctl00_MyCPH1_txtVendor").value;

    if (txtVendor == "" || txtVendor == null) {
        alert("Please enter vendor name!");
        document.getElementById("ctl00_MyCPH1_txtVendor").focus();
        return false;
    }
    else {
        return true;
    }
}

function CheckDate(txtDate1) {

    //debugger
    var tt = document.getElementById('ctl00_MyCPH1_txtFromDate').value;
    var ct = document.getElementById('ctl00_MyCPH1_txtToDate').value;

    //var date = new Date(tt);
    //var newdate = new Date(date); 
    //newdate.setDate(newdate.getDate() + 1);

    //var dd = newdate.getDate();
    //var mm = newdate.getMonth() + 1;
    //var y = newdate.getFullYear();

    //var someFormattedDate = mm + '/' + dd + '/' + y;

    //alert(someFormattedDate)
    //alert(tt)
    //alert(ct)

    //if (ct != someFormattedDate)
    //{
    //    alert("Cannot enter contract for more than 60 days")
    //    return false;
    //}


}


function AddValidation() {
    var txtFromDate = $('#ctl00_MyCPH1_txtFromDate');
    var txtToDate = $('#ctl00_MyCPH1_txtToDate');

    if (txtFromDate.val() != "" && txtToDate.val() != "") {

        var startArr = txtFromDate.val().split("/");
        var endArr = txtToDate.val().split("/");
        var startdt = new Date(startArr[1] + "/" + startArr[0] + "/" + startArr[2]);
        var enddt = new Date(endArr[1] + "/" + endArr[0] + "/" + endArr[2]);

        if (((enddt - startdt) / (1000 * 60 * 60 * 24)) > 60) {
            alert("Start date and End date difference can not be more than 60 days !!");
            return false;
        }

    }
    return true;
}