$(document).ready(function () {
    BindFileType("ddlFileType");
});
//$("#loader").show()
//$("#fileUpload").on('change', function () {
//    var files = $('#fileUpload').prop("files");
//    var url = "tool/OnPostMyUploader?handler=MyUploader";
//    formData = new FormData();
//    formData.append("MyUploader", files[0]);

//    jQuery.ajax({
//        type: 'POST',
//        url: url,
//        data: formData,
//        cache: false,
//        contentType: false,
//        processData: false,
//        beforeSend: function (xhr) {
//            xhr.setRequestHeader("XSRF-TOKEN",
//                $('input:hidden[name="__RequestVerificationToken"]').val());
//        },
//        success: function (repo) {
//            if (repo.status == "success") {
//                alert("File : " + repo.filename + " is uploaded successfully");
//            }
//        },
//        error: function () {
//            alert("Error occurs");
//        }
//    });
//});

// If you want to upload file on button click, then use below button click event
$("#btnUpload").on('click', function () {
  
    var filetype = $("#ddlFileType").val();
    var files = $('#fileUpload').prop("files");
    var url = "tool/OnPostMyUploader?handler=FileUpload&FileType=" + filetype + "";
    formData = new FormData();
    formData.append("FileUpload", files[0]);

    jQuery.ajax({
        type: 'POST',
        url: url,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function (xhr) {
            $("#loader").show();
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (repo) {
            $("#loader").hide();
            if (repo.status == "success") {
                alert("File : " + repo.filename + " is uploaded successfully");
            }
        },
        error: function () {
            alert("Error occurs");
        }
    });
});
$("#btnScrapData").click(function () {
    jQuery.ajax({
        type: 'GET',
        url: "tool/ScrapData?url=" + $("#txtUrl").val() + "",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        }, success: function (repo) {

        }, error: function () {
            alert("Error occurs");
        }
    });
});