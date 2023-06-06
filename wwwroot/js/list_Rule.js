$(document).ready(function () {
    var table = $('#tblRule').DataTable({
        lengthChange: false,
        buttons: true,
        dom: 'Bfrtip',
        //buttons: [
        //    'copy', 'excel', 'pdf'
        //],
        columnDefs: [
            { "width": "5%", "targets": 0 },
            { "width": "10%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "25%", "targets": 5 },
            { "width": "10%", "targets": 6 }
        ],
        initComplete: function () {
            this.api().columns().every(function (i) {
                if (i === 1 ||i === 3|| i === 2 || i === 4 ) {
                    var column = this;
                    $(column.header()).append("");
                    var Selid = "Sel_" + i;
                    $("#" + Selid).remove();
                    var select;
                    if (i === 2 ) {
                         select = $('<select id=' + Selid + ' style=width:350px><option value="">All</option></select>')
                            .appendTo($(column.header()))
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );
                                column.search(val ? '^' + val + '$' : '', true, false).draw();
                            });
                    }
                    if (i === 4) {
                        select = $('<select id=' + Selid + ' style=width:150px><option value="">All</option></select>')
                            .appendTo($(column.header()))
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );
                                column.search(val ? '^' + val + '$' : '', true, false).draw();
                            });
                    }
                    if (i === 1||i===3) {
                         select = $('<select id=' + Selid + '><option value="">All</option></select>')
                            .appendTo($(column.header()))
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );
                                column.search(val ? '^' + val + '$' : '', true, false).draw();
                            });
                    }
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                    if (i === 1) {
                        $("#" + Selid).select2({
                            placeholder: "Select act type",
                            allowClear: true
                        });
                    }
                    if (i === 2) {
                        $("#" + Selid).select2({
                            placeholder: "Select act",
                            allowClear: true
                        });
                    }
                    if (i === 4) {
                        $("#" + Selid).select2({
                            placeholder: "Select rule no",
                            allowClear: true
                        });
                    }
                }
            });
        }
    });
});