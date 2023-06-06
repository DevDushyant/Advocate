var clickedButton = ""; var ruleId = 0;;
var selecteactids = [];
var repealedactids = [];
var amendedactids = [];
var accesstype = "", actid;
var buttonType = "";
$(document).ready(function () {
    $('#lblFormHeading').text("Add");
    $("#notification_date").datepicker();
    $("#gazetteDate").datepicker();
    $("#comeinforseDate").datepicker();
    BindActType('ddlActType');
    BindNature('ddlNature');
    BindGazette('ddlPublishedIn');
    BindComeInforce('ddlCominforce');
    BindSubGazetteOrPart('ddlPart');
    BindBook('ddlBook');
    BindYear('ddlBookActYear');
    BindVolume('ddlBookVolume');
    BindRuleGSRSO('ddlGsrso');
    BindManageType('ddlRuleType');
    BindManageRuleSearchKind('ddlRuleKind');
    BindManageRuleSearchKind('ddlRuleKind_Repealed');
    BindNotificationType('ddlNotificationType');

    $('#ddlActType').on('change', function () {
        BindActbyActType('ddlAct', $('#ddlActType').val());
    });

    $('#ddlAct').on('change', function () {
        var ruleKind = "";
        if ($('#ddlRuleKind').val() !== "")
            ruleKind = $('#ddlRuleKind').val();
        else
            ruleKind = "NEW";
        BindRuleByAct('ddlRule', $('#ddlAct').val(), ruleKind);
    });

    $("#divExtraAct").hide();
    $('#ddlRuleKind').on('change', function () {
        BindRuleByAct('ddlRule', $('#ddlAct').val(), $('#ddlRuleKind').val());
    });

    $('#ddlRuleKind_Repealed').on('change', function () {
        BindRuleByAct('ddlRule_Repealed', $('#ddlAct').val(), $('#ddlRuleKind_Repealed').val());
    });

    $('#ddlCominforce').on('change', function () {
        if ($('#ddlCominforce').val() === "WEFPIG")
            $('#dtComeInforse').show();
        else
            $('#dtComeInforse').hide();
    });

    $("#ddlAct").select2({
        placeholder: "Please select act type",
        allowClear: true
    });

    $("#ddlExtraAct").select2({
        placeholder: "Please select act",
        allowClear: true
    });

    $("#ddlRule").select2({
        placeholder: "Please select rule",
        allowClear: true
    });

    $("#ddlRule_Repealed").select2({
        placeholder: "Please select rule",
        allowClear: true
    });

    $('#btnRepealed').click(function () {
        clickedButton = "Repealed";
        $(".acttypetable").each(function (value, index) {
            $('#chk' + value).prop('checked', false);
        });
        $("#acttypmodelpopup").modal("show");
    });

    $('#btnAmended').click(function () {
        clickedButton = "Amended";
        $(".acttypetable").each(function (value, index) {
            $('#chk' + value).prop('checked', false);
        });
        $("#acttypmodelpopup").modal("show");
    });

    var rowcount = 0;

    $('#btnAddBook').click(function () {
        rowcount = $('#tblActBook > tbody >tr').length + 1;
        BindActBookTable(rowcount);
    });

    $("#tblActBook").on("click", ".deleteIcon", function () {
        $(this).closest("tr").remove();
    });

    $('#chkExtraAct').click(function () {
        var isChecked = $(this);
        if (isChecked.is(':checked')) {
            BindActbyActType('ddlExtraAct', $('#ddlActType').val());
            $("#divExtraAct").show();
        }
        else
            $("#divExtraAct").hide();

    });

    //$('#btnFind').click(function () {
    //    clickedButton = "Edit";
    //    $('#lblFormHeading').text("Edit");
    //    $('#btnSave').val("Update");
    //    var selectedActType, selectedActId;
    //    if ($('#ddlActType').val() !== "undefined" || $('#ddlActType').val() !== "") {
    //        selectedActType = $('#ddlActType').val();
    //        selectedActId = $('#ddlAct').val();
    //    }
    //    else {
    //        selectedActType = "";
    //        selectedActId = "";
    //    }
    //    BindRuleList(selectedActType, selectedActId);
    //    $("#rulemodelpopup").modal("show");

    //});

    //$('#btnSelectRule').click(function () {
    //    $('#ruleList tbody tr').each(function () {
    //        if ($(this).find(".chkall").is(":checked")) {
    //            BindRuleFormById(parseInt($(this).find('input[type="hidden"]').val()));
    //            ruleId = parseInt($(this).find('input[type="hidden"]').val());
    //            //selecteactids.push(parseInt($(this).find('input[type="hidden"]').val()));
    //        }
    //    });
    //    $('#rulemodelpopup').modal('hide');
    //    //if (buttonType === "Copy")
    //    // BindFormDataByActId(selecteactids[0]);
    //});

    $('#btnRepeat').click(function () {
        clickedButton = "Save";
        $('#lblFormHeading').text("Add");
        $.ajax({
            beforeSend: function () {
                $("#loader").show()
            },
            type: "GET",
            url: "RepeatAction",
            dataType: 'json',
            success: function (data) {
                $("#loader").hide();
                BindFormControls(data);
            }
        });
    });

    $("#btnSave").click(function () {
        if (clickedButton === "Edit" || clickedButton === "Find")
            clickedButton = "Edit";
        else
            clickedButton = "Save";
        SaveOrUpdateRuleDetail();
    });

    $('#tblActTable tbody').on('click', 'input[type="checkbox"]', function (e) {
        if ($(this).context.checked) {
            var tblContent = "";
            var row = $(this);
            var selectedId = row.closest("tr").find('input[type="hidden"]').val();
            var selectedAct = row.closest("tr").find("td:nth-child(5)").text();
            if ($("#actList tbody tr:last").find("td:nth-child(1)").text() !== selectedId) {
                tblContent = "<tr style='border-bottom: 5px solid orange;'>";
                tblContent += "<td style='display:none;'>" + selectedId + "</td>";
                tblContent += "<td>" + selectedAct + "</td>";
                tblContent += "</tr>";
                $('#actList tbody').append(tblContent);
            }
        }
        else {
            var tblrow = $(this);
            var uncheckId = tblrow.closest("tr").find('input[type="hidden"]').val();
            var table = $("#actList tbody");
            table.find("tr").each(function (i) {
                var tds = $(this).find('td');
                if (tds.eq(0).text() === uncheckId) {
                    var rowindex = table.find("tr").eq(i).index();
                    table.find("tr").eq(i).remove();
                }
            });
        }
        e.stopPropagation();
    });

    $('#btnSelectAct').click(function () {
        $('.acttypetable tbody tr').each(function () {
            if ($(this).find(".chkall").is(":checked")) {
                if (clickedButton === "Repealed")
                    repealedactids.push(parseInt($(this).find('input[type="hidden"]').val()));
                else
                    amendedactids.push(parseInt($(this).find('input[type="hidden"]').val()));

                if (clickedButton === "Copy") {
                    BindFormDataByNotificationId(parseInt($(this).find('input[type="hidden"]').val()));
                }
                if (clickedButton === "Copy" || clickedButton === "Find") {
                    ruleId = parseInt($(this).find('input[type="hidden"]').val());
                    BindFormDataByNotificationId(parseInt($(this).find('input[type="hidden"]').val()));
                }

            }
        });
        $('#acttypmodelpopup').modal('hide');


    });

    $("#btnCopy").click(function () {
        clickedButton = "Copy";
        $(".acttypetable").each(function (value, index) {
            $('#chk' + value).prop('checked', false);
        });
        $("#acttypmodelpopup").modal("show");
    });
    $('#btnFind').click(function () {
        clickedButton = "Find";
        $('#lblFormHeading').text("Edit");
        $('#btnSave').val("Update");
        $(".acttypetable").each(function (value, index) {
            $('#chk' + value).prop('checked', false);
        });
        $("#acttypmodelpopup").modal("show");
    });

});
BindActBookTable = function (rowid) {
    if (rowid === "" || rowid === 'undefined')
        rowid = '';
    var markup = "<tr>" +
        "<td><select id='ddlBook" + rowid + "' name='rule_book' class='form-control'></td>" +
        "<td><select id='ddlBookActYear" + rowid + "' name='rule_book' class='form-control'></select></td>" +
        "<td><select id='ddlBookVolume" + rowid + "' name='rule_volume' class='form-control'></select></td>" +
        "<td><input type='text' id='book_pagenumber" + rowid + "' name='act_pagenumber_book' placeholder='Please enter page no.' class='form-control' /></td>" +
        "<td><input type='number'' id='book_serial_number" + rowid + "' name='book_serial_number' placeholder='Please enter serial no.' class='form-control' /></td>" +
        "<td><i value='Delete' type='button' class='deleteIcon fa fa-trash'></i></td>" +
        "</tr>";
    $("#tblActBook tbody").append(markup);
    BindBook('ddlBook' + rowid);
    BindYear('ddlBookActYear' + rowid);
    BindVolume('ddlBookVolume' + rowid);
}

BindRuleList = function (selectedActType, actid) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "GetRulesByActType",
        data: { actTypeId: selectedActType, act: actid },
        dataType: 'json',
        success: function (data) {
            $("#loader").hide();
            var table = $('#tblRule').DataTable();
            table.destroy();
            table = $('#tblRule').DataTable({
                lengthChange: false,
                buttons: true,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'excel', 'pdf'
                ],
                data: data,

                "columns": [
                    {
                        mRender: function (data, type, row) {
                            return "<input type='checkbox' id='chk" + row['id'] + "' class='chkall'/> <input type='hidden' id='hdnRuleId" + row['id'] + "' value='" + row['id'] + "'/>";
                        }
                    },
                    { data: "actType" },
                    { data: "ruleType" },
                    { data: "ruleNo" },
                    { data: "ruleDate" },
                    { data: "ruleName" }
                ],
                initComplete: function () {
                    this.api().columns().every(function (i) {
                        if (i === 1 || i === 2 || i === 3) {
                            var column = this;
                            $(column.header()).append("");
                            var Selid = "Sel_" + i;
                            $("#" + Selid).remove();
                            var select = $('<select id=' + Selid + '><option value="">All</option></select>')
                                .appendTo($(column.header()))
                                .on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex(
                                        $(this).val()
                                    );
                                    column.search(val ? '^' + val + '$' : '', true, false).draw();
                                });
                            column.data().unique().sort().each(function (d, j) {
                                select.append('<option value="' + d + '">' + d + '</option>')
                            });
                            //if (i === 1) {
                            //    $("#" + Selid).select2({
                            //        placeholder: "Select act type",
                            //        allowClear: true
                            //    });
                            //}
                            //if (i === 3) {
                            //    $("#" + Selid).select2({
                            //        placeholder: "Select year",
                            //        allowClear: true
                            //    });
                            //}
                        }
                    });
                }
            });
        }
    });
}

BindRuleFormById = function (selectedRuleId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "GetRuleDetailByRuleId",
        data: { ruleId: selectedRuleId },
        dataType: 'json',
        success: function (data) {
            $("#loader").hide();
            BindFormControls(data);
        }
    });
}

BindFormControls = function (data) {
    $('#ddlActType').val(data.actTypeId).trigger('change');
    $('#ddlAct').val(data.actId).trigger('change');
    $('#notificationNo').val(data.notificationNo);
    $('#ddlGsrso').val(data.gsrsO_Prefix).trigger('change');
    $('#gsrsonumber').val(data.gsrsO_No);
    $('#notification_date').val(ChangeFormateDate(data.notification_date.split('T')[0]));
    $('#underSectionRule').val(data.notifiction_SectionRule);
    $('#ddlRuleKind').val(data.notificationRuleKind).trigger('change');
    $('#ddlRule').val(data.notificationRuleId).trigger('change');
    $('#ddlPublishedIn').val(data.gazzetId).trigger('change');
    $('#ddlPart').val(data.partId).trigger('change');
    $('#ddlNature').val(data.nature).trigger('change');
    $('#gazetteDate').val(ChangeFormateDate(data.gazetteDate.split('T')[0]));
    $('#ddlNotificationType').val(data.notificationType).trigger('change');
    $('#pagenumber').val(data.pageNo);
    $('#ddlCominforce').val(data.comeInforce).trigger('change');
    $('#comeinforseDate').val(ChangeFormateDate(data.publishedInGazeteDate.split('T')[0]));
    $('#substance').val(data.substance);
    if (data.amendedNotification.length !== 0 && data.amendedNotification !== "") {
        var ruleIdArr = [];
        $.each(data.amendedNotification, function (k, val) {
            ruleIdArr.push(val.ruleId);
        });
        $("#ddlRuleKind").val('AMD').trigger('change');
        $('#ddlRule').val(ruleIdArr).trigger('change');
    }
    if (data.repealedNotification.length !== 0 && data.repealedNotification !== "") {
        var ruleIdRpd = [];
        $.each(data.repealedRuleList, function (k, val) {
            ruleIdRpd.push(val.ruleId);
        });
        $("#ddlRuleKind_Repealed").val('RPD').trigger('change');
        $('#ddlRule_Repealed').val(ruleIdRpd).trigger('change');
    }
    if (data.extraRulesAct !== 0 && data.extraRulesAct !== "") {
        var extAct = [];
        $.each(data.extraRulesAct, function (k, val) {
            extAct.push(val.actId);
        });
        $("#chkExtraAct").prop("checked", true);
        BindActbyActType('ddlExtraAct', $('#ddlActType').val());
        $("#divExtraAct").show();
        $('#ddlExtraAct').val(extAct).trigger('change');
    }

    if (data.notificationBooks !== null) {
        $.each(data.notificationBooks, function (index, value) {
            if (index === 0) {
                $('#ddlBook').val(value.bookId).trigger('change');
                $('#ddlBookActYear').val(value.bookYear).trigger('change');
                $('#ddlBookVolume').val(value.volume).trigger('change');
                $('#book_pagenumber').val(value.bookPageNo);
                $('#book_serial_number').val(value.bookSrNo);
            }
            else {
                rowid = index;
                BindActBookTable(rowid);
                $('#ddlBook' + rowid).val(value.bookId).trigger('change');
                $('#ddlBookActYear' + rowid).val(value.bookYear).trigger('change');
                $('#book_pagenumber' + rowid).val(value.bookPageNo);
                $('#book_serial_number' + rowid).val(value.bookSrNo);
                $('#ddlBookVolume' + rowid).val(value.volume).trigger('change');
            }
        })
    }
}

function ChangeFormateDate(oldDate) {
    return oldDate.toString().split("-").reverse().join("/");
}

SaveOrUpdateRuleDetail = function () {
    var rulebook = [];
    var url;
    if (clickedButton === "Save") {
        url = "/Notification/Create";
        ruleId = 0;
    }
    if (clickedButton === "Edit" || clickedButton === "Find") {
        url = "/Notification/Edit";
    }
    else {
        url = "/Notification/Create";
        ruleId = 0;
    }

    $('#tblActBook > tbody >tr').each(function () {
        var currentRow = $(this);
        var bookid = currentRow.find('td:eq(0) select').val();
        var bookyear = currentRow.find('td:eq(1) select').val();
        var bookVolume = currentRow.find('td:eq(2) select').val();
        var bookpageno = currentRow.find("td:eq(3) input[type='text']").val();
        var booksrno = currentRow.find("td:eq(4) input[type='number']").val();
        var obj = {};
        obj.bookid = bookid;
        obj.bookyear = bookyear;
        obj.Volume = bookVolume
        obj.bookpageno = bookpageno
        obj.booksrno = booksrno;
        rulebook.push(obj);
    });
    var inputparam = {
        ActTypeId: $("#ddlActType").val(),
        ActId: $("#ddlAct").val(),
        NotificationNo: $("#notificationNo").val(),
        GSRSO_Prefix: $("#ddlGsrso").val(),
        GSRSO_No: $("#gsrsonumber").val(),
        Notification_date: $("#notification_date").val(),
        Notifiction_SectionRule: $("#underSectionRule").val(),
        NotificationRuleKind: $("#ddlRuleKind").val(),
        NotificationRuleId: $("#ddlRule").val(),
        GazzetId: $("#ddlPublishedIn").val(),
        PartId: $("#ddlPart").val(),
        Nature: $("#ddlNature").val(),
        GazetteDate: $("#gazetteDate").val(),
        NotificationType: $("#ddlNotificationType").val(),
        PageNo: $("#pagenumber").val(),
        ComeInforce: $("#ddlCominforce").val(),
        PublishedInGazeteDate: $("#comeinforseDate").val(),
        // PublishedInGazeteDate: $("#rule_date").val(), 
        Substance: $("#substance").val(),
        notificationBooks: rulebook,
        AmendedRules: amendedactids.join(','),
        RepealedRules: repealedactids.join(','),
        ExtraRulesAct: $('#ddlExtraAct').val() !== null ? $('#ddlExtraAct').val().join(',') : $('#ddlExtraAct').val(),
        Id: ruleId
    };
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: url,
        data: { notiViewModel: inputparam },
        success: function (responce) {
            $('#loader').hide();
            if (responce.data > 0 && responce.statuscode === 200)
                Swal.fire(
                    'Good job!',
                    'You clicked the button!',
                    'success'
                )
            else
                Swal.fire(
                    'Good job!',
                    'You clicked the button!',
                    'error'
                )
        }
    });
}

BindFormDataByNotificationId = function (notiid) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "FindByNotificationId",
        data: { notificationId: notiid },
        dataType: 'json',
        success: function (data) {
            if (data !== null) {
                BindFormControls(data);
            }
            $('#loader').hide();
        }
    });
}

