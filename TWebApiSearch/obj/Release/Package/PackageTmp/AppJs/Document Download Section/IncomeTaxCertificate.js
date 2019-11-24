var CurrentController = "IncomeTaxCertificate";
$(document).ready(function () {
    getTaxYear();
    //$(".select2").select2();
});

function getTaxYear() {
    var urlpath = base + CurrentController + "/GetTaxYear";
    $.ajax({
        url: urlpath,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (results) {
            LoadDropdown(results, $('#ddlTax'));
        },
        error: function (data) {
            console.log(data);
        }
    })
}
function LoadDropdown(result, id) {
    $(id).get(0).options.length = 0;
    var content = '<option  value="-1">-- Select --</option>';
    $.each(result, function (i, obj) {
        content += '<option data-FromMonth="' + obj.Value1 + '" data-ToMonth="' + obj.Value2 + '" data-AYear="' + obj.Value3 + '" value="' + obj.Value + '" >' + obj.DisplayName + '</option>';
        });
    id.append(content);
    if (result.length == 1) {
        id.val(result[0].Value).trigger('change');
    }
    //$(id).select2();
}
function getObject() {
    var obj = {
        "OperationName": $('input[name=taxStatus]:checked').val() == "tax" ? "rptCostCenterWiseIncomeTaxYearly" : "rptCostCenterWiseIncomeNonTaxYearly",
        "TaxFinYearNo": $('#ddlTax option:selected').val(),
        "TaxStatus": $('input[name=taxStatus]:checked').val(),
        "FromMonth": $('#ddlTax option:selected').attr('data-FromMonth'),
        "ToMonth": $('#ddlTax option:selected').attr('data-ToMonth'),
        "AYear": $('#ddlTax option:selected').attr('data-AYear'),
        "IncomeTaxYear": $('#ddlTax option:selected').text()
    }
    return obj;
}
function ValidationData() {
    let valid = true;   
    if ($('#ddlTax option:selected').val() == "-1") {
        swal("Please Fillup Your Tax Year!", "", "warning");
        valid = false;
        return valid;
    }
    return valid;
}
$(document).off('click', '#btnPrint').on('click', '#btnPrint', function () {
    if (ValidationData() == true) {
        var Obj = JSON.stringify(getObject());
        let reportType = 'pdf';
        var url = base + CurrentController + "/OpenFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
        var win = window.open(url, '_blank');
        win.focus();
        win.print();
    }
});
$(document).off('click', '#btnDownload').on('click', '#btnDownload', function () {
    if (ValidationData() == true) {
        var Obj = JSON.stringify(getObject());
        let reportType = 'pdf';
        var url = base + CurrentController + "/DownloadFile?Param=" + encodeURIComponent(Obj) + "&ReportType=" + reportType;
        window.open(url, '_blank');
    }
});