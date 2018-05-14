function editRow(el, isCard) {
    function customFieldRequest(tableName, primaryKeyValue) {
        var url = "/api/views/config/custom-field-view/get-where/1";

        var filters = [];

        filters.push(window.getAjaxColumnFilter("WHERE", "TableName", "string", FilterConditions.IsEqualTo, tableName));
        filters.push(window.getAjaxColumnFilter("AND", "ResourceId", "string", FilterConditions.IsEqualTo, primaryKeyValue));

        var data = JSON.stringify(filters);

        return getAjaxRequest(url, "POST", data, false);
    };

    function request(primaryKeyValue) {
        var url = window.scrudFactory.formAPI + "/" + primaryKeyValue;
        return getAjaxRequest(url);
    };

    var confirmed = confirmAction();

    if (!confirmed) {
        return;
    };

    var primaryKeyValue = getPrimaryKeyValue($(el), isCard);

    if (window.scrudFactory.editUrl) {
        window.location = window.scrudFactory.editUrl + primaryKeyValue;
        return;
    };

    if (window.scrudForm === "undefined") {
        displayMessage(window.translate("NoFormFound"));
        return;
    };

    var tableName = window.scrudFactory.formTableName;

    if (checkIfProcedure()) {
        return;
    };

    var ajax = request(primaryKeyValue);

    if (window.scrudFactory.disabledOnEdit) {
        $.each(window.scrudFactory.disabledOnEdit, function () {
            var t = $('[data-property="' + this + '"]');
            t.attr("disabled", "disabled");
        });
    };

    ajax.success(function (response) {
        window.editData = response;
        window.editing = true;

        displayEdit(response, false);

        customFieldRequest(tableName, primaryKeyValue).success(function (msg) {
            displayCustomFields(msg);
            window.scrudView.hide();
            window.scrudForm.show();
        });
    });
};