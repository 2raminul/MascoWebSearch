function formatErrorMessage(jqXHR, exception) {

    if (jqXHR.status === 0) {
        return ('Not connected.\nPlease verify your network connection.[Error Code=0]');
    }
    else if (jqXHR.status == 400) {
        return ("Server understood the request but request content was invalid.[Error Code=400]");
    }
    else if (jqXHR.status == 401) {
        return ('Unauthorised access. [Error Code=401].');
    }
    else if (jqXHR.status == 403) {
        return ("Forbidden resouce can't be accessed.[Error Code=403]");
    }
    else if (jqXHR.status == 404) {
        return ('The requested page not found. [Error Code=404]');
    } else if (jqXHR.status == 500) {
        return ('Internal Server Error [Error Code=500].');

    }
    else if (jqXHR.status == 503) {
        return ('Service Unavailable. [Error Code=503]');
    }

    else if (exception === 'parsererror') {
        return ('Requested JSON parse failed.');
    } else if (exception === 'timeout') {
        return ('Time out error.');
    } else if (exception === 'abort') {
        return ('Ajax request aborted.');
    } else {
        return ('Uncaught Error.\n' + jqXHR.responseText);
    }
}
//*************8dialouge box
function ShowCustomDialog(msg, title) {
    $("<div>" + msg + '</div>').dialog({
        resizable: true,
        modal: true,
        title: title,
        height: 200,
        width: 350,
        buttons: {
            "OK": function () {
                $(this).dialog('destroy').remove();
            },
            "Close": function () { $(this).dialog('destroy').remove(); }
        }
    });

}

$().ready(function() {

    $('#formSignUp').validate({
        rules: {
            unitId: {
                required: $("#ddlUnit option:selected").val() > 0
            },
            EmpId: {
                required: "#ddlEmpId option:selected" > 0
            },
            Email: {
                required: true, 
                email:true
            },
            PassWord: "required",
            confirm_password: {
                required: true,
                equalTo: "#txtPass"
}
        },
        messages: {
            unitId: "Please Select Unit Name",
            EmpId: "Please Select Employee ID",
            Email: {
                required: "Please give your email" ,
                email:"Please provide a valid email address"
            },
            PassWord: "Please provide a password",
            confirm_password: {
                required:"Confirm the given Password",
                equalTo: "Password doesn\'t match"
            }
        }
    });
});

//*************Functions*****
function loadUnit(url) {
    $.ajax({
        type: "Post",
        contentType: 'application/json;charset=utf-8',
        data: {},
        url: url,
        dataType: 'json',
        success: function (data) {
       
            var content = '<option value="-1">Select Unit Name</option>';
           
            $.each(data,
                function (i, unit) {
                    content += '<option value="' + unit.Value + '">' + unit.DisplayName + '</option>';
                });
            $('#ddlUnit option').remove();
            $('#ddlUnit').append(content);
            $('#ddlUnit').select2();
        },
        error: function (a, b, c) {
            ShowCustomDialog(formatErrorMessage(a, c), "Unit Name Load Problem");
        }
    });
}
function loadEmpId(url,unitNo) {
    $.ajax({
        url: url,
        data: JSON.stringify({ "unitNo": unitNo }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //console.log(data);
            var content = '<option value="-1">Select Employee ID</option>';

            $.each(data,
                function (i, unit) {
                    content += '<option value="' + unit.Value + '">' + unit.DisplayName + '</option>';
                });
            $('#ddlEmpId option').remove();
            $('#ddlEmpId').append(content);
            $('#ddlEmpId').select2();
        },
        error: function (a, b, c) {
            ShowCustomDialog(formatErrorMessage(a, c), "Employee ID Load Problem");
        }
    });
}
function loadEmpInfo(url, empId) {
    $.ajax({
        url: url,
        data: JSON.stringify({ "empId": empId }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$('#txtEmpName').removeAttr('placeholder');
            $('#txtEmpName').val(data[0].EmpName);
            $('#txtEmail').val(data[0].OfficeMail);
            $('#txtOfcCell').val(data[0].OfficeCell);
        },
        error: function (a, b, c) {
            ShowCustomDialog(formatErrorMessage(a, c), "Employee ID Load Problem");
        }
    });
}
function saveUser(url, user) {
    //console.log(user);
    $.ajax({
        url: url,
        data: JSON.stringify({ "user": user }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            if (data) {
                window.location.href = "/LogIn/LogIn";
            } else {
                alert("User can not created");
            }
        },
        error: function (a, b, c) {
            ShowCustomDialog(formatErrorMessage(a, c), "Some problem occured");
        }
    });
}

