
var selecteactids = [];
var accesstype = "", actid;
var buttonType = "";
$(document).ready(function () {
    //$("#lblRepealedInfo").append(localStorage.getItem("repealedAct").replace(/['"]+/g, ''));
    $("#gazetteDate").datepicker();
    $("#assentdate").datepicker();
    BindActType('ddlActType');
    BindYear('ddlYear');
    BindAssent('ddAssentBy');
    BindNature('ddlNature');
    BindGazette('ddlPublishedIn');
    BindComeInforce('ddlCominforce');
    BindSubGazetteOrPart('ddlPart');
    BindSubject('ddlSubject');
    BindBook('ddlBook');
    BindYear('ddlBookActYear');
    $('#ddlYear').on('change', function () {
        if ($('#hdnScreenType').val() === "create_year")
            CheckActExistOrNot($('#ddlActType').val(), $('#actnumber').val(), $('#ddlYear').val());
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


    var categoryIndex = 0;
    $("#tblActTable th").each(function (i) {
        if ($($(this)).html() === "Act Type") {
            categoryIndex = i; return false;
        }
    });

    $("form input:radio").change(function () {
        $('#loader').show();
        BindActPartialData();
        if ($(this).val() == "repealed") {
            $(".acttypetable").each(function (value, index) {
                $('#chk' + value).prop('checked', false);
            });
            $("#acttypmodelpopup").modal("show");
        }
        if ($(this).val() == "amended") {
            $(".acttypetable").each(function (value, index) {
                $('#chk' + value).prop('checked', false);
            });
            $("#acttypmodelpopup").modal("show");
        }
        $('#loader').hide();
    });

    $("#btnSave").click(function () {
        SaveActData();
    });

    $("#btnCopy").click(function () {
        BindActPartialData();
        buttonType = "Copy";
        $("#acttypmodelpopup").modal("show");
    });

    $("#btnRepeat").click(function () {
        BindRepeateData();
    });


    $('#btnSelectAct').click(function () {
        $('.acttypetable tbody tr').each(function () {
            if ($(this).find(".chkall").is(":checked")) {
                selecteactids.push(parseInt($(this).find('input[type="hidden"]').val()));
            }
        });
        $('#acttypmodelpopup').modal('hide');

        if (buttonType === "Copy")
            BindFormDataByActId(selecteactids[0]);
    });

    var rowcount = 0;
    $('#btnAddBook').click(function () {
        rowcount = $('#tblActBook > tbody >tr').length + 1;
        BindActBookTable(rowcount);
    });

    $("#tblActBook").on("click", ".deleteIcon", function () {
        $(this).closest("tr").remove();
    });

    const urlParams = new URLSearchParams(window.location.search);
    const actId = urlParams.get('actId');
    actid = actId;
    accesstype = urlParams.get("accessType");
    yearchange = urlParams.get("accessyear");
    if (actId !== null) {
        $("#btnRepeat").hide();
        $("#btnCopy").hide();
        $("#btnSave").text("Update");
        BindFormDataByActId(actId);
    }

});
BindActBookTable = function (rowid) {
    if (rowid === "" || rowid === 'undefined')
        rowid = '';
    var markup = "<tr>" +
        "<td><select id='ddlBook" + rowid + "' name='act_book' class='form-control'></td>" +
        "<td><select id='ddlBookActYear" + rowid + "' name='act_year' class='form-control'></select></td>" +
        "<td><input type='text' id='book_pagenumber" + rowid + "' name='act_pagenumber_book' placeholder='Please enter page no.' class='form-control' /></td>" +
        "<td><input type='number'' id='book_serial_number" + rowid + "' name='book_serial_number' placeholder='Please enter serial no.' class='form-control' /></td>" +
        "<td><i value='Delete' type='button' class='deleteIcon fa fa-trash'></i></td>" +
        "</tr>";
    $("#tblActBook tbody").append(markup);
    BindBook('ddlBook' + rowid);
    BindYear('ddlBookActYear' + rowid);
}

SaveActData = function () {
    var assentDate = $('#assentdate').val().split("/").reverse().join("-");
    var gazzetDate = $('#gazetteDate').val().split("/").reverse().join("-");
    var actBook = [];
    $('#tblActBook > tbody >tr').each(function () {
        var currentRow = $(this);
        var bookid = currentRow.find('td:eq(0) select').val();
        var bookyear = currentRow.find('td:eq(1) select').val();
        var bookpageno = currentRow.find("td:eq(2) input[type='number']").val();
        var booksrno = currentRow.find("td:eq(3) input[type='number']").val();
        var obj = {};
        obj.bookid = bookid;
        obj.bookyear = bookyear;
        obj.bookpageno = bookpageno
        obj.booksrno = booksrno;
        actBook.push(obj);
    });
    var inputparam = {
        ActCategory: $('input[name="act_nature_type"]:checked').val(),
        ActTypeId: $('#ddlActType').val(),
        ActNumber: $('#actnumber').val(),
        ActYear: $('#ddlYear').val(),
        AssentBy: $('#ddAssentBy').val(),
        AssentDate: assentDate,
        ActName: $('#actname').val(),
        PublishedInId: $('#ddlPublishedIn').val(),
        PartId: $('#ddlPart').val(),
        GazetteNuture: $('#ddlNature').val(),
        GazetteDate: gazzetDate,
        PageNumber: $('#pagenumber').val(),
        ComeInforce: $('#ddlCominforce').val(),
        SubjectId: $('#ddlSubject').val(),
        selectedActListId: selecteactids,
        SubjectAct: $('#ddlSubject').val().join(','),
        actBookList: actBook,
        Id: parseInt(actid)
    };
    var url;


    if (accesstype === "edit" || yearchange === "yes")
        url = '/Act/Edit';
    else
        url = '/Act/Create';


    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: url,
        data: { actViewModel: inputparam },
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

function ChangeFormateDate(oldDate) {
    return oldDate.toString().split("-").reverse().join("/");
}

BindFormDataByActId = function (actId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "ActDetail",
        data: { actId: actId },
        dataType: 'json',
        success: function (data) {
            $('#hdnScreenType').val('edit_year');
            if (data !== null) {
                BindForDataInEditMode(data);
            }
            $('#loader').hide();
        }
    });
}

BindRepeateData = function () {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "RepeateActDetail",
        dataType: 'json',
        success: function (data) {
            $('#hdnScreenType').val('edit_year');
            $("#loader").hide()
            if (data !== null) {
                BindForDataInEditMode(data);
            }
        }
    });
}

BindForDataInEditMode = function (data) {
    $('#ddlActType').val(data.actTypeId).trigger('change');
    $('#actnumber').val(data.actNumber);

    if ($('#hdnScreenType').val() === "edit_year")
        $('#ddlYear').val(data.actYear).trigger('change');
    $('#ddAssentBy').val(data.assentBy).trigger('change');
    if (data.assentDate !== null)
        $('#assentdate').val(ChangeFormateDate(data.assentDate.split('T')[0])).trigger('change');
    $('#actname').val(data.actName);
    $('#ddlPublishedIn').val(data.gazetteId).trigger('change');
    $('#ddlPart').val(data.partId).trigger('change');
    $('#ddlNature').val(data.nature).trigger('change');
    if (data.gazetteDate !== null)
        $('#gazetteDate').val(ChangeFormateDate(data.gazetteDate.split('T')[0]));
    $('#pagenumber').val(data.pageNo);
    $('#ddlCominforce').val(data.comeInforce).trigger('change');
    if (data.subjectAct !== null && data.subjectAct !== "") {
        var subjectList = [];
        if (data.subjectAct.indexOf(',') !== -1) {
            var subjectdata = data.subjectAct.substring(0, data.subjectAct.length - 1);
            subjectList = subjectdata.split(",");
        }
        else
            subjectList.push(data.subjectAct);
        $('#ddlSubject').val(subjectList).trigger('change');
    }

    if (data.ruleBookList !== null) {
        $.each(data.ruleBookList, function (index, value) {
            if (index === 0) {
                $('#ddlBook').val(value.bookId).trigger('change');
                $('#ddlBookActYear').val(value.bookYear).trigger('change');
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
            }
        })
    }

    if (data.amendedActList !== null) {
        $.each(data.amendedActList, function (index, val) {
            $('#tblActTable tbody tr').each(function (i) {
                var td = $(this).eq(0).find('input[type="hidden"]').val();
                if (parseInt(td) === val.actID) {
                    $(this).find('input[type="checkbox"]').prop('checked', true);
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
            });
        });

        $("form input:radio").change(function () {
            $("#acttypmodelpopup").modal("show");
        });
    }

    if (data.repealedActList !== null) {
        $.each(data.repealedActList, function (index, val) {
            $('#tblActTable tbody tr').each(function (i) {
                var td = $(this).eq(0).find('input[type="hidden"]').val();
                if (parseInt(td) === val.actID) {
                    $(this).find('input[type="checkbox"]').prop('checked', true);
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
            });
        });
    }


}

BindActPartialData = function () {
    var table = $("#tblActTable").dataTable({
        "processing": true,
        bAutoWidth: false,
        lengthMenu: [[-1, 10, 25, 50], ["All", 10, 25, 50]],
        aoColumns: [
            { sWidth: '5%' },
            { sWidth: '10%' },
            { sWidth: '10%' },
            { sWidth: '10%' },
            { sWidth: '65%' }
        ],
        initComplete: function () {
            this.api().columns().every(function (i) {
                if (i === 1 || i === 3) {
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

CheckActExistOrNot = function (actTypeId, actNumber, actYear) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "ActDetailByActType_Number_Year",
        data: { TypeId: actTypeId, ActNumber: actNumber, Year: actYear },
        dataType: 'json',
        success: function (data) {
            $("#loader").hide();
            if (data !== 0) {
                Swal.fire({
                    title: 'Do you want to edit the record',
                    showDenyButton: true,
                    showCancelButton: true,
                    confirmButtonText: 'Yes',
                    denyButtonText: 'No',
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        // window.location.href = "Edit?actId=" + data +"&accessyear=yes";
                        yearchange = "yes"
                        actid = data;
                        $("#btnRepeat").hide();
                        $("#btnCopy").hide();
                        $("#btnSave").text("Update");
                        BindFormDataByActId(data);
                    }
                })
            }
        }
    });
}