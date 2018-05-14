function resetForm() {
    window.editing = false;
    var fields = $(".form-field:not(:hidden), .custom-field:not(:hidden), .form-field[data-primarykey]");

    $(".huge.header .sub.header").html("");
    $("span.live").html("");
    $(".initial.value").html(window.formTitle);

    $(".uploader.field .ui.image.preview").attr("src", "/Static/images/mixerp-logo-light.png");

    if (window.scrudFactory.disabledOnEdit) {
        $.each(window.scrudFactory.disabledOnEdit, function () {
            var el = $('[data-property="' + this + '"]');
            el.removeAttr("disabled");
        });
    };

    $.each(fields, function () {
        var el = $(this);
        var defaultValue = el.attr("data-default-value");

        if (!defaultValue) {
            defaultValue = "";
        };

        if (el.hasClass("dropdown")) {
            if (!el.hasClass("disabled")) {
                var select = el.find("select");
                var search = el.find(".search");
                var text = el.find(".text");

                select.val("").trigger("change");
                search.html("").trigger("change");
                text.html("").trigger("change");
            };
        } else {
            el.val(defaultValue).trigger("change");
            el.attr("data-val", defaultValue);
        };
    });
};