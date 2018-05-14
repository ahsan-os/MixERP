function getClassName() {
    if (window.scrudFactory.className) {
        return window.scrudFactory.className;
    };

    var table = window.scrudFactory.formTableName;

    if (!table) {
        table = window.scrudFactory.viewTableName;
    }

    var plural = toProperCase(table.split(".")[1]);
    return plural.singularize();
};