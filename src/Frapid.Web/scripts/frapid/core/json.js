//table to JSON
function serializeTable(table, strict) {
    function getMeta() {
        const candidates = table.find("thead th[data-member]");
        const keys = [];

        $.each(candidates, function () {
            const el = $(this);
            const index = el.index();
            const member = el.attr("data-member");
            const dataType = el.attr("data-member-data-type");

            keys.push({
                Key: member,
                Index: index,
                DataType: dataType
            });
        });

        return keys;
    };

    const collection = [];

    const meta = getMeta();

    table.find("tbody tr").each(function () {
        const row = $(this);
        const item = {};

        $.each(meta, function () {
            const cell = row.find("td:eq(" + this.Index + ")");
            var value = cell.text();

            switch (this.DataType) {
                case "date":
                    value = window.parseDate(value);
                    break;
                case "integer":
                case "int":
                    if (strict) {
                        value = window.parseIntStrict(value);
                    } else {
                        value = window.parseInt(value);
                    }
                    break;
                case "float":
                case "currency":
                case "money":
                case "decimal":
                    if (strict) {
                        value = window.parseFloatStrict(value);
                    } else {
                        value = window.parseFloat2(value);
                    }
                    break;
            };


            item[this.Key] = value;
        });

        collection.push(item);
    });

    return collection;
};
