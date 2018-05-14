var ignoredQueryStrings = ["TitleSuffix", "View", "Page", "Filter", "data-tab", "ReturnUrl"];

$("#FilterName").text(window.translate("Untitled"));

function getFilterType(columnName) {
    var type = window.Enumerable.From(metaDefinition.Columns)
        .Where(function (x) { return x.ColumnName === columnName }).FirstOrDefault();

    if (type) {
        return type.DataType;
    };

    return "string";
};


function getFilterName() {    
    var el = $("[data-selected-filter]");
    if (el.length) {
        return el.attr("data-selected-filter");
    };

    return "";
};

var getQuerystringFilters = function () {
    function getType(propertyName) {
        var type = window.Enumerable.From(metaDefinition.Columns)
            .Where(function (x) { return x.PropertyName === propertyName }).FirstOrDefault();
        
        if (type) {
            return type.DbDataType;
        };

        return "";
    };

    function isString(propertyName) {
        var type = getType(propertyName);

        if (stringTypes.indexOf(type) > -1) {
            return true;
        };

        return false;
    };

    function parseValue(propertyName, val) {
        var type = getType(propertyName);
        if (isNullOrWhiteSpace(val)) {
            val = null;
        } else {
            if (wholeNumbers.indexOf(type) > -1) {
                val = window.parseInt2(val);
            } else if (decimalNumber.indexOf(type) > -1) {
                val = window.parseFloat2(val);
            } else if (dateTypes.indexOf(type) > -1) {
                val = window.parseLocalizedDate(val);
            } else if (booleans.indexOf(type) > -1) {
                val = (["true", "t", "yes", "y", "1"].indexOf((val || "").toString().toLowerCase()) > -1);
            };
        };

        return val;
    };


    var filters = [];
    var queryStrings = getQueryStrings();
    $.each(queryStrings, function (index, item) {
        if (ignoredQueryStrings.indexOf(item.key) === -1) {
            var key = toUnderscoreCase(item.key);
            var value = parseValue(item.key, item.value);
            var type = getFilterType(key);

            if (type) {
                var targetEl = $("#filter_" + key);
                if (targetEl.length) {
                    targetEl.val(value).trigger("change");
                };

                if (isString(item.key)) {
                    filters.push(getAjaxColumnFilter("WHERE", key, type, FilterConditions.IsLike, value));
                } else {
                    filters.push(getAjaxColumnFilter("WHERE", key, type, FilterConditions.IsEqualTo, value));
                };
            };
        };
    });

    return filters;
};


function loadFilterConditions() {
    var el = $('[data-scope="filter-condition"]');
    bindSelect(el, filterConditions, "value", "text");
};

function loadColumns() {
    var el = $('[data-scope="column"]');
    el.html("");
    bindSelect(el, localizedHeaders, "columnName", "localized");
};

$("#FilterNameInputText").keyup(function () {
    $("#FilterName").html(window.translate("Untitled"));
    if ($(this).val()) {
        var filterName = $(this).val();
        $("#FilterName").html(filterName);
    };
});

filterConditionSelect.change(function () {
    var val = window.parseFloat(filterConditionSelect.val() || 0);

    if (val >= 6 && val <= 7) {
        andInputText.removeAttr("disabled");
        return;
    };

    andInputText.attr("disabled", "disabled");
    andInputText.val("").trigger("change");
});


function validateFilters() {
    var isValid = true;

    $("#FilterForm [required]").each(function () {
        var el = $(this);

        if (isNullOrWhiteSpace(el.val())) {
            isValid = false;
            makeDirty(el);
            return;
        };

        removeDirty(el);
    });

    return isValid;
};

$("#AddFilterButton").click(function () {
    var grid = $("#FilterTable");

    var isValid = validateFilters();
    if (!isValid) {
        return;
    };

    var selectedColumn = columnSelect.getSelectedText();
    var filterCondition = filterConditionSelect.getSelectedText();

    var value = valueInputText.val();
    var and = andInputText.val();

    var duplicate = false;

    grid.find("tbody tr").each(function () {
        var el = $(this);
        var columnName = el.find("td:nth-child(2)").text();
        var condition = el.find("td:nth-child(3)").text();

        if (columnName === selectedColumn &&
            condition === filterCondition) {

            el.find("td:nth-child(4)").text(value);
            el.find("td:nth-child(5)").text(and);

            el.addClass("warning");
            duplicate = true;
            return false;
        };
    });


    if (!duplicate) {
        addFilterRow(selectedColumn, filterCondition, value, and);
    };
});

function addFilterRow(selectedColumn, filterCondition, value, and, css) {
    var html = "<tr>";
    html += "<td>" + commandItems + "</td>";
    html += "<td>" + selectedColumn + "</td>";
    html += "<td>" + filterCondition + "</td>";
    html += "<td>" + value + "</td>";
    html += "<td>" + and + "</td>";
    html += "</tr>";

    var row = $(html);

    if (css) {
        row.addClass(css);
    };

    $("#FilterTable").removeClass("inverted red").find("tbody").append(row);
    columnSelect.parent().find(".search").focus();
};

$("#ManageFiltersButton").click(function () {
    $('.filter.modal').modal('show');
});

$("#SaveFilterButton").click(function () {
    function request(filterName, filters) {
        var url = "/api/filters/recreate/" + window.scrudFactory.viewTableName + "/" + filterName;
        var data = JSON.stringify(filters);

        return getAjaxRequest(url, "PUT", data);
    };

    var table = $("#FilterTable");
    var filterNameInputText = $("#FilterNameInputText");

    if (isNullOrWhiteSpace(filterNameInputText.val())) {
        makeDirty(filterNameInputText);
        return;
    };

    removeDirty(filterNameInputText);

    if (!table.find("tbody tr").length) {
        table.addClass("inverted red");
        return;
    };

    table.removeClass("inverted red");

    var error = table.find("tr.error td:nth-child(2)").text();

    if (error.length) {
        var message = window.translate("ColumnInvalidAreYouSure");

        var confirmed = confirm(stringFormat(message, error));
        if (!confirmed) {
            return;
        };
    };

    var filters = [];

    $("#FilterTable tbody tr").each(function () {
        var el = $(this);
        var columnName = el.find("td:nth-child(2)").text();
        var condition = el.find("td:nth-child(3)").text();

        var filter = new Object();

        filter.FilterId = window.filterId;
        filter.FilterStatement = "AND";
        filter.ObjectName = window.scrudFactory.viewTableName;
        filter.FilterName = filterNameInputText.val();

        filter.ColumnName = Enumerable.From(localizedHeaders)
            .Where(function (x) { return x.localized === columnName }).ToArray()[0].columnName;

        filter.DataType = getFilterType(filter.ColumnName);

        filter.FilterCondition = window.parseInt(
            Enumerable.From(filterConditions)
            .Where(function (x) { return x.text === condition })
            .ToArray()[0].value
            || 0);

        filter.FilterValue = el.find("td:nth-child(4)").text();
        filter.FilterAndValue = el.find("td:nth-child(5)").text();
        filters.push(filter);
    });

    var ajax = request(filterNameInputText.val(), filters);

    ajax.success(function () {
        window.filterId = 0;
        displayMessage(window.translate("TaskCompletedSuccessfully"), "success");
    });

    ajax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});


function getSelectedFilter() {
    var filters = [];
    var els = $("[data-filter-labels] .ui.label");

    $.each(els, function () {
        var el = $(this);
        var columnName = el.attr("data-column-name");
        var dataType = el.attr("data-type");
        var filterCondition = el.attr("data-filter-condition");
        var filtervalue = el.attr("data-filter-value");
        var andValue = el.attr("data-filter-and-value");

        filters.push(getAjaxColumnFilter("WHERE", columnName, dataType, filterCondition, filtervalue, andValue));
    });

    var qs = getQuerystringFilters();
    filters = filters.concat(qs);

    return filters;
};

function showFilters(filters) {

    var target = $("[data-filter-labels]");
    target.html("");

    $.each(filters, function(i, filter) {
        var label = $("<a class='ui filter member label'>");
        label.attr("data-column-name", filter.ColumnName);
        label.attr("data-type", filter.DataType);
        label.attr("data-filter-condition", filter.FilterCondition);
        label.attr("data-filter-value", filter.FilterValue);
        label.attr("data-filter-and-value", filter.FilterAndValue);

        var filterCondition = Enumerable.From(filterConditions)
            .Where(function (x) { return x.value === filter.FilterCondition.toString() })
            .Select(function (x) { return x.operator }).ToArray()[0];

        var span = $("<span>");
        var text = filter.ColumnName.replace(/_/g, " ");
        text = toPascalCase(text);

        text = text + " " + filterCondition + " " + filter.FilterValue;

        if (filter.FilterAndValue) {
            text = text + " AND " + filter.FilterAndValue;
        };

        span.text(text);

        var icon = $('<i class="delete icon"></i>');

        label.append(span);
        label.append(icon);
        target.append(label);
    });

    $(".filter.section").show();
    $(".filter.member.label i").click(function () {

        var anchor = $(this).parent();
        var container = anchor.parent();
        anchor.remove();

        if (!container.find(".label").length) {
            $(".filter.section").hide();
        };

        loadPageCount(loadGrid);
    });
};

function createFilters(filters) {
    $("#FilterTable").find("tbody").html("");

    $.each(filters, function (i, filter) {
        var selectedColumn = filter.ColumnName;
        var filterCondition = filter.FilterCondition;
        var value = filter.FilterValue;
        var and = (filter.FilterAndValue || "");

        var css = "";

        filterCondition = Enumerable.From(filterConditions)
            .Where(function (x) { return x.value === filterCondition.toString() })
            .Select(function (x) { return x.text }).ToArray()[0];

        var column = Enumerable.From(localizedHeaders)
            .Where(function (x) { return x.columnName === selectedColumn }).ToArray()[0];

        if (column) {
            selectedColumn = column.localized;
        } else {
            css = "error";
        };

        addFilterRow(selectedColumn, filterCondition, value, and, css);
    });
};


function deleteFilter(el) {
    if (confirmAction()) {
        $(el).parent().parent().remove();
    };

};

$("#RemoveDefaultFilterButton").click(function () {
    function request() {
        var url = "/api/filters/remove-default/" + window.scrudFactory.viewTableName;
        return getAjaxRequest(url, "DELETE");
    };

    var ajax = request();

    ajax.success(function () {
        displayMessage(window.translate("TaskCompletedSuccessfully"), "success");

        $(".filter.modal").modal("close");
    });

    ajax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});
$("#MakeUserDefaultFilterButton").click(function () {
    function request(filterName) {
        var url = "/api/filters/make-default/" + window.scrudFactory.viewTableName + "/" + filterName;
        return getAjaxRequest(url, "PUT");
    };

    var filterName = $("#FilterSelect").getSelectedText();
    var ajax = request(filterName);

    ajax.success(function () {
        displayMessage(window.translate("TaskCompletedSuccessfully"), "success");

        $(".filter.modal").modal("close");
    });

    ajax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});

