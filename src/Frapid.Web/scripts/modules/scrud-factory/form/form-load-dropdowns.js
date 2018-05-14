function loadDropdowns() {
    if (!window.dropdownsLoaded) {
        var dropdowns = $(".form.factory select");

        dropdowns.each(function (i, v) {
            var dropdown = $(v);

            var selectedValue = "";

            if (dropdown.val()) {
                selectedValue = (dropdown.val().toString() || "");
            };

            if (!selectedValue) {
                selectedValue = dropdown.attr("data-value");

                if (selectedValue) {
                    dropdown.val(selectedValue.toString()).trigger("change");
                };
            };
        });

        window.dropdownsLoaded = true;
    };
};