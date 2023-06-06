$(document).ready(function ()
{
    var table = $('.acttypetable').DataTable({
        lengthChange: false,
        buttons: true,
        dom: 'Bfrtip',
        buttons: [
            'copy', 'excel', 'pdf'
        ],
        columnDefs: [
            { "width": "5%", "targets": 0 },
            { "width": "10%", "targets": 1 },
            { "width": "10%", "targets": 2 },
            { "width": "10%", "targets": 3 }
        ],
        initComplete: function () {
            this.api().columns().every(function (i) {
                if (i === 1 || i === 3 ) {
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

    DownloadFileFormat = function (fileName) {
        $.ajax({
            url: "/download-act-file",
            method: "POST",
            data: { fileName: fileName },
            success: function (data) {
                //Convert the Byte Data to BLOB object.
                var blob = new Blob([data], { type: "application/octetstream" });

                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, fileName);
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", fileName);
                    a.attr("href", link);
                    $("body").append(a);
                    a[0].click();
                    $("body").remove(a);
                }
            }
        });
    }

    UploadActPdf = function () {
        $("#uploadModelPopup").modal("show");
    }

    $('#btnUpload').click(function () {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('username', 'Manas');

            $.ajax({
                url: '/Home/UploadFiles',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    alert(result);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    });


});

checkactnature = function (actIds) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "POST",
        url: "Act/GetActRepealedBy",
        data: { actId: actIds },
        dataType: 'json',
        success: function (data) {
            $("#loader").hide();
            localStorage.setItem("repealedAct", "");
            if (data.length !== 0) {               
                var title = "Selected Record has been repealed by : ";
                var tableBody = "";
                tableBody += "<table><tr><th>This act/ord has been repealed By : </th></tr>"
                title += "<table>"
                $.each(data, function (index, val) {
                    tableBody += "<tr><td style='padding-left:15px;font-size:x-large;color:red;'>" + data[index].actName + "(" + data[index].actType + "," + data[index].actNumber + "of ," + data[index].year + ")</td><tr>"
                    title += "<tr><td style='padding-left: 15px;font-size:x-large;color:red;'>" + data[index].actName + "(" + data[index].actType + "," + data[index].actNumber + "of ," + data[index].year + ")</td><tr>"
                });
                tableBody += "</table >";
                title += "</table > <br/> Do you want to continue?";                
                localStorage.setItem("repealedAct", tableBody);
                Swal.fire({
                    title: title,
                    width: 800,
                    showDenyButton: true,
                    showCancelButton: true,
                    confirmButtonText: 'Yes',
                    denyButtonText: 'No',
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        window.location.href = "act/edit?actId=" + actIds;
                    }
                })
            }
            else
                window.location.href = "act/edit?actId=" + actIds;
        }
    });
}

