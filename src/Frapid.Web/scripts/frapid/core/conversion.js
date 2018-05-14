var parseFloat2 = function (arg) {
	if(!arg){
		return 0;
	};

    var input = arg;

    if (window.currencySymbol) {
        input = input.toString().replace(window.currencySymbol, "");
    };

    var val = parseFloat(parseFormattedNumber(input.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};

var parseFloatStrict = function (arg) {
    if (typeof (arg) === "undefined") {
        return null;
    };
	
	if(!arg){
		return null;
	};
	
	return parseFloat2(arg);
};

var parseInt2 = function (arg) {
	if(!arg){
		return 0;
	};

    var val = parseInt(parseFormattedNumber(arg.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};


var parseIntStrict = function (arg) {
    if (typeof (arg) === "undefined") {
        return null;
    };
	
	if(!arg){
		return null;
	};

	return parseInt(arg);
};

function parseDate(str) {
    return new Date(Date.parse(str));
};

function parseSerializedDate(str) {
    str = str.replace(/[^0-9 +]/g, '');
    return new Date(parseInt(str));
};

function round(number, decimalPlaces) {    
    return +(Math.round(number + "e+" + decimalPlaces)  + "e-" + decimalPlaces);
};