﻿function loadDefaultValues() {
    if (window.defaultValuesLoaded) {
        return;
    };

    var qs = getQueryStrings();

    $.each(qs, function (i, v) {
        var name = v.key;
        var value = v.value;

        var el = $("[data-property=" + name + "]");

        if (el.length) {
            if (el.is("select")) {
                el.addClass("disabled");
                el.attr("data-value", value);
                el.attr("data-default-value", value);
            } else {
                el.val(value).trigger("change");
            };
        };
    });

    $("input.live").trigger("keyup");
    window.defaultValuesLoaded = true;
};