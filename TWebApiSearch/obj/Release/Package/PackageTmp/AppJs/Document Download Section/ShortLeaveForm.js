var CurrentController = "ShortLeaveForm";
$(document).ready(function () {
    getInitialData();
    $("#txtDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
    $("#EmployeeCopytxtDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
});
function getInitialData() {
    var urlpath = base + CurrentController + "/GetInitialData";
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
    $('#unitName').text(results.UnitEName);
    $('#unitAddress').text(results.UnitEAddress);
    $('#empId').text(results.EMP_CODE);
    $('#empSection').text(results.EmpSection);
    $('#empName').text(results.EmpName);
    $('#empDesignation').text(results.Empdesignation);
    ///////////Employee Copy///////////
    $('#EmployeeCopyunitName').text(results.UnitEName);
    $('#EmployeeCopyunitAddress').text(results.UnitEAddress);
    $('#EmployeeCopyempId').text(results.EMP_CODE);
    $('#EmployeeCopyempSection').text(results.EmpSection);
    $('#EmployeeCopyempName').text(results.EmpName);
    $('#EmployeeCopyempDesignation').text(results.Empdesignation);
}
$("#txtDate").change(function () {
    $("#EmployeeCopytxtDate").val($("#txtDate").val());
});
$("#txtTimeFrom").keyup(function () {
    $("#EmployeeCopytxtTimeFrom").val($("#txtTimeFrom").val());
});
$("#txtTimeTo").keyup(function () {
    $("#EmployeeCopytxtTimeTo").val($("#txtTimeTo").val());
});
$("#txtReason").keyup(function () {
    $("#EmployeeCopytxtReason").val($("#txtReason").val());
});
$("#txtConcernPersonDesignation").keyup(function () {
    $("#EmployeeCopytxtConcernPersonDesignation").val($("#txtConcernPersonDesignation").val());
});

function getObject() {
    var obj = {
        "OperationName": "rptHourlyLeave",
        "EMP_CODE": $("#empId").text(),
        "EmpName": $("#empName").text(),
        "Empdesignation": $("#empDesignation").text(),
        "EmpSection": $("#empSection").text(),
        "ConcernPersonnameDesig": $("#txtConcernPersonDesignation").val(),
        "TimeFrom": $("#txtTimeFrom").val(),
        "TimeTo": $("#txtTimeTo").val(),
        "Date": $("#txtDate").val(),
        "Reason": $("#txtReason").val(),
        "UnitEAddress": $("#unitAddress").text(),
        "UnitEName": $("#unitName").text()
    }
    return obj;
}
$(document).off('click', '#btnPrint').on('click', '#btnPrint', function () {
    var Obj = JSON.stringify(getObject());
    let reportType = 'pdf';
    var url = base + CurrentController + "/OpenFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
    var win = window.open(url, '_blank');
    win.focus();
    win.print();

});
$(document).off('click', '#btnDownload').on('click', '#btnDownload', function () {
    var Obj = JSON.stringify(getObject());
    let reportType = 'pdf';
    var url = base + CurrentController + "/DownloadFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
    window.open(url, '_blank');
});