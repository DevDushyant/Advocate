BindActType = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-acttype",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select act type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindYear = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-year",
        dataType: "json",
        async: false,
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select year",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindAssent = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-assentby",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select assent by",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindNature = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-nature",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select nature",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindGazette = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-gazette",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select gazette",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindSubGazetteOrPart = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-gazette-part",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select part",
                allowClear: true
            });
            $("#loader").hide();

        }
    });
}

BindComeInforce = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-comeinoforce",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select comeinforce",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindSubject = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-subject",
        dataType: "json",
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select subject",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindBook = function (dropdownId) {
    $.ajax({
        beforeSend: function () {
            $("#loader").show()
        },
        type: "GET",
        url: "/get-all-book",
        dataType: "json",
        async: false,
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select subject",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindActbyActType = function (dropdownId, acttype) {
    $.ajax({
        type: "GET",
        url: "/get-all-actbyacttype/" + acttype,
        dataType: "json",
        async: false,
        beforeSend: function () {
            $("#loader").show()
        },
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select act type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}


BindVolume = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/get-book-volume",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select volume",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindRuleGSRSO = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/get-rule-gsr",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select gsr/so",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindManageType = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/get-all-managetype",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}
BindManageRuleSearchKind = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/GetSearchRuleKind",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindRuleByAct = function (dropdownId, actId, ruleKind) {
    $.ajax({
        type: "GET",
        url: "/get-all-rulebyActId/" + actId + "/" + ruleKind,
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        async: false,
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select rule",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}
BindNotificationType = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/get-all-notificationType",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        async: false,
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Please select notification type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}

BindFileType = function (dropdownId) {
    $.ajax({
        type: "GET",
        url: "/ddl-data-ext-type",
        dataType: "json",
        beforeSend: function () {
            $("#loader").show()
        },
        async: false,
        success: function (responce) {
            $("#" + dropdownId).empty();
            $("#" + dropdownId).append($("<option></option>"));
            $.each(responce, function (data, value) {
                $("#" + dropdownId).append($("<option></option>").val(value.value).html(value.text));
            });
            $("#" + dropdownId).select2({
                placeholder: "Select File Type",
                allowClear: true
            });
            $("#loader").hide();
        }
    });
}
