function initializeAjaxRequest() {
    $.each(window.scrudFactory.keys, function () {
        var el = $("[data-property=" + this.property + "]");
        var prop = el.attr("data-property");

        if (el.length) {
            var selectedValue = "";

            if (window.editData) {
                selectedValue = window.editData[prop] || "";
            };

            var queryStringData = getQueryStringByName(prop);

            if (queryStringData) {
                selectedValue = queryStringData;
            };

            if (this.url) {
                ajaxDataBind(this.url, el, this.data, this.valueField, this.textField, selectedValue.toString(), null, this.isArray);
            };
        };
    });
};