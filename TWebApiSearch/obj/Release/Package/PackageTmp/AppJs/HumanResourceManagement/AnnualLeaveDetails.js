var BonusPayslipController = "BonusPayslip";
var CurrentController = "MascoSearch";
$(document).ready(function () {
    getFinalcialYear();
});

function getFinalcialYear() {
    var urlpath = base + BonusPayslipController + "/GetFinalCialYear";
    $.ajax({
        url: urlpath,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(results) {
            LoadDropdown(results, $('#ddlFinalcialYear'));
            $('#ddlFinalcialYear').val(5).trigger('change');
        },
        error: function(data) {

        }
    });
}
$('#ddlFinalcialYear').change(function () {
    let selectedValue = $(this).val();
    if (selectedValue != "-1") {
        GetAnnualLeaveDataByFinYear(selectedValue);
    }
    else {
        $('#tblAnnualLeaveDetails tbody').empty();
    }
});
function LoadDropdown(result, id) {
    $(id).get(0).options.length = 0;
    var content = '<option  value="-1">-- Select --</option>';
    $.each(result, function (i, obj) {
        if (obj.IsSelected == "True") {
            content += '<option  value="' + obj.Value + '" selected>' + obj.DisplayName + '</option>';
        }
        else {
            content += '<option  value="' + obj.Value + '" >' + obj.DisplayName + '</option>';
        }

    });
    id.append(content);
    if (result.length == 1) {
        id.val(result[0].Value).trigger('change');
    }
    //$(id).select2();
}

function GetAnnualLeaveDataByFinYear(finalcialYear) {
    if (ValidationData() == true) {
        var urlpath = base + CurrentController + "/GetAnnualLeaveDataByFinYear";
        var data = JSON.stringify({ "finalcialYear": parseInt(finalcialYear) });
        $.ajax({
            url: urlpath,
            type: "POST",
            dataType: "json",
            data: data,
            contentType: "application/json; charset=utf-8",
            success: function(results) {
                debugger;
                SetData(JSON.parse(results));
                //if (results == "Send") {
                //    swal("Good job!", "You Send Salary Pay Slip!", "success")
                //}
                //else {
                //    swal("Something Wrong!", "", "warning");
                //}
            },
            error: function (data) {
            }
        });
    }
}

function ValidationData() {
    let valid = true;
    if ($('#ddlFinalcialYear option:selected').val() == "-1") {
        swal("Please Fillup Your Financial Year!", "", "warning");
        valid = false;
        return valid;
    }
    return valid;
}

function SetData(result) {
    let content = '';
    $.each(result, function(index, obj) {
        content += '<tr>' +
            '<td>' + obj.NumberofLeaveDays + '</td>' +
            '<td>' + obj.PerDayAmount + '</td>' +
            '<td>' + obj.TotalAmount + '</td>' +
            '</tr>';

    });
    $('#tblAnnualLeaveDetails tbody').empty();
    $('#tblAnnualLeaveDetails tbody').append(content);
}