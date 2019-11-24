var ShortLeaveFormController = "ShortLeaveForm";
$(document).ready(function () {
    getInitialData();
    $("#txtDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
    $("#EmployeeCopytxtDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
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
    $('#unitName').text(results.UnitEName);
    $('#unitAddress').text(results.UnitEAddress);
    $('#empId').text(results.EMP_CODE);
    $('#empSection').text(results.EmpSection);
    $('#empName').text(results.EmpName);
    $('#empDesignation').text(results.Empdesignation);
}

function getObject() {
    var obj = {
        "OperationName": "rptOutStationDuty",
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
        "UnitEName": $("#unitName").text(),
        "OutstationPlaceName": $("#dutyPlace1").val() + " " + $("#dutyPlace2").val()
    }
    return obj;
}
$(document).off('click', '#btnPrint').on('click', '#btnPrint', function () {

    var Obj = JSON.stringify(getObject());
    let reportType = 'pdf';
    var url = base + ShortLeaveFormController + "/OpenFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
    var win = window.open(url, '_blank');
    win.focus();
    win.print();

});
$(document).off('click', '#btnDownload').on('click', '#btnDownload', function () {
    var Obj = JSON.stringify(getObject());
    let reportType = 'pdf';
    var url = base + ShortLeaveFormController + "/DownloadFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
    window.open(url, '_blank');
});