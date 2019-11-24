var ShortLeaveFormController = "ShortLeaveForm";
$(document).ready(function () {
    getInitialData();
    $("#txtDateFrom").datepicker({
        dateFormat: 'dd-M-yy',
        onSelect: function (dateText, inst) {
            var selectedDate = new Date(dateText);
            $('#txtDateTo').datepicker('option', 'minDate', selectedDate);
        }
    });

    $("#txtApplicationDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
    $(".datePick").datepicker({ dateFormat: 'dd-M-yy' });
});
function getInitialData() {
    var urlpath = base + ShortLeaveFormController + "/GetInitialData";
    $.ajax({
        url: urlpath,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (results) {
            SetData(results);
        },
        error: function (data) {
        }
    });
}
function SetData(results) {
    $('.unitName').text(results.UnitEName);
    $('#unitAddress').text(results.UnitEAddress);
    $('.empId').text(results.EMP_CODE);
    $('.Section').text(results.EmpSection);
    $('#empName').text(results.EmpName);
    $('#empDesignation').text(results.Empdesignation);

    $('.DOJ').text(results.DOJ);
    $('.Department').text(results.DeptEName);
    for (var i = 0; i < results.lstLeaveStatus.length; i++) {
        switch (results.lstLeaveStatus[i].LeaveTypeNo) {
            case 1:
                $('#chkCL').attr("data-Avail",results.lstLeaveStatus[i].Avail);
                $('#chkCL').attr("data-MaxBalance",results.lstLeaveStatus[i].MaxBalance);
                break;
            case 2:
                $('#chkSL').attr("data-Avail", results.lstLeaveStatus[i].Avail);
                $('#chkSL').attr("data-MaxBalance", results.lstLeaveStatus[i].MaxBalance);
                break;
            case 3:
                $('#chkEL').attr("data-Avail", results.lstLeaveStatus[i].Avail);
                $('#chkEL').attr("data-MaxBalance", results.lstLeaveStatus[i].MaxBalance);
                break;
            case 4:
                $('#chkLWP').attr("data-Avail", results.lstLeaveStatus[i].Avail);
                $('#chkLWP').attr("data-MaxBalance", results.lstLeaveStatus[i].MaxBalance);
                break;
            case 5:
                $('#chkML').attr("data-Avail", results.lstLeaveStatus[i].Avail);
                $('#chkML').attr("data-MaxBalance", results.lstLeaveStatus[i].MaxBalance);
                break;
            default:

        }
    }
    //$('#empDesignation').text(results.Empdesignation);
    //$('#empDesignation').text(results.Empdesignation);
    //$('#empDesignation').text(results.Empdesignation);
    //$('#empDesignation').text(results.Empdesignation);
    
}
// the selector will match all input controls of type :checkbox
// and attach a click event handler 
$("input:checkbox").on('click', function () {
    // in the handler, 'this' refers to the box clicked on
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
        let avail = $box.attr('data-Avail');
        let MaxBalance = $box.attr('data-MaxBalance');
        let alreadyEnjoyed = parseInt(MaxBalance) - parseInt(avail);
        $('#tbltdTotalEntile').text(MaxBalance);
        $('#tbltdAlreadyEnjoyed').text(avail);
        $('#tbltdBalance').text(alreadyEnjoyed);
    } else {
        $box.prop("checked", false);
        $('#tbltdTotalEntile').text('');
        $('#tbltdAlreadyEnjoyed').text('');
        $('#tbltdBalance').text('');
    }
});

$('#txtDateTo').change(function () {
    let leaveFrom = $('#txtDateFrom').val();
    if (leaveFrom == "") {
        $('#txtDateFrom').focus();
        $('#txtDateTo').val("");
        swal("Please Fillup Your Leave From!", "", "warning");
    }
    else {
        var start = new Date(leaveFrom)
        var end = new Date($(this).val());
        var diff = new Date(end - start);
        var days = diff / 1000 / 60 / 60 / 24;
        $('#totalDay').text(parseInt(days) + 1);
        $('#isAllowDays').text(parseInt(days) + 1);
        $('#isAllowFrom').text(leaveFrom);
        $('#isAllowTo').text($(this).val());
        
        
    }

});

function getObject() {
    let chkLeave = $('#chkCL').is(':Checked') ? "CL" : $('#chkSL').is(':Checked') ? "SL" : $('#chkEL').is(':Checked') ? "EL" : $('#chkML').is(':Checked') ? "ML" : $('#chkLWP').is(':Checked') ? "LWP" : "0";
    let lstLeaveStatus = {
        "MaxBalance": $("#tbltdTotalEntile").text() == "" ? 0 : $("#tbltdTotalEntile").text(),
        "Avail": $("#tbltdAlreadyEnjoyed").text() == "" ? 0 : $("#tbltdAlreadyEnjoyed").text()//$("#tbltdAlreadyEnjoyed").text()
    }
    var objleave = new Array();
    objleave.push(lstLeaveStatus);
    var obj = {
        "OperationName": "rptLeaveApplicationFrom",
        "EMP_CODE": $("#empId").text(),
        "EmpName": $("#empName").text(),
        "Empdesignation": $("#empDesignation").text(),
        "DeptEName": $("#empDepartment").text(),
        "EmpSection": $("#empSection").text(),
        "DOJ": $("#empDOJ").text(),
        "TimeFrom": $("#txtDateFrom").val(),
        "TimeTo": $("#txtDateTo").val(),
        "TotalDays": $("#totalDay").text(),
        "ApplicationDate": $("#txtApplicationDate").val(),
        "LeaveType":chkLeave,
        "Reason": $("#txtLeaveReason").val(),
        "AddressDuringLeave": $("#txtLeaveDuringLeave").val(),
        "JoinAfterLeave": $("#txtJoinAfterLeave").val(),
        "UnitEName": $("#unitName").text(),
        "lstLeaveStatus": objleave
    }
    return obj;
}
function ValidationData() {
    let valid = true;
    //let chkLeave = $('#chkCL').is(':Checked') ? "CL" : $('#chkSL').is(':Checked') ? "SL" : $('#chkEL').is(':Checked') ? "EL" : $('#chkML').is(':Checked') ? "ML" : $('#chkLWP').is(':Checked') ? "LWP" : "";
    //if (chkLeave == "") {
    //    swal("Please Fillup Your Leave Type!", "", "warning");
    //    valid = false;
    //    return valid;
    //}
    return valid;
}
$(document).off('click', '#btnPrint').on('click', '#btnPrint', function () {
    if (ValidationData() == true) {
        var Obj = JSON.stringify(getObject());
        let reportType = 'pdf';
        var url = base + ShortLeaveFormController + "/OpenFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
        var win = window.open(url, '_blank');
        win.focus();
        win.print();
        //window.print(win);
    }
});
$(document).off('click', '#btnDownload').on('click', '#btnDownload', function () {
    var Obj = JSON.stringify(getObject());
    let reportType = 'pdf';
    var url = base + ShortLeaveFormController + "/DownloadFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
    window.open(url, '_blank');
    //window.print(url, '_blank');
});