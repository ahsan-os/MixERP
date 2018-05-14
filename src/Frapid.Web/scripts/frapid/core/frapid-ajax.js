var appendParameter = function (data, parameter, value) {
    if (!isNullOrWhiteSpace(data)) {
        data += ",";
    };

    if (value === undefined) {
        value = "";
    };

    data += JSON.stringify(parameter) + ':' + JSON.stringify(value);

    return data;
};

var getData = function (data) {
    if (data) {
        return "{" + data + "}";
    };

    return null;
};


function getHeaders() {
    var headers = {};

    function getRequestVerificationToken() {
        return $("[name=__RequestVerificationToken]").val();
    };

    function getAccessToken() {
        return localStorage.getItem("access_token");
    };

    function addHeader(name, value) {
        if (name) {
            headers[name] = value;
        };
    };


    var token = getAccessToken();

    if (token) {
        addHeader("Authorization", "Bearer " + token);
    };

    addHeader("RequestVerificationToken", getRequestVerificationToken());
    return headers;
};

var getAjaxRequest = function (url, type, data) {
    if (!type) {
        type = "GET";
    };

    var ajax = $.ajax({
        type: type,
        url: url,
        headers: getHeaders(),
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });

    return ajax;
};

jQuery.fn.bindAjaxData = function (ajaxData, nullable, selectedValue, keyField, valueField, isArray) {
    var selected;

    var targetControl = $(this);
    targetControl.empty();


    if (ajaxData.length === 0) {
        appendOption(targetControl, "", window.i18n.None);
        return;
    };

    if (!nullable) {
        appendOption(targetControl, "", window.i18n.Select);
    };

    if (!keyField) {
        keyField = "Value";
    };

    if (!valueField) {
        valueField = "Text";
    };

    var valueIsExpression = keyField.substring(2, 0) === "{{" && keyField.slice(-2) === "}}";
    var textIsExpression = valueField.substring(2, 0) === "{{" && valueField.slice(-2) === "}}";

    $.each(ajaxData, function () {
        var text;
        var value;
        selected = false;

        if (typeof (isArray) === "undefined") {
            isArray = false;
        };

        if (isArray) {
            text = this;
            value = this;
        };

        if (!isArray) {
            var expression;


            if (textIsExpression) {
                expression = valueField.replace("{{", "").replace("}}", "");
                text = eval(expression);
            } else {
                text = (this[valueField] || "").toString();
            };

            if (valueIsExpression) {
                expression = keyField.replace("{{", "").replace("}}", "");
                value = eval(expression);
            } else {
                value = (this[keyField] || "").toString();
            };
        };

        if (selectedValue) {
            if (value === selectedValue.toString()) {
                selected = true;
            };
        };

        appendOption(targetControl, value, text, selected);
    });
};

function appendOption(selectEl, value, text, selected) {
	var option = $("<option></option>");
	option.val(value).html(text).trigger('change');

	if (selected) {
		option.attr("selected", true);
	};

	selectEl.append(option);
};


function ajaxDataBind(url, targetControl, data, keyField, valueField, selectedValue, callback, isArray, nullable) {
    if (!targetControl || targetControl.length === 0) {
        return;
    };

    var ajax = new window.getAjaxRequest(url, "GET", data);

    ajax.success(function (msg) {
        result = msg;

        if (!result) {
            return;
        };

        if (targetControl.length === 1) {
            targetControl.bindAjaxData(result, nullable, selectedValue, keyField, valueField, isArray);
        };

        if (targetControl.length > 1) {
            targetControl.each(function () {
                $(this).bindAjaxData(result, nullable, selectedValue, keyField, valueField, isArray);
            });
        };

        if (typeof window.ajaxDataBindCallBack === "function") {
            window.ajaxDataBindCallBack(targetControl);
        };

        if (typeof callback === "function") {
            callback(targetControl, result);
        };

    });

    ajax.error(function (xhr) {
        if (typeof callback === "function") {
            callback();
        };

        var err = $.parseJSON(xhr.responseText);
        appendOption(targetControl, 0, err.Message);
    });
};


var getAjaxErrorMessage = function (xhr) {
    if (xhr) {
        var err;

        try {
            if (xhr.statusText) {
                err = xhr.statusText;
            };

            if (xhr.responseText) {
                var response = JSON.parse(xhr.responseText);

                if (response) {
                    if (response.Message) {
                        err = response.Message;
                    };

                    if (response.ExceptionMessage) {
                        err = response.ExceptionMessage;
                    };

                    if (response.InnerException) {
                        err = response.InnerException.Message + " " + response.InnerException.ExceptionMessage;
                    };
                };
            };
        } catch (e) {
            err = xhr.responseText.Message;
        }

        if (err) {
            return err;
        };

        return xhr.responseText;
    }

    return "";
};

function getAjaxColumnFilter(statement, columnName, dataType, filterCondition, filtervalue, andValue) {
    var filter = new Object();

    filter.FilterStatement = (statement || "WHERE");
    filter.ColumnName = columnName;
    filter.DataType = dataType;
    filter.FilterCondition = filterCondition;
    filter.FilterValue = filtervalue;
    filter.FilterAndValue = andValue;

    return filter;
};

function getAjaxPropertyFilter(statement, propertyName, filterCondition, filtervalue, andValue) {
    var filter = new Object();

    filter.FilterStatement = (statement || "WHERE");
    filter.PropertyName = propertyName;
    filter.FilterCondition = filterCondition;
    filter.FilterValue = filtervalue;
    filter.FilterAndValue = andValue;

    return filter;
};

jQuery.cachedScript = function (url, options) {
    options = $.extend(options || {}, {
        dataType: "script",
        cache: true,
        url: url
    });

    return jQuery.ajax(options);
};