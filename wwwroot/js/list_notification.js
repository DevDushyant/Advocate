$(document).ready(function () {
    var table = $('#tblNotification').DataTable({
        lengthChange: false,
        buttons: true,
        dom: 'Bfrtip',
        buttons: [
            'copy', 'excel', 'pdf'
        ],
        columnDefs: [
            { "width": "5%", "targets": 0 },
            { "width": "8%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "32%", "targets": 3 },
            { "width": "20%", "targets": 4 },            
            { "width": "10%", "targets": 5 }
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
});