function createFieldGroup(el, column, cssClass) {
    function getLabel(columnName) {
        return window.translate(columnName);
    };

    if (typeof (column) === "undefined") {
        return;
    };

    if (["audit_user_id", "audit_ts", "deleted"].indexOf(column.ColumnName) >= 0) {
        return;
    };

    var field = $("<div/>");
    field.addClass(cssClass);

    var label = $("<label/>");

    label.text(window.translate(column.PropertyName));

    label.attr("for", column.ColumnName);


    var element = getField(column.PropertyName, column.DbDataType, column.IsNullable, column.MaxLength);


    element.attr("id", column.ColumnName);


    if (column.DbDataType.indexOf("strict") > -1) {
        element.attr("data-validation", column.DbDataType);
    };

    element.attr("data-property", column.PropertyName);
    element.attr("data-type", column.DbDataType);

    if (typeof (window.scrudFactory.live) !== "undefined") {
        if (column.PropertyName === window.scrudFactory.live) {
            element.addClass("live");
        };
    };

    if (typeof (window.scrudFactory.readonlyColumns) === "undefined") {
        window.scrudFactory.readonlyColumns = [];
    };

    if (typeof (window.scrudFactory.hiddenColumns) === "undefined") {
        window.scrudFactory.hiddenColumns = [];
    };

    if (window.scrudFactory.readonlyColumns.indexOf(column.PropertyName) > -1) {
        element.attr("disabled", "");
    };

    if (window.scrudFactory.hiddenColumns.indexOf(column.PropertyName) > -1) {
        field.addClass("hidden column");
    };

    if (column.IsSerial) {
        element.attr("disabled", "disabled");
    };

    if (column.IsPrimaryKey) {
        element.attr("data-primaryKey", "");
        element.removeClass();
        if (window.scrudFactory.hidePrimaryKey) {
            field.addClass("hidden column");
        };
    };

    if (column.MaxLength > 0) {
        element.attr("maxlength", column.MaxLength);
    };

    if (!column.IsNullable) {
        element.attr("required", "required");
        field.addClass("required");
    };

    var value = column.Value;

    if (window.editData) {
        if (window.editData.hasOwnProperty(column.PropertyName)) {
            value = window.editData[column.PropertyName];
        };
    };

    if (element.hasClass("date")) {
        if (value) {
            value = value.toFormattedDate();
        };
    };

    //if (element.attr("data-type") === "time") {
        //if (value) {
            //value = getTime(value);
        //};
    //};

    if (column.DbDataType === "bool") {
        value = value === true ? "yes" : "no";
    };


    if (column.DbDataType === "bit") {
        value = value === "1" ? "yes" : "no";
    };

    if (value) {
        element.attr("data-value", value);
        element.val(value).trigger("change");
    };

    if (column.IsCustomField) {
        element.addClass("custom-field");
    } else {
        element.addClass("form-field");
    };

    if (window.editData) {
        if (window.scrudFactory.disabledOnEdit) {
            if (window.scrudFactory.disabledOnEdit.indexOf(column.PropertyName) !== -1) {
                element.attr("disabled", "disabled");
            };
        };
    };

    field.append(label);
    field.append(element);

    if (column.Description) {
        field.append("<span class='ui basic pointing label'>" + column.Description + "</span>");
    };

    el.append(field);
};
