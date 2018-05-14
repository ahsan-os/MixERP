saveButton.click(function () {
    triggerOnSavingEvent();

    function getType() {
        function skipPrimaryKey() {
            var skip = scrudFactory.skipPrimaryKey;

            if(skip === undefined){
                skip = $("[data-primarykey]").is("[disabled]");                
            };

            return skip;
        };

        var type = "add";

        if (!skipPrimaryKey()) {
            type += "/false";
        };

        if (window.editing) {
            var primaryKey = $("[data-primarykey]").val();
            type = "edit/" + primaryKey;
        };

        return type;
    };


    function request(type, entity, customFields) {
        var url = window.scrudFactory.formAPI + "/" + type;

        var form = [];

        form.push(window.metaDefinition);
        form.push(entity);
        form.push(customFields);

        var data = JSON.stringify(form);
        var verb = type.substring(0, 4) === "edit" ? "PUT" : "POST";
        return getAjaxRequest(url, verb, data);
    };

    var isValid = validate();

    if (!isValid) {
        return;
    };

    saveButton.addClass("loading").prop("disabled", true);
    var bigError = $(".big.error");
    var entity = new Object();
    var customFields = [];

    bigError.removeClass("vpad16").html("");

    function getFormValue(columnName, type, isSerial, dbDataType) {
        var el = $("#" + columnName);
        var val = null;
        if (el.length) {
            val = el.val();
            if (isNullOrWhiteSpace(val)) {
                val = null;
            } else {
                if (wholeNumbers.indexOf(type) > -1) {
                    val = window.parseInt2(val);
                } else if (decimalNumber.indexOf(type) > -1) {
                    val = window.parseFloat2(val);
                } else if (dateTypes.indexOf(type) > -1) {
                    if (dbDataType === "time") {
                        val = val || null;
                    } else {
                        val = window.parseLocalizedDate(val) || null;
                    };
                } else if (booleans.indexOf(type) > -1) {
                    val = (["true", "t", "yes", "y", "1"].indexOf((val || "").toString().toLowerCase()) > -1);
                } else {
                    val = val.toString().trim();

                    if (el.is("select")) {
                        if (!val) {
                            val = null;
                        };
                    };
                };
            };
        };

        if (isSerial) {
            if (isNullOrWhiteSpace(val)) {
                val = null;
            };
        };

        return val;
    };

    $.each(metaInfo.Columns, function (i, v) {
        var value = getFormValue(v.ColumnName, v.DataType, v.IsSerial, v.DbDataType);

        if (window.editData) {
            if (window.scrudFactory.disabledOnEdit && window.scrudFactory.disabledOnEdit.length) {
                if (window.scrudFactory.disabledOnEdit.indexOf(v.PropertyName) !== -1) {
                    return true;
                };
            };
        };

        entity[v.PropertyName] = value;
    });

    if (window.metaCustomFields) {
        $.each(window.metaCustomFields, function (i, v) {
            var dataType = getCustomFieldDataType(v);
            var value = getFormValue(v.FieldName, dataType, false);

            var customField = new Object();
            customField.FieldName = v.FieldName;
            customField.Value = value;

            customFields.push(customField);
        });
    };


    var ajax = request(getType(), entity, customFields);

    ajax.success(function (response) {
        saveButton.removeClass("loading").prop("disabled", false);
		
		if(typeof(window.scrudSaveButtonCallback) === "function"){
			window.scrudSaveButtonCallback(response);
			return;
		};
		
        var confirmed = confirm(window.translate("TaskCompletedSuccessfullyReturnToView"));

        resetForm();

        if (confirmed) {
            window.scrudForm.hide();

            if (getReturnUrl()) {
                window.location.href = getReturnUrl();
            };

            if (window.scrudView.length) {
                showView();
            };
        };
    });

    ajax.fail(function (xhr) {
        var error = getAjaxErrorMessage(xhr);

        saveButton.removeClass("loading").prop("disabled", false);
        bigError.addClass("vpad16").text(error);
    });
});
