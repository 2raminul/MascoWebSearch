var CurrentController = "BonusPayslip";
$(document).ready(function () {
    //$(".select2").select2();
    getFinalcialYear();
    //
});

function getFinalcialYear() {
    var urlpath = base + CurrentController + "/GetFinalCialYear";
    $.ajax({        
        url: urlpath,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (results) {
            LoadDropdown(results, $('#ddlFinalcialYear'));
            let finalcialYear = $('#ddlFinalcialYear option:selected').val();
            $('#ddlFinalcialYear').val(finalcialYear).trigger('change');
        },
        error: function (data) {

        }
    })
}
$('#ddlFinalcialYear').change(function () {
    let selectedValue = $(this).val();
    if (selectedValue != "-1") {
        getMonth(selectedValue);
    }
    else {
        $('#ddlMonth').get(0).options.length = 0;
    }
});
$('#ddlMonth').change(function () {
    $('#ddlBonusType').get(0).options.length = 0;
    let selectedValue = $(this).val();
    var content = '';
    if (selectedValue == "1") {
        content += '<option  value="1" >Eid-Ul-Fitr</option>';
    }
    else if (selectedValue == "2") {
        content += '<option  value="2" >Eid-Ul-Azha</option>';
    }
    
    $('#ddlBonusType').append(content);
});

function getMonth(selectedValue) {
    var urlpath = base + CurrentController + "/GetMonth";
    $.ajax({
        url: urlpath,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ "FinYear": selectedValue }),
        contentType: "application/json; charset=utf-8",
        success: function(results) {
            LoadDropdown(results, $('#ddlMonth'));
        },
        error: function(data) {

        }
    });
}
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

function getObject() {
    var obj = {
        "FinYear": $('#ddlFinalcialYear option:selected').val(),
        "FromMonth": $('#ddlMonth option:selected').text(),
        "BonusTypeNo": $('#ddlBonusType option:selected').val()
    }
    return obj;
}
function ValidationData() {
    let valid = true;
    if ($('#ddlFinalcialYear option:selected').val() == "-1") {
        swal("Please Fillup Your Financial Year!", "", "warning");
        valid = false;
        return valid;
    }
    if ($('#ddlMonth option:selected').val() == "-1") {
        swal("Please Fillup Your Financial Month!", "", "warning");
        valid = false;
        return valid;
    }
    return valid;
}
$(document).off('click', '#btnSendMail').on('click', '#btnSendMail', function () {
    if (ValidationData() == true) {
        var urlpath = base + CurrentController + "/SendMail";
        var data = JSON.stringify(getObject());
        $.ajax({
            url: urlpath,
            type: "POST",
            dataType: "json",
            data: data,
            contentType: "application/json; charset=utf-8",
            success: function (results) {
                if (results == "Send") {
                    swal("Good job!", "You Send Salary Pay Slip!", "success")
                }
                else {
                    swal("Something Wrong!", "", "warning");
                }
            },
            error: function (data) {
            }
        });
    }
});