var myController = "PromotedEmployee";
$(document).ready(function () {
    getFinalcialYear();
    GetAll();
});


function LoadMonth(FinancialYear) {
    var urlpath = "../" + myController + "/GetFinalCialMonth";
    if (FinancialYear > 0) {
            $.ajax({
                url: urlpath,
                type: "POST",
                dataType: "json",
                data: JSON.stringify({ "FinancialYear": FinancialYear }),
                contentType: "application/json; charset=utf-8",
                success: function (results) {
                    var content = '<option  value="-1">- Select Month -</option>';
                    $.each(results.Data, function (i, obj) {
                        content += '<option  value="' + obj.ID + '" >' + obj.Text + '</option>';
                       });
                    $("#ddlFinalcialMonth").append(content);
                    $("#ddlFinalcialMonth").val('-1');
                },
                error: function (data) {

                }
            });
        }
    }
    function getFinalcialYear() {
        $.ajax({
            url: '../MascoSearch/GetFinalCialYear',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (results) {
                $('#ddlFinalcialYear').get(0).options.length = 0;
                //  $('#ddlFinalcialYear').get(0).options[0] = new Option('Select--------', '');
                $.map(results.Data, function (item) {
                    $('#ddlFinalcialYear').get(0).options[$('#ddlFinalcialYear').get(0).options.length] = new Option(item.Text, item.ID);
                });

                $('#ddlFinalcialYear').val(results.SelectedVal);
                //let finalcialYear = $('#ddlFinalcialYear').val();

                LoadMonth(7);
            },
            error: function (data) {

            }
        });
    }

    function GetAll() {
        var urlpath = "../" + myController + "/GetAllUnit";
        //if (FinancialYear > 0) {
            $.ajax({
                url: urlpath,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (results) {
                    var content = '<option  value="-1">- Select Unit -</option>';
                    $.each(results.Data, function (i, obj) {
                        content += '<option  value="' + obj.ID + '" >' + obj.Text + '</option>';
                    });
                    $("#ddlUnitName").append(content);
                    $("#ddlUnitName").val('-1');

                    $("#ddlUnitName").select2();
                },
                error: function (data) {

                }
            });
        //}
    }

    $(document).on("change", ".promoted", function (parameters) {
    var year = $("#ddlFinalcialYear").val();
    var month = $("#ddlFinalcialMonth").val();
    var category = $("#ddlCategory").val();
    var unit = $("#ddlUnitName").val();

    if (year !="" && category !="" && unit !="-1") {
        GetAllPromotedData(year, month, category, unit);
    }

});

function GetAllPromotedData(year, month, category, unit) {
    var urlpath = "../" + myController + "/GetPromotedData";
    if (year>0 && category>0 && unit>0) {
        $.ajax({
            url: urlpath,
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ "year": year, "month": month, "category": category, "unit": unit,"Tag":"PI" }),
            contentType: "application/json; charset=utf-8",
            success: function (results) {
                if (results.Data !=null || results.Data !="") {
                    BindToTable(results.Data);
                }
            },
            error: function (data) {

            }
        });
    }
}

function BindToTable(Result) {
    var content = "";
    $.each(Result, function (i, obj) {
        content += "<tr>";
        content += "<td>" + (i + 1) + "</td>";
        content += "<td>" + obj.UnitName + "</td>";
        content += "<td>" + obj.EmpName + "</td>";
        content += "<td>" + obj.Department + "</td>";
        content += "<td>" + obj.Section + "</td>";
        content += "<td>" + obj.PreviousDesignation + "</td>";
        content += "<td>" + obj.PromotedDesignation + "</td>";
        content += "<td><img style='border-radius: 50%;width: 100px;' src='data:image/jpeg;base64," + obj.img + "' /></td>";
        content += "</tr>";
    });
    $("#gvPromotedEmployee tbody").empty();
    $("#gvPromotedEmployee tbody").append(content);
}