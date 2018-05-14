var stringTypes = ["varchar", "text", "nvarchar"];
var wholeNumbers = ["_bigint", "_int2", "_int4", "int", "_int8", "_integer", "_integer_strict", "_integer_strict2", "_serial4", "_serial8", "_smallint", "bigint", "bigint[]", "int2", "int2[]", "int4", "int4[]", "int8", "int8[]", "integer", "integer[]", "integer_strict", "integer_strict2", "integer_strict2[]", "integer_strict[]", "serial4", "serial4[]", "serial8", "serial8[]", "smallint", "smallint[]"];
var decimalNumber = ["_decimal_strict", "_decimal_strict2", "_float4", "_float8", "_money", "_money_strict", "_money_strict2", "_numeric", "decimal_strict", "decimal_strict2", "decimal_strict2[]", "decimal_strict[]", "float4", "float4[]", "float8", "float8[]", "money", "money[]", "money_strict", "money_strict2", "money_strict2[]", "money_strict[]", "numeric", "numeric[]", "decimal", "decimal?"];
var booleans = ["bit", "bool", "bool?", "System.Boolean", "System.Boolean?"];
var dateTypes = ["System.DateTime", "System.DateTime?", "datetimeoffset", "datetime", "timestamp", "timestamptz", "date"];

function toUnderscoreCase(name) {
    const letters = [name[0]];

    const length = name.length;

    for (let i = 1; i < length; i++) {
        const letter = name[i];
        const isNumer = $.isNumeric(letter);

        if (!isNumer && letter.toUpperCase() === letter) {
            letters.push('_');
            letters.push(letter);
        }
        else if (isNumer) {
            const previous = name[i - 1];

            if (!$.isNumeric(previous)) {
                letters.push('_');
            };

            letters.push(letter);


            if (i + 1 >= length) {
                continue;
            };

            const next = name[i + 1];

            if (next && !$.isNumeric(next)) {
                letters.push('_');
            };
        }
        else {
            letters.push(letter);
        };
    };

    const result = letters.join('').toLowerCase();
    return result;
};

function toProperCase(str) {
    var result = str.replace(/_([a-z0-9])/g, function (g) { return g[1].toUpperCase(); });
    return result.charAt(0).toUpperCase() + result.slice(1);
};
