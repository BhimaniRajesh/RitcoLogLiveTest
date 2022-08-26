﻿/// <reference path="GSTCommon.js" />
/// <reference path="../../../Js/Webx.Common.js" />

var btnSubmit, hdnCompanyCode, hdnValid;
var btnNext, tdCompanyHeaderDetail, tdCompanyDetail, Table1Submit, txtCompanyName, tdCompanyCriteria;
var txtGSTCity, ddlGSTState, hdnGSTCity, hdnGSTState
var tdCompanyDetailExcel, rbEntrytype;

$(document).ready(function () {

    btnSubmit = $("#" + btnSubmitID);

    btnNext = $("#" + btnNextID);
    tdCompanyCriteria = $("#" + tdCompanyCriteriaID);
    tdCompanyHeaderDetail = $("#" + tdCompanyHeaderDetailID);
    tdCompanyDetail = $("#" + tdCompanyDetailID);
    Table1Submit = $("#" + Table1SubmitID);
    txtCompanyName = $("#" + txtCompanyNameID);
    hdnCompanyCode = $("#" + hdnCompanyCodeID);
    hdnValid = $("#" + hdnValidID);
    tdCompanyDetailExcel = $("#" + tdCompanyDetailExcelID);
    rbEntrytype = $("#" + rbEntrytype);

    $("select[id*='ddlStatetype']").each(function () {
        var ddlStatetype = $(this);
        var lblGSTType = $('#' + ($(this)[0].id.replace('ddlStatetype', 'lblGSTType')));
        var hdnGSTTypeCode = $('#' + ($(this)[0].id.replace('ddlStatetype', 'hdnGSTTypeCode')));
        var hdnStateTypeCode = $('#' + ($(this)[0].id.replace('ddlStatetype', 'hdnStateTypeCode')));
        if (ddlStatetype.val() == "UT") {
            lblGSTType.text('UTGST');
            hdnGSTTypeCode.val('UT');
            hdnStateTypeCode.val('UT');
        }
        else {
            lblGSTType.text('SGST');
            hdnGSTTypeCode.val('ST');
            hdnStateTypeCode.val('ST');
        }
    });

    txtGSTCity = $("#" + txtGSTCityID);
    ddlGSTState = $("#" + ddlGSTStateID);
    hdnGSTCity = $("#" + hdnGSTCityID);
    hdnGSTState = $("#" + hdnGSTStateID);

    if (hdnValid.val() == '0') {
        tdCompanyHeaderDetail.hide();
        tdCompanyDetail.hide();
        Table1Submit.hide();
    }

    var value = $("#ctl00_MyCPH1_rbEntrytype").find(":checked").val();
    if (value == '0') {
        tdCompanyDetailExcel.hide();
    } else {
        tdCompanyDetail.hide();
    }

    InitAutoCompleteSelection();
    InitAutoComplete();

    //InitAutoComplete();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        // InitAutoComplete();
        btnSubmit = $("#" + btnSubmitID);

        btnNext = $("#" + btnNextID);
        tdCompanyCriteria = $("#" + tdCompanyCriteriaID);
        tdCompanyHeaderDetail = $("#" + tdCompanyHeaderDetailID);
        tdCompanyDetail = $("#" + tdCompanyDetailID);
        Table1Submit = $("#" + Table1SubmitID);
        txtCompanyName = $("#" + txtCompanyNameID);
        hdnCompanyCode = $("#" + hdnCompanyCodeID);
        hdnValid = $("#" + hdnValidID);
        txtGSTCity = $("#" + txtGSTCityID);
        ddlGSTState = $("#" + ddlGSTStateID);
        hdnGSTCity = $("#" + hdnGSTCityID);
        hdnGSTState = $("#" + hdnGSTStateID);

        tdCompanyDetailExcel = $("#" + tdCompanyDetailExcelID);
        rbEntrytype = $("#" + rbEntrytypeID);

        if (hdnValid.val() == '0') {
            tdCompanyHeaderDetail.hide();
            tdCompanyDetail.hide();
            Table1Submit.hide();
        }
        var value = $("#ctl00_MyCPH1_rbEntrytype").find(":checked").val();
        if (value == '0') {
            tdCompanyDetailExcel.hide();
        } else {
            tdCompanyDetail.hide();
        }

        $("select[id*='ddlStatetype']").each(function () {
            var ddlStatetype = $(this);
            var lblGSTType = $('#' + ($(this)[0].id.replace('ddlStatetype', 'lblGSTType')));
            var hdnGSTTypeCode = $('#' + ($(this)[0].id.replace('ddlStatetype', 'hdnGSTTypeCode')));
            var hdnStateTypeCode = $('#' + ($(this)[0].id.replace('ddlStatetype', 'hdnStateTypeCode')));
            if (ddlStatetype.val() == "UT") {
                lblGSTType.text('UTGST');
                hdnGSTTypeCode.val('UT');
                hdnStateTypeCode.val('UT');
            }
            else {
                lblGSTType.text('SGST');
                hdnGSTTypeCode.val('ST');
                hdnStateTypeCode.val('ST');
            }
        });

        InitAutoCompleteSelection();
        InitAutoComplete();
    }
});

function InitAutoCompleteSelection() {
    AutoCompleteForGSTMaster(txtCompanyName, serviceUrl + '/CompanyAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
                'searchText', 'l', 'l', 'l', 'v', 'v', hdnCompanyCodeID, 'No entries found!', 2, null, true);

    //AutoCompleteForGSTMaster(ddlGSTState, serviceUrl + '/StateAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
    //          'searchText', 'l', 'l', 'l', 'v', 'v', hdnGSTStateID, 'No entries found!', 2, 'StateType=' + '');

    AutoCompleteForGSTMaster(txtGSTCity, serviceUrl + '/CityAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
              'searchText', 'l', 'l', 'l', 'v', 'v', hdnGSTCityID, 'No entries found!', 2, 'StateId=' + hdnGSTState.attr("id"), true);
}

function onBillingTypeChange(e) {
    var rbEntrytype = $('#' + $(e)[0].id);
    if ($("#ctl00_MyCPH1_rbEntrytype").find(":checked").val() == '0') {
        tdCompanyDetail.show();
        tdCompanyDetailExcel.hide();
    } else if ($("#ctl00_MyCPH1_rbEntrytype").find(":checked").val() == '1') {
        tdCompanyDetailExcel.show();
        tdCompanyDetail.hide();
    }

}

function onChangesStateType(e) {
    var ddlStatetype = $('#' + $(e)[0].id);
    var lblGSTType = $('#' + $(e)[0].id.replace('ddlStatetype', 'lblGSTType'));
    var hdnGSTTypeCode = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnGSTTypeCode'));
    var hdnStateTypeCode = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnStateTypeCode'));

    if (ddlStatetype.val() == "UT") {
        lblGSTType.text('UTGST');
        hdnGSTTypeCode.val('UT');
        hdnStateTypeCode.val('UT');
    }
    else {
        lblGSTType.text('SGST');
        hdnGSTTypeCode.val('ST');
        hdnStateTypeCode.val('ST');
    }

    var txtStateUTName = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtStateUTName'));
    var hdnStateUTCode = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnStateUTCode'));
    var txtAddress = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtAddress'));
    var hdnAddress = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnAddress'));
    var txtCity = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtCity'));
    var hdnCityCode = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnCityCode'));
    var txtLocation = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtLocation'));
    var hdnLocationCode = $('#' + $(e)[0].id.replace('ddlStatetype', 'hdnLocationCode'));
    //var txtProvisionalNumber = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtProvisionalNumber'));//commented on 04-jul-2017, to provide one textbox for both Provisional / GSTIN Number. 
    var txtGSTTiNumber = $('#' + $(e)[0].id.replace('ddlStatetype', 'txtGSTTiNumber'));

    txtStateUTName.val(""); hdnStateUTCode.val("");
    txtAddress.val(""); hdnAddress.val("");
    txtCity.val(""); hdnCityCode.val("");
    txtLocation.val(""); hdnLocationCode.val("");
    //txtProvisionalNumber.val("");//commented on 04-jul-2017, to provide one textbox for both Provisional / GSTIN Number. 
    txtGSTTiNumber.val("");
}

function InitAutoComplete() {
    var repeater = document.getElementById("tblOrderDetail");
    var inputs = repeater.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {

        if (inputs[i].type == "text" && inputs[i].id.indexOf("txtStateUTName") > 0) {
            var hdnStateUTCode = inputs[i].id.replace("txtStateUTName", "hdnStateUTCode");
            var ddlStatetype = inputs[i].id.replace("txtStateUTName", "ddlStatetype");
            AutoCompleteForGSTMaster("#" + inputs[i].id, serviceUrl + '/StateAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
           'searchText', 'l', 'l', 'l', 'v', 'v', hdnStateUTCode, 'No entries found!', 2, 'StateType=' + ddlStatetype, true);
        }

        if (inputs[i].type == "text" && inputs[i].id.indexOf("txtCity") > 0) {
            var hdnCityCode = inputs[i].id.replace("txtCity", "hdnCityCode");
            var hdnStateUTCode = inputs[i].id.replace("txtCity", "hdnStateUTCode");
            AutoCompleteForGSTMaster("#" + inputs[i].id, serviceUrl + '/CityAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
                  'searchText', 'l', 'l', 'l', 'v', 'v', hdnCityCode, 'No entries found!', 2, 'StateId=' + hdnStateUTCode, true);
        }

        if (inputs[i].type == "text" && inputs[i].id.indexOf("txtLocation") > 0) {
            var hdnCityCode = inputs[i].id.replace("txtLocation", "hdnLocationCode");
            var hdnStateUTCode = inputs[i].id.replace("txtLocation", "hdnStateUTCode");
            AutoCompleteForGSTMaster("#" + inputs[i].id, serviceUrl + '/LocationAutoComplate', 'POST', 'application/json; charset=utf-8', 'json',
                  'searchText', 'v', 'v', 'l', 'v', 'v', hdnCityCode, 'No entries found!', 2, 'StateCode=' + hdnStateUTCode, true);
        }

    }
}

function CheckValidInputData(control, caller) {
    if (control.value.trim() == '') {
        control.focus();
        alert('Please Enter ' + caller);
        return false;
    }
    return true;
}

function onSubmitClick() {
    try {
        if ($(rbEntrytype).find(":checked").val() == "0") {
            //if (!CheckValidInputData(document.getElementById(ddlGSTState.attr("id")), ' GST Billing State Name', true)) { return false; }
            //if (!CheckValidInputData(document.getElementById(txtGSTCity.attr("id")), ' GST Billing City Name.', true)) { return false; }
            //if (!CheckValidInputData(document.getElementById(txtGSTBillingAddresID), ' GST Billing Address.', true)) { return false; }
            //if (!CheckValidInputData(document.getElementById(txtCGSTNumberID), ' CGST Number.', true)) { return false; }
            //if (!CheckValidInputData(document.getElementById(txtIGSTNumberID), ' IGST Number.', true)) { return false; }

            var repeater = document.getElementById("tblOrderDetail");
            var inputs = repeater.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "text" && inputs[i].id.indexOf("txtStateUTName") > 0) {
                    if (!CheckValidInputData(inputs[i], ' State/UT Name.', true)) { return false; }
                }
                if (inputs[i].type == "text" && inputs[i].id.indexOf("txtAddress") > 0) {
                    if (!CheckValidInputData(inputs[i], ' Address', true)) { return false; }
                }
                else if (inputs[i].type == "text" && inputs[i].id.indexOf("txtCity") > 0) {
                    if (!CheckValidInputData(inputs[i], ' City', true)) { return false; }
                }
                else if (inputs[i].type == "text" && inputs[i].id.indexOf("txtLocation") > 0) {
                    if (!CheckValidInputData(inputs[i], ' Location', true)) { return false; }
                }
                    //else if (inputs[i].type == "text" && inputs[i].id.indexOf("txtProvisionalNumber") > 0) {
                    //    if ($(inputs[i]).val() == "" || $(inputs[i].id.replace('txtProvisionalNumber', 'txtGSTTiNumber')).val() == "") {
                    //        alert("Please enter any one value for Provisional Number or GSTIN Number.");
                    //        $(inputs[i]).focus();
                    //        return false;
                    //    }
                    //}
                    //commented on 04-jul-2017, to provide one textbox for both Provisional / GSTIN Number.
                    //else if (inputs[i].type == "text" && inputs[i].id.indexOf("txtProvisionalNumber") > 0) {
                    //    if (!CheckValidInputData(inputs[i], ' Provisional Number', true)) { return false; }
                    //}
                else if (inputs[i].type == "text" && inputs[i].id.indexOf("txtGSTTiNumber") > 0) {
                    if (!CheckValidInputData(inputs[i], ' Provisional / GSTTIN Number', true)) { return false; }
                    //added on 04-jul-2017, for validate 16 alphanumeric Provisional / GSTIN Number.
                    if ($(inputs[i]).val().trim() != "" && !IsValidGSTINNumber($(inputs[i]))) {
                        alert("Please enter valid alphanumeric Provisional / GSTIN Number (maximum 16 characters).");
                        $(inputs[i]).val("");
                        return false;
                    }
                }
            }

            if (confirm("Are you sure to save Company GST Registration details?")) {
                btnSubmit.hide();
                return true;
            }
            else {
                return false;
            }
        }
        return true;
    }
    catch (e) { alert(e.message); return false; }
}

function OnAddRow(id) {
    var CmdAddRows = $("#" + id);
    var txtStateUTName = $("#" + id.replace("CmdAddRows", "txtStateUTName"));
    var txtAddress = $("#" + id.replace("CmdAddRows", "txtAddress"));
    var txtCity = $("#" + id.replace("CmdAddRows", "txtCity"));
    //var txtProvisionalNumber = $("#" + id.replace("CmdAddRows", "txtProvisionalNumber"));//commented on 04-jul-2017, to provide one textbox for both Provisional / GSTIN Number.
    var txtGSTTiNumber = $("#" + id.replace("CmdAddRows", "txtGSTTiNumber"));

    if (txtStateUTName.val() == "") {
        alert("Please Enter the State/UT Name.");
        txtStateUTName.focus();
        return false;
    }
    if (txtAddress.val() == "") {
        alert("Please Enter the Address.");
        txtAddress.focus();
        return false;
    }
    if (txtCity.val() == "") {
        alert("Please Enter the City Name.");
        txtCity.focus();
        return false;
    }
    //commented on 04-jul-2017, to provide one textbox for both Provisional / GSTIN Number.
    //if (txtProvisionalNumber.val() == "") {
    //    alert("Please enter the Provisional Number.");
    //    txtProvisionalNumber.focus();
    //    return false;
    //}
    if (txtGSTTiNumber.val() == "") {
        alert("Please Enter Provisional / GSTIN Number.");
        txtGSTTiNumber.focus();
        return false;
    }
}

//function CheckCompanyId(e) {
//    try {
//        //setTimeout(function () {
//            var txtCompanyName = $("#" + $(e)[0].id);
//            var hdnCompanyCode = $("#" + $(e)[0].id.replace("txtCompanyName", "hdnCompanyCode"));
//            var hdnValid = $("#" + $(e)[0].id.replace("txtCompanyName", "hdnValid"));
//            var lblCutomerID = $("#" + $(e)[0].id.replace("txtCompanyName", "lblCutomerID"));

//            if (txtCompanyName.val() == "" || txtCompanyName.val().trim() == "") {
//                return false;
//            }
//            var param = { CompanyCode: hdnCompanyCode.val() };
//            $.ajax({
//                type: "POST",
//                url: "CompanyGSTRegistration.aspx/CheckCompanyCode",
//                data: JSON.stringify(param),
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                async: false,
//                success: function (response) {
//                    if (response != null && response.d != null && response.d != "") {
//                        var objArray = JSON.parse(response.d);
//                        if (objArray.length == 0) {
//                            var rtn = false;
//                            //if (confirm('Company does not exist in master.\n Do you want to add in master?')) {
//                            //    hdnValid.val("1");
//                            //    rtn = false;
//                            //} else {
//                            //    txtCompanyName.val('');
//                            //    hdnValid.val("0");
//                            //    rtn = false;
//                            //}
//                            hdnValid.val("0");
//                            return rtn;
//                        }
//                        else {
//                            var obj = objArray[0];
//                            if (obj.ACTIVEFLAG != 'Y') {
//                                alert("Inactive Company is Selected !")
//                                txtCompanyName.val('');
//                                hdnValid.val("0");
//                                return false;
//                            } else {
//                                hdnValid.val("1");
//                            }
//                        }
//                    }
//                }
//            });

//            return true;
//        //}, 200);
//    } catch (e) {
//        alert(e.message);
//    }
//}

function onNextClick(e) {
    var hdnValid = $("#" + $(e)[0].id.replace("btnSubmitCriteria", "hdnValid"));
    var txtCompanyName = $("#" + $(e)[0].id.replace("btnSubmitCriteria", "txtCompanyName"));
    if (txtCompanyName.val() == '') {
        alert("Please Enter the Company !")
        txtCompanyName.val('');
        return false;
    } else {
        if (hdnValid.val() == '1') {
            tdCompanyHeaderDetail.show();
            tdCompanyDetail.show();
            Table1Submit.show();
            //tdCompanyCriteria.prop("disabled", true);
        }
    }
    return true;
}

function onDdlBillingStateSelection_Change(e) {
    try {
        hdnGSTState.val($(e).val());
        txtGSTCity.val("");
        hdnGSTCity.val("");
        return true;
    }
    catch (e) { alert(e.message); return false; }
}