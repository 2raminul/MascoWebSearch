var myController = "/MascoSearch";
$(document).ready(function () {
    getFinalcialYear();
    //$("#ddlMonth").select2();
});
function GetOutStationDuty(finalcialYear, Month) {
   
    var urlpath = "../" + myController + "/GetOutStationDuty";
    $.ajax({
        url: urlpath,
        type: "POST",
        data: JSON.stringify({ "finalcialYear": finalcialYear, "Month": Month }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (results) {
            $("#gvHolidayHistory tbody").empty();
            var ddd = JSON.parse(results);
            // var HolDayBill = 0;
           
            for (var i = 0; i < ddd.length; i++) {
                let tr = "<tr>";
                tr += "<td>" + (i + 1) + "</td>";
                tr += "<td>" + ddd[i].FromDate + "</td>";
                tr += "<td>" + ddd[i].ToDate + "</td>";
                tr += "<td>" + ddd[i].NumberOfDays + "</td>";
                tr += "<td>" + ddd[i].Remarks + "</td>";
                tr += "</tr>";

                // HolDayBill += parseInt(ddd[i].HolDayAmt);
                $("#gvHolidayHistory tbody").append(tr);
            }

            //var tRow = $("#gvHolidayHistory tfoot tr");
            //$(tRow).find('td:eq(1)').text("Total");
            //$(tRow).find('td:eq(5)').text(HolDayBill);
        },
        error: function (data) {

        }
    });
}
function getFinalcialYear() {
    var urlpath = "../" + myController + "/GetFinalCialYear";
    $.ajax({
        url: urlpath,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (results) {
            $('#ddlFinalcialYear').get(0).options.length = 0;
            $('#ddlFinalcialYear').get(0).options[0] = new Option('Select--------', '');

            $.map(results.Data, function (item) {
                $('#ddlFinalcialYear').get(0).options[$('#ddlFinalcialYear').get(0).options.length] = new Option(item.Text, item.ID);
            });

            $('#ddlFinalcialYear').val(results.SelectedVal);
            let finalcialYear = $('#ddlFinalcialYear').val();

            if ($("#ddlMonth").val() == '-1') {
                GetOutStationDuty(finalcialYear, '');
            } else {
                GetOutStationDuty(finalcialYear, $("#ddlMonth").val());
            }
            //$('#ddlFinalcialYear').select2();
        },
        error: function (data) {

        }
    });

    $(document).on('change', '#ddlFinalcialYear', function () {
       
        let finalcialYear = $(this).val();
        if (finalcialYear == "") { $("#gvHolidayHistory tbody").empty(); return false; } else {
            if ($("#ddlMonth").val() == '-1') {
                GetOutStationDuty(finalcialYear, '');
            } else {
                GetOutStationDuty(finalcialYear, $("#ddlMonth").val());
            }
        }

    });
    $(document).on('change', '#ddlMonth', function () {
        let finalcialYear = $("#ddlFinalcialYear").val();
        if (finalcialYear == "") { $("#gvHolidayHistory tbody").empty(); return false; } else {
            if ($("#ddlMonth").val() == '-1') {
                GetOutStationDuty(finalcialYear, '');
            } else {
                GetOutStationDuty(finalcialYear, $("#ddlMonth").val());
            }
        }

    });
}