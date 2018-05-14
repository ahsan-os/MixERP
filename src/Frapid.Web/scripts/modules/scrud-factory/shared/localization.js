function localizeHeaders(el) {
    el.find("thead tr:first-child th").each(function () {
        var cell = $(this);
        var key = cell.text();
        var text = translate(key);
        cell.text(text);

        var column = new Object();

        column.columnName = key;
        column.localized = text;

        localizedHeaders.push(column);
    });
};