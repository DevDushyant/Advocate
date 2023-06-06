var tblId = 0;
$(document).ready(function () {
    $("#gazzetedate").datepicker();
    BindBook('ddlBook');
    BindYear('ddlBookActYear');
    BindVolume('ddlBookVolume');
    $("#ddlTallyData").select2({
        placeholder: "Please select tally type",
        allowClear: true
    });
    $("#ddlLagislative").select2({
        placeholder: "Please select Lagislative",
        allowClear: true
    });

    $('input[type=radio][name=date_datetype]').change(function () {
        BindTallyData();
    });

    if ($('input[name="date_datetype"]:checked').val() === "gazzetedate") {
        $('#ddlTallyData').change(function () { BindTallyData(); });

    }

    $('#btnSave').click(function () {
        SaveBookEntryDetail();
    });
});

SaveBookEntryDetail = function () {
    var inputparm =
    {
        BookId: $("#ddlBook").val(),
        BookYear: $("#ddlBookActYear").val(),
        BookVolume: $("#ddlBookVolume").val(),
        BookPageNo: $("#book_pagenumber").val(),
        BookSerialNo: $("#book_serial_number").val(),
        GazetteDate: $("#gazzetedate").val().split("/").reverse().join("-"),
        TallyType: $('#ddlTallyData').val(),
        DateType: $('input[name="date_datetype"]:checked').val(),
        TypeId: $("#ddlSelectedType").val(),
        LegislativeNature: $("#ddlLagislative").val(),
        Id: tblId
    }
    var url;
    if (tblId === 0)
        url = "/Book/SaveBookEntryDetail";
    else
        url = "/Book/EditBookEntryDetail";

    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: url,
        data: { bookentryviewmodel: inputparm },
        success: function (responce) {
            $('#loader').hide();
            if (responce.data > 0 && responce.statuscode === 200) {
                Swal.fire(
                    'Good job!',
                    'You clicked the button!',
                    'success'
                ).then(function () {
                    ResetForm();
                });               
            }
            else
                Swal.fire(
                    'Good job!',
                    'You clicked the button!',
                    'error'
                )
        }

    });
}
ResetForm = function () {
    //$("#ddlBook").val();
    //$("#ddlBookActYear").val();
    $("#ddlBookVolume").val(null).trigger('change');;
    $("#book_pagenumber").val('');
    $("#book_serial_number").val('');
    $("#gazzetedate").val('');
    $('#ddlTallyData').val(null);
    $("#ddlSelectedType").val(null).trigger('change');;
    $("#ddlLagislative").val(null).trigger('change');;
}

BindTallyData = function () {
    var tallytype = $('#ddlTallyData').val();
    var selectedDateType = $('input[name="date_datetype"]:checked').val();
    var selectedDate = $("#gazzetedate").val().split("/").reverse().join("-");
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-tallydata/" + tallytype + "/" + selectedDateType + "/" + selectedDate + "",
        dataType: "json",
        success: function (responce) {
            $("#ddlSelectedType").empty();
            $("#ddlSelectedType").append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#ddlSelectedType").append($("<option></option>").val(value.value).html(value.text));
            });
            $("#ddlSelectedType").select2({
                placeholder: "Please select " + tallytype,
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}