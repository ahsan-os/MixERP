var areYouSure = translate("AreYouSure");

var sumOfColumn = function (tableSelector, columnPosition) {
    var total = 0;

    $(tableSelector).find("tr").each(function () {
        var value = parseFloat2($("td", this).eq(columnPosition).text());
        total += value;
    });

    return total;
};

var getColumnText = function (row, columnPosition) {
    return row.find("td:eq(" + columnPosition + ")").text();
};

var setColumnText = function (row, columnPosition, value) {
    row.find("td:eq(" + columnPosition + ")").html(value);
};

var toggleDanger = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("negative");
};

var addDanger = function (row) {
    row.removeClass("negative");
    row.addClass("negative");
};

var toggleSuccess = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("positive");
};

var removeRow = function (cell) {
    var result = confirm(areYouSure);

    if (result) {
        cell.closest("tr").remove();
    }
};