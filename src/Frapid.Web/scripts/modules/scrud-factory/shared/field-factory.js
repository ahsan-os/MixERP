function getField(propertyName, dataType, nullable, maxLength) {
    if (typeof (window.scrudFactory.keys) === "undefined") {
        window.scrudFactory.keys = [];
    };

    if ((propertyName || "").toString().toLowerCase() === "password") {
        dataType = "password";
    };

    var hasKey = Enumerable.From(window.scrudFactory.keys || [])
        .Where(function (x) { return x.property === propertyName }).ToArray()[0];

    if (hasKey) {
        return $("<select class='chosen' />");//$("<select class='' />");
    };

    switch (dataType.replace("?")) {
        case "photo":
            return $("<input type='text' class='image' />");
        case "bigint":
        case "integer":
        case "int":
        case "int4":
        case "int8":
        case "integer_strict":
        case "integer_strict2":
            return $("<input type='text' class='integer' />");
        case "float8":
        case "decimal":
        case "decimal_strict":
        case "decimal_strict2":
        case "numeric":
            return $("<input type='text' class='decimal' />");
        case "money_strict2":
        case "money_strict":
            return $("<input type='text' class='currency' />");
        case "varchar":
        case "nvarchar":
            if (maxLength === -1) {
                return $("<textarea />");
            };

            return $("<input type='text' />");
        case "text":
            return $("<textarea />");
        case "datetimeoffset":
        case "datetime":
        case "timestamp":
        case "timestamptz":
        case "date":
            return $("<input type='text' class='date' />");
        case "bool":
        case "bit":
            var el = $("<select class='chosen' />");//$("<select class='chosen' />");
            var option = "<option";

            if (nullable) {
                option += " value='Select'>";
            } else {
                option += " value=''>";
            };

            option += window.translate("Select") + "</option>";
            option += "<option value='yes'>" + window.translate("Yes") + "</option>";
            option += "<option value='no'>" + window.translate("No") + "</option>";

            el.append(option);

            return el;
        default:
            return $("<input type='text' />");
    };
};


function editScrudFormElement(targetEl, value) {
    if (targetEl.length) {
        if (targetEl.hasClass("date")) {
            value = value.toString().toFormattedDate();
        };

        //if (targetEl.attr("data-type") === "time") {
            //value = getTime(value);
        //};

        targetEl.val(value).trigger("change");

        if (targetEl.attr("data-type") === "photo") {
            if (window.scrudFactory.uploadHanlder) {
                initializeUploader();

                $(".uploader input.file").attr("data-handler", window.scrudFactory.uploadHanlder);
            };
        };

        if (targetEl.is("select")) {
            var type = targetEl.attr("data-type");

            if (type.substr(0, 4) === "bool") {
                value = value ? "yes" : "no";
            };

            if (type === "bit") {
                if (typeof (value) === "boolean") {
                    value = value ? "yes" : "no";
                } else {
                    value = value === "1" ? "yes" : "no";
                };
            };

            targetEl.val(value.toString()).trigger("change");
            
            targetEl.trigger("blur");
            targetEl.trigger("change");
            return;
        };

        targetEl.trigger("blur");
        targetEl.trigger("change");
        targetEl.trigger("keyup");
    };
};