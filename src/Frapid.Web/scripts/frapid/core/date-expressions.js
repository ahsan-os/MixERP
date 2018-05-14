function convertDate(d) {
    try {
        const date = new Date(window.parseInt(d.substr(6)));
        return date;
    } catch (e) {
        return null;
    };
};

function getTimestampWithoutTimeZone(string){
    var date = new Date(string)
    var userTimezoneOffset = new Date().getTimezoneOffset()*60000;
    return new Date(date.getTime() - userTimezoneOffset);
};

function parseLocalizedDate(dateString) {
    if (!dateString) {
        return "";
    };

    const date = Date.parseExact(dateString, window.shortDateFormat);

    if (date) {
        const offset = date.getTimezoneOffset() * 60000;
        return new Date(date.getTime() - offset).toISOString();
    };

    return dateString;
};

function getTime(date) {
    if (!date) {
        return "";
    };

    function padMinutes(minutes) {
        if (window.parseInt(minutes || 0) < 10) {
            return `0${minutes}`;
        };

        return minutes;
    };

    date = new Date(date);

    value = date.getHours() + ":" + padMinutes(date.getMinutes());

    return value;
};

function dateAdd(d, expression, number) {
    var ret = new Date();

    if (expression === "d") {
        ret = new Date(d.getFullYear(), d.getMonth(), d.getDate() + window.parseInt(number));
    };

    if (expression === "m") {
        ret = new Date(d.getFullYear(), d.getMonth() + window.parseInt(number), d.getDate());
    };

    if (expression === "y") {
        ret = new Date(d.getFullYear() + window.parseInt(number), d.getMonth(), d.getDate());
    };

    return ret.toString(window.shortDateFormat);
};

function getDate() {
    var d;
    if (window.today) {
        d = new Date(window.today);
    };

    if (window.customVars && window.customVars.Today) {
        d = new Date(window.customVars.Today);
    };

    if (!d) {
        d = new Date();
    };

    return d;
};

function convertNetDateFormat(format) {
    //Convert the date
    format = (format || "M/d/yyyy").replace("dddd", "DD");
    format = format.replace("ddd", "D");

    //Convert month
    if (format.indexOf("MMMM") !== -1) {
        format = format.replace("MMMM", "MM");
    }
    else if (format.indexOf("MMM") !== -1) {
        format = format.replace("MMM", "M");
    }
    else if (format.indexOf("MM") !== -1) {
        format = format.replace("MM", "mm");
    }
    else{
        format = format.replace("M", "m");        
    }


    //Convert year
    format = format.indexOf("yyyy") >= 0 ? format.replace("yyyy", "yy") : format.replace("yy", "y");

    return format;
};

function loadDatepicker() {
    loadPersister();

    if (!$.isFunction($.fn.datepicker)) {
        return;
    };

    if (typeof (window.datepickerFormat) === "undefined") {
        window.datepickerFormat = "";
    }
    if (typeof (window.datepickerShowWeekNumber) === "undefined") {
        window.datepickerShowWeekNumber = false;
    }
    if (typeof (window.datepickerWeekStartDay) === "undefined") {
        window.datepickerWeekStartDay = "1";
    }
    if (typeof (window.datepickerNumberOfMonths) === "undefined") {
        window.datepickerNumberOfMonths = "";
    }
    if (typeof (window.language) === "undefined") {
        window.language = "";
    }

    const candidates = $("input.date:not([readonly]), input[type=date]:not([readonly])");

    candidates.datepicker(
        {
            dateFormat: datepickerFormat,
            showWeek: datepickerShowWeekNumber,
            firstDay: datepickerWeekStartDay,
            constrainInput: false,
            numberOfMonths: eval(datepickerNumberOfMonths)
        },
        $.datepicker.regional[language]);


    $.each(candidates, function () {
        const el = $(this);

        //Chrome does not support <input type="date" /> and jQuery UI datepicker
        if (el.attr("type") === "date") {
            el.attr("type", "text");
        };

        const val = el.val();
        var expression = el.attr("data-expression");

        if (expression) {
            el.val(expression).trigger("blur");
        };
    });

    candidates.blur(function () {
        var date = getDate();
        var control = $(this);
        var expression = control.val().trim().toLowerCase();
        var result;
        var number;

        if (expression === "bom") {
            result = new Date(window.customVars.MonthStartDate).toString(window.shortDateFormat);
        };

        if (expression === "eom") {
            result = new Date(window.customVars.MonthEndDate).toString(window.shortDateFormat);
        };

        if (expression === "boq") {
            result = new Date(window.customVars.QuarterStartDate).toString(window.shortDateFormat);
        };


        if (expression === "eoq") {
            result = new Date(window.customVars.QuarterEndDate).toString(window.shortDateFormat);
        };


        if (expression === "boh") {
            result = new Date(window.customVars.FiscalHalfStartDate).toString(window.shortDateFormat);
        };


        if (expression === "eoh") {
            result = new Date(window.customVars.FiscalHalfEndDate).toString(window.shortDateFormat);
        };

        if (expression === "boy") {
            result = new Date(window.customVars.FiscalYearStartDate).toString(window.shortDateFormat);
        };

        if (expression === "eoy") {
            result = new Date(window.customVars.FiscalYearEndDate).toString(window.shortDateFormat);
        };


        if (expression === "d") {
            result = dateAdd(date, "d", 0);
        }; //Today
        if (expression === "w" || expression === "+w") {
            result = dateAdd(date, "d", 7);
        }; //Next Week
        if (expression === "m" || expression === "+m") {
            result = dateAdd(date, "m", 1);
        }; //Next Month
        if (expression === "y" || expression === "+y") {
            result = dateAdd(date, "y", 1);
        }; //Next Year


        if (expression === "-d") {
            result = dateAdd(date, "d", -1);
        }; //YesterDay      
        if (expression === "+d") {
            result = dateAdd(date, "d", 1);
        }; //Tomorrow
        if (expression === "-w") {
            result = dateAdd(date, "d", -7);
        }; //Last Week
        if (expression === "-m") {
            result = dateAdd(date, "m", -1);
        }; //Last Month
        if (expression === "-y") {
            result = dateAdd(date, "y", -1);
        };

        if (!result) {
            if (expression.indexOf("d") >= 0) {
                number = window.parseInt(expression.replace("d"));
                result = dateAdd(date, "d", number);
            };
            if (expression.indexOf("w") >= 0) {
                number = window.parseInt(expression.replace("w"));
                result = dateAdd(date, "d", number * 7);
            };
            if (expression.indexOf("m") >= 0) {
                number = window.parseInt(expression.replace("m"));
                result = dateAdd(date, "m", number);
            };
            if (expression.indexOf("y") >= 0) {
                number = window.parseInt(expression.replace("y"));
                result = dateAdd(date, "y", number);
            };
        };

        if (result) {
            control.val(result).trigger("change");
        };
    });

    $('[data-type="time"], .time').timepicker({ timeFormat: "H:i" });
    $('[data-type="time"], .time').attr("placeholder", "hh:mm");
    candidates.trigger("blur");
};

function getFormattedDate(date){
	var formatted = "";
	var d = moment(date).local();
	var time = d.hour() + d.minute() + d.second();

	if(!time){
		formatted = d.format('LL');
	}else{
		formatted = d.format('LLLL');
	};
	
	return formatted;
};

function initializeCalendar() {	
    function getDatePickerOptions(dateOnly) {
        //Todo: localization: localize week names.
        const options = {
            parser: {
                date: function (text) {
                    return new Date(text);
                }
            },
            formatter: {
                date: function (date) {
                    if (!date) {
						return ''
					};
					
                    //return getFormattedDate(date);
                    return $.datepicker.formatDate(window.convertNetDateFormat(window.longDateFormat), date);
                }
            },
            initialDate: window.today ? new Date(window.today) : new Date()
        };

        if (dateOnly) {
            options.type = 'date';
        };

        return options;
    };


    $('.ui.date.only.picker').calendar(getDatePickerOptions(true));
    $('.ui.date.time.picker').calendar(getDatePickerOptions(false));
    $('.ui.date.only.picker').calendar("set date", new Date(window.today), false, false);
};

$(document).ready(function () {
    loadDatepicker();
});